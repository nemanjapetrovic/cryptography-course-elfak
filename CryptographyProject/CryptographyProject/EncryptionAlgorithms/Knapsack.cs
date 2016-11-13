using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyProject.EncryptionAlgorithms
{
    public class KnapsackAlg
    {
        public static int _N = -1;
        public static int N
        {
            set { _N = value; }
            get
            {
                if (_N == -1)
                {
                    throw new Exception("N is not set!");
                }
                return _N;
            }
        }

        public static int _m = -1;
        public static int m
        {
            set { _m = value; }
            get
            {
                if (_m == -1)
                {
                    throw new Exception("m is not set!");
                }
                return _m;
            }
        }

        public static int[] _j = new int[8];
        public static int[] J
        {
            set { _j = value; }
            get
            {
                if (_j == null || _j.Length == 0)
                {
                    throw new Exception("J is not defined!");
                }
                if (_j[0] == -1)
                {
                    throw new Exception("J is not defined!");
                }
                return _j;
            }
        }

        public static int[] _p = new int[8];
        public static int[] P
        {
            set { _p = value; }
            get
            {
                if (_p == null || _p.Length == 0)
                {
                    throw new Exception("P is not defined!");
                }
                if (_p[0] == -1)
                {
                    throw new Exception("P is not defined!");
                }
                return _p;
            }
        }

        public static int im;
        public static void GenerateKeys(int n, int M)
        {
            N = n; m = M;
            im = 1;
            while (im * m % N != 1)
                im++;
            Random rnd = new Random();
            P[0] = rnd.Next(1, 10);
            P[1] = P[0] + rnd.Next(1, 20);
            P[2] = P[0] + P[1] + rnd.Next(1, 20);
            P[3] = P[0] + P[1] + P[2] + rnd.Next(1, 20);
            P[4] = P[0] + P[1] + P[2] + P[3] + rnd.Next(1, 20);
            P[5] = P[0] + P[1] + P[2] + P[3] + P[4] + rnd.Next(1, 20);
            P[6] = P[0] + P[1] + P[2] + P[3] + P[4] + P[5] + rnd.Next(1, 20);
            P[7] = P[0] + P[1] + P[2] + P[3] + P[4] + P[5] + P[6] + rnd.Next(1, 20);

            for (int i = 0; i < 8; i++)
                J[i] = (P[i] * m % N);
        }

        public static void SetKeys(int[] keysP, int n, int M)
        {
            for (int i = 0; i < 8; i++)
            {
                P[i] = keysP[i];
            }

            N = n;
            m = M;
            im = 1;
            while (im * m % N != 1)
            {
                im++;
            }
            for (int i = 0; i < 8; i++)
                J[i] = (P[i] * m % N);
        }

        public static string Encrypt(byte[] fileBytes)
        {
            //Read all bytes
            string encrypted_stream = "";
            for (int i = 0; i < fileBytes.Length; i++)
            {
                int sum = 0;
                for (int j = 0; j < 8; j++)
                {
                    if (fileBytes[i] % 2 != 0)
                        sum += J[7 - j];
                    fileBytes[i] >>= 1;
                }
                encrypted_stream += sum.ToString() + " ";
            }
            return encrypted_stream;
            //write all text       
        }

        public static byte[] Decrypt(string Data)
        {
            //Read all Text
            byte[] decrypted_byte_array = new byte[Data.Length];
            string[] encrypted_array = Data.Split(' ');
            for (int i = 0; i < encrypted_array.Length; i++)
            {
                int T;
                try
                {
                    T = int.Parse(encrypted_array[i]);
                }
                catch
                {
                    continue;
                }
                int TC = T * im % N;
                byte decrypted_byte = 0;
                for (int k = 0; k < 8; k++)
                {
                    if (TC >= P[7 - k])
                    {
                        decrypted_byte += (byte)Math.Pow(2.0, (double)k);
                        TC -= P[7 - k];
                    }
                }
                decrypted_byte_array[i] = decrypted_byte;
            }
            return decrypted_byte_array;
            //Write all bytes
        }




    }
}
