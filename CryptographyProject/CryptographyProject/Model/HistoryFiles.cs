using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyProject.Model
{
    /// <summary>
    /// History data, used to get the history of the encrypted/decrypted files. 
    /// Used to check if the file was alredy encrypted/decrypted so we can skip it.
    /// </summary>
    public class HistoryFiles
    {
        public string FileName { get; set; }
        public string DateModified { get; set; }
        public string Path { get; set; }

        public bool Equals(HistoryFiles file)
        {
            int number = 0;
            if (this.FileName.Equals(file.FileName))
            {
                number++;
            }

            if (this.DateModified.Equals(file.DateModified))
            {
                number++;
            }

            if (this.Path.Equals(file.Path))
            {
                number++;
            }

            return (number == 3);
        }
    }
}
