using CryptographyProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace CryptographyProject.Controller
{
    public class HistoryController
    {
        //Files
        public List<HistoryFiles> historyFiles;

        public HistoryController()
        {
            historyFiles = LoadHistoryFiles();
        }

        public void ClearHistory()
        {
            try
            {
                if (!File.Exists("history.json"))
                {
                    historyFiles.Clear();
                    using (File.Create("history.json")) { }
                    return;
                }
                File.WriteAllText("history.json", String.Empty);
                historyFiles.Clear();
            }
            catch (Exception)
            {

            }
        }

        public void AddToHistory(string filename, string path, string datemodified)
        {
            historyFiles.Add(new HistoryFiles()
            {
                FileName = filename,
                Path = path,
                DateModified = datemodified
            });
            WriteHistory();
        }

        public void WriteHistory()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("history.json"))
                {
                    string json = JsonConvert.SerializeObject(historyFiles);
                    sw.Write(json);
                }
            }
            catch (Exception)
            {
            }
        }

        public List<HistoryFiles> LoadHistoryFiles()
        {
            try
            {
                using (StreamReader sr = new StreamReader("history.json"))
                {
                    string json = sr.ReadToEnd();
                    if(string.IsNullOrEmpty(json))
                    {
                        return new List<HistoryFiles>();
                    }
                    return JsonConvert.DeserializeObject<List<HistoryFiles>>(json);
                }
            }
            catch (Exception)
            {
                using (File.Create("history.json")) { }
                return new List<HistoryFiles>(); // probably there is no history.json
            }
        }

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
                if(item.Equals(tmp))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
