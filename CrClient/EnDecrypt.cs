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
        internal static byte[] Encrypt(int id, byte[] data)
        {
            KeyPair kp = PublicKeyBox.GenerateKeyPair();
            byte[] SNonce = new byte[24];
            TweetNaCl.TweetNaCl.RandomBytes(SNonce);
            //byte[] nonce = Nonce.Generate(kp.PublicKey);
            byte[] nonce = GenericHash.Hash(kp.PublicKey.Concat(Keys.ServerKey).ToArray(), null, 24);
            byte[] SharedKey = TweetNaCl.TweetNaCl.CryptoBoxBeforenm(Keys.ServerKey, kp.PrivateKey);
            if(id == 10100)
            {
                return data;
            }
            if (id == 10101)
            {
                byte[] crypted = TweetNaCl.TweetNaCl.CryptoBoxAfternm(Encoding.UTF8.GetBytes(GlobalValues.SessionKey).Concat(SNonce).ToArray().Concat(data).ToArray(), nonce, SharedKey);
                crypted =  kp.PublicKey.Concat(crypted).ToArray();
                return crypted;
            }
            else
            {
                Utilities.Increment(SNonce);
                return TweetNaCl.TweetNaCl.CryptoBoxAfternm(data, SNonce, SharedKey);
            }
        }
    }
}
