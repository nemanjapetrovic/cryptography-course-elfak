using CryptographyProject.Helper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptographyProject.Controller
{
    /// <summary>
    /// Logging data into a view list box.
    /// </summary>
    public class LoggerController
    {
        //ThreadLock
        public static bool _LOG_THREAD_RUNNING;

        //Listbox
        public static ListBox listBox;

        //Queue for files
        private BlockingCollection<string> queueLogs;

        //Constructor
        public LoggerController()
        {
            queueLogs = new BlockingCollection<string>();
            _LOG_THREAD_RUNNING = true;
        }

        //Adding a data to log
        public void Add(string log)
        {
            queueLogs.Add(log);
        }

        //Work in a thread
        public void PrintLog()
        {
            _LOG_THREAD_RUNNING = true;
            while (_LOG_THREAD_RUNNING)
            {
                //Add a new data into a listbox             
                if (queueLogs.Count > 0)
                {
                    var item = queueLogs.Take();

                    if (item.Equals(Constants.LogCommands.CLEAR))
                    {
                        listBox.Invoke(new Action(() => listBox.Items.Clear()));
                    }

                    listBox.Invoke(new Action(() => listBox.Items.Add(item)));
                }
                Thread.Sleep(1);
            }
        }
    }
}
