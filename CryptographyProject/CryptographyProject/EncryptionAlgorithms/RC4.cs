using CryptographyProject.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyProject.EncryptionAlgorithms
{
    public class RC4
    {
        //Loaded Key
        public static byte[] _Key;
        public static byte[] Key
        {
            set
            {
                _Key = value;
            }
            get
            {
                if (_Key == null || _Key.Length == 0)
                {
                    throw new Exception("The key is empty!");
                }

                return _Key;
            }
        }

        public static byte[] KSA()
        {
            byte tmp;
            byte[] state = new byte[Constants.RC4Algorithm.LENGTH];

            for (int i = 0; i < Constants.RC4Algorithm.LENGTH; i++)
            {
                state[i] = Convert.ToByte(i);
            }

            int j = 0;

            for (int i = 0; i < Constants.RC4Algorithm.LENGTH; i++)
            {
                j = (j + state[i] + Key[i % Key.Length]) % Constants.RC4Algorithm.LENGTH;
                tmp = state[i];
                state[i] = state[j];
                state[j] = tmp;
            }

            return state;
        }

        public static byte PRGA(ref int i, ref int j, ref byte[] state)
        {
            i = (i + 1) % Constants.RC4Algorithm.LENGTH;
            j = (j + state[i]) % Constants.RC4Algorithm.LENGTH;
            var tmp = state[i];
            state[i] = state[j];
            state[j] = tmp;
            int index = ((state[i] + state[j]) % Constants.RC4Algorithm.LENGTH);
            return state[index];
        }

        public static byte Encrypt(byte input, byte prga)
        {
            return (byte)(input ^ prga);
        }

        public static byte Decrypt(byte input, byte prga)
        {
            return (byte)(input ^ prga);
        }
    }
}
