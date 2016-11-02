using CryptographyProject.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyProject.Common
{
    /// <summary>
    /// Used to manage .binary file names when we create an output file.
    /// </summary>
    public class FileNameCreatorBIN
    {
        /// <summary>
        /// Calling only when we encrypt files. It's only create file with .enc extension.        
        /// </summary>
        /// <param name="outputFolder"></param>
        /// <param name="fileName"></param>
        /// <param name="algorithmName"></param>
        /// <returns></returns>
        public static string CreateFileEncryptedName(string outputFolder, string fileName, string algorithmName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(outputFolder)
              .Append("\\")
              .Append(Path.GetFileNameWithoutExtension(fileName))
              .Append("_")
              .Append(algorithmName)
              .Append(Constants.FileName.ENC);

            // ..\filename_algorithmname.enc
            return sb.ToString();
        }

        /// <summary>
        /// Calling only when we decrypt files. It's creating file with passed file extension
        /// </summary>
        /// <param name="outputFolder"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string CreateFileDecryptedName(string outputFolder, string fileName, string fileExtension)
        {
            //From "somefile_algorithmname.enc" extract only name "somefile"
            string newFileName = Path.GetFileNameWithoutExtension(fileName);

            if (newFileName.Contains("_"))
            {
                newFileName = newFileName.Substring(0, fileName.IndexOf("_"));
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(outputFolder)
              .Append("\\")
              .Append(newFileName)
              .Append(fileExtension);

            // ..\somefile.someextension
            return sb.ToString();
        }
    }
}
