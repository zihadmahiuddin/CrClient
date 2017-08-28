using Sodium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrClient
{
    public class ClientState : State
    {
        public KeyPair ClientKey;
        public byte[] ServerKey, Nonce;
        public ServerState ServerState;
    }
}
