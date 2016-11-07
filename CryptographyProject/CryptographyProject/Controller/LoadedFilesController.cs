using System;
using System.IO;
using System.Collections.Concurrent;
using CryptographyProject.Model;
using System.Threading;
using CryptographyProject.EncryptionAlgorithms;
using CryptographyProject.Common;
using CryptographyProject.View;
using System.Text;

namespace CryptographyProject.Controller
{
    public class LoadedFilesController : IEncryptionAlgorithms
    {
        //Logger
        private LoggerController loggerController;

        //Queue for files
        private BlockingCollection<FileInfo> queueFiles;//Files (Disposable!)

        //Threads data
        private static int _NUMBER_OF_THREADS; //Number of current threads
        public static bool _FILE_CREATOR_THREAD_RUNNING;
        public static bool _END_OF_FILE_THREADS;

        //HistoryController
        private HistoryController historyController;

        //Constructor
        public LoadedFilesController(HistoryController history)
        {
            loggerController = new LoggerController();
            queueFiles = new BlockingCollection<FileInfo>();
            historyController = history;

            //Reset
            _NUMBER_OF_THREADS = 0;
            _FILE_CREATOR_THREAD_RUNNING = false;
            _END_OF_FILE_THREADS = false;

            //Starting the logger thread
            new Thread(() => loggerController.PrintLog()).Start();
        }

        //Adds a file in a queue
        public void Add(FileInfo file)
        {
            this.queueFiles.Add(file);
            loggerController.Add(" + Added file: " + file.Name);
        }

        //Starts the whole process
        public void StartEncDec(FormModel model)
        {
            loggerController.Add(" # STARTING THE ENC/DEC");
            LoadedFilesController._FILE_CREATOR_THREAD_RUNNING = true;
            while (LoadedFilesController._FILE_CREATOR_THREAD_RUNNING)
            {
                if (LoadedFilesController._NUMBER_OF_THREADS < model.ThreadsNumber && queueFiles.Count > 0)
                {
                    LoadedFilesController._NUMBER_OF_THREADS++;

                    //Choose algorithm
                    switch (model.AlgorithmIndex)
                    {
                        case (int)Algorithms.SimpleSubstitution:
                            {
                                if (model.EncryptionChosen)
                                {
                                    new Thread(() => SimpleSubstitutionEncryption(queueFiles.Take(), model)).Start();
                                }
                                else
                                {
                                    new Thread(() => SimpleSubstitutionDecryption(queueFiles.Take(), model)).Start();
                                }
                                break;
                            }
                        case (int)Algorithms.RC4:
                            {
                                if (model.EncryptionChosen)
                                {
                                    new Thread(() => RC4Encryption(queueFiles.Take(), model)).Start();
                                }
                                else
                                {
                                    new Thread(() => RC4Decryption(queueFiles.Take(), model)).Start();
                                }
                                break;
                            }
                    }
                }
                Thread.Sleep(500);
            }
            loggerController.Add(" # STOPING THE ENC/DEC");
        }

        //Stops the whole process
        public void StopEncDec()
        {
            //Kill main thread for creating file editing threads
            LoadedFilesController._FILE_CREATOR_THREAD_RUNNING = false;
            //Kill all threads that are currently working with files
            LoadedFilesController._END_OF_FILE_THREADS = true;
        }

        //Call this as event handler when thread ends   -------
        private void ThreadEnds(FileInfo file, bool threadSuccesfull, DateTime timeStarted)
        {
            //History
            if (threadSuccesfull)
            {
                if (timeStarted != null)
                {
                    loggerController.Add("Finished: " + DateTime.Now.ToString("dd/MM/yy HH:mm:ss"));
                }
                historyController.AddToHistory(file.Name, file.FullName, file.LastWriteTime.ToString("dd/MM/yy HH:mm:ss"));
            }
            //Threads number
            LoadedFilesController._NUMBER_OF_THREADS--;
            //Log
            loggerController.Add(" # Threads number: " + LoadedFilesController._NUMBER_OF_THREADS);
        }


        //
        //-----------------------------------------------------------------------------------------------------
        // Algorithms
        //-----------------------------------------------------------------------------------------------------
        //

        public void SimpleSubstitutionEncryption(FileInfo file, FormModel model)
        {
            bool threadSuccesfull = false;
            var timeStarted = DateTime.Now;
            try
            {
                //OutputFileName
                string outputFileName = FileNameCreator.CreateFileEncryptedName(
                    model.Folders.OutputFolder,
                    file.Name,
                    model.AlgorithmName);

                //Log                
                loggerController.Add(" ! File enc: " + file.Name + ", Alg: " + model.AlgorithmName);
                loggerController.Add("Started: " + timeStarted.ToString("dd/MM/yy HH:mm:ss"));

                //Read a file char by char, and encrypt it
                using (StreamReader sr = new StreamReader(file.FullName))
                {
                    using (StreamWriter sw = new StreamWriter(outputFileName))
                    {
                        while (sr.Peek() >= 0)
                        {
                            char character = (char)sr.Read();
                            character = Char.ToLower(character);
                            if (character < 97 || character > 122)
                            {
                                sw.Write(character);
                            }
                            else
                            {
                                sw.Write(SimpleSubstituionCipher.Encrypt(character));
                            }

                            if (LoadedFilesController._END_OF_FILE_THREADS)
                            {
                                sr.Dispose();
                                sw.Dispose();
                                File.Delete(outputFileName);
                                Thread.CurrentThread.Abort();
                            }
                        }
                    }
                }
                threadSuccesfull = true;
                Thread.Sleep(250);
            }
            catch (Exception ex)
            {
                loggerController.Add(" ? Enc exception: " + ex.Message);
                threadSuccesfull = false;
            }
            finally
            {
                this.ThreadEnds(file, threadSuccesfull, timeStarted);
            }

        }

        public void SimpleSubstitutionDecryption(FileInfo file, FormModel model)
        {
            bool threadSuccesfull = false;
            var timeStarted = DateTime.Now;
            try
            {
                //OutputFileName
                string outputFileName = FileNameCreator.CreateFileDecryptedName(
                    model.Folders.OutputFolder,
                    file.Name,
                    "");

                //Log
                loggerController.Add(" ! File dec: " + file.Name + ", Alg: " + model.AlgorithmName);
                loggerController.Add("Started: " + timeStarted.ToString("dd/MM/yy HH:mm:ss"));

                //Read a file char by char, and decrypt it
                using (StreamReader sr = new StreamReader(file.FullName))
                {
                    using (StreamWriter sw = new StreamWriter(outputFileName))
                    {
                        while (sr.Peek() >= 0)
                        {
                            char character = (char)sr.Read();
                            character = Char.ToUpper(character);
                            if (character < 65 || character > 90)
                            {
                                sw.Write(character);
                            }
                            else
                            {
                                sw.Write(SimpleSubstituionCipher.Decrypt(character));
                            }

                            if (LoadedFilesController._END_OF_FILE_THREADS)
                            {
                                sr.Dispose();
                                sw.Dispose();
                                File.Delete(outputFileName);
                                Thread.CurrentThread.Abort();
                            }
                        }
                    }
                }
                threadSuccesfull = true;
                Thread.Sleep(250);
            }
            catch (Exception ex)
            {
                loggerController.Add(" ? Dec exception: " + ex.Message);
                threadSuccesfull = false;
            }
            finally
            {
                this.ThreadEnds(file, threadSuccesfull, timeStarted);
            }
        }

        public void RC4Encryption(FileInfo file, FormModel model)
        {
            bool threadSuccesfull = false;
            var timeStarted = DateTime.Now;
            try
            {
                //OutputFileName
                string outputFileName = FileNameCreator.CreateFileEncryptedName(
                    model.Folders.OutputFolder,
                    file.Name,
                    model.AlgorithmName);

                //Log
                loggerController.Add(" ! File enc: " + file.Name + ", Alg: " + model.AlgorithmName);
                loggerController.Add("Started: " + timeStarted.ToString("dd/MM/yy HH:mm:ss"));

                //Read a file char by char, and encrypt it
                using (FileStream fsr = new FileStream(file.FullName, FileMode.Open))
                {
                    using (BinaryReader br = new BinaryReader(fsr, new ASCIIEncoding()))
                    {
                        using (FileStream fsw = new FileStream(outputFileName, FileMode.Create))
                        {
                            using (BinaryWriter bw = new BinaryWriter(fsw, new ASCIIEncoding()))
                            {
                                int i = 0;
                                int j = 0;
                                byte[] state = RC4.KSA();
                                byte readedValue = 0;
                                while (br.BaseStream.Position < br.BaseStream.Length)
                                {
                                    //ENC
                                    readedValue = br.ReadByte();
                                    byte prga = RC4.PRGA(ref i, ref j, ref state);
                                    byte encryptedValue = RC4.Encrypt(readedValue, prga);
                                    bw.Write(encryptedValue);

                                    if (LoadedFilesController._END_OF_FILE_THREADS)
                                    {
                                        bw.Dispose();
                                        fsw.Dispose();
                                        br.Dispose();
                                        fsr.Dispose();
                                        File.Delete(outputFileName);
                                        Thread.CurrentThread.Abort();
                                    }
                                }
                            }
                        }
                    }
                }
                threadSuccesfull = true;
                Thread.Sleep(250);
            }
            catch (Exception ex)
            {
                loggerController.Add(" ? Enc exception: " + ex.Message);
                threadSuccesfull = false;
            }
            finally
            {
                this.ThreadEnds(file, threadSuccesfull, timeStarted);
            }
        }

        public void RC4Decryption(FileInfo file, FormModel model)
        {
            bool threadSuccesfull = false;
            var timeStarted = DateTime.Now;
            try
            {
                //Used to find file extension
                var historyFile = new HistoryFiles()
                {
                    FileName = file.Name,
                    Path = file.FullName,
                    DateModified = file.LastWriteTime.ToString("dd/MM/yy HH:mm:ss")
                };

                //OutputFileName
                string outputFileName = FileNameCreator.CreateFileDecryptedName(
                    model.Folders.OutputFolder,
                    file.Name,
                    historyController.GetFileExtension(historyFile));

                //Log
                loggerController.Add(" ! File dec: " + file.Name + ", Alg: " + model.AlgorithmName);
                loggerController.Add("Started: " + timeStarted.ToString("dd/MM/yy HH:mm:ss"));

                //Read a file char by char, and decrypt it
                using (FileStream fsr = new FileStream(file.FullName, FileMode.Open))
                {
                    using (BinaryReader br = new BinaryReader(fsr, new ASCIIEncoding()))
                    {
                        using (FileStream fsw = new FileStream(outputFileName, FileMode.Create))
                        {
                            using (BinaryWriter bw = new BinaryWriter(fsw, new ASCIIEncoding()))
                            {
                                int i = 0;
                                int j = 0;
                                byte[] state = RC4.KSA();
                                byte readedValue = 0;
                                while (br.BaseStream.Position < br.BaseStream.Length)
                                {
                                    //ENC
                                    readedValue = br.ReadByte();
                                    byte prga = RC4.PRGA(ref i, ref j, ref state);
                                    byte decryptedValue = RC4.Decrypt(readedValue, prga);
                                    bw.Write(decryptedValue);

                                    if (LoadedFilesController._END_OF_FILE_THREADS)
                                    {
                                        bw.Dispose();
                                        fsw.Dispose();
                                        br.Dispose();
                                        fsr.Dispose();
                                        File.Delete(outputFileName);
                                        Thread.CurrentThread.Abort();
                                    }
                                }
                            }
                        }
                    }
                }
                threadSuccesfull = true;
                Thread.Sleep(250);
            }
            catch (Exception ex)
            {
                loggerController.Add(" ? Dec exception: " + ex.Message);
                threadSuccesfull = false;
            }
            finally
            {
                this.ThreadEnds(file, threadSuccesfull, timeStarted);
            }
        }
    }
}