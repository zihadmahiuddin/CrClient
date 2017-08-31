using Sodium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace CrClient
{
    public partial class Form1 : Form
    {
        KeyPair kp = PublicKeyBox.GenerateKeyPair();
        byte[] PrivateKey;
        byte[] PublicKey;
        Socket sck = GlobalValues.sck;
        public Form1()
        {
            InitializeComponent();
            PrivateKey = kp.PrivateKey;
            PublicKey = kp.PublicKey;
            GlobalValues.ClientNonce = Nonce.Generate(PublicKey);
        }

        public void btnConnect_Click(object sender, EventArgs e)
        {
            sck.Connect("192.168.0.101", 9339);
        }

        public void btnDisconnect_Click(object sender, EventArgs e)
        {
            sck.Disconnect(true);
        }

        public void btnHello_Click(object sender, EventArgs e)
        {
            rtbPacket.Clear();
            rtbEncrypted.Clear();
            rtbDecrypted.Clear();
            List<byte> Packet = new List<byte>();
            Packet.AddInt(1);
            Packet.AddInt(11);
            Packet.AddInt(3);
            Packet.AddInt(1);
            Packet.AddInt(377);
            Packet.AddString("54955624828a47165ddf06c73ba01d72a2542ce7");
            Packet.AddInt(2);
            Packet.AddInt(2);
            byte[] data = Packet.ToArray();
            byte[] encrypted = Crypto.Encrypt(10100, 1, data,PrivateKey,PublicKey);
            Console.WriteLine(ByteArrayToHexString(encrypted));
            sck.Send(encrypted);
            Console.WriteLine(ByteArrayToHexString(data));
            Logger.Write(ByteArrayToHexString(encrypted), PacketInfos.GetPacketName(10100));
            byte[] serverHello = new byte[2048];
            sck.Receive(serverHello);
            GlobalValues.SessionKey = Encoding.UTF8.GetString(serverHello.Skip(11).ToArray());
            Console.WriteLine(GlobalValues.SessionKey);
            tbSessionKey.Text = GlobalValues.SessionKey;
            rtbPacket.Text = Encoding.UTF8.GetString(Packet.ToArray());
        }

        public void btnLogin_Click(object sender, EventArgs e)
        {
            rtbPacket.Clear();
            rtbEncrypted.Clear();
            rtbDecrypted.Clear();
            List<byte> Packet = new List<byte>();
            Packet.AddInt(39);
            Packet.AddInt(17680028);
            Packet.AddString("dwnegp67n9f7dfhhaes6bs2pfftyrs4anxjjmjg4");
            Packet.AddVInt(3);
            Packet.AddVInt(0);
            Packet.AddVInt(377);
            Packet.AddString("54955624828a47165ddf06c73ba01d72a2542ce7");
            Packet.AddString("");
            Packet.AddString("c0389670ea3b1978");
            Packet.AddString("");
            Packet.AddString("C8817D");
            Packet.AddString("aa3e6cf0-0162-43d3-8719-f3d3b00356b7");
            Packet.AddString("4.4.2");
            Packet.AddInt(1);
            Packet.AddString("");
            Packet.AddString("c0389670ea3b1978");
            Packet.AddString("en-US");
            Packet.Add(new byte());
            Packet.Add(new byte());
            Packet.AddString("");
            Packet.Add(new byte());
            Packet.AddString("");
            Packet.Add(2);
            Packet.AddString("");
            Packet.AddString("");
            Packet.AddString("");
            Packet.AddString("");
            Packet.Add(new byte());
            byte[] data = Packet.ToArray();
            Console.WriteLine(ByteArrayToHexString(Encoding.UTF8.GetBytes("bb860300ff0103098ba19417a00800040005027f030002001f08bcf2d617a4138301060092010495cfcd17190001002b00a9a3df1701001f00910109bdddc117b6010c15002100bba6e21701001f0032088ae2ea17b10c00050000000003001013400000010380cd80010380cd80010380cd8001000000055a69686164000885318b0282280000000000002200000000000810050183c4010502b809050304050400050583c401050cbf0e050d00050e00050fab0c05108f040511a10405129004051391040516a613051c00051d8688d544001e3c008d013c01b9a4023c02b9a4023c03b9a4023c040a3c050a3c060a3c0786013c0886013c0986013c0a013c0b3d3c0c3d3c0d3d3c0e013c0f013c10013c11203c12093c13093c14093c15bd053c16bd053c17bd053c181b3c191b3c1a1b3c1bb9a4023c1cb9a4023c1db9a402173c00013c01013c02013c03013c04013c05013c06013c07013c08013c09013c0a013c0e013c11013c12013c13013c15013c16013c17013c18013c19013c1a013c1b013c1c01090506bb340507ae0f050886010509aaeae518050ab9a402050b2205140905159510051b0a8c011a00001a01001a02001a03001a04001a05001a06001a07001a08001a09001a0a001a0b001a0c001a0d001a0e001a0f001a10001a11001a12001a13001a14001a15001a16001a17001a18001a19001a1aa9031a1b001a1c001a1d8b011a1e001a1f001a20001a21001a22001a23a7011a24001a2598011a26001a27001a28001a29001a2a001a2b001a2d001a2e001a30001a31001a36001a370c1b00001b01001b02001b03001b04001b05001b06001b07001b08001b09001b0a001c00001c01001c02001c03001c04001c05001c06001c07001c08001c09001c0a001c0bb5031c0c001c0d001c10000000bb07bb07b5cc020a0009218b8c5d0000000c424420454d504952452056329601049d489b0100b41daa1f7fb1021c00000002279c9aee10279c9aee10279c9aee10000000007f0100000000000000000022000000000008080501a4020502030503000505a402050d00050e00051005051d9588d54400033c07083c08083c09080002050808050b22061a00041a01031a03031a0d031c00021c01010000a401a40104010000000000000000030000000100")));
            Logger.Write(ByteArrayToHexString(data), PacketInfos.GetPacketName(10101));
            byte[] encrypted = Crypto.Encrypt(10101,1,data,PrivateKey,PublicKey);
            sck.Send(encrypted);
            //byte[] encrypted = EnDecrypt.Encrypt(10101, data);
            //List<byte> Header = new List<byte>();
            //Header.AddUShort(10101);
            //Header.Add(0);
            //Header.AddUShort((ushort)encrypted.Length);
            //Header.AddUShort(1);
            //byte[] header = Header.ToArray();
            //byte[] final = header.Concat(encrypted).ToArray();
            //sck.Send(final);
            rtbPacket.Text = ByteArrayToHexString(Packet.ToArray());
            rtbEncrypted.Text = ByteArrayToHexString(encrypted);
        }
        public static string ByteArrayToHexString(byte[] byteArray)
        {
            string hex = BitConverter.ToString(byteArray);
            return hex.Replace("-", "");
        }
    }
}
