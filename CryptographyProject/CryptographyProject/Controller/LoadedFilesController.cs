﻿using System;
using System.IO;
using System.Collections.Concurrent;
using CryptographyProject.Model;
using System.Threading;
using CryptographyProject.EncryptionAlgorithms;
using CryptographyProject.Common;

namespace CryptographyProject.Controller
{
    public class LoadedFilesController : IEncryptionAlgorithms
    {
        //Logger
        private LoggerController loggerController;

        //Queue for files
        private BlockingCollection<FileInfo> queueFiles;//Files (Disposable!)

        //Thread data
        private static int _NUMBER_OF_THREADS; //Number of current threads
        public static bool _isRunning;
        public static bool _END_OF_PROGRAM_KILL_THREADS;

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
            _isRunning = false;
            _END_OF_PROGRAM_KILL_THREADS = false;

            //Starting the logger thread
            new Thread(() => loggerController.PrintLog()).Start();
        }

        //Adds a file in a queue
        public void Add(FileInfo file)
        {
            this.queueFiles.Add(file);
            loggerController.Add("Added file: " + file.Name);
        }

        //Starts the whole process
        public void StartEncDec(FormModel model)
        {
            loggerController.Add(" # STARTING THE ENC/DEC");
            LoadedFilesController._isRunning = true;
            while (LoadedFilesController._isRunning)
            {
                if (LoadedFilesController._NUMBER_OF_THREADS < model.ThreadsNumber && queueFiles.Count > 0)
                {
                    LoadedFilesController._NUMBER_OF_THREADS++;

                    //Choose algorithm
                    switch (model.AlgorithmName)
                    {
                        case "simple substitution":
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
                    }
                }
                Thread.Sleep(1000);
            }
            loggerController.Add(" # STOPING THE ENC/DEC");
        }

        //Stops the whole process
        public void StopEncDec()
        {
            LoadedFilesController._isRunning = false;
        }

        //Call this as event handler when thread ends   -------
        private void ThreadEnds(FileInfo file, bool threadSuccesfull)
        {
            //History
            if (threadSuccesfull)
            {
                historyController.AddToHistory(file.Name, file.FullName, file.LastWriteTime.ToString("dd/MM/yy HH:mm:ss"));
            }
            //Threads number
            LoadedFilesController._NUMBER_OF_THREADS--;
            //Log
            loggerController.Add(" # Threads number: " + LoadedFilesController._NUMBER_OF_THREADS);
        }

        public void SimpleSubstitutionEncryption(FileInfo file, FormModel model)
        {
            bool threadSuccesfull = false;
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

                            if (LoadedFilesController._END_OF_PROGRAM_KILL_THREADS)
                            {
                                sr.Close();
                                sw.Close();
                                File.Delete(outputFileName);
                                Thread.CurrentThread.Abort();
                            }
                        }
                    }
                }
                threadSuccesfull = true;
            }
            catch (Exception ex)
            {
                loggerController.Add(" ? Enc exception: " + ex.Message);
                threadSuccesfull = false;
            }
            finally
            {
                this.ThreadEnds(file, threadSuccesfull);
            }

        }

        public void SimpleSubstitutionDecryption(FileInfo file, FormModel model)
        {
            bool threadSuccesfull = false;
            try
            {
                //OutputFileName
                string outputFileName = FileNameCreator.CreateFileDecryptedName(
                    model.Folders.OutputFolder,
                    file.Name);

                //Log
                loggerController.Add(" ! File dec: " + file.Name + ", Alg: " + model.AlgorithmName);

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

                            if (LoadedFilesController._END_OF_PROGRAM_KILL_THREADS)
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
            }
            catch (Exception ex)
            {
                loggerController.Add(" ? Dec exception: " + ex.Message);
                threadSuccesfull = false;
            }
            finally
            {
                this.ThreadEnds(file, threadSuccesfull);
            }
        }
    }
}