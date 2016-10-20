using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class UtilityFunctions
    {
        DisplayFunctions df = DisplayFunctions.GetInstance();
        public void InitializePlayfield(ref bool isWhiteTurn, ref bool isGameDone, 
            List<int[]> highlight, Piece[,] playfield, Piece[,] whiteTaken, 
            Piece[,] blackTaken, ref int[] whiteTakenIndeces, ref int[] blackTakenIndeces)
        {
            //reseting parameters
            df.Clear();
            isWhiteTurn = true;
            isGameDone = false;
            highlight.Clear();
            whiteTakenIndeces = new int[] { 0, 0 };
            blackTakenIndeces = new int[] { 0, 0 };
            for (int row = 0; row < Board.Rows; row++)
            {
                for (int col = 0; col < Board.Cols; col++)
                {
                    //set piece type
                    playfield[row, col] = new Piece() { Row = row, Col = col };
                    if (row == Board.FirstRow || row == Board.LastRow)
                    {
                        if (col == Board.FirstCol || col == Board.LastCol)
                            playfield[row, col].PieceType = Piece.Type.Rook;
                        if (col == Board.FirstCol + 1 || col == Board.LastCol - 1)
                            playfield[row, col].PieceType = Piece.Type.Knight;
                        if (col == Board.FirstCol + 2 || col == Board.LastCol - 2)
                            playfield[row, col].PieceType = Piece.Type.Bishop;
                        if (col == Board.FirstCol + 3)
                            playfield[row, col].PieceType = Piece.Type.Queen;
                        if (col == Board.FirstCol + 4)
                            playfield[row, col].PieceType = Piece.Type.King;
                    }
                    if (row == Board.FirstRow + 1 || row == Board.LastRow - 1)
                        playfield[row, col].PieceType = Piece.Type.Pawn;
                    //set piece color
                    //all pieces in the top two rows are black
                    if (row <= Board.FirstRow + 1)
                        playfield[row, col].PieceColour = Piece.Colour.Black;
                    //all pieces in the bottom two rows are white
                    if (row >= Board.LastRow - 1)
                        playfield[row, col].PieceColour = Piece.Colour.White;
                    //set those pieces to existing
                    if (row <= Board.FirstRow + 1 || row >= Board.LastRow - 1)
                        playfield[row, col].Exists = true;
                }

                //Reset the taken grids
                for (int col = 0; col < 2; col++)
                {
                    whiteTaken[row, col] = new Piece();
                    blackTaken[row, col] = new Piece();
                }
            }
        }

        public string GetImageFile(Piece piece, int row, int col)
        {
            //Add file location
            string fileName = "pack://application:,,,/Chess;component/Images/";
            //Add piece colour
            if (piece.Exists)
            {
                fileName += piece.PieceColour;
                fileName += piece.PieceType;
            }
            //Add background colour
            if (row % 2 == col % 2)
                fileName += "Light.png";
            else
                fileName += "Dark.png";
            return fileName;
        }

        public Piece.Colour OtherColour(Piece.Colour colour)
        {
            Piece.Colour otherColour;
            if (colour == Piece.Colour.Black)
                otherColour = Piece.Colour.White;
            else if (colour == Piece.Colour.White)
                otherColour = Piece.Colour.Black;
            else
                otherColour = Piece.Colour.Empty;
            return otherColour;
        }

        public int[] FindKing(Piece.Colour colour, Piece[,] board)
        {
            int[] kingIndeces = new int[2];
            bool isKingFound = false;
            for (int row = 0; row < Board.Rows; row++)
            {
                for (int col = 0; col < Board.Cols; col++)
                {
                    if (board[row, col].PieceType == Piece.Type.King && board[row, col].PieceColour == colour)
                    {
                        kingIndeces = new int[] { row, col };
                        isKingFound = true;
                        break;
                    }
                    if (isKingFound)
                        break;
                }
            }
            return kingIndeces;
        }

        public bool IsInBounds(int rowOrCol)
        {
            return (rowOrCol >= Board.FirstCol && rowOrCol <= Board.LastCol);
        }

        public bool IsInBounds(int row, int col)
        {
            return (row >= Board.FirstRow && row <= Board.LastRow 
                && col >= Board.FirstCol && col <= Board.LastCol);
        }
    }
}
