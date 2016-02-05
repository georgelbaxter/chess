using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class CheckDisplayFunctions
    {
        UtilityFunctions uf = new UtilityFunctions();
        DisplayFunctions df = DisplayFunctions.GetInstance();
        CheckFunctions cf = new CheckFunctions();
        MoveHighlightFunctions mhf = new MoveHighlightFunctions();

        public void checkForCheck(Piece[,] board, string colour)
        {
            if (cf.isThreatened(uf.FindKing(uf.OtherColour(colour), board), colour, board))
                df.Check();
            else
                df.Clear();
        }

        public void checkForCheckmate(Piece[,] board, string colour, bool gameDone)
        {
            //runs after each move, still the piece that just moved's turn
            bool checkmate = true;
            List<int[]> toMove = new List<int[]>();
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (board[row, col].Colour == uf.OtherColour(colour))
                        mhf.highlightMoveable(board[row, col], board, row, col, toMove);
                    if (toMove.Count > 0)
                    {
                        checkmate = false;
                        break;
                    }
                }
                if (!checkmate)
                    break;
            }
            //don't need to check for mate if there are available moves
            if (checkmate)
            {
                int[] kingIndeces = uf.FindKing(uf.OtherColour(colour), board);
                if (!gameDone && checkmate && cf.isThreatened(kingIndeces[0], kingIndeces[1], colour, board))
                {
                    df.Checkmate(uf.OtherColour(colour));
                    gameDone = true;
                }
                else if (!gameDone && checkmate)
                {
                    df.Stalemate();
                    gameDone = true;
                }
            }
        }
    }
}
