using Sodium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrClient
{
    class EnDecrypt
    {
        internal static byte[] Encrypt(int id,byte[] data)
        {
            KeyPair kp = PublicKeyBox.GenerateKeyPair();
            byte[] SNonce = new byte[24];
            TweetNaCl.TweetNaCl.RandomBytes(SNonce);
            byte[] nonce = Nonce.Generate(kp.PublicKey);
            byte[] SharedKey = TweetNaCl.TweetNaCl.CryptoBoxBeforenm(Keys.ServerKey, kp.PrivateKey);
            if (id == 10101)
            {
                byte[] crypted = TweetNaCl.TweetNaCl.CryptoBoxAfternm(GlobalValues.SessionKeyBytes.Concat(SNonce).ToArray().Concat(data).ToArray(),nonce,SharedKey);
                //byte[] crypted = TweetNaCl.TweetNaCl.CryptoBoxAfternm(data.Concat(SNonce).ToArray().Concat(GlobalValues.SessionKeyBytes).ToArray(),nonce,SharedKey);
                return kp.PublicKey.Concat(crypted).ToArray();
            }
            Utilities.Increment(SNonce);
            return TweetNaCl.TweetNaCl.CryptoBoxAfternm(data, SNonce, SharedKey);
        }
    }
}
