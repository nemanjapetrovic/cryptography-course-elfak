using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;
using CryptographyProject.Model;

namespace CryptographyProject.Controller
{
    public class LoadedFilesController
    {
        private BlockingCollection<FileInfo> queueFiles;//Files (Disposable!)
        private static int _NUMBER_OF_THREADS; //Number of current threads
        private static bool _isRunning;

        //Constructor
        public LoadedFilesController()
        {
            queueFiles = new BlockingCollection<FileInfo>();

            _NUMBER_OF_THREADS = 0;
            _isRunning = false;
        }

        //Adds a file in a queue
        public void Add(FileInfo file)
        {
            this.queueFiles.Add(file);
        }

        //Imam lock prom koju stavim na true u start, zatim pokrenem while(true)
        //i sve tako dokle god ne popunim broj procesa pozivam ReadFileAndEcny ili onu drugu
        //u zavisnosti od sta se trazi, kada to zavrsim.
        //pri pozivu novog taska, aktivirace se i izvrsavace se novi thread.
        //kada thread zavrsi posao enc/dec treba da pozove handler, koji smanjuje broj ukupnih aktivnih threadova
        //tako da while petlja moze da aktivira novi thread
        //kada aktivira se stop, loc se setuje na false, tada se gasi ona while petlja i time vidimo da smo zavrsili enc/dec
        //prilikom gasenja pozeljno je da se disposuju svi objekti
        //takodje na event handleru koji se aktivira kad se zavrsi thread snimiti fajl u bazu

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
                    Task task = Task.Factory.StartNew(() => ReadFileAndEncrypt()).ContinueWith(tsk => { TaskEnd(); });
                }
            }
        }

        //Stops the whole process
        public void StopEncDec()
        {
            LoadedFilesController._isRunning = false;
        }

        //Call this as event handler when task is done
        private void TaskEnd()
        {
            LoadedFilesController._NUMBER_OF_THREADS--;
        }

        //Encryption
        private void ReadFileAndEncrypt()
        {
            for(int i = 0; i< 1000;i++)
            {
                Console.WriteLine(i);
            }
        }

        //Decryption
        private void ReadFileAndDecrypt()
        {

        }
    }
}