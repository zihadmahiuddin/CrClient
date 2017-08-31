// *******************************************************
// Created at 27/08/2017 - Last Edit at 27/08/2017
// *******************************************************

namespace CrClient.Packets.Definitions.Types.Client
{
    class Keep_Alive : Type
    {
        public Keep_Alive(Reader reader, ushort id)
            : base(reader, id)
        {
        }

        public override void Decode()
        {
        }
    }
}