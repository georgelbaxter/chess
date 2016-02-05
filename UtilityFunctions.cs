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
        const string pawn = "Pawn", rook = "Rook", knight = "Knight", bishop = "Bishop", queen = "Queen", king = "King", white = "White", black = "Black", empty = "empty";
        public void InitializePlayfield(ref bool whiteTurn, ref bool gameDone, List<int[]> highlight, Piece[,] playfield, Piece[,] whiteTaken, Piece[,] blackTaken, ref int[] whiteTakenIndeces, ref int[] blackTakenIndeces)
        {
            //reseting parameters
            df.Clear();
            whiteTurn = true;
            gameDone = false;
            highlight.Clear();
            whiteTakenIndeces = new int[] { 0, 0 };
            blackTakenIndeces = new int[] { 0, 0 };
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    //set piece type
                    playfield[row, col] = new Piece() { Row = row, Col = col };
                    if (row == 0 || row == 7)
                    {
                        if (col == 0 || col == 7)
                            playfield[row, col].Type = rook;
                        if (col == 1 || col == 6)
                            playfield[row, col].Type = knight;
                        if (col == 2 || col == 5)
                            playfield[row, col].Type = bishop;
                        if (col == 3)
                            playfield[row, col].Type = queen;
                        if (col == 4)
                            playfield[row, col].Type = king;
                    }
                    if (row == 1 || row == 6)
                        playfield[row, col].Type = pawn;
                    //set piece color
                    if (row <= 1)
                        playfield[row, col].Colour = black;
                    if (row >= 6)
                        playfield[row, col].Colour = white;
                    if (row <= 1 || row >= 6)
                        playfield[row, col].Exists = true;
                }
                for (int col = 0; col < 2; col++)
                {
                    whiteTaken[row, col] = new Piece();
                    blackTaken[row, col] = new Piece();
                }
            }
        }

        public string GetImageFile(Piece piece, int row, int col)
        {
            string fileName = "pack://application:,,,/Chess;component/Images/";
            if (piece.Exists)
            {
                fileName += piece.Colour;
                fileName += piece.Type;
            }
            if (row % 2 == col % 2)
                fileName += "Light.png";
            else
                fileName += "Dark.png";
            return fileName;
        }

        public string OtherColour(string colour)
        {
            string otherColour;
            if (colour == black)
                otherColour = white;
            else if (colour == white)
                otherColour = black;
            else
                otherColour = string.Empty;
            return otherColour;
        }

        public int[] FindKing(string colour, Piece[,] board)
        {
            int[] kingIndeces = new int[2];
            bool isKingFound = false;
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (board[row, col].Type == king && board[row, col].Colour == colour)
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
            if (rowOrCol >= 0 && rowOrCol <= 7)
                return true;
            else
                return false;
        }

        public bool IsInBounds(int row, int col)
        {
            if (row >= 0 && row <= 7 && col >= 0 && col <= 7)
                return true;
            else
                return false;
        }
    }
}
