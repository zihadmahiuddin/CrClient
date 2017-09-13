using Sodium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrClient
{
    public class EnDecrypt
    {
        public void Encrypt(int id, byte[] plainText)
        {
            //byte[] encrypted;
            //byte[] clientRNonce = Utils.GenerateRandomBytes(24);
            //byte[] clientPublicKey = plainText.Take(32).ToArray();
            //byte[] clientSNonce = plainText.Skip(32).Take(24).ToArray();
            //byte[] clientSharedKey = plainText.Skip(32).Skip(24).Take(24).ToArray();
            //switch (id)
            //{
            //    case 20103:
            //        byte[] nonce = GenericHash.Hash(clientSNonce.Concat(clientPublicKey).Concat(Keys.ServerKey).ToArray(), null, 24);
            //        plainText = clientRNonce.Concat(clientSharedKey).Concat(plainText).ToArray();
            //        encrypted = PublicKeyBox.Create(plainText, nonce, Utils.GenerateRandomBytes(32), clientPublicKey);
            //        break;
            //    case 20104:
            //        nonce = GenericHash.Hash(clientSNonce.Concat(clientPublicKey).Concat(Keys.ServerKey).ToArray(), null, 24);
            //        plainText = clientRNonce.Concat(clientSharedKey).Concat(plainText).ToArray();
            //        encrypted = PublicKeyBox.Create(plainText, nonce, Utils.GenerateRandomBytes(32), clientPublicKey);
            //        break;
            //    default:
            //        clientRNonce = Utilities.Increment(Utilities.Increment(clientRNonce));
            //        encrypted = SecretBox.Create(plainText, clientRNonce, clientSharedKey).Skip(16).ToArray();
            //        break;
            //}
        }

        public static byte[] Decrypt(byte[] encrypted)
        {
            byte[] decrypted;
            ushort id;
            int length;
            ushort version;
            using(var reader = new Reader(encrypted))
            {
                id = reader.ReadUInt16();
                length = reader.ReadInt24();
                version = reader.ReadUInt16();
            }
            encrypted = encrypted.Skip(2).Skip(3).Skip(2).ToArray();
            switch (id)
            {
                case 10100:
                    decrypted = encrypted;
                    break;
                case 10101:
                    ServerConfig.clientPublicKey = encrypted.Take(32).ToArray();
                    encrypted = encrypted.Skip(32).ToArray();
                    ServerConfig.clientSharedKey = ServerConfig.clientPublicKey;
                    ServerConfig.clientRNonce = Utils.GenerateRandomBytes(24);
                    byte[] nonce = GenericHash.Hash(ServerConfig.clientPublicKey.Concat(Keys.ServerKey).ToArray(), null, 24);
                    decrypted = PublicKeyBox.Open(encrypted, nonce, ServerConfig.privateKey, ServerConfig.clientPublicKey);
                    ServerConfig.clientSessionKey = decrypted.Take(24).ToArray();
                    ServerConfig.clientSNonce = decrypted.Skip(24).Take(24).ToArray();
                    ServerConfig.clientSNonce = ServerConfig.clientSNonce;
                    decrypted = decrypted.Skip(24).Skip(24).ToArray();
                    Console.WriteLine(BitConverter.ToString(ServerConfig.clientSNonce).Replace("-", ""));
                    Console.WriteLine(BitConverter.ToString(ServerConfig.clientSessionKey).Replace("-", ""));
                    Console.WriteLine(BitConverter.ToString(nonce).Replace("-", ""));
                    Console.WriteLine(BitConverter.ToString(ServerConfig.clientPublicKey).Replace("-", ""));
                    break;
                default:
                    ServerConfig.clientSNonce = Utilities.Increment(Utilities.Increment(ServerConfig.clientSNonce));
                    decrypted = SecretBox.Open(new byte[16].Concat(encrypted).ToArray(), ServerConfig.clientSNonce, ServerConfig.clientSharedKey);
                    break;
            }
            return decrypted;
        }
    }

    public class ServerConfig
    {
        public static byte[] clientSNonce { get; set; }
        public static byte[] clientSharedKey { get; set; }
        public static byte[] clientSessionKey { get; set; }
        public static byte[] clientPublicKey { get; set; }
        public static byte[] clientRNonce { get; set; }

        public static byte[] ServerKey = Utils.HexToByteArray("72f1a4a4c48e44da0c42310f800e96624e6dc6a641a9d41c3b5039d8dfadc27e");
        public static byte[] privateKey = Utils.HexToByteArray("1891D401FADB51D25D3A9174D472A9F691A45B974285D47729C45C6538070D85");
    }
}
