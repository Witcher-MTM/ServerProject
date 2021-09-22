using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using ClientProject;


namespace ServerProject
{
    class Server
    {
        
        public string ipAddr;
        public int port;
        public IPEndPoint ipPoint;
        public Socket socket;
        public Socket socketclient;
        public List<Client> clients;

        public Server()
        {
            this.ipAddr = "127.0.0.1";
            this.port = 8000;
            this.ipPoint = new IPEndPoint(IPAddress.Parse(ipAddr), port);
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.clients = new List<Client>();

        }
        public void StartServer()
        {
            try
            {

                this.socket.Bind(ipPoint);
                this.socket.Listen(10);
                Console.WriteLine("------Waiting for second player------");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
        public void ConnectOne()
        {
            bool check = true;
            while (check)
            {
                this.socketclient = this.socket.Accept();
                clients.Add(new Client(socketclient));
                if (clients.Count > 0)
                {
                    check = false;
                }
            }

        }
        public void Connects()
        {
            while (true)
            {
                this.socketclient = this.socket.Accept();
                clients.Add(new Client(socketclient));
              
            }

        }
        public StringBuilder GetMsg()
        {

            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            byte[] data = new byte[256];
            foreach (var item in clients)
            {
                do
                {

                    bytes = item.socket.Receive(data);

                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                } while (item.socket.Available > 0);
            }

            return builder;
        }
        public void SendMsg(string message)
        {
            byte[] data = new byte[256];
            foreach (var item in clients)
            {
                item.socket.Send(Encoding.Unicode.GetBytes(message));
            }
        }
        public void DrawField()
        {

            Console.WriteLine($"{TicTacToe.Field[0, 0]}|{TicTacToe.Field[0, 1]}|{TicTacToe.Field[0, 2]}");
            Console.WriteLine($"—————");
            Console.WriteLine($"{TicTacToe.Field[1, 0]}|{TicTacToe.Field[1, 1]}|{TicTacToe.Field[1, 2]}");
            Console.WriteLine($"—————");
            Console.WriteLine($"{TicTacToe.Field[2, 0]}|{TicTacToe.Field[2, 1]}|{TicTacToe.Field[2, 2]}\n");
        }
    }
}
