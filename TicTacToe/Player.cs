using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Player
    {
        public char myPlayer { get; set; }
        public char opponent { get; set; }

        public Player(char myPlayer)
        {
            this.myPlayer = myPlayer;
            opponent = myPlayer == 'X' ? 'O' : 'X';
        }

        public char[,] Play(char[,] board)
        {
            Move bestMove = Helper.findBestMove(board, myPlayer, opponent);
            board[bestMove.row, bestMove.col] = myPlayer;
            return board;
        }
    }
}
