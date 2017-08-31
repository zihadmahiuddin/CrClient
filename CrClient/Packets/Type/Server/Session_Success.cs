// *******************************************************
// Created at 22/08/2017
// *******************************************************

namespace CrClient.Packets.Definitions.Types.Server
{
    class Session_Success : Type
    {
        public Session_Success(Reader reader, ushort id)
            : base(reader, id)
        {
        }

        public override void Decode()
        {
            this.Json.Add("sessionKey", this.Reader.ReadString());
        }
    }
}