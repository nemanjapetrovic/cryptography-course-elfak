using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;
using CryptographyProject.Model;
using System.Threading;

namespace CryptographyProject.Controller
{
    public class LoadedFilesController
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

                    if (model.EncryptionChosen)
                    {
                        new Thread(() => ReadFileAndEncrypt()).Start();
                    }
                    else
                    {
                        new Thread(() => ReadFileAndDecrypt()).Start();
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

        //Encryption -----------
        private void ReadFileAndEncrypt()
        {
            try
            {
                for (int i = 0; i < 1000; i++)
                {
                    Console.WriteLine(i);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.ThreadEnds();
            }

        }

        //Decryption ------------
        private void ReadFileAndDecrypt()
        {
            try
            {
                for (int i = 0; i < 1000; i++)
                {
                    Console.WriteLine(i);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.ThreadEnds();
            }
        }
    }
}