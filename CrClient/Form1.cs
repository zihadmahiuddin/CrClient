using Sodium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace CrClient
{
    public partial class Form1 : Form
    {
        Socket sck = GlobalValues.sck;
        public Form1()
        {
            InitializeComponent();
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
            Packet.AddUShort(10100);
            Packet.Add(0);
            Packet.AddUShort(72);
            Packet.AddUShort(1);
            Packet.AddInt(1);
            Packet.AddInt(11);
            Packet.AddInt(3);
            Packet.AddInt(1);
            Packet.AddInt(377);
            Packet.AddString("a4e1c4d3eb2e38c13b7cff4962938abfc4b65a0c");
            Packet.AddInt(2);
            Packet.AddInt(2);
            byte[] data = Packet.ToArray();
            sck.Send(data);
            byte[] serverHello = new byte[2048];
            GlobalValues.SessionKeyBytes = serverHello;
            tbSessionKey.Text = Encoding.ASCII.GetString(serverHello);
            rtbPacket.Text = Encoding.ASCII.GetString(Packet.ToArray());
        }

        public void btnLogin_Click(object sender, EventArgs e)
        {
            rtbPacket.Clear();
            rtbEncrypted.Clear();
            rtbDecrypted.Clear();
            List<byte> Packet = new List<byte>();
            List<byte> EncryptedPacket = new List<byte>();
            Packet.AddInt(0);
            Packet.AddInt(0);
            Packet.AddString("");
            Packet.AddInt(3);
            Packet.AddInt(0);
            Packet.AddInt(377);
            Packet.AddString("622384571aafa79a8453424fb4907c5f1e4268ce");
            Packet.AddString("");
            Packet.AddString("7ed2508c74ed4115");
            Packet.AddString("");
            Packet.AddString("GT-S7270");
            Packet.AddString("462e6d36-797e-4670-a5c3-b21ca6f9dfad");
            Packet.AddString("4.4.4");
            Packet.AddString("1");
            Packet.AddString("");
            Packet.AddString("7ed2508c74ed4115");
            Packet.AddString("en-US");
            Packet.Add(new byte());
            Packet.Add(new byte());
            Packet.AddString("");
            Packet.Add(new byte());
            Packet.AddString("");
            Packet.AddInt(2);
            Packet.AddString("");
            Packet.AddString("");
            Packet.AddString("");
            Packet.AddString("");
            Packet.Add(new byte());
            byte[] data = Packet.ToArray();
            byte[] encrypted = EnDecrypt.Encrypt(10100, data);
            List<byte> Header = new List<byte>();
            Header.AddUShort(10100);
            Header.Add(0);
            Header.AddUShort((ushort)data.Length);
            byte[] header = Header.ToArray();
            byte[] final = header.Concat(encrypted).ToArray();
            sck.Send(final);
            rtbPacket.Text = Encoding.ASCII.GetString(Packet.ToArray());
            rtbEncrypted.Text = Encoding.ASCII.GetString(final);
        }
    }
}
