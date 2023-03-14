using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {

        static Socket clientSokcket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        static void Main(string[] args)
        {
            SetupClient();
            Console.ReadLine();
        }

        static void SetupClient()
        {
            try {
                clientSokcket.Connect(IPAddress.Loopback, 100);
                Console.WriteLine("Client connected to {0}", clientSokcket.RemoteEndPoint.ToString());
                AskRequest();
            } catch (Exception e) {
                Console.WriteLine("Exception : {0}", e.Message);
            }
        }

        static void AskRequest()
        {
            while (true)
            {
                Console.Write("Enter your request: ");
                var request = Console.ReadLine();
                var requestBytes = Encoding.ASCII.GetBytes(request);
                clientSokcket.Send(requestBytes);
                PrintResponse();
            }
        }

        static void PrintResponse()
        {
            var buffer = new byte[1024];
            var receiveLength = clientSokcket.Receive(buffer);
            var response = Encoding.ASCII.GetString(buffer, 0, receiveLength);
            Console.WriteLine("Response: {0}", response);
        }
    }
}
