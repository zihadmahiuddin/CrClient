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
            sck.Connect("127.0.0.1", 9338);
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
            Packet.AddString(null);
            Packet.AddString("c0389670ea3b1978");
            Packet.AddString(null);
            Packet.AddString("C8817D");
            Packet.AddString("aa3e6cf0-0162-43d3-8719-f3d3b00356b7");
            Packet.AddString("4.4.2");
            Packet.AddInt(1);
            Packet.AddString(null);
            Packet.AddString("c0389670ea3b1978");
            Packet.AddString("en-US");
            Packet.Add(new byte());
            Packet.Add(new byte());
            Packet.AddString(null);
            Packet.Add(new byte());
            Packet.AddString(null);
            Packet.Add(2);
            Packet.AddString(null);
            Packet.AddString(null);
            Packet.AddString(null);
            Packet.AddString(null);
            Packet.Add(new byte());
            byte[] data = Packet.ToArray();
            Console.WriteLine($"Payload: \n{ByteArrayToHexString(data)}");
            Logger.Write(ByteArrayToHexString(data), PacketInfos.GetPacketName(10101));
            byte[] encrypted = Crypto.Encrypt(10101,1,data,PrivateKey,PublicKey);
            Console.WriteLine($"ClientNonce: \n{ByteArrayToHexString(GlobalValues.ClientNonce)}");
            Console.WriteLine($"Encrypted Payload: \n{ByteArrayToHexString(encrypted)}");
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
