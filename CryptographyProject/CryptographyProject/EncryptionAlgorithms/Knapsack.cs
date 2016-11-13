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
            while (((im * m) % n != 1) && (m < n))
            {
                im++;
            }
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
                J[i] = (P[i] * m) % N;
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
            while (((im * m) % n != 1) && (m < n))
            {
                im++;
            }
            for (int i = 0; i < 8; i++)
                J[i] = (P[i] * m) % N;
        }

        public static int Encrypt(string binaryByte)
        {
            int C = 0;

            for (int i = 0; i < binaryByte.Length; i++)
            {
                char c = binaryByte[i];
                if (c == '1')
                {
                    C += Convert.ToInt32(J[i]);
                }
            }

            return C;
        }


        public static string Decrypt(int C)
        {
            int n = Convert.ToInt32(KnapsackAlg.N);
            int im = Convert.ToInt32(KnapsackAlg.im);

            int d = (im * C) % n;
            int startIndex = 7;
            string dcr = "";

            while (startIndex >= 0 && d > 0)
            {
                int val = (int)P[startIndex];
                if (val > d)
                {
                    dcr = "0" + dcr;
                }
                else
                {
                    dcr = "1" + dcr;
                    d -= val;
                }

                startIndex--;
            }

            return dcr;
        }

    }
}
