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
            string usernick;
            Socket ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ServerAdress = new IPEndPoint(IPAddress.Any, 8889); //Socket created, listening any ip on the net on port 8889.
            ServerSocket.Bind(ServerAdress); //Adress associated with the socket

            while (true)
            {
                byte[] BytesToReceive, bytesOfNick;
                bytesOfNick = new byte[26];
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
                    BytesToReceive = new byte[1024];
                    int byteStreamReceived;
                    byteStreamReceived = Listening.Receive(BytesToReceive, 0, BytesToReceive.Length, 0); //Array of text in bytes received
                    Array.Resize(ref BytesToReceive, byteStreamReceived); //Resizing the array
                    string Client_IP = Listening.RemoteEndPoint.ToString();
                    Buffer.BlockCopy(BytesToReceive, 0, bytesOfNick, 0, 26);
                    Buffer.BlockCopy(BytesToReceive, 26, BytesToReceive, 0, 998);
                    string TextReceived = Encoding.Default.GetString(BytesToReceive); //Array is converted to text
                    usernick = Encoding.Default.GetString(bytesOfNick);
                    Console.Write(Client_IP);
                    Console.Write("  (");
                    Console.Write(usernick);
                    Console.Write(")");
                    Console.WriteLine(" : " + TextReceived);
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
