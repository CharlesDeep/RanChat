using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Text.RegularExpressions;

namespace RanChat_Server
{
    class RanChatServer
    {
        static void Main(string[] args)
        {
            Console.WriteLine("RanChat Server Alpha");
            InitServer();
        }
        
        public static void InitServer()
        {
            Socket ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ServerAdress = new IPEndPoint(IPAddress.Any, 8889); //Socket created, listening any ip on the net on port 8889.
            while (true)
            {
                try
                {
                    ServerSocket.Bind(ServerAdress); //Adress associated with the socket
                    ServerSocket.Listen(1);//Server is listening
                    Console.WriteLine("Server is listening...");
                    Socket Listen = ServerSocket.Accept(); //Waiting for incoming connections...
                    Console.WriteLine("Connected succesfully.");
                    byte[] ByteToReceive = new byte[1024];
                    int byteStreamReceived = ServerSocket.Receive(ByteToReceive, 0, ByteToReceive.Length, 0); //Array of text in bytes received
                    Array.Resize(ref ByteToReceive, byteStreamReceived); //Resizing the array
                    string TextReceived = Encoding.Default.GetString(ByteToReceive); //Array is converted to text
                    Console.WriteLine("Client:" + TextReceived);
                    ServerSocket.Close();
                }
                catch (Exception error)
                {
                    Console.WriteLine(error.ToString());
                }
            }
        }
    }
}
