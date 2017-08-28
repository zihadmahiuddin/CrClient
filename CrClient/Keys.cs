using System;
using System.Linq;

namespace CrClient
{
    public class Keys
    {
        //public static byte[] ServerKey = HexToByteArray("ac30dcbea27e213407519bc05be8e9d930e63f873858479946c144895fa3a26b");
        public static byte[] ServerKey = HexToByteArray("72f1a4a4c48e44da0c42310f800e96624e6dc6a641a9d41c3b5039d8dfadc27e");
        internal static byte[] HexToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
