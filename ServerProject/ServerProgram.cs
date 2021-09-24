using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using ClientProject;
namespace ServerProject
{
    class ServerProgram
    {

        static void Main(string[] args)
        {
            Server server = new Server();
            server.StartServer();
           
            try

            {
                Task.Factory.StartNew(() => server.Connects());
               
                while (true)
                {
                   
                    Console.WriteLine("Enter a message");
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
