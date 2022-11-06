using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using TicTacToe;

internal class Program
{
    static string winner = "none";
    private static void Main(string[] args)
    {
        //char[,] board2 = {{ 'O', 'X', '-' },
        //                 { 'X', '-', '-' },
        //                 { '-', '-', '-' }};


        char[,] board = new char[3, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i, j] = '-';
            }
        }

        string exit;
        do
        {
            Console.Write("Starting a new game.\n" +
            "(1) play Vs Computer.\n" +
            "(2) Test the AI (Computer Vs Computer).\n" +
            "Type (1 or 2): ");
            string input = Console.ReadLine();
            while (input == null || (!input.Equals("1") && !input.Equals("2")))
            {
                Console.Write("Wrong input, please type (1 or 2): ");
                input = Console.ReadLine();
            }

            if (input.Equals("1"))
            {
                char humanSymbol;
                while (true)
                {
                    Console.Write("\n(Hint: 'X' Plays First)\n" +
                        "Choose your symbol('X' or 'O') : ");
                    string symbol = Console.ReadLine().ToUpper();
                    if (symbol.Equals("X") || symbol.Equals("O"))
                    {
                        humanSymbol = Convert.ToChar(symbol);
                        break;
                    }
                    Console.WriteLine("Invalid Input, try again");
                }
                
                Human_Vs_Computer(board, humanSymbol);
            }
            else if (input.Equals("2"))
            {
                Computer_Vs_Computer(board);
            }


            //Computer_Vs_Computer(board);

            //Human_Vs_Computer(board, 'O');


            if (winner == "Tie")
            {
                Console.WriteLine("No Winner, Game is Tie");
            }
            else
            {
                Console.WriteLine("The Winner is : " + winner);
            }

            Helper.PrintBoard(board);
            Console.Write("\t\t Game Over!\n" +
                "press Enter to start new game, or type (e) to Exit: ");
            exit = Console.ReadLine();
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - -");
            board = ResetBoard(board);
        } while (!exit.Equals("e"));

    }

    public static void Computer_Vs_Computer(char[,] board)
    {
        Player player1 = new Player('X');
        Player player2 = new Player('O');
        Player currentPlayer = player1;
        
        Helper.PrintBoard(board);
        do
        {
            board = currentPlayer.Play(board);
            Console.WriteLine(currentPlayer.myPlayer + " move:");
            Helper.PrintBoard(board);
            currentPlayer = currentPlayer == player1 ? player2 : player1;
            winner = Helper.checkWinner(board, currentPlayer.myPlayer, currentPlayer.opponent);

        } while (winner.Equals("none"));       
    }

    public static void Human_Vs_Computer(char[,] board, char humanSymbol)
    {
        char computerSymbol = humanSymbol == 'X' ? 'O' : 'X';
        Player player1 = new Player(computerSymbol);
        char turn = 'X';

        do
        {
            if (humanSymbol == turn)
            {
                int i;
                int j;
                while (true)
                {
                    Console.WriteLine("Enter the Row and Column positions to your move: ");   
                    try
                    {
                        Console.Write("Row(1 , 2 , 3): ");
                        i = Convert.ToInt32(Console.ReadLine()) -1;

                        Console.Write("Col(1 , 2 , 3): ");
                        j = Convert.ToInt32(Console.ReadLine()) -1;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Please enter valid numbers for Row and Col: ");
                        continue;
                    }

                    if( !(i >=0 && i<=3) || !(j >=0 && j<=3) )
                    {
                        Console.WriteLine("Please enter valid numbers for Row and Col: ");
                        continue;
                    }

                    if (board[i, j] != '-')
                    {
                        Console.WriteLine("The Position you entered enter is not empty, try again: ");
                        continue;
                    }

                    break;
                }

                board[i, j] = humanSymbol;
            }


            else
            {
                board = player1.Play(board);
            }

            Console.WriteLine(turn + " move: ");
            Helper.PrintBoard(board);

            winner = Helper.checkWinner(board, player1.myPlayer, player1.opponent);

            turn = turn == 'X' ? 'O' : 'X';
        } while (winner.Equals("none"));
    }

    public static char[,] ResetBoard(char[,] board)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i, j] = '-';
            }
        }
        return board;
    }

}