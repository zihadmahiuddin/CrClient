using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CrClient
{
    internal class PacketReceiver
    {
        public static void receive(int bytesReceived, Socket socket, State state)
        {
            var bytesRead = 0;
            int payloadLength, bytesAvailable, bytesNeeded;
            while (bytesRead < bytesReceived)
            {
                bytesAvailable = bytesReceived - bytesRead;
                if (bytesReceived > 0)
                {
                    if (state.Packet.Length >= 7)
                    {
                        payloadLength =
                            BitConverter.ToInt32(new byte[1].Concat(state.Packet.Skip(2).Take(3)).Reverse().ToArray(), 0);
                        bytesNeeded = payloadLength - (state.Packet.Length - 7);
                        if (bytesAvailable >= bytesNeeded)
                        {
                            state.Packet = state.Packet.Concat(state.Buffer.Skip(bytesRead).Take(bytesNeeded)).ToArray();
                            bytesRead += bytesNeeded;
                            bytesAvailable -= bytesNeeded;
                            if (state.GetType() == typeof(ClientState))
                            {
                                ClientCrypto.DecryptPacket(socket, (ClientState)state, state.Packet);
                            }
                            else if (state.GetType() == typeof(ServerState))
                            {
                                ServerCrypto.DecryptPacket(socket, (ServerState)state, state.Packet);
                            }
                            state.Packet = new byte[0];
                        }
                        else
                        {
                            state.Packet =
                                state.Packet.Concat(state.Buffer.Skip(bytesRead).Take(bytesAvailable)).ToArray();
                            bytesRead = bytesReceived;
                            bytesAvailable = 0;
                        }
                    }
                    else if (bytesAvailable >= 7)
                    {
                        state.Packet = state.Packet.Concat(state.Buffer.Skip(bytesRead).Take(7)).ToArray();
                        bytesRead += 7;
                        bytesAvailable -= 7;
                    }
                    else
                    {
                        state.Packet = state.Packet.Concat(state.Buffer.Skip(bytesRead).Take(bytesAvailable)).ToArray();
                        bytesRead = bytesReceived;
                        bytesAvailable = 0;
                    }
                }
            }
        }
    }
}
