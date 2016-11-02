using CryptographyProject.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyProject.EncryptionAlgorithms
{
    /// <summary>
    /// Simple substitution cipher algorithm implementation.
    /// </summary>
    public class SimpleSubstituionCipher
    {
        //Standard data
        public static readonly char[] StandardAlphabet =
        {
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'
        };

        //Loaded or imported data
        private static char[] _EncryptionAlphabetChars;
        public static char[] EncryptionAlphabetChars
        {
            set
            {
                if (value.Length > Constants.SimpleSubstitutionAlgorithm.NUMBER_OF_CHARS ||
                    value.Length < Constants.SimpleSubstitutionAlgorithm.NUMBER_OF_CHARS)
                {
                    throw new Exception("Encryption alphabet chars is not valid length!");
                }

                _EncryptionAlphabetChars = value.ToArray<char>();
            }
            get
            {
                if (_EncryptionAlphabetChars == null || _EncryptionAlphabetChars.Length == 0)
                {
                    throw new Exception("Encryption alphabet chars is not set!");
                }

                if (_EncryptionAlphabetChars.Length < Constants.SimpleSubstitutionAlgorithm.NUMBER_OF_CHARS ||
                    _EncryptionAlphabetChars.Length > Constants.SimpleSubstitutionAlgorithm.NUMBER_OF_CHARS)
                {
                    throw new Exception("Encryption alphabet chars is not valid length!");
                }

                return _EncryptionAlphabetChars;
            }
        }

        public static char Encrypt(char character)
        {
            int index = Array.IndexOf(StandardAlphabet, character);
            return Char.ToUpper(EncryptionAlphabetChars[index]);
        }

        public static char Decrypt(char character)
        {
            int index = Array.IndexOf(EncryptionAlphabetChars, character);
            return Char.ToLower(StandardAlphabet[index]);
        }
    }
}
