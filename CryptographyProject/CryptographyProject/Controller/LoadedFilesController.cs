using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;
using CryptographyProject.Model;
using System.Threading;
using CryptographyProject.EncryptionAlgorithms;

namespace CryptographyProject.Controller
{
    public class LoadedFilesController : IEncryptionAlgorithm
    {
        //Queue for files
        private BlockingCollection<FileInfo> queueFiles;//Files (Disposable!)

        //Thread data
        private static int _NUMBER_OF_THREADS; //Number of current threads
        private static bool _isRunning;

        //Constructor
        public LoadedFilesController()
        {
            queueFiles = new BlockingCollection<FileInfo>();

            //Reset
            _NUMBER_OF_THREADS = 0;
            _isRunning = false;
        }

        //Adds a file in a queue
        public void Add(FileInfo file)
        {
            this.queueFiles.Add(file);
        }

        //Starts the whole process
        public void StartEncDec(FormModel model)
        {
            LoadedFilesController._isRunning = true;
            while (true)
            {
                if (!LoadedFilesController._isRunning)
                {
                    break;
                }

                if (LoadedFilesController._NUMBER_OF_THREADS < model.ThreadsNumber)
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

                        //default: // LOG this throw new Exception("Switch case algortihm does not exists!");
                    }                    
                }
            }
        }

        //Stops the whole process
        public void StopEncDec()
        {
            LoadedFilesController._isRunning = false;
        }

        //Call this as event handler when thread ends --------------DB
        private void ThreadEnds()
        {
            LoadedFilesController._NUMBER_OF_THREADS--;
        }

        public void SimpleSubstitutionEncryption(FileInfo file, FormModel model)
        {
            try
            {
                //Create full valid path for an output file
                StringBuilder sb = new StringBuilder();
                sb.Append(model.Folders.OutputFolder)
                  .Append("\\")
                  .Append(FormModel.ENC)
                  .Append("_")
                  .Append(model.AlgorithmName)
                  .Append("_")
                  .Append(file.Name);

                //Read a file char by char, and encrypt it
                using (StreamReader sr = new StreamReader(file.FullName))
                {
                    using (StreamWriter sw = new StreamWriter(sb.ToString()))
                    {
                        while (sr.Peek() >= 0)
                        {
                            char character = (char)sr.Read();
                            character = Char.ToLower(character);
                            if (character < 97 || character > 122)
                            {
                                continue;
                            }
                            sw.Write(SimpleSubstituionCipher.Encrypt(character));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Log the exception                
            }
            finally
            {
                this.ThreadEnds();
            }

        }

        public void SimpleSubstitutionDecryption(FileInfo file, FormModel model)
        {
            try
            {
                //Create full valid path for an output file
                StringBuilder sb = new StringBuilder();
                sb.Append(model.Folders.OutputFolder)
                  .Append("\\")
                  .Append(file.Name.Replace(FormModel.ENC, "").Replace(model.AlgorithmName, "").Replace("_", ""));
                  
                //Read a file char by char, and encrypt it
                using (StreamReader sr = new StreamReader(file.FullName))
                {
                    using (StreamWriter sw = new StreamWriter(sb.ToString()))
                    {
                        while (sr.Peek() >= 0)
                        {
                            char character = (char)sr.Read();
                            character = Char.ToUpper(character);
                            if (character < 65 || character > 90)
                            {
                                continue;
                            }
                            sw.Write(SimpleSubstituionCipher.Decrypt(character));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Log
            }
            finally
            {
                this.ThreadEnds();
            }
        }
    }
}