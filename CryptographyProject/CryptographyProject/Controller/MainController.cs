using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptographyProject.Model;
using System.IO;
using System.Threading;

namespace CryptographyProject.Controller
{
    public class MainController
    {
        //Form model
        private FormModel _dataModel;
        public FormModel DataModel
        {
            get
            {
                if (_dataModel == null)
                {
                    _dataModel = new FormModel();
                }
                return _dataModel;
            }
        }

        //File Controller
        private LoadedFilesController loadedFilesController;

        //FileWatcher
        private FileSystemWatcher watcher;

        //HistroyData
        private HistoryController historyController;

        //Constructor
        public MainController()
        {
            historyController = new HistoryController();
            loadedFilesController = new LoadedFilesController(historyController);

            //Watcher
            watcher = new FileSystemWatcher();
            watcher.NotifyFilter = NotifyFilters.LastWrite
                    | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = "*.txt";
            watcher.Changed += new FileSystemEventHandler(OnChanged);
        }

        //Validator
        public void ValidateData()
        {
            //This is not the right way to do this, we should use ValidationArguments for the properties of the classes

            //Algorithm selection validation - if it's not valid it will throw exception
            var alg = this.DataModel.AlgorithmIndex;
            var algname = this.DataModel.AlgorithmName;

            //Folders validator - if it's not valid it will throw exception
            var inputFolder = this.DataModel.Folders.InputFolder;
            var outputFolder = this.DataModel.Folders.OutputFolder;

            //Encryption alphabet
            var encryptionAlphabet = EncryptionAlgorithms.SimpleSubstituionCipher.EncryptionAlphabetChars;
        }

        //Starting the watcher and the main functionality
        public void StartTheProcess()
        {
            if (watcher == null)
            {
                throw new Exception("File watcher is null!");
            }

            //Load the existing files
            this.LoadAllFiles();

            //Start the file watcher
            watcher.Path = this.DataModel.Folders.InputFolder;
            watcher.EnableRaisingEvents = true;

            //Start the LoadedFilesControllerProcesses
            new Thread(() => loadedFilesController.StartEncDec(this.DataModel)).Start();
        }

        //Stopping the watcher and the main functionality
        public void StopTheProcess()
        {
            if (watcher == null)
            {
                throw new Exception("File watcher is null!");
            }

            //Stop the file watcher
            watcher.EnableRaisingEvents = false;

            //Stop the LoadedFilesControllerProcesses
            loadedFilesController.StopEncDec();

            //Save the history
            historyController.WriteHistory();
        }

        //File validator
        private bool FileNotValid(FileInfo file)
        {
            //Extension validation
            if (!file.Extension.Equals(".txt"))
            {
                return true;
            }

            //Want encryption and file cotains ENC prefix the name property
            if (this.DataModel.EncryptionChosen && file.Name.ToLower().Contains(FormModel.ENC.ToLower()))
            {
                return true;
            }

            //Want decryption and file does not contain ENC prefix in the name property
            if (!this.DataModel.EncryptionChosen && !file.Name.ToLower().Contains(FormModel.ENC.ToLower()))
            {
                return true;
            }

            //File was in database, so we can skip it, this file was encrypted sometime
            if (historyController.FileExists(file))
            {
                return true;
            }

            return false;
        }

        //Load all files at the start
        private void LoadAllFiles()
        {
            FileInfo file = null;
            string[] files = Directory.GetFiles(this.DataModel.Folders.InputFolder);
            foreach (string item in files)
            {
                file = new FileInfo(item);
                if (FileNotValid(file))//Validate
                {
                    continue;
                }
                loadedFilesController.Add(file);
            }
        }

        //Watcher ChangeEvent
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            var file = new FileInfo(e.FullPath);

            //Validate if the file is NOT:
            // - already encrypted using -> FormModel.ENC
            // - old (file was encrypted before, there is data in the "database" using -> 
            if (FileNotValid(file))
            {
                return;
            }

            loadedFilesController.Add(file);
        }
    }
}
