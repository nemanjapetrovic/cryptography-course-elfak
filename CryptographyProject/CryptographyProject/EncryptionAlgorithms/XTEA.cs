using CryptographyProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyProject.EncryptionAlgorithms
{
    /// <summary>
    /// Written by Nenad Kragovic
    /// https://www.linkedin.com/in/nenad-kragovic-397b76106
    /// </summary>  
    public class XTEA
    {
        public static string key;

        public static UInt32[] K = new UInt32[4];

        public static string Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
                string asciiKey = UtilConverter.FromCharArrayToString(UtilConverter.GetAsciiChars(key, key));

                int point = 0;
                for (int i = 0; i < K.Length; i++)
                {
                    uint output;

                    output = ((uint)asciiKey[point] - 48);
                    output += (((uint)asciiKey[point + 1] - 48) << 8);
                    output += (((uint)asciiKey[point + 2] - 48) << 16);
                    output += (((uint)asciiKey[point + 3] - 48) << 24);
                    point += 4;
                    K[i] = output;
                }


            }
        }

        public static UInt32[] iv = new UInt32[2];
        public static string Iv;
        public static string IV
        {
            get
            {
                return Iv;
            }
            set
            {
                key = value;
                if (key != "")
                {
                    string asciiKey = UtilConverter.FromCharArrayToString(UtilConverter.GetAsciiChars(key, key));

                    int point = 0;
                    for (int i = 0; i < iv.Length; i++)
                    {
                        uint output;

                        output = ((uint)asciiKey[point] - 48);
                        output += (((uint)asciiKey[point + 1] - 48) << 8);
                        output += (((uint)asciiKey[point + 2] - 48) << 16);
                        output += (((uint)asciiKey[point + 3] - 48) << 24);
                        point += 4;
                        iv[i] = output;
                    }
                }

            }
        }

        public static uint delta = 0x9e3779b9;

        public static byte[] Encrypt(byte[] Data)
        {
            UInt64[] res = new UInt64[Data.Length / 2];

            int k = 0;

            for (int j = 0; j < Data.Length; j += 2)
            {
                uint L = Data[j];
                uint R = Data[j + 1];

                uint L1 = L;
                uint R1 = R;

                uint v0 = L, v1 = R, sum = 0;
                for (int i = 0; i < 32; i++)
                {
                    v0 += (((v1 << 4) ^ (v1 >> 5)) + v1) ^ (sum + K[sum & 3]);
                    sum += delta;
                    v1 += (((v0 << 4) ^ (v0 >> 5)) + v0) ^ (sum + K[(sum >> 11) & 3]);
                }
                L = v0; R = v1;

                res[k++] = ((UInt64)L << 32) | (UInt64)R;
            }

            byte[] result = new byte[res.Length * sizeof(UInt64)];

            k = 0;
            foreach (UInt64 o in res)
            {
                byte[] temp = BitConverter.GetBytes(o);
                for (int i = 0; i < sizeof(UInt64); i++)
                    result[k + i] = temp[i];
                k += 8;
            }

            return result;
        }

        public static byte[] Decrypt(byte[] Data)
        {
            UInt64[] o = new UInt64[((byte[])Data).Length / 8];

            int x = 0;
            for (int i = 0; i < Data.Length; i += 8)
            {
                o[x] = (UInt64)Data[i] | (UInt64)Data[i + 1] << 8 | (UInt64)Data[i + 2] << 16 |
                    (UInt64)Data[i + 3] << 24 | (UInt64)Data[i + 4] << 32 | (UInt64)Data[i + 5] << 40 |
                    (UInt64)Data[i + 6] << 48 | (UInt64)Data[i + 7] << 56;

                x++;
            }

            byte[] dataBytes = new byte[o.Length * 2];
            x = 0;

            for (int j = 0; j < o.Length; j++)
            {
                uint L = Convert.ToUInt32(o[j] >> 32);
                uint R = Convert.ToUInt32(o[j] & 0x00000000FFFFFFFF);

                uint L1 = L;
                uint R1 = R;

                uint v0 = L, v1 = R, sum = delta * 32;
                for (int i = 0; i < 32; i++)
                {
                    v1 -= (((v0 << 4) ^ (v0 >> 5)) + v0) ^ (sum + K[(sum >> 11) & 3]);
                    sum -= delta;
                    v0 -= (((v1 << 4) ^ (v1 >> 5)) + v1) ^ (sum + K[sum & 3]);
                }
                L = v0; R = v1;


                dataBytes[x++] = (byte)L;
                dataBytes[x++] = (byte)R;
            }

            return dataBytes;
        }

    }
}
