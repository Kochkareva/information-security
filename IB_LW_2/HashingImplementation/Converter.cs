using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IB_LW_2.HashingImplementation
{
    public static class Converter
    {
        public static byte[] UIntArrayToByteArray(uint[] words)
        {
            byte[] result = new byte[words.Length * 4];

            for (int a = 0; a < words.Length; a++)
            {
                byte[] word = BitConverter.GetBytes(words[a]);
                Array.Reverse(word);

                for (int b = 0; b < 4; b++)
                {
                    result[b + a * 4] = word[b];
                }
            }

            return result;
        }

        public static uint ByteArrayToUInt(byte[] bytes)
        {
            return ((uint)bytes[0] << 24) | ((uint)bytes[1] << 16) | ((uint)bytes[2] << 8) | ((uint)bytes[3]);
        }

        public static byte[] LongToByteArray(long l)
        {
            byte[] array = BitConverter.GetBytes(l);
            Array.Reverse(array);
            return array;
        }

        public static string ToHex(this byte[] bytes)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);

            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString("x2"));

            return result.ToString();
        }
    }
}
