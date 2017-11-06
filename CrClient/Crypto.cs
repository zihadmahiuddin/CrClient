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
        public static byte[] encryptedPayload;
        public static byte[] decryptedPayload;
        public static byte[] Encrypt(byte[] Payload, int Id, int Version, KeyPair keyPair)
        {
            if (!Form1.Config.UseRC4)
            {
                switch (Id)
                {
                    case 10100:
                        encryptedPayload = Payload;
                        break;
                    case 10101:
                        ClientConfig.Nonce = GenericHash.Hash(keyPair.PublicKey.Concat(Keys.ServerKey).ToArray(), null, 24);
                        ClientConfig.SNonce = Utils.GenerateRandomBytes(24);
                        Payload = ClientConfig.SessionKey.Concat(ClientConfig.SNonce).Concat(Payload).ToArray();
                        encryptedPayload = PublicKeyBox.Create(Payload, ClientConfig.Nonce, keyPair.PrivateKey, Keys.ServerKey);
                        encryptedPayload = keyPair.PublicKey.Concat(encryptedPayload).ToArray();
                        Console.WriteLine(BitConverter.ToString(ClientConfig.Nonce).Replace("-", ""));
                        Console.WriteLine(BitConverter.ToString(ClientConfig.SNonce).Replace("-", ""));
                        Console.WriteLine(BitConverter.ToString(ClientConfig.SessionKey).Replace("-", ""));
                        break;
                    default:
                        ClientConfig.SNonce = Utilities.Increment(Utilities.Increment(ClientConfig.SNonce));
                        encryptedPayload = SecretBox.Create(Payload, ClientConfig.SNonce, ClientConfig.SharedKey);
                        Console.WriteLine($"Encrypted ID: {Id} with Nonce: {BitConverter.ToString(ClientConfig.SNonce).Replace("-", "")}");
                        break;
                }
            }
            else if(Form1.Config.UseRC4)
            {
                encryptedPayload = RC4.Encrypt(Payload);
            }
            else
            {

            }
            byte[] packet = BitConverter.GetBytes(Id).Reverse().Skip(2).Concat(BitConverter.GetBytes(encryptedPayload.Length).Reverse().Skip(1)).Concat(BitConverter.GetBytes(Version).Reverse().Skip(2)).Concat(encryptedPayload).ToArray();
            return packet;
        }
        public static byte[] Decrypt(byte[] Payload,KeyPair keyPair)
        {
            ushort Id;
            int Length;
            ushort Version;
            using(Reader reader = new Reader(Payload))
            {
                Id = reader.ReadUInt16();
                Length = reader.ReadInt24();
                Version = reader.ReadUInt16();
                Payload = Payload.Skip(2).Skip(3).Skip(2).ToArray();
            }
            if(!Form1.Config.UseRC4)
            {
                switch (Id)
                {
                    case 20100:
                        decryptedPayload = Payload;
                        break;
                    case 20103:
                        decryptedPayload = Payload;
                        break;
                    case 20104:
                        ClientConfig.ServerNonce = GenericHash.Hash(ClientConfig.SNonce.Concat(keyPair.PublicKey).Concat(Keys.ServerKey).ToArray(), null, 24);
                        decryptedPayload = PublicKeyBox.Open(Payload, ClientConfig.ServerNonce, keyPair.PrivateKey, Keys.ServerKey);
                        ClientConfig.RNonce = decryptedPayload.Take(24).ToArray();
                        ClientConfig.SharedKey = decryptedPayload.Skip(24).Take(32).ToArray();
                        decryptedPayload = decryptedPayload.Skip(24).Skip(32).ToArray();
                        break;
                    default:
                        ClientConfig.RNonce = Utilities.Increment(Utilities.Increment(ClientConfig.RNonce));
                        byte[] toDecrypt = new byte[16].Concat(Payload).ToArray();
                        decryptedPayload = SecretBox.Open(toDecrypt, ClientConfig.RNonce, ClientConfig.SharedKey);
                        Logger.Write(Encoding.UTF8.GetString(decryptedPayload), "Decrypted OHD");
                        break;
                }
            }
            else if(Form1.Config.UseRC4)
            {
                decryptedPayload = RC4.Decrypt(Payload);
            }
            else
            {

            }
            return decryptedPayload;
        }
    }
}