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
                Console.WriteLine("Сервер запущен. Ожидание подключений...");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
        public void Connect()
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

    }
}
