using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrClient
{
    public class Protocol
    {
        public static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                var state = (State)ar.AsyncState;
                var socket = state.Socket;
                var bytesReceived = socket.EndReceive(ar);
                PacketReceiver.receive(bytesReceived, socket, state);
                socket.BeginReceive(state.Buffer, 0, State.BufferSize, 0, ReceiveCallback, state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void SendCallback(IAsyncResult ar)
        {
            try
            {
                var state = (State)ar.AsyncState;
                var socket = state.Socket;
                var bytesSent = socket.EndSend(ar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
