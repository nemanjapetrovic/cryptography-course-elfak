using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;

namespace CryptographyProject.Controller
{
    public class LoadedFilesController
    {
        //Files (Disposable!)
        private BlockingCollection<FileInfo> queueFiles;
        private static int _NUMBER_OF_THREADS; //Number of current threads

        //Constructor
        public LoadedFilesController()
        {
            queueFiles = new BlockingCollection<FileInfo>();
            _NUMBER_OF_THREADS = 0;
        }

        //Adds a file in a queue
        public void Add(FileInfo file)
        {
            this.queueFiles.Add(file);
        }

        //Starts the whole process
        public void StartEncDec()
        {
            
        }
        
        //Stops the whole process
        public void StopEncDec()
        {

        }

        //Encryption
        private void ReadFileAndEncrypt()
        {

        }

        //Decryption
        private void ReadFileAndDecrypt()
        {

        }
    }
}
