using System;
using System.Linq;

namespace RC4
{
    public class RC4
    {
        public RC4(byte[] key)
        {
            Key = KSA(key);
        }

        public RC4(string key)
        {
            Key = KSA(StringToByteArray(key));
        }

        public byte[] Key { get; set; }

        public byte i { get; set; }
        public byte j { get; set; }

        public byte PRGA()
        {
            var temp = (byte)0;

            i = (byte)((i + 1) % 256);
            j = (byte)((j + Key[i]) % 256);

            temp = Key[i];
            Key[i] = Key[j];
            Key[j] = temp;

            return Key[(Key[i] + Key[j]) % 256];
        }

        public static byte[] KSA(byte[] key)
        {

            var keyLength = key.Length;
            var S = new byte[256];

            for (var i = 0; i != 256; i++) S[i] = (byte)i;

            var j = (byte)0;

            for (var i = 0; i != 256; i++)
            {
                j = (byte)((j + S[i] + key[i % keyLength]) % 256);

                var temp = S[i];
                S[i] = S[j];
                S[j] = temp;
            }
            return S;
        }

        public static byte[] StringToByteArray(string str)
        {
            var bytes = new byte[str.Length];
            for (var i = 0; i < str.Length; i++) bytes[i] = (byte)str[i];
            return bytes;
        }
    }

    public class RC4_Core
    {
        public const string InitialKey = "fhsd6f86f67rt8fw78fw789we78r9789wer6re";
        public const string InitialNonce = "nonce";
        public RC4 Encryptor { get; set; }
        public RC4 Decryptor { get; set; }

        public RC4_Core()
        {
            this.InitializeCiphers(RC4_Core.InitialKey + RC4_Core.InitialNonce);
        }

        public RC4_Core(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            this.InitializeCiphers(key);
        }

        public void Encrypt(ref byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            for (var k = 0; k < data.Length; k++)
                data[k] ^= this.Encryptor.PRGA();
        }

        public void Decrypt(ref byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            for (var k = 0; k < data.Length; k++)
                data[k] ^= this.Decryptor.PRGA();
        }

        public void UpdateCiphers(uint clientSeed, byte[] serverNonce)
        {
            if (serverNonce == null)
                throw new ArgumentNullException(nameof(serverNonce));

            var newNonce = RC4_Core.ScrambleNonce((ulong)clientSeed, serverNonce);
            var key = RC4_Core.InitialKey + newNonce;
            this.InitializeCiphers(key);
        }

        public void InitializeCiphers(string key)
        {
            this.Encryptor = new RC4(key);
            this.Decryptor = new RC4(key);

            for (var k = 0; k < key.Length; k++)
            {
                this.Encryptor.PRGA();
                this.Decryptor.PRGA();
            }
        }

        public static byte[] GenerateNonce()
        {
            var buffer = new byte[new Random().Next(15, 25)];
            new Random().NextBytes(buffer);
            return buffer;
        }

        public static string ScrambleNonce(ulong clientSeed, byte[] serverNonce)
        {
            var scrambler = new Scrambler(clientSeed);
            var byte100 = 0;
            for (var i = 0; i < 100; i++)
                byte100 = scrambler.GetByte();
            return serverNonce.Aggregate(string.Empty,
                (current, t) => current + (char)(t ^ (scrambler.GetByte() & byte100)));
        }
    }
}
