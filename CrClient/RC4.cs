using RC4;

namespace CrClient
{
    public class RC4
    {
        public static RC4_Core RC4Core = new RC4_Core();
        public static byte[] Encrypt(byte[] payload)
        {
            byte[] Encrypted = payload;
                RC4Core.Encrypt(ref Encrypted);
            return Encrypted;
        }
        public static byte[] Decrypt(byte[] payload)
        {
            byte[] Decrypted = payload;
            RC4Core.Decrypt(ref Decrypted);
            return Decrypted;
        }
    }
}
