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
            client.Connect();
            Console.WriteLine(client.GetMsg());
            string choose = String.Empty;
            string[,] field = new string[3, 3] { { " ", " ", " " }, { " ", " ", " " }, { " ", " ", " " } };
            bool playerturn = true;
            while (true)
            {
               
                if (playerturn == true)
                {
                    while (true)
                    {
                        client.DrawField(field);
                        if (client.CheckWin(field))
                        {
                            break;
                        }
                        Console.WriteLine("Enter cords: >1,1<");
                        choose = Console.ReadLine();
                        try
                        {
                            if (field[int.Parse(choose.ToString().Split(',')[0]) - 1, int.Parse(choose.ToString().Split(',')[1]) - 1] == " ")
                            {
                                break;
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Incorrect coords!");
                            System.Threading.Thread.Sleep(1000);
                        }
                       
                           
                        Console.Clear();
                    }
                    client.SendMsg(choose);
                    Console.Clear();
                    playerturn = false;
                }
                else
                {
                    Console.WriteLine("Wait other player");
                    StringBuilder serverturn = client.GetMsg();
                 
                   
                 
                   
                  

                    if (serverturn.ToString().Contains("GG"))
                    {
                      
                        Console.Clear();
                        client.DrawField(field);
                        Console.WriteLine(serverturn.ToString());
                        Console.ReadLine();
                        break;
                    }

                    client.UpdateField(field, serverturn);
                    Console.Clear();
                    playerturn = true;
                }
            }
        }
    }
}
