using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ServerProject
{
    class ServerProgram
    {
       
        static void Main(string[] args)
        {
            Server server = new Server();
           

            try
            {
                server.StartServer();
                Task.Factory.StartNew(() => server.Connect());

                Console.ReadLine();
                server.SendMsg("Welcome");

              

                while (true)
                {
                    server.SendMsg(Console.ReadLine());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
           
        }
    }
}
