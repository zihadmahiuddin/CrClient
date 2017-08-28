using Sodium;
using Blake2Sharp;

namespace CrClient
{
    public class Nonce
    {
        public static byte[] Generate(byte[] publicKey)
        {
            Hasher Blake2b = new Blake2BHasher(new Blake2BConfig() { OutputSizeInBytes = 24 });
            Blake2b.Init();
            Blake2b.Update(publicKey);
            Blake2b.Update(Keys.ServerKey);
            byte[] nonce = Blake2b.Finish();
            return nonce;
        }
    }
}
