using NaCl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrClient
{
    public class ServerState : State
    {
        public byte[] ClientKey, Nonce, SessionKey, SharedKey;
        public ClientState ClientState;

        public KeyPair ServerKeys;
    }
}
