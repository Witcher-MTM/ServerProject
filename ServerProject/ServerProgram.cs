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
            TicTacToe.Init();
            string choose = String.Empty;
            try
            {
                server.StartServer();
                server.ConnectOne();
                server.SendMsg("You are connected");

                while (true)
                {

                    if (TicTacToe.Player2Turn == false)
                    {
                        Console.WriteLine("Wait for other player to make turn!");
                        string clientturn = server.GetMsg().ToString();
                        TicTacToe.Field[int.Parse(clientturn.Split(',')[0]) - 1, int.Parse(clientturn.Split(',')[1]) - 1] = TicTacToe.Player1Symb;
                        if (TicTacToe.CheckWin())
                        {
                            break;
                        }
                        Console.Clear();
                        TicTacToe.Player2Turn = true;
                    }
                    else
                    {
                        while (true)
                        {
                            server.DrawField();

                            Console.WriteLine("Your turn! Enter cords like this: 1,1");
                            choose = Console.ReadLine();
                            try
                            {
                                if (TicTacToe.Field[int.Parse(choose.Split(',')[0]) - 1, int.Parse(choose.Split(',')[1]) - 1] == ' ')
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
                        TicTacToe.Field[int.Parse(choose.Split(',')[0]) - 1, int.Parse(choose.Split(',')[1]) - 1] = TicTacToe.Player2Symb;
                        if (TicTacToe.CheckWin())
                        {
                            server.SendMsg(TicTacToe.FieldToString());
                            break;
                        }

                        server.SendMsg(TicTacToe.FieldToString());
                        Console.Clear();
                        TicTacToe.Player2Turn = false;
                    }
                }
                Console.Clear();
                server.DrawField();
                if (TicTacToe.Player2Turn == false)
                {
                   
                    Console.WriteLine("GG! Player 1 Won!");
                    server.SendMsg("GG! Player 1 Won!");
                  
                }
                   
                else
                {
                   
                    Console.WriteLine("GG! Player 2 Won!");
                    server.SendMsg("GG! Player 2 Won!");
                   
                }
                   


                
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

        }
    }
}
