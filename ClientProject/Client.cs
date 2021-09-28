﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientProject
{
    public class Client
    {
        public int ID;
        public string ipAddr;
        public int port;
        public IPEndPoint iPEndPoint;
        public Socket socket;
        public Client()
        {
            this.ID++;
            this.ipAddr = "127.0.0.1";
            this.port = 8000;
            this.iPEndPoint = new IPEndPoint(IPAddress.Parse(ipAddr), port);
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public Client(Socket socket)
        {
          
            this.socket = socket;
            
        }

        public void Connect()
        {
            socket.Connect(iPEndPoint);
        }
        public void SendMsg(string sms)
        {
            byte[] data = new byte[256];
            data = Encoding.Unicode.GetBytes(sms);
            socket.Send(data);
        }
        public void SendMsg(string[] sms)
        {
            string outstr = String.Empty;
            byte[] data = new byte[256];
            foreach (var item in sms)
            {
                data = Encoding.Unicode.GetBytes(outstr+=$"{Path.GetFileName(item)}\n");
            }
         
          
            socket.Send(data);
        }
        public StringBuilder GetMsg()
        {
            int bytes = 0;
            byte[] data = new byte[256];
            StringBuilder stringBuilder = new StringBuilder();
            do
            {
                bytes = socket.Receive(data);
                stringBuilder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            } while (socket.Available > 0);
            if(stringBuilder.ToString().ToLower() == "exit")
            {
                Environment.Exit(0);
            }
            return stringBuilder;
        }
        public void GetServerCommand(StringBuilder command)
        {
            if (command.ToString().ToLower().Contains("start"))
            {
                if (command.ToString().ToLower().Contains("chrome"))
                {
                    Process.Start(@"C:\Program Files\Google\Chrome\Application\chrome.exe");
                }
                if (command.ToString().ToLower().Contains("opera"))
                {
                    Process.Start(@"C:\Program Files\Opera\launcher.exe");
                }
                if (command.ToString().ToLower().Contains("mozilla"))
                {
                    Process.Start(@"C:\Program Files\Mozilla Firefox\firefox.exe");
                }
                if (command.ToString().ToLower().Contains("edge"))
                {
                    Process.Start(@"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe");
                }
            }
            else
            {
                if (Directory.Exists(command.ToString()))
                {
                    try
                    {
                        SendMsg(Directory.GetFiles(command.ToString(), "*.exe"));
                    }
                    catch (Exception ex)
                    {

                        SendMsg(ex.Message);
                    }
                      
                }
                else
                {
                    SendMsg("Not found Directory");
                }
            }
           
        }

    }
}
