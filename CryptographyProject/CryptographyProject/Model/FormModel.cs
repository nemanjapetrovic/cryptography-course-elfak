using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyProject.Model
{
    /// <summary>
    /// Collecting data from the form controls, model for the main form view.
    /// </summary>
    public class FormModel
    {
        //Folders
        private Folders _folders;
        public Folders Folders
        {
            set { _folders = value; }
            get { return _folders; }
        }

        //Threads
        private int _threadsNumber;
        public int ThreadsNumber
        {
            set { _threadsNumber = value; }
            get { return _threadsNumber; }
        }

        //Started/Stopped
        private bool _coderStarted;
        public bool CoderStarted
        {
            set { _coderStarted = value; }
            get { return _coderStarted; }
        }

        //Algoritham choice
        private int _algorithmIndex = -1;
        public int AlgorithmIndex
        {
            set { _algorithmIndex = value; }
            get
            {
                if (_algorithmIndex < 0)
                {
                    throw new Exception("Choose one algorithm!");
                }
                return _algorithmIndex;
            }
        }

        //Algorithm name
        private string _algorithmName;
        public string AlgorithmName
        {
            set { _algorithmName = value; }
            get
            {
                if (string.IsNullOrEmpty(_algorithmName))
                {
                    throw new Exception("Choose one algorithm!");
                }
                return _algorithmName;
            }
        }

        //Encryption or decryption
        private bool _encryptionChosen;
        public bool EncryptionChosen
        {
            set { _encryptionChosen = value; }
            get { return _encryptionChosen; }
        }
    }
}
