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
        public static void AddVInt(this List<byte> Packet,int v2)
        {

            if (v2 <= -1)
            {
                if (!((v2 + 63 < 0) ^ true))
                {
                    Packet.Add((byte)(v2 & 0x3F | 0x40));
                    Packet.AddRange(Packet.ToArray());
                }
                if (v2 >= -8191)
                {
                    Packet.Add((byte)(v2 | 0xC0));
                    v2 >>= 6 & 0x7F;
                    Packet.Add((byte)v2);
                    Packet.AddRange(Packet.ToArray());
                }
                if (v2 >= -1048575)
                {
                    Packet.Add((byte)(v2 | 0xC0));
                    Packet.Add((byte)(v2 >> 6 | 0x80));
                    v2 >>= 13 & 0x7F;
                    Packet.Add((byte)v2);
                    Packet.AddRange(Packet.ToArray());
                }
                Packet.Add((byte)(v2 | 0xC0));
                Packet.Add((byte)(v2 >> 6 | 0x80));
                Packet.Add((byte)(v2 >> 13 | 0x80));
                v2 >>= 20;
                if (v2 <= -134217728)
                {
                    Packet.Add((byte)(v2 | 0x80));
                    v2 >>= 27 & 0xF;
                    Packet.Add((byte)v2);
                    Packet.AddRange(Packet.ToArray());
                }
                Packet.Add((byte)(v2 & 0x7F));
                Packet.AddRange(Packet.ToArray());
            }
            if (v2 > 63)
            {
                if (v2 < 0x2000)
                {
                    Packet.Add((byte)(v2 & 0x3F | 0x80));
                    v2 >>= 6 & 0x7F;
                    Packet.Add((byte)v2);
                    Packet.AddRange(Packet.ToArray());
                }
                if (v2 < 0x100000)
                {
                    Packet.Add((byte)(v2 & 0x3F | 0x80));
                    Packet.Add((byte)(v2 >> 6 | 0x80));
                    v2 >>= 13 & 0x7F;
                    Packet.Add((byte)v2);
                    Packet.AddRange(Packet.ToArray());
                }
                Packet.Add((byte)(v2 & 0x3F | 0x80));
                Packet.Add((byte)(v2 >> 6 | 0x80));
                Packet.Add((byte)(v2 >> 13 | 0x80));
                v2 >>= 20;

                if (v2 >= 0x8000000)
                {
                    Packet.Add((byte)(v2 | 0x80));
                    v2 >>= 27 & 0xF;
                    Packet.Add((byte)v2);
                    Packet.AddRange(Packet.ToArray());
                }

                Packet.Add((byte)(v2 & 0x7F));
                Packet.AddRange(Packet.ToArray());
            }

            v2 = v2 & 0x3F;
            Packet.Add((byte)v2);
            Packet.AddRange(Packet.ToArray());
        }
    }
}