using System;
using System.IO;

namespace CryptographyProject.Model
{
    /// <summary>
    /// Data for the folders from the main form.
    /// </summary>
    public class Folders
    {
        private string _inputFolder;
        public string InputFolder
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("Input folder field is empty!");
                }

                if (!Directory.Exists(value))
                {
                    throw new Exception("Defined folder does not exist!");
                }
                _inputFolder = value;
            }
            get
            {
                if (string.IsNullOrEmpty(_inputFolder))
                {
                    throw new Exception("Input folder field is empty!");
                }

                if (!Directory.Exists(_inputFolder))
                {
                    throw new Exception("Defined folder does not exist!");
                }
                return _inputFolder;
            }
        }

        private string _outputFolder;
        public string OutputFolder
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("Output folder field is empty!");
                }

                if (!Directory.Exists(value))
                {
                    throw new Exception("Defined folder does not exist!");
                }
                _outputFolder = value;
            }
            get
            {
                if (string.IsNullOrEmpty(_outputFolder))
                {
                    throw new Exception("Output folder field is empty!");
                }

                if (!Directory.Exists(_outputFolder))
                {
                    throw new Exception("Defined folder does not exist!");
                }
                return _outputFolder;
            }
        }
    }
}
