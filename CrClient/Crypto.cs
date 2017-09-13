using System;
using System.IO;
using System.Linq;
using Sodium;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

namespace CrClient
{
    public class Crypto
    {
        public static byte[] Encrypt(byte[] Payload, int Id, int Version, KeyPair keyPair)
        {
            byte[] encryptedPayload;
            switch (Id)
            {
                case 10100:
                    encryptedPayload = Payload;
                    break;
                case 10101:
                    Config.Nonce = GenericHash.Hash(keyPair.PublicKey.Concat(Keys.ServerKey).ToArray(), null, 24);
                    Config.SNonce = Utils.GenerateRandomBytes(24);
                    Payload = Config.SessionKey.Concat(Config.SNonce).Concat(Payload).ToArray();
                    encryptedPayload = PublicKeyBox.Create(Payload, Config.Nonce, keyPair.PrivateKey, Keys.ServerKey);
                    encryptedPayload = keyPair.PublicKey.Concat(encryptedPayload).ToArray();
                    Console.WriteLine(BitConverter.ToString(Config.Nonce).Replace("-", ""));
                    Console.WriteLine(BitConverter.ToString(Config.SNonce).Replace("-",""));
                    Console.WriteLine(BitConverter.ToString(Config.SessionKey).Replace("-", ""));
                    break;
                default:
                    Config.SNonce = Utilities.Increment(Utilities.Increment(Config.SNonce));
                    encryptedPayload = SecretBox.Create(Payload, Config.SNonce, Config.SharedKey);
                    break;
            }
            byte[] packet = BitConverter.GetBytes(Id).Reverse().Skip(2).Concat(BitConverter.GetBytes(encryptedPayload.Length).Reverse().Skip(1)).Concat(BitConverter.GetBytes(Version).Reverse().Skip(2)).Concat(encryptedPayload).ToArray();
            return packet;
        }
        public static byte[] Decrypt(byte[] Payload,KeyPair keyPair)
        {
            ushort Id;
            int Length;
            ushort Version;
            byte[] decryptedPayload;
            using(Reader reader = new Reader(Payload))
            {
                Id = reader.ReadUInt16();
                Length = reader.ReadInt32();
                Version = reader.ReadUInt16();
                Payload = Payload.Skip(2).Skip(3).Skip(2).ToArray();
            }
            switch (Id)
            {
                case 20100:
                    decryptedPayload = Payload;
                    break;
                case 20103:
                    Config.ServerNonce = GenericHash.Hash(Config.SNonce.Concat(keyPair.PublicKey).Concat(Keys.ServerKey).ToArray(), null, 24);
                    decryptedPayload = PublicKeyBox.Open(Payload, Config.ServerNonce, keyPair.PrivateKey, Keys.ServerKey);
                    Config.RNonce = decryptedPayload.Take(24).ToArray();
                    Config.SharedKey = decryptedPayload.Skip(24).Take(32).ToArray();
                    break;
                case 20104:
                    Config.ServerNonce = GenericHash.Hash(Config.SNonce.Concat(keyPair.PublicKey).Concat(Keys.ServerKey).ToArray(), null, 24);
                    decryptedPayload = PublicKeyBox.Open(Payload, Config.ServerNonce, keyPair.PrivateKey, Keys.ServerKey);
                    Config.RNonce = decryptedPayload.Take(24).ToArray();
                    Config.SharedKey = decryptedPayload.Skip(24).Take(32).ToArray();
                    decryptedPayload = decryptedPayload.Skip(24).Skip(32).ToArray();
                    break;
                default:
                    Config.RNonce = Utilities.Increment(Utilities.Increment(Config.RNonce));
                    decryptedPayload = SecretBox.Open(Payload, Config.RNonce, Config.SharedKey);
                    break;
            }
            return decryptedPayload;
        }
    }
}