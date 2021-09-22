using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientProject
{
    class ClientProgram
    {
      
        static void Main(string[] args)
        {
            Client client = new Client();
            try
            {
                client.Connect();
                while (true)
                {
                    Console.WriteLine(client.GetMsg());
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
