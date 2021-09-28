﻿using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using ClientProject;
using System.IO;

namespace ServerProject
{
    class Server
    {
        public int Client_ID;
        private string ipAddr;
        private int port;
        private IPEndPoint ipPoint;
        public Socket socket;
        public Socket socketclient;
        public List<Client> clients;


        public Server()
        {
            this.Client_ID = 0;
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

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            Console.WriteLine("Start server\nWait of connect");

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
                this.Client_ID++;
                clients[clients.Count - 1].ID = this.Client_ID;

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
                if (File.Exists(message) && Path.GetFileName(message).Contains(".txt") || Path.GetFileName(message).Contains(".rtf"))
                {
                    item.socket.Send(Encoding.Unicode.GetBytes(File.ReadAllText(message)));
                }
                else
                {
                    item.socket.Send(Encoding.Unicode.GetBytes(message));
                }
            }

        }
        public void SendMsg(string message, int user)
        {
            byte[] data = new byte[256];
            if (File.Exists(message) && Path.GetFileName(message).Contains(".txt") || Path.GetFileName(message).Contains(".rtf"))
            {
                clients[user-1].socket.Send(Encoding.Unicode.GetBytes(File.ReadAllText(message)));
            }
            else
            {
                clients[user-1].socket.Send(Encoding.Unicode.GetBytes(message));
            }

        }
        public void SendCommand(int choice)
        {

            int server_choice = 0;
            int user_choice = 0;
            bool check = false;
            Exception exception = new Exception();
            do
            {
                foreach (var item in clients)
                {
                    Console.WriteLine($"<{item.ID}> " + $"{item.socket.Available} ",$"{item.socket.AddressFamily.ToString()}");
                }
                Console.WriteLine("Choice ID");
                try
                {
                    user_choice = int.Parse(Console.ReadLine());
                    if (user_choice > clients.Count)
                    {
                        exception.GetBaseException();
                    }
                    check = true;
                }
                catch (Exception)
                {
                    check = false;
                    Console.Clear();
                }
            } while (!check) ;

            switch (choice)
            {
                case 1:
                    {
                        foreach (var item in clients)
                        {
                            if (item.ID == user_choice)
                            {
                                Console.WriteLine("Choice a Browser\nOpera: 1\nChrome: 2\nMozilla FireFox: 3\n Edge: 4");
                                server_choice = int.Parse(Console.ReadLine());
                                SendBrowser(server_choice, user_choice);
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        foreach (var item in clients)
                        {
                            if (item.ID == user_choice)
                            {

                            }
                        }
                        break;
                    }
                default:
                    break;
            }
        }
        public void SendBrowser(int choice)
        {
            switch (choice)
            {
                case 1:
                    {
                        SendMsg("Start Opera");
                        break;
                    }
                case 2:
                    {
                        SendMsg("Start Chrome");
                        break;
                    }
                case 3:
                    {
                        SendMsg("Start Mozilla");
                        break;
                    }
                case 4:
                    {
                        SendMsg("Start Edge");
                        break;
                    }
                default:
                    break;
            }
        }
        public void SendBrowser(int choice, int user)
        {
            switch (choice)
            {
                case 1:
                    {
                        SendMsg("Start Opera", user);
                        break;
                    }
                case 2:
                    {
                        SendMsg("Start Chrome", user);
                        break;
                    }
                case 3:
                    {
                        SendMsg("Start Mozilla", user);
                        break;
                    }
                case 4:
                    {
                        SendMsg("Start Edge", user);
                        break;
                    }
                default:
                    break;
            }
        }

    }
}
