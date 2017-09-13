using Sodium;
using System.Linq;
using System.Security.Cryptography;

namespace CrClient
{
    public class Nonce
    {
        public static byte[] GenerateNonce(byte[] publicKey)
        {
            return GenericHash.Hash(publicKey.Concat(Keys.ServerKey).ToArray(),null,24);
        }
        public static byte[] GenerateRandomNonce(int length)
        {
            byte[] d = new byte[length];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetNonZeroBytes(d);
            }
            return d;
        }
    }
}
