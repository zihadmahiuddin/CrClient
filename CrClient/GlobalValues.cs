using System.Net.Sockets;

namespace CrClient
{
    public class GlobalValues
    {
        public static string SessionKeyFilter = "N?\0\0\u001c\0\0\0\0\0\u0018";
        public static string SessionKey { get; set; }
        public static byte[] SessionKeyBytes { get; set; }
        public static Socket sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }
}
