using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptographyProject.Logic
{
    public class MainController
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
        private int _algorithmIndex;
        public int AlgorithmIndex
        {
            set { _algorithmIndex = value; }
            get { return _algorithmIndex; }
        }

        //Validator
        public bool ValidateData()
        {
            return false;
        }

        public void StartTheProcess()
        {
            
        }

        public void StopTheProcess()
        {
            
        }
    }
}
