using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;

namespace MotorProtection.Core.Crypto
{
    public static class CryptoUtils
    {
        public static string BytesToString(byte[] bytes)
        {
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                strBuilder.Append(bytes[i] < 16 ? "0" + bytes[i].ToString("x") : bytes[i].ToString("x"));
            }
            return strBuilder.ToString();
        }

        public static byte[] StringToBytes(string str)
        {
            byte[] bytes = new byte[str.Length / 2];
            for (int i = 0; i < str.Length / 2; i++)
            {
                bytes[i] = Convert.ToByte(str.Substring(i * 2, 2), 16);
            }
            return bytes;
        }

        public static string ComputeHash(string source)
        {
            return GetHashAlgo().ComputeHash(source);
        }

        private static HashAlgo GetHashAlgo()
        {
            return new MD5Algo();
        }
    }
}
