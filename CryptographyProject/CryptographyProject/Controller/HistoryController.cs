using CryptographyProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace CryptographyProject.Controller
{
    /// <summary>
    /// History controller contains the instances of history files and operations on those instances.
    /// History file class is about files that are already encrypted/decrypted.
    /// </summary>
    public class HistoryController
    {
        private const string _HISTORY_FILE = "history.json";
        public List<HistoryFiles> historyFiles;

        public HistoryController()
        {
            historyFiles = LoadHistoryFiles();
        }

        //Removes history.json file
        public void FlushHistory()
        {
            //If the file exists - remove it
            if (!File.Exists(HistoryController._HISTORY_FILE))
            {
                historyFiles.Clear();
                using (File.Create(HistoryController._HISTORY_FILE)) { }
                return;
            }
            //Fiel does not exsit, just create a new empty one
            File.WriteAllText(HistoryController._HISTORY_FILE, String.Empty);
            historyFiles.Clear();
        }

        //Create a new hisotry file object and write it to history.json
        public void AddToHistory(string filename, string path, string datemodified)
        {
            historyFiles.Add(new HistoryFiles()
            {
                FileName = filename,
                Path = path,
                DateModified = datemodified
            });
        }

        //Writing the history.json file data
        public void WriteHistory()
        {
            using (StreamWriter sw = new StreamWriter(HistoryController._HISTORY_FILE))
            {
                string json = JsonConvert.SerializeObject(historyFiles);
                sw.Write(json);
            }
        }

        //Load the data from the history file
        public List<HistoryFiles> LoadHistoryFiles()
        {
            try
            {
                using (StreamReader sr = new StreamReader(HistoryController._HISTORY_FILE))
                {
                    string json = sr.ReadToEnd();
                    if (string.IsNullOrEmpty(json))
                    {
                        return new List<HistoryFiles>();
                    }
                    return JsonConvert.DeserializeObject<List<HistoryFiles>>(json);
                }
            }
            catch (Exception)
            {
                using (File.Create(HistoryController._HISTORY_FILE)) { }
                return new List<HistoryFiles>(); // probably there is no history.json
            }
        }

        //Check if the file was already encrypted/decrypted and if it's is present in history.json file
        public bool FileExists(FileInfo file)
        {
            var tmp = new HistoryFiles()
            {
                DateModified = file.LastWriteTime.ToString("dd/MM/yy HH:mm:ss"),
                FileName = file.Name,
                Path = file.FullName
            };

            foreach (var item in historyFiles)
            {
                if (item.Equals(tmp))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
