using System;
using System.Linq;
using System.Security.Cryptography;

namespace CrClient
{
    public class Utils
    {
        public static byte[] HexToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        public static byte[] GenerateRandomBytes(int length)
        {
            byte[] bytes = new byte[length];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(bytes);
            return bytes;
        }
    }
}
