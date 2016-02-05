using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class MovementFunctions
    {
        const string pawn = "Pawn", rook = "Rook", knight = "Knight", bishop = "Bishop", queen = "Queen", king = "King", white = "White", black = "Black", empty = "empty";
        UtilityFunctions uf = new UtilityFunctions();
        DisplayFunctions df = DisplayFunctions.GetInstance();
        public void movePiece(Piece selectedPiece, Piece[,] Playfield, Piece[,] WhiteTaken, Piece[,] BlackTaken, int[] whiteTakenIndeces, int[] blackTakenIndeces, int[] selectedIndeces, int row, int col)
        {
            selectedPiece.HasMoved = true;
            int rowsMoved = selectedPiece.Row - row;
            //check if capture and move piece to Taken
            if (Playfield[row, col].Exists)
                df.AddTaken(Playfield[row, col], WhiteTaken, BlackTaken, whiteTakenIndeces, blackTakenIndeces);
            //move piece
            Playfield[row, col] = selectedPiece;
            Playfield[row, col].Row = row;
            Playfield[row, col].Col = col;
            //clear previous piece
            Playfield[selectedIndeces[0], selectedIndeces[1]] = new Piece();
            //enPassant
            if (selectedPiece.Type == pawn && Math.Abs(rowsMoved) == 2)
                Playfield[row, col].EnPassant = true;
        }

        public void castle(int destinationCol, Piece[,] Playfield, int[] selectedIndeces)
        {
            if (destinationCol == 2)
            {
                Playfield[selectedIndeces[0], 0].HasMoved = true;
                Playfield[selectedIndeces[0], 0].Col = 3;
                Playfield[selectedIndeces[0], 3] = Playfield[selectedIndeces[0], 0];
                Playfield[selectedIndeces[0], 0] = new Piece();
            }
            if (destinationCol == 6)
            {
                Playfield[selectedIndeces[0], 7].HasMoved = true;
                Playfield[selectedIndeces[0], 7].Col = 5;
                Playfield[selectedIndeces[0], 5] = Playfield[selectedIndeces[0], 7];
                Playfield[selectedIndeces[0], 7] = new Piece();
            }
        }

        public void performEnPassant(int row, int col, Piece[,] Playfield)
        {
            if (row == 2)
                Playfield[3, col] = new Piece();
            if (row == 5)
                Playfield[4, col] = new Piece();
        }

        public void Promote(string promoteTo, int row, int col, Piece[,] Playfield)
        {
            Playfield[row, col].Type = promoteTo;
        }
    }
}
