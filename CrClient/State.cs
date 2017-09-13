using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CrClient
{
    public class State
    {
        public const int BufferLength = 2048;
        public byte[] Buffer = new byte[BufferLength];
        public byte[] Packet = new byte[0];
        public Socket Socket;
    }
}
