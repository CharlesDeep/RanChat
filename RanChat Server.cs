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
            int x = 0;
            Socket ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ServerAdress = new IPEndPoint(IPAddress.Any, 8889); //Socket created, listening any ip on the net on port 8889.
            ServerSocket.Bind(ServerAdress); //Adress associated with the socket

            while (true)
            {
                byte[] ByteToReceive;
                try
                {
                    
                    ServerSocket.Listen(0);//Server is listening
                    if (x==0)
                    {
                        Console.WriteLine("Server is listening...");
                        x++;
                    }

                    Socket Listening = ServerSocket.Accept(); //Waiting for incoming connections...
                    if (x == 1)
                    {
                        Console.WriteLine("Connected succesfully.");
                        x++;
                    }
                    ByteToReceive = new byte[1024];
                    int byteStreamReceived;
                    byteStreamReceived = Listening.Receive(ByteToReceive, 0, ByteToReceive.Length, 0); //Array of text in bytes received
                    Array.Resize(ref ByteToReceive, byteStreamReceived); //Resizing the array
                    string TextReceived = Encoding.Default.GetString(ByteToReceive); //Array is converted to text
                    string Client_IP = Listening.RemoteEndPoint.ToString();
                    Console.Write(Client_IP);
                    Console.Write("      : " + TextReceived);
                    Listening.Close();
                }
                catch (Exception error)
                {
                    Console.WriteLine(error.ToString());
                }
            }
            Console.ReadKey();
        }
    }
}
