using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using Sodium;
using System.Text;
using System.Windows.Forms;

namespace CrClient
{
    public class Crypto
    {
        public static byte[] Encrypt(int id, int version, byte[] data, byte[] privateKey, byte[] publicKey)
        {
            byte[] encrypted;
            switch (id)
            {
                case 10100:
                    encrypted = data;
                    break;
                case 10101:
                    byte[] nonce = GenericHash.Hash(publicKey.Concat(Keys.ServerKey).ToArray(), null, 24);
                    data = Encoding.UTF8.GetBytes(GlobalValues.SessionKey).Concat(GlobalValues.ClientNonce).Concat(data).ToArray();
                    encrypted = PublicKeyBox.Create(data, nonce, privateKey, Keys.ServerKey);
                    encrypted = publicKey.Concat(encrypted).ToArray();
                    break;
                default:
                    encrypted = data;
                    Console.WriteLine("Couldn't recognize message id.");
                    MessageBox.Show("Couldn't recognize message id.");
                    break;
            }
            byte[] packet = BitConverter.GetBytes(id).Reverse().Skip(2).Concat(BitConverter.GetBytes(encrypted.Length).Reverse().Skip(1)).Concat(BitConverter.GetBytes(version).Reverse().Skip(2)).Concat(encrypted).ToArray();
            return packet;
        }

        public static byte[] Decrypt(byte[] packet, byte[] privateKey, byte[] publicKey)
        {
            byte[] decrypted;
            using (var reader = new Reader(packet))
            {
                ushort id = reader.ReadUInt16();
                reader.Seek(3, SeekOrigin.Current);
                ushort version = reader.ReadUInt16();
                byte[] encrypted = reader.ReadAllBytes;
                switch (id)
                {
                    case 20100:
                        decrypted = encrypted;
                        break;
                    case 20103:
                        decrypted = encrypted;
                        break;
                    case 20104:
                        byte[] nonce = GenericHash.Hash(GlobalValues.ClientNonce.Concat(publicKey).Concat(Keys.ServerKey).ToArray(), null, 24);

                        decrypted = PublicKeyBox.Open(encrypted, nonce, privateKey, Keys.ServerKey);

                        GlobalValues.ServerNonce = decrypted.Take(24).ToArray();
                        GlobalValues.ServerSharedKey = decrypted.Skip(24).Take(32).ToArray();

                        decrypted = decrypted.Skip(24).Skip(32).ToArray();
                        break;
                    default:
                        GlobalValues.ServerNonce = Utilities.Increment(Utilities.Increment(GlobalValues.ServerNonce));

                        decrypted = SecretBox.Open(new byte[16].Concat(encrypted).ToArray(), GlobalValues.ServerNonce, GlobalValues.ServerSharedKey);
                        break;

                }
            }
            return decrypted;
        }
    }
}