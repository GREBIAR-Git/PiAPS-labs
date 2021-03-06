using System;
using System.Net.Sockets;
using System.Text;
 
namespace Client
{
    public class ClientMessage
    {
        static TcpClient client=null;
        static string userName;
        public static void SignUp(string address, int port)
        {
            Console.Clear();
            Console.Write("Введите свое имя:");
            userName = Console.ReadLine();
            client = new TcpClient(address, port);
            NetworkStream stream = client.GetStream();
            byte[] data = Encoding.Unicode.GetBytes(String.Format(userName + ": вошёл в чат "));
            stream.Write(data, 0, data.Length);
        }

        public static void Send()
        {
            NetworkStream stream = client.GetStream();
            while(true)
            {
                Console.Write("Вы: ");
                string message = Console.ReadLine();
                byte[] data = Encoding.Unicode.GetBytes(String.Format("{0}: {1}", userName, message));
                stream.Write(data, 0, data.Length);
            }
        }
        public static void Receive()
        {
            NetworkStream stream = client.GetStream();
            while(true)
            {
                byte[] data = new byte[64]; // буфер для получаемых данных
                int bytes = stream.Read(data, 0, data.Length); 
                string message = Encoding.Unicode.GetString(data, 0, bytes);
                Console.WriteLine("\n"+ message);
            }
        }
        public static void CloseMessage()
        {
            client.Close();
        }
    }
}