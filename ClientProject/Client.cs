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
        public void DrawField(string[,] field)
        {

            Console.WriteLine($"{field[0, 0]} | {field[0, 1]} | {field[0, 2]}");
            Console.WriteLine($"_________");
            Console.WriteLine($"{field[1, 0]} | {field[1, 1]} | {field[1, 2]}");
            Console.WriteLine($"_________");
            Console.WriteLine($"{field[2, 0]} | {field[2, 1]} | {field[2, 2]}\n");
        }
        public void UpdateField(string[,] field,StringBuilder serverturn)
        {
            field[0, 0] = serverturn.ToString().Split('.')[0];
            field[0, 1] = serverturn.ToString().Split('.')[1];
            field[0, 2] = serverturn.ToString().Split('.')[2];
            field[1, 0] = serverturn.ToString().Split('.')[3];
            field[1, 1] = serverturn.ToString().Split('.')[4];
            field[1, 2] = serverturn.ToString().Split('.')[5];
            field[2, 0] = serverturn.ToString().Split('.')[6];
            field[2, 1] = serverturn.ToString().Split('.')[7];
            field[2, 2] = serverturn.ToString().Split('.')[8];


        }
        public bool CheckWin(string [,] field)
        {
            
                for (int i = 0; i < field.GetLength(0); i++)
                {
                    if (field[i, 0].Equals(field[i, 1]) && field[i, 1].Equals(field[i, 2]) && field[i, 0] != " ")
                        return true;

                    if (field[0, i].Equals(field[1, i]) && field[1, i].Equals(field[2, i]) && field[0, i] != " ")
                        return true;
                }

                if (field[0, 0].Equals(field[1, 1]) && field[1, 1].Equals(field[2, 2]) && field[1, 1] != " ")
                    return true;

                if (field[0, 2].Equals(field[1, 1]) && field[1, 1].Equals(field[2, 0]) && field[1, 1] != " ")
                    return true;

                return false;
            
        }
    }
}
