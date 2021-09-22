using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientProject
{
    public class Client
    {
        public string ipAddr;
        public int port;
        public IPEndPoint iPEndPoint;
        public Socket socket;
        public Client()
        {
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
            return stringBuilder;
        }
    }
}
