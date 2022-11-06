using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    
    public class Helper
    {
      
        public static void PrintBoard(char[,] board)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(board[i, j]);
                    Console.Write("  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n");
        }


        // This function returns true if there are moves
        // remaining on the board. It returns false if
        // there are no moves left to play.
        public static bool isMovesLeft(char[,] board)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (board[i, j] == '-')
                        return true;
            return false;
        }


        // This is the evaluation function 
        // it evaluate the board and return +10 if the player
        // calling the function is winning, -10 if the 
        // opponent is winning, and 0 in case of no winner
        // and the game is a Tie
        public static int evaluate(char[,] board, char player, char opponent)
        {
            // Checking for Rows for X or O victory.
            for (int row = 0; row < 3; row++)
            {
                if (board[row, 0] == board[row, 1] &&
                    board[row, 1] == board[row, 2])
                {
                    if (board[row, 0] == player)
                        return +10;
                    else if (board[row, 0] == opponent)
                        return -10;
                }
            }

            // Checking for Columns for X or O victory.
            for (int col = 0; col < 3; col++)
            {
                if (board[0, col] == board[1, col] &&
                    board[1, col] == board[2, col])
                {
                    if (board[0, col] == player)
                        return +10;

                    else if (board[0, col] == opponent)
                        return -10;
                }
            }

            // Checking for Diagonals for X or O victory.
            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            {
                if (board[0, 0] == player)
                    return +10;
                else if (board[0, 0] == opponent)
                    return -10;
            }

            if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            {
                if (board[0, 2] == player)
                    return +10;
                else if (board[0, 2] == opponent)
                    return -10;
            }

            // Else if none of them have won then return 0
            return 0;
        }


        // This is the minimax function. It considers all
        // the possible ways the game can go and returns
        // the value of the board
        public static int minimax(char[,] board, int depth, bool isMax, char player, char opponent)
        {
            int score = evaluate(board, player, opponent);

            // If Maximizer has won the game
            // return his/her evaluated score
            if (score == 10)
                return 10 - depth;

            // If Minimizer has won the game
            // return his/her evaluated score
            if (score == -10)
                return depth - 10;

            // If there are no more moves and
            // no winner then it is a tie
            if (isMovesLeft(board) == false)
                return 0;

            // If this maximizer's move
            if (isMax)
            {
                int best = -1000;

                // Traverse all cells
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        // Check if cell is empty
                        if (board[i, j] == '-')
                        {
                            // Make the move
                            board[i, j] = player;

                            // Call minimax recursively and choose
                            // the maximum value
                            best = Math.Max(best, minimax(board,
                                            depth + 1, !isMax, player, opponent));

                            // Undo the move
                            board[i, j] = '-';
                        }
                    }
                }
                return best;
            }

            // If this minimizer's move
            else
            {
                int best = 1000;

                // Traverse all cells
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        // Check if cell is empty
                        if (board[i, j] == '-')
                        {
                            // Make the move
                            board[i, j] = opponent;                           

                            // Call minimax recursively and choose
                            // the minimum value
                            best = Math.Min(best, minimax(board,
                                            depth + 1, !isMax, player, opponent));
                            
                            // Undo the move
                            board[i, j] = '-';
                        }
                    }
                }
                return best;
            }
        }


        // This will return the best possible
        // move for the player
        public static Move findBestMove(char[,] board, char player, char opponent)
        {
            int bestVal = -1000;
            Move bestMove = new Move();
            bestMove.row = -1;
            bestMove.col = -1;

            // Traverse all cells, evaluate minimax function
            // for all empty cells. And return the cell
            // with optimal value.
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Check if cell is empty
                    if (board[i, j] == '-')
                    {
                        // Make the move
                        board[i, j] = player;

                        // compute evaluation function for this
                        // move.
                        int moveVal = minimax(board, 0, false, player, opponent);
                        
                        // Undo the move
                        board[i, j] = '-';

                        // If the value of the current move is
                        // more than the best value, then update
                        // best/
                        if (moveVal > bestVal)
                        {
                            bestMove.row = i;
                            bestMove.col = j;
                            bestVal = moveVal;
                        }
                    }
                }
            }

            return bestMove;
        }


        // Check for a winner and return its symbol
        // or return 'Tie' in case of no winner
        // or return 'none' in case of the game is not finished
        public static string checkWinner(char[,] board, char player, char opponent)
        {
            int score = evaluate(board, player, opponent);

            // If the Player calling the function has won the game
            // return his symbol (X or O)
            if (score == 10)
                return player.ToString();

            // If Minimizer has won the game
            // return his symbol (X or O)
            if (score == -10)
                return opponent.ToString();

            // If there are no winner then it is a Tie
            if (isMovesLeft(board) == false)
                return "Tie";

            return "none";
        }

    }


    public class Move
    {
        public int row { get; set; }
        public int col { get; set; }

        public Move() { }

        public Move(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
    }
}
