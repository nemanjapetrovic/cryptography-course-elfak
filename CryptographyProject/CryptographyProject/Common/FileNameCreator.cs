using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyProject.Common
{
    public class FileNameCreator
    {
        //Additional file name        
        public const string ENC = ".enc";

        /// <summary>
        /// Calling only when we encrypt files. It's only create file with .enc extension. File that is created is NOT binary file.
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
              .Append(FileNameCreator.ENC);

            //C:/Tmp/somefile_algorithmname.enc
            return sb.ToString();
        }

        /// <summary>
        /// Calling only when we decrypt files. It's creating a .txt file name.
        /// </summary>
        /// <param name="outputFolder"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string CreateFileDecryptedName(string outputFolder, string fileName)
        {
            //From "somefile_algorithmname.enc" extract only name "somefile"
            string newFileName = Path.GetFileNameWithoutExtension(fileName).Substring(0, fileName.IndexOf("_"));

            StringBuilder sb = new StringBuilder();
            sb.Append(outputFolder)
              .Append("\\")
              .Append(newFileName)
              .Append(".txt");

            //C:/Tmp/somefile.txt
            return sb.ToString();
        }
    }
}
