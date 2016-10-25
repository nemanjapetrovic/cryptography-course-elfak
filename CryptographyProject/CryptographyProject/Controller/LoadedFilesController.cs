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

        public LoadedFilesController()
        {
            queueFiles = new BlockingCollection<FileInfo>();            
        }

        public void Add(FileInfo file)
        {
            this.queueFiles.Add(file);
        }
    }
}
