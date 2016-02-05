using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class CheckFunctions
    {
        UtilityFunctions uf = new UtilityFunctions();
        ThreatenHighlightFunctions thm = new ThreatenHighlightFunctions();
        DisplayFunctions df = DisplayFunctions.GetInstance();

        public bool isThreatened(int row, int col, string threatenedBy, Piece[,] board)
        {
            bool threatened = false;
            List<int[]> covered = new List<int[]>();
            List<int[]> filler = new List<int[]>();
            for (int rows = 0; rows < 8; rows++)
            {
                for (int cols = 0; cols < 8; cols++)
                {
                    if (board[rows, cols].Colour == threatenedBy)
                    {
                        thm.highlightThreatened(board[rows, cols], rows, cols, covered, filler, board, false);
                    }
                }
            }
            foreach (int[] pair in covered)
            {
                if (pair[0] == row && pair[1] == col)
                {
                    threatened = true;
                    break;
                }
            }
            return threatened;
        }

        public bool isThreatened(int[] indeces, string threatenedBy, Piece[,] board)
        {
            return isThreatened(indeces[0], indeces[1], threatenedBy, board);
        }

        public bool createCheck(int rowFrom, int colFrom, int rowTo, int colTo, Piece[,] board)
        {
            int[] moveTo = { rowTo, colTo }, moveFrom = { rowFrom, colFrom };
            return createCheck(moveFrom, moveTo, board);
        }

        public bool createCheck(int[] moveFrom, int[] moveTo, Piece[,] board)
        {
            string colour = board[moveFrom[0], moveFrom[1]].Colour;
            Piece[,] tmpPlayfield = new Piece[8, 8];
            //clone playfield
            for (int row = 0; row < 8; row++)
                for (int col = 0; col < 8; col++)
                    tmpPlayfield[row, col] = new Piece(board[row, col]);
            //move piece
            tmpPlayfield[moveTo[0], moveTo[1]] = tmpPlayfield[moveFrom[0], moveFrom[1]];
            tmpPlayfield[moveFrom[0], moveFrom[1]] = new Piece();
            //check if the king is threatened
            if (isThreatened(uf.FindKing(colour, tmpPlayfield), uf.OtherColour(colour), tmpPlayfield))
                return true;
            else
                return false;
        }
    }
}
