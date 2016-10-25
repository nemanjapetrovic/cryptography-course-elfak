using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptographyProject.Model;

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
        private LoadedFilesController fileController;

        //FileWatcher


        //Constructor
        public MainController()
        {
            fileController = new LoadedFilesController();
        }

        //Validator
        public void ValidateData()
        {
            //This is not the right way to do this, we should use ValidationArguments for the properties of the classes

            //Algorithm selection validation - if it's not valid it will throw exception
            var alg = DataModel.AlgorithmIndex;

            //Folders validator - if it's not valid it will throw exception
            var inputFolder = DataModel.Folders.InputFolder;
            var outputFolder = DataModel.Folders.OutputFolder;
        }

        public void StartTheProcess()
        {

        }

        public void StopTheProcess()
        {

        }
    }
}
