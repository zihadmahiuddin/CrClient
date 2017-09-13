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
        KeyPair keyPair = PublicKeyBox.GenerateKeyPair();
        public byte[] PrivateKey;
        public byte[] PublicKey;
        public byte[] Buffer;
        public byte[] encrypted;
        public Socket sck = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
        public Form1()
        {
            InitializeComponent();
            PrivateKey = keyPair.PrivateKey;
            PublicKey = keyPair.PublicKey;
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
            byte[] data = new byte[128];
            MemoryStream stream = new MemoryStream(data);
            using (var writer = new MessageWriter(stream))
            {
                writer.Write(1);
                writer.Write(11);
                writer.Write(3);
                writer.Write(1);
                writer.Write(377);
                writer.Write("54955624828a47165ddf06c73ba01d72a2542ce7");
                writer.Write(2);
                writer.Write(2);
            }
            encrypted = Crypto.Encrypt(data,10100,1,keyPair);
            ReadData(data,10100);
            Console.WriteLine(ByteArrayToHexString(encrypted));
            sck.Send(encrypted);
            Console.WriteLine(ByteArrayToHexString(data));
            Logger.Write(ByteArrayToHexString(encrypted), PacketInfos.GetPacketName(10100));
            byte[] serverHello = new byte[2048];
            int received = sck.Receive(serverHello);
            Array.Resize(ref serverHello, received);
            Config.SessionKey = serverHello.Skip(11).ToArray();
            tbSessionKey.Text = BitConverter.ToString(Config.SessionKey).Replace("-","");
            Console.WriteLine(tbSessionKey.Text);
            rtbPacket.Text = Encoding.UTF8.GetString(data);
        }

        public void btnLogin_Click(object sender, EventArgs e)
        {
            rtbPacket.Clear();
            rtbEncrypted.Clear();
            rtbDecrypted.Clear();
            byte[] data = new byte[4096];
            MemoryStream stream = new MemoryStream(data);
            using (var writer = new MessageWriter(stream))
            {
                writer.Write(167521404572);
                writer.Write("dwnegp67n9f7dfhhaes6bs2pfftyrs4anxjjmjg4");//LoL It's a training camp account...
                writer.Write(VInt.WritevInt(3));
                writer.Write(VInt.WritevInt(1));
                writer.Write(VInt.WritevInt(377));
                writer.Write("54955624828a47165ddf06c73ba01d72a2542ce7");
                writer.Write("");
                writer.Write("c0389670ea3b1978");
                writer.Write("");
                writer.Write("C8817D");
                writer.Write("aa3e6cf0-0162-43d3-8719-f3d3b00356b7");
                writer.Write("4.4.2");
                writer.Write(true);
                writer.Write("");
                writer.Write("c0389670ea3b1978");
                writer.Write("en-US");
                writer.Write(false);
                writer.Write(false);
                writer.Write("");
                writer.Write(false);
                writer.Write("");
                writer.Write(VInt.WritevInt(2));
                writer.Write("");
                writer.Write("");
                writer.Write("");
                writer.Write("");
                writer.Write(false);
            }

            Console.WriteLine($"Payload: \n{ByteArrayToHexString(data)}");
            Logger.Write(ByteArrayToHexString(data), PacketInfos.GetPacketName(10101));
            encrypted = Crypto.Encrypt(data,10101,1,keyPair);
            ReadData(data,10101);
            Console.WriteLine($"ClientNonce: \n{ByteArrayToHexString(Config.Nonce)}");
            Console.WriteLine($"Encrypted Payload: \n{ByteArrayToHexString(encrypted)}");
            sck.Send(encrypted);
            rtbPacket.Text = ByteArrayToHexString(data);
            rtbEncrypted.Text = ByteArrayToHexString(encrypted);
            byte[] loginResult = new byte[9999];
            int received = sck.Receive(loginResult);
            Array.Resize(ref loginResult, received);
            byte[] decrypted;
            using (var reader = new Reader(loginResult))
            {
                var id = reader.ReadUInt16();
                switch (id)
                {
                    case 20104:
                        Console.WriteLine(id);
                        Console.WriteLine(BitConverter.ToString(loginResult).Replace("-", ""));
                        decrypted = Crypto.Decrypt(loginResult, keyPair);
                        GetDefinition(id, decrypted);
                        break;
                    case 20103:
                        Console.WriteLine(id);
                        decrypted = Crypto.Decrypt(loginResult, keyPair);
                        break;
                    default:
                        Console.WriteLine($"Unknown response from server [{id}].");
                        break;
                }
            }
            }
        public static string ByteArrayToHexString(byte[] byteArray)
        {
            string hex = BitConverter.ToString(byteArray);
            return hex.Replace("-", "");
        }

        private void ReadData(byte[] payload,ushort id)
        {
            Console.WriteLine($"ID: {id}");
            GetDefinition(id, payload);
        }

        private void GetDefinition(ushort id,byte[] payload)
        {
            switch (id)
            {
                case 10100:
                    using (Reader reader = new Reader(payload))
                    {
                        Console.WriteLine($"Protocol: {reader.ReadInt32()}");
                        Console.WriteLine($"KeyVersion: {reader.ReadInt32()}");
                        Console.WriteLine($"MajorVersion: {reader.ReadInt32()}");
                        Console.WriteLine($"MinorVersion: {reader.ReadInt32()}");
                        Console.WriteLine($"BuildVersion: {reader.ReadInt32()}");
                        Console.WriteLine($"Hash: {reader.ReadString()}");
                        Console.WriteLine($"DeviceType: {reader.ReadInt32()}");
                        Console.WriteLine($"AppStore: {reader.ReadInt32()}");
                    }
                    break;
                case 10101:
                    payload = EnDecrypt.Decrypt(encrypted);
                    using (Reader reader = new Reader(payload))
                    {
                        Console.WriteLine($"UserId: {reader.ReadInt64()}");
                        Console.WriteLine($"Token: {reader.ReadString()}");
                        Console.WriteLine($"MajorVersion: {reader.ReadVInt()}");
                        Console.WriteLine($"MinorVersion: {reader.ReadVInt()}");
                        Console.WriteLine($"BuildVersion: {reader.ReadVInt()}");
                        Console.WriteLine($"Hash: {reader.ReadString()}");
                        Console.WriteLine($"UDID: {reader.ReadString()}");
                        Console.WriteLine($"Open UdId: {reader.ReadString()}");
                        Console.WriteLine($"Mac Address: {reader.ReadString()}");
                        Console.WriteLine($"Device Model: {reader.ReadString()}");
                        Console.WriteLine($"Advertising GuId: {reader.ReadString()}");
                        Console.WriteLine($"OS Version: {reader.ReadString()}");
                        Console.WriteLine($"isAndroid: {reader.ReadBoolean()}");
                        reader.ReadString();
                        Console.WriteLine($"Android Id: {reader.ReadString()}");
                        Console.WriteLine($"Preferred device language: {reader.ReadString()}");
                        reader.ReadByte();
                        Console.WriteLine($"Preferred language: {reader.ReadByte()}");
                        Console.WriteLine($"Facebook attribution Id: {reader.ReadString()}");
                        Console.WriteLine($"Advertising enabled: {reader.ReadByte()}");
                        Console.WriteLine($"Apple IFV: {reader.ReadString()}");
                        Console.WriteLine($"AppStore: {reader.ReadVInt()}");
                        Console.WriteLine($"Kunlun SSO: {reader.ReadString()}");
                        Console.WriteLine($"Kunlun UID: {reader.ReadString()}");
                        reader.ReadString();
                        reader.ReadString();
                        reader.ReadByte();
                    }
                    break;
                case 20104:
                    Console.WriteLine(BitConverter.ToString(payload).Replace("-", ""));
                    using (var reader = new Reader(payload))
                    {
                        Console.WriteLine($"UserId: {reader.ReadInt64()}");
                        Console.WriteLine($"HomeId: {reader.ReadInt64()}");
                        Console.WriteLine($"Token: {reader.ReadString()}");
                        Console.WriteLine($"GameCenter Id: {reader.ReadString()}");
                        Console.WriteLine($"Facebook ID: {reader.ReadString()}");
                        Console.WriteLine($"Server Major Version: {reader.ReadVInt()}");
                        Console.WriteLine($"Server Build Version: {reader.ReadVInt()}");
                        reader.ReadVInt();
                        Console.WriteLine($"Content Version: {reader.ReadVInt()}");
                        Console.WriteLine($"Environment: {reader.ReadString()}");
                        Console.WriteLine($"Session Count: {reader.ReadVInt()}");
                        Console.WriteLine($"Play Time Seconds: {reader.ReadVInt()}");
                        Console.WriteLine($"Days since started playing: {reader.ReadVInt()}");
                        Console.WriteLine($"Facebook App Id: {reader.ReadString()}");
                        Console.WriteLine($"Server Time: {reader.ReadString()}");
                        Console.WriteLine($"Account creation date: {reader.ReadString()}");
                        reader.ReadVInt();
                        Console.WriteLine($"Google service id: {reader.ReadString()}");
                        reader.ReadString();
                        reader.ReadString();
                        Console.WriteLine($"Region: {reader.ReadString()}");
                        Console.WriteLine($"Content URL: {reader.ReadString()}");
                        Console.WriteLine($"Event Asset URL: {reader.ReadString()}");
                        reader.ReadByte();
                    }
                    break;
            }
        }
    }
}
