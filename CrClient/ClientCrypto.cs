using System.Linq;
using CrClient;
using System;
using Sodium;
using System.Net.Sockets;

namespace CrClient
{
    public class ClientCrypto
    {
        public static byte[] ServerKey = Keys.ServerKey;
        public static KeyPair ClientKeys = PublicKeyBox.GenerateKeyPair();

        public static byte[] EncryptPacket(Socket socket, ClientState state, int messageId, int unknown, byte[] plainText)
        {
            byte[] cipherText;
            if (messageId == 10100)
            {
                cipherText = plainText;
                return cipherText;
            }
            else if (messageId == 10101)
            {
                var nonce = GenericHash.Hash(state.ClientKey.PublicKey.Concat(state.ServerKey).ToArray(), null, 24);
                plainText = state.ServerState.SessionKey.Concat(state.Nonce).Concat(plainText).ToArray();
                cipherText = PublicKeyBox.Create(plainText, nonce, state.ClientKey.PrivateKey, state.ServerKey);
                cipherText = state.ClientKey.PublicKey.Concat(cipherText).ToArray();
                return cipherText;
            }
            else
            {
                cipherText = SecretBox.Create(plainText, state.Nonce, state.ServerState.SharedKey).Skip(16).ToArray();
                return cipherText;
            }
            var packet =
                BitConverter.GetBytes(messageId)
                    .Reverse()
                    .Skip(2)
                    .Concat(BitConverter.GetBytes(cipherText.Length).Reverse().Skip(1))
                    .Concat(BitConverter.GetBytes(unknown).Reverse().Skip(2))
                    .Concat(cipherText)
                    .ToArray();
            socket.BeginSend(packet, 0, packet.Length, 0, Protocol.SendCallback, state);
        }

        public static void DecryptPacket(Socket socket, ClientState state, byte[] packet)
        {
            var messageId = BitConverter.ToInt32(new byte[2].Concat(packet.Take(2)).Reverse().ToArray(), 0);
            var payloadLength = BitConverter.ToInt32(new byte[1].Concat(packet.Skip(2).Take(3)).Reverse().ToArray(), 0);
            var unknown = BitConverter.ToInt32(new byte[2].Concat(packet.Skip(2).Skip(3).Take(2)).Reverse().ToArray(), 0);
            var cipherText = packet.Skip(2).Skip(3).Skip(2).ToArray();
            byte[] plainText;

            if (messageId == 20100)
            {
                plainText = cipherText;
            }
            else if (messageId == 20104)
            {
                var nonce =
                    GenericHash.Hash(state.Nonce.Concat(state.ClientKey.PublicKey).Concat(state.ServerKey).ToArray(),
                        null, 24);
                plainText = PublicKeyBox.Open(cipherText, nonce, state.ClientKey.PrivateKey, state.ServerKey);
                state.ServerState.Nonce = plainText.Take(24).ToArray();
                state.ServerState.SharedKey = plainText.Skip(24).Take(32).ToArray();
                plainText = plainText.Skip(24).Skip(32).ToArray();
            }
            else
            {
                state.ServerState.Nonce = Utilities.Increment(Utilities.Increment(state.ServerState.Nonce));
                plainText = SecretBox.Open(new byte[16].Concat(cipherText).ToArray(), state.ServerState.Nonce,
                    state.ServerState.SharedKey);
            }
            Console.WriteLine($"[UCR]    {PacketInfos.GetPacketName(messageId)} " + Environment.NewLine + "{Utilities.BinaryToHex(packet.Take(7).ToArray()) + Utilities.BinaryToHex(plainText)}");
            ServerCrypto.EncryptPacket(state.ServerState.Socket, state.ServerState, messageId, unknown, plainText);
        }

        internal static byte[] HexToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
