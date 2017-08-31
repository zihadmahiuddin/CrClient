using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CrClient
{
    public static class Writer
    {
        public static void AddInt(this List<byte> _Packet, int _Value)
        {
            _Packet.AddRange(BitConverter.GetBytes(_Value).Reverse());
        }
        public static void AddLong(this List<byte> _Packet, long _Value)
        {
            _Packet.AddRange(BitConverter.GetBytes(_Value).Reverse());
        }

        public static void AddString(this List<byte> _Packet, string _Value)
        {
            if (_Value == null)
            {
                _Packet.AddRange(BitConverter.GetBytes(-1).Reverse());
            }
            else
            {
                byte[] _Buffer = Encoding.UTF8.GetBytes(_Value);

                _Packet.AddInt(_Buffer.Length);
                _Packet.AddRange(_Buffer);
            }
        }

        public static void AddUShort(this List<byte> _Packet, ushort _Value)
        {
            _Packet.AddRange(BitConverter.GetBytes(_Value).Reverse());
        }

        public static void AddULong(this List<byte> _Packet, ulong _Value)
        {
            _Packet.AddRange(BitConverter.GetBytes(_Value).Reverse());
        }
        public static void AddUInt(this List<byte> _Packet, uint _Value)
        {
            _Packet.AddRange(BitConverter.GetBytes(_Value).Reverse());
        }

        public static byte[] HexaToBytes(this string _Value)
        {
            string _Tmp = _Value.Replace("-", string.Empty);
            return Enumerable.Range(0, _Tmp.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(_Tmp.Substring(x, 2), 16)).ToArray();
        }

        public static void AddHex(this List<byte> _Packet, string _Value)
        {
            string _Tmp = _Value.Replace("-", string.Empty);
            _Packet.AddRange(Enumerable.Range(0, _Tmp.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(_Tmp.Substring(x, 2), 16)).ToArray());
        }
    }
}