using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyProject.Common
{
    public class UtilConverter
    {
        /// <summary>
		/// Converts a string of length 4 to a 32-bit unsigned integer.
		/// </summary>
		/// <param name="Input">The string of length 4 to convert.</param>
		/// <returns>An encoded integer value representing a string of length 4.</returns>
		public static uint ConvertStringToUInt(string Input)
        {
            uint output;
            output = ((uint)Input[0]);
            output += ((uint)Input[1] << 8);
            output += ((uint)Input[2] << 16);
            output += ((uint)Input[3] << 24);
            return output;
        }

        /// <summary>
        /// Converts a 32-bit unsigned integer to a string of length 4.
        /// </summary>
        /// <param name="Input">The unsigned integer to convert to a string.</param>
        /// <returns>A string value represented by the encoded integer.</returns>
        public static string ConvertUIntToString(uint Input)
        {
            StringBuilder output = new System.Text.StringBuilder();
            output.Append((char)((Input & 0xFF)));
            output.Append((char)((Input >> 8) & 0xFF));
            output.Append((char)((Input >> 16) & 0xFF));
            output.Append((char)((Input >> 24) & 0xFF));
            return output.ToString();
        }

        public static string ConvertByteArrayToString(byte[] array)
        {
            string tmp = "";
            foreach (var item in array)
            {
                tmp += Convert.ToString(item);
            }
            return tmp;
        }
    }
}
