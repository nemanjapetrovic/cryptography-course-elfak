using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptographyProject.Model;
using System.IO;
using System.Threading;
using CryptographyProject.Common;
using CryptographyProject.Helper;

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
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);
        }

        //Validator for the data from the model -> view
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
            //FIX THIS
            //if (this.DataModel.AlgorithmIndex == 0)
            //  char[] encryptionAlphabet = EncryptionAlgorithms.SimpleSubstituionCipher.EncryptionAlphabetChars;
        }

        //Starting the watcher and the main functionality
        public void StartTheProcess()
        {
            if (watcher == null)
            {
                throw new Exception("File watcher is null!");
            }

            //Load the existing files in a input folder
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

        //File validator, imported file
        private bool FileNotValid(FileInfo file)
        {
            //Extension validation
            //  if (!file.Extension.Equals(".txt") && this.DataModel.EncryptionChosen)
            //   {
            //       return true;
            //   }

            //Want encryption and the file cotains .enc extension
            if (this.DataModel.EncryptionChosen && file.Extension.ToLower().Contains(Constants.FileName.ENC))
            {
                return true;
            }

            //Want decryption and the file does not contain .enc extension
            if (!this.DataModel.EncryptionChosen && !file.Extension.ToLower().Contains(Constants.FileName.ENC))
            {
                return true;
            }

            //File was in database (history.json), so we can skip it, this file was encrypted/decrypted sometime
            if (historyController.FileExists(file))
            {
                return true;
            }

            //File OK
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

        //Watcher OnChange event
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            var file = new FileInfo(e.FullPath);

            //Validate
            if (FileNotValid(file))
            {
                return;
            }

            loadedFilesController.Add(file);
        }

        //Watcher FileRenamed event
        private void OnRenamed(object source, RenamedEventArgs e)
        {
            var file = new FileInfo(e.FullPath);

            //Validate
            if (FileNotValid(file))
            {
                return;
            }

            loadedFilesController.Add(file);
        }

        //Remove the history data
        public void FlushHistory()
        {
            historyController.FlushHistory();
        }

        //Write the history data
        public void WriteHistory()
        {
            historyController.WriteHistory();
        }
    }
}
