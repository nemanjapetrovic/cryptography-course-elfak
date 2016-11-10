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
        public static bool _THREAD_CREATOR_RUNNING;
        public static bool _END_OF_ENC_DEC_THREADS;

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
            _THREAD_CREATOR_RUNNING = false;
            _END_OF_ENC_DEC_THREADS = false;

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
            LoadedFilesController._THREAD_CREATOR_RUNNING = true;
            while (LoadedFilesController._THREAD_CREATOR_RUNNING)
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
                        case (int)Algorithms.TEA:
                            {
                                if (model.EncryptionChosen)
                                {
                                    new Thread(() => TEAEcnryption(queueFiles.Take(), model)).Start();
                                }
                                else
                                {
                                    new Thread(() => TEADecryption(queueFiles.Take(), model)).Start();
                                }
                                break;
                            }
                        case (int)Algorithms.XTEA:
                            {
                                if (model.EncryptionChosen)
                                {
                                    new Thread(() => XTEAEcnryption(queueFiles.Take(), model)).Start();
                                }
                                else
                                {
                                    new Thread(() => XTEADecryption(queueFiles.Take(), model)).Start();
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
            LoadedFilesController._THREAD_CREATOR_RUNNING = false;
            //Kill all threads that are currently working with files
            LoadedFilesController._END_OF_ENC_DEC_THREADS = true;
        }

        //Call this as event handler when thread ends   -------
        private void ThreadEnds(FileInfo file, bool threadSuccesfull, DateTime timeStarted)
        {
            //History
            if (threadSuccesfull)
            {
                if (timeStarted != null)
                {
                    TimeSpan span = DateTime.Now.Subtract(timeStarted);
                    loggerController.Add("Finished: " + file.Name + " , time: " + span);
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

                //Read a file char by char, and encrypt it
                using (StreamReader sr = new StreamReader(file.FullName))
                {
                    using (StreamWriter sw = new StreamWriter(outputFileName))
                    {
                        //Writing the extension                                
                        char[] extension = file.Extension.Substring(1, file.Extension.Length - 1).ToCharArray();
                        char extensionLength = (char)extension.Length;
                        sw.Write(extensionLength);
                        for (var k = 0; k < extension.Length; k++)
                        {
                            sw.Write(extension[k]);
                        }

                        //Reading - encrypting - saving data
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

                            if (LoadedFilesController._END_OF_ENC_DEC_THREADS)
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
                string outputFileName = "";

                //Log
                loggerController.Add(" ! File dec: " + file.Name + ", Alg: " + model.AlgorithmName);

                //Read a file char by char, and decrypt it
                using (StreamReader sr = new StreamReader(file.FullName))
                {
                    //Reading the extension                                          
                    var extensionLength = (int)sr.Read();
                    char[] extension = new char[extensionLength];
                    for (var i = 0; i < extensionLength; i++)
                    {
                        extension[i] = (char)sr.Read();
                    }
                    var finalExtesnion = "." + new string(extension);

                    //Output file name
                    outputFileName = FileNameCreator.CreateFileDecryptedName(
                            model.Folders.OutputFolder,
                            file.Name,
                            finalExtesnion);

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

                            if (LoadedFilesController._END_OF_ENC_DEC_THREADS)
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

                //Read a file char by char, and encrypt it
                using (FileStream fsr = new FileStream(file.FullName, FileMode.Open))
                {
                    using (BinaryReader br = new BinaryReader(fsr, new ASCIIEncoding()))
                    {
                        using (FileStream fsw = new FileStream(outputFileName, FileMode.Create))
                        {
                            using (BinaryWriter bw = new BinaryWriter(fsw, new ASCIIEncoding()))
                            {
                                //Writing the extension                                
                                char[] extension = file.Extension.Substring(1, file.Extension.Length - 1).ToCharArray();
                                char extensionLength = (char)extension.Length;
                                bw.Write(extensionLength);
                                for (var k = 0; k < extension.Length; k++)
                                {
                                    bw.Write(extension[k]);
                                }

                                //Reading and encrypting files
                                int i = 0;
                                int j = 0;
                                byte[] state = RC4.KSA();
                                byte inputValue = 0;
                                while (br.BaseStream.Position < br.BaseStream.Length)
                                {
                                    //ENC
                                    inputValue = br.ReadByte();
                                    byte prga = RC4.PRGA(ref i, ref j, ref state);
                                    byte encryptedValue = RC4.Encrypt(inputValue, prga);
                                    bw.Write(encryptedValue);

                                    if (LoadedFilesController._END_OF_ENC_DEC_THREADS)
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
                //OutputFileName
                string outputFileName = "";

                //Log
                loggerController.Add(" ! File dec: " + file.Name + ", Alg: " + model.AlgorithmName);

                //Read a file char by char, and decrypt it
                using (FileStream fsr = new FileStream(file.FullName, FileMode.Open))
                {
                    using (BinaryReader br = new BinaryReader(fsr, new ASCIIEncoding()))
                    {
                        //Reading the extension                        
                        var extensionLength = (int)br.ReadByte();
                        char[] extension = new char[extensionLength];
                        for (var i = 0; i < extensionLength; i++)
                        {
                            extension[i] = (char)br.ReadByte();
                        }
                        var finalExtesnion = "." + new string(extension);

                        //OutputFileName
                        outputFileName = FileNameCreator.CreateFileDecryptedName(
                            model.Folders.OutputFolder,
                            file.Name,
                            finalExtesnion);

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
                                    //DEC
                                    readedValue = br.ReadByte();
                                    byte prga = RC4.PRGA(ref i, ref j, ref state);
                                    byte decryptedValue = RC4.Decrypt(readedValue, prga);
                                    bw.Write(decryptedValue);

                                    if (LoadedFilesController._END_OF_ENC_DEC_THREADS)
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

        public void TEAEcnryption(FileInfo file, FormModel model)
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

                //Read a file char by char, and encrypt it
                using (FileStream fsr = new FileStream(file.FullName, FileMode.Open))
                {
                    using (BinaryReader br = new BinaryReader(fsr, new ASCIIEncoding()))
                    {
                        using (FileStream fsw = new FileStream(outputFileName, FileMode.Create))
                        {
                            using (BinaryWriter bw = new BinaryWriter(fsw, new ASCIIEncoding()))
                            {
                                //Writing the extension                                
                                char[] extension = file.Extension.Substring(1, file.Extension.Length - 1).ToCharArray();
                                char extensionLength = (char)extension.Length;
                                bw.Write(extensionLength);
                                for (var k = 0; k < extension.Length; k++)
                                {
                                    bw.Write(extension[k]);
                                }

                                //Reading and encrypting files                             
                                byte[] inputValue = new byte[8];//64bits at the time
                                while (br.BaseStream.Position < br.BaseStream.Length)
                                {
                                    //ENC
                                    br.Read(inputValue, 0, 8);
                                    string data = UtilConverter.ConvertByteArrayToString(inputValue);
                                    var encryptedValue = TEA.EncryptString(data, TEA.Key);
                                    bw.Write(encryptedValue);

                                    if (LoadedFilesController._END_OF_ENC_DEC_THREADS)
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

        public void TEADecryption(FileInfo file, FormModel model)
        {
            bool threadSuccesfull = false;
            var timeStarted = DateTime.Now;
            try
            {
                //OutputFileName
                string outputFileName = "";

                //Log
                loggerController.Add(" ! File dec: " + file.Name + ", Alg: " + model.AlgorithmName);

                //Read a file char by char, and decrypt it
                using (FileStream fsr = new FileStream(file.FullName, FileMode.Open))
                {
                    using (BinaryReader br = new BinaryReader(fsr, new ASCIIEncoding()))
                    {
                        //Reading the extension                        
                        var extensionLength = (int)br.ReadByte();
                        char[] extension = new char[extensionLength];
                        for (var i = 0; i < extensionLength; i++)
                        {
                            extension[i] = (char)br.ReadByte();
                        }
                        var finalExtesnion = "." + new string(extension);

                        //OutputFileName
                        outputFileName = FileNameCreator.CreateFileDecryptedName(
                            model.Folders.OutputFolder,
                            file.Name,
                            finalExtesnion);

                        using (FileStream fsw = new FileStream(outputFileName, FileMode.Create))
                        {
                            using (BinaryWriter bw = new BinaryWriter(fsw, new ASCIIEncoding()))
                            {
                                //Reading and decryptiong files
                                byte[] inputValue = new byte[8];//64bits at the time
                                while (br.BaseStream.Position < br.BaseStream.Length)
                                {
                                    //DEC
                                    br.Read(inputValue, 0, 8);
                                    string data = UtilConverter.ConvertByteArrayToString(inputValue);
                                    var decryptedValue = TEA.Decrypt(data, TEA.Key);
                                    bw.Write(decryptedValue);

                                    if (LoadedFilesController._END_OF_ENC_DEC_THREADS)
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

        public void XTEAEcnryption(FileInfo file, FormModel model)
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

                //Read a file char by char, and encrypt it
                using (FileStream fsr = new FileStream(file.FullName, FileMode.Open))
                {
                    using (BinaryReader br = new BinaryReader(fsr, new ASCIIEncoding()))
                    {
                        using (FileStream fsw = new FileStream(outputFileName, FileMode.Create))
                        {
                            using (BinaryWriter bw = new BinaryWriter(fsw, new ASCIIEncoding()))
                            {
                                //Writing the extension                                
                                char[] extension = file.Extension.Substring(1, file.Extension.Length - 1).ToCharArray();
                                char extensionLength = (char)extension.Length;
                                bw.Write(extensionLength);
                                for (var k = 0; k < extension.Length; k++)
                                {
                                    bw.Write(extension[k]);
                                }

                                //Reading and encrypting files                             
                                byte[] readedValue = new byte[2];
                                Encoding extendedAscii = Encoding.GetEncoding(850);
                                while (br.BaseStream.Position < br.BaseStream.Length)
                                {
                                    //ENC
                                    br.Read(readedValue, 0, 2);
                                    var data = extendedAscii.GetString(readedValue);
                                    var encryptedValue = XTEA.EncryptString(data, XTEA.Key);
                                    byte[] write = extendedAscii.GetBytes(encryptedValue);
                                    bw.Write(write);

                                    if (LoadedFilesController._END_OF_ENC_DEC_THREADS)
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

        public void XTEADecryption(FileInfo file, FormModel model)
        {
            bool threadSuccesfull = false;
            var timeStarted = DateTime.Now;
            try
            {
                //OutputFileName
                string outputFileName = "";

                //Log
                loggerController.Add(" ! File dec: " + file.Name + ", Alg: " + model.AlgorithmName);

                //Read a file char by char, and decrypt it
                using (FileStream fsr = new FileStream(file.FullName, FileMode.Open))
                {
                    using (BinaryReader br = new BinaryReader(fsr, new ASCIIEncoding()))
                    {
                        //Reading the extension                        
                        var extensionLength = (int)br.ReadByte();
                        char[] extension = new char[extensionLength];
                        for (var i = 0; i < extensionLength; i++)
                        {
                            extension[i] = (char)br.ReadByte();
                        }
                        var finalExtesnion = "." + new string(extension);

                        //OutputFileName
                        outputFileName = FileNameCreator.CreateFileDecryptedName(
                            model.Folders.OutputFolder,
                            file.Name,
                            finalExtesnion);

                        using (FileStream fsw = new FileStream(outputFileName, FileMode.Create))
                        {
                            using (BinaryWriter bw = new BinaryWriter(fsw, new ASCIIEncoding()))
                            {
                                byte[] readedValue = new byte[8];
                                Encoding extendedAscii = Encoding.GetEncoding(850);
                                while (br.BaseStream.Position < br.BaseStream.Length)
                                {
                                    //DEC
                                    br.Read(readedValue, 0, 8);
                                    var data = extendedAscii.GetString(readedValue);
                                    var decryptedValue = XTEA.Decrypt(data, XTEA.Key);
                                    byte[] write = extendedAscii.GetBytes(decryptedValue);
                                    bw.Write(write);

                                    if (LoadedFilesController._END_OF_ENC_DEC_THREADS)
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