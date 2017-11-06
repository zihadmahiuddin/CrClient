// *******************************************************
// Created at 22/08/2017
// *******************************************************

namespace CrClient
{
    using System.Collections.Generic;
    using System.IO;

    static class VInt
    {
        public static int ReadVInt(this BinaryReader br)
        {
            int v5;
            byte b = br.ReadByte();
            v5 = b & 0x80;
            int _LR = b & 0x3F;

            if ((b & 0x40) != 0)
            {
                if (v5 != 0)
                {
                    b = br.ReadByte();
                    v5 = ((b << 6) & 0x1FC0) | _LR;
                    if ((b & 0x80) != 0)
                    {
                        b = br.ReadByte();
                        v5 = v5 | ((b << 13) & 0xFE000);
                        if ((b & 0x80) != 0)
                        {
                            b = br.ReadByte();
                            v5 = v5 | ((b << 20) & 0x7F00000);
                            if ((b & 0x80) != 0)
                            {
                                b = br.ReadByte();
                                _LR = (int)(v5 | (b << 27) | 0x80000000);
                            }
                            else
                            {
                                _LR = (int)(v5 | 0xF8000000);
                            }
                        }
                        else
                        {
                            _LR = (int)(v5 | 0xFFF00000);
                        }
                    }
                    else
                    {
                        _LR = (int)(v5 | 0xFFFFE000);
                    }
                }
            }
            else
            {
                if (v5 != 0)
                {
                    b = br.ReadByte();
                    _LR |= (b << 6) & 0x1FC0;
                    if ((b & 0x80) != 0)
                    {
                        b = br.ReadByte();
                        _LR |= (b << 13) & 0xFE000;
                        if ((b & 0x80) != 0)
                        {
                            b = br.ReadByte();
                            _LR |= (b << 20) & 0x7F00000;
                            if ((b & 0x80) != 0)
                            {
                                b = br.ReadByte();
                                _LR |= b << 27;
                            }
                        }
                    }
                }
            }

            return _LR;
        }
        public static long ReadVInt64(this BinaryReader br)
        {
            byte temp = br.ReadByte();
            long i = 0;
            int Sign = (temp >> 6) & 1;
            i = temp & 0x3FL;

            while (true)
            {
                if ((temp & 0x80) == 0)
                {
                    break;
                }

                temp = br.ReadByte();
                i |= (temp & 0x7FL) << 6;

                if ((temp & 0x80) == 0)
                {
                    break;
                }

                temp = br.ReadByte();
                i |= (temp & 0x7FL) << (6 + 7);

                if ((temp & 0x80) == 0)
                {
                    break;
                }

                temp = br.ReadByte();
                i |= (temp & 0x7FL) << (6 + 7 + 7);

                if ((temp & 0x80) == 0)
                {
                    break;
                }

                temp = br.ReadByte();
                i |= (temp & 0x7FL) << (6 + 7 + 7 + 7);

                if ((temp & 0x80) == 0)
                {
                    break;
                }

                temp = br.ReadByte();
                i |= (temp & 0x7FL) << (6 + 7 + 7 + 7 + 7);

                if ((temp & 0x80) == 0)
                {
                    break;
                }

                temp = br.ReadByte();
                i |= (temp & 0x7FL) << (6 + 7 + 7 + 7 + 7 + 7);

                if ((temp & 0x80) == 0)
                {
                    break;
                }

                temp = br.ReadByte();
                i |= (temp & 0x7FL) << (6 + 7 + 7 + 7 + 7 + 7 + 7);

                if ((temp & 0x80) == 0)
                {
                    break;
                }

                temp = br.ReadByte();
                i |= (temp & 0x7FL) << (6 + 7 + 7 + 7 + 7 + 7 + 7 + 7);
            }
            i ^= -Sign;
            return i;
        }

        public static byte[] WritevInt(int v2)
        {
            var stream = new MemoryStream(5);

            if (v2 <= -1)
            {
                if (!((v2 + 63 < 0) ^ true))
                {
                    stream.WriteByte((byte)(v2 & 0x3F | 0x40));
                    return stream.ToArray();
                }
                if (v2 >= -8191)
                {
                    stream.WriteByte((byte)(v2 | 0xC0));
                    v2 >>= 6 & 0x7F;
                    stream.WriteByte((byte)v2);
                    return stream.ToArray();
                }
                if (v2 >= -1048575)
                {
                    stream.WriteByte((byte)(v2 | 0xC0));
                    stream.WriteByte((byte)(v2 >> 6 | 0x80));
                    v2 >>= 13 & 0x7F;
                    stream.WriteByte((byte)v2);
                    return stream.ToArray();
                }
                stream.WriteByte((byte)(v2 | 0xC0));
                stream.WriteByte((byte)(v2 >> 6 | 0x80));
                stream.WriteByte((byte)(v2 >> 13 | 0x80));
                v2 >>= 20;
                if (v2 <= -134217728)
                {
                    stream.WriteByte((byte)(v2 | 0x80));
                    v2 >>= 27 & 0xF;
                    stream.WriteByte((byte)v2);
                    return stream.ToArray();
                }
                stream.WriteByte((byte)(v2 & 0x7F));
                return stream.ToArray();
            }
            if (v2 > 63)
            {
                if (v2 < 0x2000)
                {
                    stream.WriteByte((byte)(v2 & 0x3F | 0x80));
                    v2 >>= 6 & 0x7F;
                    stream.WriteByte((byte)v2);
                    return stream.ToArray();
                }
                if (v2 < 0x100000)
                {
                    stream.WriteByte((byte)(v2 & 0x3F | 0x80));
                    stream.WriteByte((byte)(v2 >> 6 | 0x80));
                    v2 >>= 13 & 0x7F;
                    stream.WriteByte((byte)v2);
                    return stream.ToArray();
                }
                stream.WriteByte((byte)(v2 & 0x3F | 0x80));
                stream.WriteByte((byte)(v2 >> 6 | 0x80));
                stream.WriteByte((byte)(v2 >> 13 | 0x80));
                v2 >>= 20;

                if (v2 >= 0x8000000)
                {
                    stream.WriteByte((byte)(v2 | 0x80));
                    v2 >>= 27 & 0xF;
                    stream.WriteByte((byte)v2);
                    return stream.ToArray();
                }

                stream.WriteByte((byte)(v2 & 0x7F));
                return stream.ToArray();
            }

            v2 = v2 & 0x3F;
            stream.WriteByte((byte)v2);
            return stream.ToArray();
        }


        //List
        public static void AddVInt(this List<byte> _Packet, int _Value)
        {
            if (_Value > 63)
            {
                _Packet.Add((byte)(_Value & 0x3F | 0x80));

                if (_Value > 8191)
                {
                    _Packet.Add((byte)(_Value >> 6 | 0x80));

                    if (_Value > 1048575)
                    {
                        _Packet.Add((byte)(_Value >> 13 | 0x80));

                        if (_Value > 134217727)
                        {
                            _Packet.Add((byte)(_Value >> 20 | 0x80));
                            _Value >>= 27 & 0x7F;
                        }
                        else
                            _Value >>= 20 & 0x7F;
                    }
                    else
                        _Value >>= 13 & 0x7F;

                }
                else
                    _Value >>= 6 & 0x7F;
            }
            _Packet.Add((byte)_Value);
        }

        public static void AddVInt64(this List<byte> _Packet, long _Value)
        {
            var Stream = new MemoryStream(6);
            byte Temp = 0;

            Temp = (byte)((_Value >> 57) & 0x40L);
            _Value = _Value ^ (_Value >> 63);
            Temp |= (byte)(_Value & 0x3FL);

            _Value >>= 6;

            if (_Value != 0)
            {
                Temp |= 0x80;
                Stream.WriteByte(Temp);

                while (true)
                {
                    Temp = (byte)(_Value & (0x7FL));
                    _Value >>= 7;

                    Temp |= (byte)((_Value != 0 ? 1 : 0) << 7);

                    Stream.WriteByte(Temp);

                    if (_Value == 0)
                        break;
                }
            }
            else
            {
                Stream.WriteByte(Temp);
            }

            _Packet.Add(0);
            _Packet.AddRange(Stream.ToArray());
        }

    }
}