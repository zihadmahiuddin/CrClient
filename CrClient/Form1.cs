using Sodium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace CrClient
{
    public partial class Form1 : Form
    {
        KeyPair keyPair;
        public byte[] PrivateKey;
        public byte[] PublicKey;
        public byte[] Buffer;
        public byte[] encrypted;
        public Socket sck = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
		public string token;
        public int tick;
        public int ECTSeed;
        public long checksum;
        public static Config Config = Config.Load();
        System.Timers.Timer timer = new System.Timers.Timer();
        System.Timers.Timer tickTimer = new System.Timers.Timer();
        public Form1()
        {
            InitializeComponent();
            keyPair = PublicKeyBox.GenerateKeyPair();
            //keyPair = new KeyPair(Utils.HexToByteArray("441758670C22377A74676896D9F8B95A7B79CA99888055D1858DEAF5A0FC8567"), Utils.HexToByteArray("A854DBA70429943C36D2F0E7E9AC8DEA112B19A402451F5809FBB797AC6B4930"));
            PrivateKey = keyPair.PrivateKey;
            PublicKey = keyPair.PublicKey;
			WebClient wc = new WebClient();
			token = wc.DownloadString("http://localhost/Tokens/cr_account_token.txt");
        }

        public void btnConnect_Click(object sender, EventArgs e)
        {
            //sck.Connect("80.182.142.125", 9339);
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
            Packet.AddInt(Config.MajorVersion);
            Packet.AddInt(Config.MinorVersion);
            Packet.AddInt(Config.BuildVersion);
            Packet.AddString(Config.ResourceHash);
            Packet.AddInt(2);
            Packet.AddInt(2);
            byte[] data = Packet.ToArray();
            //byte[] data = new byte[128];
            //MemoryStream stream = new MemoryStream(data);
            //using (var writer = new MessageWriter(stream))
            //{
            //    writer.Write(1);
            //    writer.Write(11);
            //    writer.Write(Config.MajorVersion);
            //    writer.Write(Config.MinorVersion);
            //    writer.Write(Config.BuildVersion);
            //    writer.Write(Config.ResourceHash);
            //    writer.Write(2);
            //    writer.Write(2);
            //}
            encrypted = Crypto.Encrypt(data,10100,1,keyPair);
            ReadData(data,10100);
            Console.WriteLine(ByteArrayToHexString(encrypted));
            sck.Send(encrypted);
            Console.WriteLine(ByteArrayToHexString(data));
            Logger.Write(Encoding.UTF8.GetString(data), PacketInfos.GetPacketName(10100));
            byte[] serverHello = new byte[32768];
            int received = sck.Receive(serverHello);
            Array.Resize(ref serverHello, received);
            ushort id;
            using (var reader = new Reader(serverHello))
            {
                id = reader.ReadUInt16();
            }
            switch(id)
            {
                case 20100:
                ClientConfig.SessionKey = serverHello.Skip(11).Take(24).ToArray();
                tbSessionKey.Text = BitConverter.ToString(ClientConfig.SessionKey).Replace("-","");
                Console.WriteLine(tbSessionKey.Text);
                rtbPacket.Text = Encoding.UTF8.GetString(data);
                    break;
                case 20103:
                ReadData(serverHello,id);
                    break;
                default:
                Console.WriteLine($"Unknown response from server.\nID: {id}");
                    break;
            }
        }

        public void btnLogin_Click(object sender, EventArgs e)
        {
            rtbPacket.Clear();
            rtbEncrypted.Clear();
            rtbDecrypted.Clear();
            long id = 12885955392;
            //long id = 167521404572; //Noob
            //token = "dwnegp67n9f7dfhhaes6bs2pfftyrs4anxjjmjg4"; //Noob
            List<byte> Packet = new List<byte>();
            Packet.AddLong(id);
            Packet.AddString(token);
            Packet.AddVInt(Config.MajorVersion);
            Packet.AddVInt(Config.MinorVersion);
            Packet.AddVInt(Config.BuildVersion);
            Packet.AddString(Config.ResourceHash);
            Packet.AddString("");
            Packet.AddString("c0389670ea3b1978");
            Packet.AddString("");
            Packet.AddString("C8817D");
            Packet.AddString("aa3e6cf0-0162-43d3-8719-f3d3b00356b7");
            Packet.AddString("4.4.2");
            Packet.AddBool(true);
            Packet.AddString("");
            Packet.AddString("c0389670ea3b1978");
            Packet.AddString("en-US");
            Packet.AddBool(false);
            Packet.AddBool(false);
            Packet.AddString("");
            Packet.AddBool(false);
            Packet.AddString("");
            Packet.AddVInt(2);
            Packet.AddString("");
            Packet.AddString("");
            Packet.AddString("");
            Packet.AddString("");
            Packet.AddBool(false);
            byte[] data = Packet.ToArray();
            //byte[] data = new byte[4096];
            //MemoryStream stream = new MemoryStream(data);
            //using (var writer = new MessageWriter(stream))
            //{
            //    writer.Write(id);
            //    writer.Write(token);
            //    writer.Write(VInt.WritevInt(Config.MajorVersion));
            //    writer.Write(VInt.WritevInt(Config.MinorVersion));
            //    writer.Write(VInt.WritevInt(Config.BuildVersion));
            //    writer.Write(Config.ResourceHash);
            //    writer.Write("");
            //    writer.Write("c0389670ea3b1978");
            //    writer.Write("");
            //    writer.Write("C8817D");
            //    writer.Write("aa3e6cf0-0162-43d3-8719-f3d3b00356b7");
            //    writer.Write("4.4.2");
            //    writer.Write(true);
            //    writer.Write("");
            //    writer.Write("c0389670ea3b1978");
            //    writer.Write("en-US");
            //    writer.Write(false);
            //    writer.Write(false);
            //    writer.Write("");
            //    writer.Write(false);
            //    writer.Write("");
            //    writer.Write(VInt.WritevInt(2));
            //    writer.Write("");
            //    writer.Write("");
            //    writer.Write("");
            //    writer.Write("");
            //    writer.Write(false);
            //}
            Console.WriteLine($"Payload: \n{ByteArrayToHexString(data)}");
            encrypted = Crypto.Encrypt(data,10101,1,keyPair);
            //EnDecrypt.Decrypt(encrypted);
            //ReadData(data,10101);
            sck.Send(encrypted);
            Logger.Write(Encoding.UTF8.GetString(data), PacketInfos.GetPacketName(10101));
            rtbPacket.Text = ByteArrayToHexString(data);
            rtbEncrypted.Text = ByteArrayToHexString(encrypted);
            byte[] loginResult = new byte[32768];
            int received = sck.Receive(loginResult);
            Array.Resize(ref loginResult, received);
            byte[] decrypted;
            using (var reader = new Reader(loginResult))
            {
                var Id = reader.ReadUInt16();
                switch (Id)
                {
                    case 20104:
                        Console.WriteLine(Id);
                        Console.WriteLine(BitConverter.ToString(loginResult).Replace("-", ""));
                        decrypted = Crypto.Decrypt(loginResult, keyPair);
                        GetDefinition(Id, decrypted);
                        timer.Enabled = true;
                        timer.Interval = 10000;
                        timer.Elapsed += TimerElapsed;
                        tickTimer.Enabled = true;
                        tickTimer.Interval = 50;
                        tickTimer.Elapsed += CountTicks;
                        break;
                    case 20103:
                        Console.WriteLine(Id);
                        decrypted = Crypto.Decrypt(loginResult, keyPair);
                        GetDefinition(Id, decrypted);
                        break;
                    default:
                        Console.WriteLine($"Unknown response from server [{Id}].");
                        Console.WriteLine($"Payload: {BitConverter.ToString(loginResult).Replace("-","")}.");
                        break;
                }
            }
            }

        private void CountTicks(object sender, System.Timers.ElapsedEventArgs e)
        {
            tick++;
        }

        private void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            byte[] keepAlive = new byte[0];
            byte[] encryptedKeepAlive = Crypto.Encrypt(keepAlive, 10108, 1, keyPair);
            sck.Send(encryptedKeepAlive);
        }

        public static string ByteArrayToHexString(byte[] byteArray)
        {
            string hex = BitConverter.ToString(byteArray);
            return hex.Replace("-", " ");
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
                case 20103:
                using (Reader reader = new Reader(payload))
                {
                        Console.WriteLine($"Error Code: {reader.ReadVInt()}");
                        Console.WriteLine($"ResourceFingerprintData: {reader.ReadString()}");
                        Console.WriteLine($"RedirectDomain: {reader.ReadString()}");
                        Console.WriteLine($"ContentURL: {reader.ReadString()}");
                        Console.WriteLine($"UpdateURL: {reader.ReadString()}");
                        Console.WriteLine($"Reason: {reader.ReadString()}");
                        Console.WriteLine($"SecondsUntilMaintenanceEnd: {reader.ReadInt32()}");
                        reader.ReadByte();
                        reader.ReadString();
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
                case 10108:
                    Console.WriteLine("So, you want to see that what's inside the KeepAlive packet?\nIt's empty dude!");
                    break;
                case 20108:
                    Console.WriteLine("So, you want to see that what's inside the KeepAlive packet?\nIt's empty dude!");
                    break;
                case 14315:
                    payload = EnDecrypt.Decrypt(payload);
                    Console.WriteLine(ByteArrayToHexString(payload));
                    using(var reader = new Reader(payload))
                    {
                        Console.WriteLine($"Message: {reader.ReadString()}");
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
                        Console.WriteLine($"Server Type: {reader.ReadVInt()}");
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
                        reader.ReadString();
                        Console.WriteLine($"Content URL: {reader.ReadString()}");
                        Console.WriteLine($"Event Asset URL: {reader.ReadString()}");
                        reader.ReadByte();
                    }
                    byte[] ohd = new byte[99999999];
                    int received = sck.Receive(ohd);
                    Array.Resize(ref ohd, received);
                    Console.WriteLine($"Encrypted OHD: {BitConverter.ToString(ohd).Replace("-","")}");
                    payload = Crypto.Decrypt(ohd, keyPair);
                    ReadData(payload, 24101);
                    Console.WriteLine($"Decrypted OHD: {BitConverter.ToString(payload).Replace("-","")}");
                    break;
                case 24101:
                    using (var reader = new Reader(payload))
                    {
                        Console.WriteLine($"User ID: {reader.ReadInt32()}/{reader.ReadInt32()}");
                        ECTSeed = reader.ReadVInt();
                        GenerateChecksum();
                        Console.WriteLine($"ECT Seed: {ECTSeed}");
                        Console.WriteLine($"Age/Time: {reader.ReadVInt()}");
                        Console.WriteLine($"Donation cooldown/Seconds until next free chest: {reader.ReadVInt()}");
                        Console.WriteLine($"Donation capacity: {reader.ReadVInt()}");
                        Console.WriteLine($"Login Time: {reader.ReadVInt()}");
                        reader.ReadByte();
                        Console.WriteLine($"Total Decks Amount: {reader.ReadVInt()}");
                        Console.WriteLine($"Deck 1 Cards Amount: {reader.ReadVInt()}");
                        Console.WriteLine($"Deck 1 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 1 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 1 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 1 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 1 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 1 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 1 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 1 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 2 Cards Amount: {reader.ReadVInt()}");
                        Console.WriteLine($"Deck 2 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 2 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 2 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 2 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 2 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 2 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 2 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 2 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 3 Cards Amount: {reader.ReadVInt()}");
                        Console.WriteLine($"Deck 3 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 3 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 3 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 3 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 3 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 3 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 3 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 3 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 4 Cards Amount: {reader.ReadVInt()}");
                        Console.WriteLine($"Deck 4 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 4 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 4 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 4 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 4 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 4 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 4 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 4 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 5 Cards Amount: {reader.ReadVInt()}");
                        Console.WriteLine($"Deck 5 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 5 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 5 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 5 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 5 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 5 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 5 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Deck 5 Card: {CardInfos.GetName(reader.ReadVInt())}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        Console.WriteLine($"Unknown: {reader.ReadVInt()}");
                        var baseStream = reader.BaseStream;
                        Console.WriteLine(baseStream.Position);
                    }
                    break;
            }
        }

        private void GenerateChecksum()
        {
            checksum = (70 - 8) << 16 | ECTSeed;
            Console.WriteLine($"Checksum: {checksum}");
        }

        private void btnSendClanChatMessage_Click(object sender, EventArgs e)
        {
            List<byte> Packet = new List<byte>();
            Packet.AddString(tbClanChatMessage.Text);
            byte[] chat = Packet.ToArray();
            byte[] encryptedChat = Crypto.Encrypt(chat, 14315, 1, keyPair);
            sck.Send(encryptedChat);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            int commandId = 509;
            int startTick = tick - 1;
            int endtick = tick - 1;
            long id = 12885955392;
            List<byte> FreeChest = new List<byte>();
            FreeChest.AddVInt(commandId);
            FreeChest.AddVInt(startTick);
            FreeChest.AddVInt(endtick);
            FreeChest.AddVInt64(id);
            byte[] array = FreeChest.ToArray();
            List<byte> ETC = new List<byte>();
            ETC.AddVInt(tick);
            ETC.AddVInt64(checksum);
            ETC.AddVInt(1);
            ETC.AddRange(array);
            ETC.AddVInt(-1);
            ETC.AddVInt(-1);
            ETC.AddVInt(-1);
            ETC.AddVInt(-1);
            byte[] endClientTurn = ETC.ToArray();
            //byte[] endClientTurn = new byte[2048];
            //Stream stream = new MemoryStream(endClientTurn);
            //using (var writer = new MessageWriter(stream))
            //{
            //    writer.Write(VInt.WritevInt(tick));
            //    writer.Write(VInt.WritevInt(Convert.ToInt32(checksum)));
            //    writer.Write(VInt.WritevInt(1));
            //    writer.Write(array);
            //    writer.Write(VInt.WritevInt(-1));
            //    writer.Write(VInt.WritevInt(-1));
            //    writer.Write(VInt.WritevInt(-1));
            //    writer.Write(VInt.WritevInt(-1));
            //}
            Console.WriteLine(BitConverter.ToString(endClientTurn).Replace("-", ""));
            byte[] encryptedArray = Crypto.Encrypt(endClientTurn, 14102, 1, keyPair);
            sck.Send(encryptedArray);
            checksum++;
        }

        private void btnJoinClan_Click(object sender, EventArgs e)
        {
            long id = TagIDTools.GetIDFromHashTag(tbClanTag.Text);
            List<byte> JoinClan = new List<byte>();
            JoinClan.AddLong(id);
            byte[] joinClan = JoinClan.ToArray();
            //byte[] joinClan = new byte[16];
            //Stream stream = new MemoryStream(joinClan);
            //using (var writer = new MessageWriter(stream))
            //{
            //    writer.Write(id);
            //}
            byte[] encrypted = Crypto.Encrypt(joinClan, 14305, 1, keyPair);
            sck.Send(encrypted);
        }

        private void btnLeaveCurrentClan_Click(object sender, EventArgs e)
        {
            byte[] leaveClan = new byte[0];
            byte[] encrypted = Crypto.Encrypt(leaveClan, 14308, 1, keyPair);
            sck.Send(encrypted);
        }

        private void btnGoHome_Click(object sender, EventArgs e)
        {
            List<byte> GoHome = new List<byte>();
            GoHome.AddVInt64(12885955392);
            byte[] goHome = GoHome.ToArray();
            //byte[] goHome = new byte[128];
            //Stream goHomeStream = new MemoryStream(goHome);
            //using (var writer = new MessageWriter(goHomeStream))
            //{
            //    writer.Write(VInt.WritevInt(3));
            //    writer.Write(VInt.WritevInt(1053504));
            //}
            byte[] encrypted = Crypto.Encrypt(goHome, 14101, 1, keyPair);
            sck.Send(encrypted);
        }
        public void BruteForceTourney()
        {
            BruteForcer.BruteForceTouney();
        }
    }
}