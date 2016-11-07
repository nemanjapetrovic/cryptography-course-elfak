using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyProject.Helper
{
    internal class Constants
    {
        internal static class FileName
        {
            public const string ENC = ".enc";
        }

        internal static class History
        {
            public const string HISTORY_FILE = "history.json";
        }

        internal static class LogCommands
        {
            public const string CLEAR = "clear";
        }

        internal static class SimpleSubstitutionAlgorithm
        {
            //Maximum length of the encoding alphabet
            public const int NUMBER_OF_CHARS = 26;
        }

        internal static class RC4Algorithm
        {
            //One byte => 8bits => 2^8
            public const int LENGTH = 256;
        }
    }
}
