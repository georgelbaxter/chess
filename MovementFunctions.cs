using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class MovementFunctions
    {
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
            if (selectedPiece.PieceType == Piece.Type.Pawn && Math.Abs(rowsMoved) == 2)
                Playfield[row, col].EnPassant = true;
        }

        public void castle(int destinationCol, Piece[,] Playfield, int[] selectedIndeces)
        {
            //Castle queenside
            if (destinationCol == Board.FirstCol + 2)
            {
                Playfield[selectedIndeces[0], Board.FirstCol].HasMoved = true;
                Playfield[selectedIndeces[0], Board.FirstCol].Col = Board.FirstCol + 3;
                Playfield[selectedIndeces[0], Board.FirstCol + 3] = Playfield[selectedIndeces[0], Board.FirstCol];
                Playfield[selectedIndeces[0], Board.FirstCol] = new Piece();
            }
            //castle kingside
            if (destinationCol == Board.LastCol - 1)
            {
                Playfield[selectedIndeces[0], Board.LastCol].HasMoved = true;
                Playfield[selectedIndeces[0], Board.LastCol].Col = Board.LastCol - 2;
                Playfield[selectedIndeces[0], Board.LastCol - 2] = Playfield[selectedIndeces[0], Board.LastCol];
                Playfield[selectedIndeces[0], Board.LastCol] = new Piece();
            }
        }

        public void performEnPassant(int row, int col, Piece[,] Playfield)
        {
            if (row == Board.FirstRow + 2)
                Playfield[Board.FirstRow + 3, col] = new Piece();
            if (row == Board.LastRow - 2)
                Playfield[Board.LastRow - 3, col] = new Piece();
        }

        public void Promote(Piece.Type promoteTo, int row, int col, Piece[,] Playfield)
        {
            Playfield[row, col].PieceType = promoteTo;
        }
    }
}
