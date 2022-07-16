using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientTcp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string ip = "127.0.0.1";
            const int port = 8080;

            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            var tcpSoket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("Введите собщения");
            var massage = Console.ReadLine();

            var data = Encoding.UTF8.GetBytes(massage);

            tcpSoket.Connect(tcpEndPoint);

            tcpSoket.Send(data);

            var buffer = new byte[256];
            var size = 0;
            var answer = new StringBuilder();

            do
            {
                size = tcpSoket.Receive(buffer);
                answer.Append(Encoding.UTF8.GetString(buffer, 0, size));
            }
            while (tcpSoket.Available > 0);

            Console.WriteLine(answer.ToString());
            tcpSoket.Shutdown(SocketShutdown.Both);

            tcpSoket.Close();

            Console.ReadLine();



        }
    }
}
