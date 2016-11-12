using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyProject.Common
{
    public class UtilConverter
    {
        public static string FromCharArrayToString(char[] start)
        {
            string ret = "";
            for (int i = 0; i < start.Length; i++)
            {
                ret += start[i];
            }

            return ret;
        }

        public static char[] GetAsciiChars(string source, string key)
        {
            Encoding ascii = Encoding.ASCII;
            Encoding enc_default = Encoding.Default;

            byte[] asciiBytes = Encoding.Convert(enc_default, ascii, enc_default.GetBytes(key));
            char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];

            ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);

            return asciiChars;
        }
    }
}
