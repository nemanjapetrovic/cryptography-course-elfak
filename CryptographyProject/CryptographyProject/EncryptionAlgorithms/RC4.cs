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

        public static byte[] Encrypt()
        {
            return null;
        }

        public static byte[] Decrypt()
        {
            return null;
        }
    }
}
