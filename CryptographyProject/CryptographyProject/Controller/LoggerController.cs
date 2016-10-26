using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptographyProject.Controller
{
    public class LoggerController
    {
        //ThreadLock
        public static bool _THREAD_END;
        //Listbox
        public static ListBox listBox;

        //Queue for files
        private BlockingCollection<string> queueLogs;

        //Constructor
        public LoggerController()
        {
            queueLogs = new BlockingCollection<string>();
            _THREAD_END = false;
        }

        public void Add(string log)
        {
            queueLogs.Add(log);
        }

        //Work in a thread
        public void PrintLog()
        {
            _THREAD_END = false;
            while (true)
            {
                if (_THREAD_END)
                {
                    break;
                }

                if (queueLogs.Count > 0)
                {
                    listBox.Invoke(new Action(() => listBox.Items.Add(queueLogs.Take())));
                }
            }
        }
    }
}
