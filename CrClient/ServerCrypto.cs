using Sodium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CrClient
{
    public class ServerCrypto
    {
        public static void DecryptPacket(Socket socket, ServerState state, byte[] packet)
        {
            var messageId = BitConverter.ToInt32(new byte[2].Concat(packet.Take(2)).Reverse().ToArray(), 0);
            var payloadLength = BitConverter.ToInt32(new byte[1].Concat(packet.Skip(2).Take(3)).Reverse().ToArray(), 0);
            var unknown = BitConverter.ToInt32(new byte[2].Concat(packet.Skip(2).Skip(3).Take(2)).Reverse().ToArray(), 0);
            var cipherText = packet.Skip(2).Skip(3).Skip(2).ToArray();
            byte[] plainText;

            if (messageId == 10100)
            {
                plainText = cipherText;
            }
            else if (messageId == 10101)
            {
                state.ClientKey = cipherText.Take(32).ToArray();
                var nonce = GenericHash.Hash(state.ClientKey.Concat(state.ServerKeys.PublicKey).ToArray(), null, 24);
                cipherText = cipherText.Skip(32).ToArray();
                plainText = PublicKeyBox.Open(cipherText, nonce, state.ServerKeys.SecretKey, state.ClientKey);
                state.SessionKey = plainText.Take(24).ToArray();
                state.ClientState.Nonce = plainText.Skip(24).Take(24).ToArray();
                plainText = plainText.Skip(24).Skip(24).ToArray();
            }
            else
            {
                state.ClientState.Nonce = Utilities.Increment(Utilities.Increment(state.ClientState.Nonce));
                plainText = SecretBox.Open(new byte[16].Concat(cipherText).ToArray(), state.ClientState.Nonce,
                    state.SharedKey);
            }
            Console.WriteLine("[UCR]    {0}" + Environment.NewLine + "{1}", PacketInfos.GetPacketName(messageId),
                Utilities.BinaryToHex(packet.Take(7).ToArray()) + Utilities.BinaryToHex(plainText));
            ClientCrypto.EncryptPacket(state.ClientState.Socket, state.ClientState, messageId, unknown, plainText);
        }

        public static void EncryptPacket(Socket socket, ServerState state, int messageId, int unknown, byte[] plainText)
        {
            byte[] cipherText;
            if (messageId == 20100)
            {
                cipherText = plainText;
            }
            else if (messageId == 20104)
            {
                var nonce =
                    GenericHash.Hash(
                        state.ClientState.Nonce.Concat(state.ClientKey).Concat(state.ServerKeys.PublicKey).ToArray(),
                        null, 24);
                plainText = state.Nonce.Concat(state.SharedKey).Concat(plainText).ToArray();
                cipherText = PublicKeyBox.Create(plainText, nonce, state.ServerKeys.SecretKey, state.ClientKey);
            }
            else
            {
                cipherText = SecretBox.Create(plainText, state.Nonce, state.SharedKey).Skip(16).ToArray();
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
    }
}
