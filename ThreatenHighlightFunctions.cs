using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class ThreatenHighlightFunctions
    {
        UtilityFunctions uf = new UtilityFunctions();
        const string pawn = "Pawn", rook = "Rook", knight = "Knight", bishop = "Bishop", queen = "Queen", king = "King", white = "White", black = "Black", empty = "empty";

        public void highlightThreatened(Piece piece, int row, int col, List<int[]> threatened, List<int[]> enPassant, Piece[,] board, bool filtered = true)
        {
            switch (piece.Type)
            {
                case (pawn):
                    pawnThreatenHighlight(piece, board, row, col, threatened, enPassant);
                    break;
                case (rook):
                    rookThreatenHighlight(piece.Colour, row, col, threatened, board);
                    break;
                case (knight):
                    knightThreatenHighlight(piece.Colour, row, col, threatened);
                    break;
                case (bishop):
                    bishopThreatenHighlight(piece.Colour, row, col, threatened, board);
                    break;
                case (queen):
                    queenThreatenHighlight(piece.Colour, row, col, threatened, board);
                    break;
                case (king):
                    kingThreatenedHighlight(piece, row, col, threatened);
                    break;
            }
            //filtered list only squares that can be moved to, ie. not pieces of the same colour, (need to add) or threatened pieces of the other colour for kings
            if (filtered)
            {
                List<int[]> toRemove = new List<int[]>();
                foreach (int[] pair in threatened)
                    if (board[row, col].Colour == board[pair[0], pair[1]].Colour)
                        toRemove.Add(pair);
                //if (piece.Type == king)
                //    foreach (int[] pair in threatened)
                //        if (isThreatened(pair[0], pair[1], uf.OtherColour(piece.Colour), Playfield))
                //            toRemove.Add(pair);
                foreach (int[] pair in toRemove)
                    threatened.Remove(pair);
            }
        }

        void pawnThreatenHighlight(Piece piece, Piece[,] board, int row, int col, List<int[]> threatened, List<int[]> enPassant)
        {
            //capturing
            if (piece.Colour == black)
            {
                addThreatened(piece.Colour, piece.Row, piece.Col, 1, 1, threatened);
                addThreatened(piece.Colour, piece.Row, piece.Col, 1, -1, threatened);
            }
            if (piece.Colour == white)
            {
                addThreatened(piece.Colour, piece.Row, piece.Col, -1, 1, threatened);
                addThreatened(piece.Colour, piece.Row, piece.Col, -1, -1, threatened);
            }
            //en passant
            if (piece.Colour == black)
            {
                addEnPassant(piece.Colour, piece.Row, piece.Col, 1, 1, enPassant, board);
                addEnPassant(piece.Colour, piece.Row, piece.Col, 1, -1, enPassant, board);
            }
            if (piece.Colour == white)
            {
                addEnPassant(piece.Colour, piece.Row, piece.Col, -1, 1, enPassant, board);
                addEnPassant(piece.Colour, piece.Row, piece.Col, -1, -1, enPassant, board);
            }
        }

        void rookThreatenHighlight(string colour, int row, int col, List<int[]> threatened, Piece[,] board)
        {
            int i = 1;
            //check down
            while (addThreatened(colour, row, col, i, 0, threatened))
            {
                if (board[row + i, col].Exists)
                    break;
                i++;
            }
            //check up
            i = 1;
            while (addThreatened(colour, row, col, -i, 0, threatened))
            {
                if (board[row - i, col].Exists)
                    break;
                i++;
            }
            //check right
            i = 1;
            while (addThreatened(colour, row, col, 0, i, threatened))
            {
                if (board[row, col + i].Exists)
                    break;
                i++;
            }
            //check left
            i = 1;
            while (addThreatened(colour, row, col, 0, -i, threatened))
            {
                if (board[row, col - i].Exists)
                    break;
                i++;
            }
        }

        void knightThreatenHighlight(string colour, int row, int col, List<int[]> threatened)
        {
            //check down right
            addThreatened(colour, row, col, 2, 1, threatened);
            //check right down
            addThreatened(colour, row, col, 1, 2, threatened);
            //check right up
            addThreatened(colour, row, col, -1, 2, threatened);
            //check up right
            addThreatened(colour, row, col, -2, 1, threatened);
            //check up left
            addThreatened(colour, row, col, -2, -1, threatened);
            //check left up
            addThreatened(colour, row, col, -1, -2, threatened);
            //check left down
            addThreatened(colour, row, col, 1, -2, threatened);
            //check down left
            addThreatened(colour, row, col, 2, -1, threatened);
        }

        void bishopThreatenHighlight(string colour, int row, int col, List<int[]> threatened, Piece[,] board)
        {
            int i = 1;
            //check down right
            while (addThreatened(colour, row, col, i, i, threatened))
            {
                if (board[row + i, col + i].Exists)
                    break;
                i++;
            }
            //check up right
            i = 1;
            while (addThreatened(colour, row, col, -i, i, threatened))
            {
                if (board[row - i, col + i].Exists)
                    break;
                i++;
            }
            //check up left
            i = 1;
            while (addThreatened(colour, row, col, -i, -i, threatened))
            {
                if (board[row - i, col - i].Exists)
                    break;
                i++;
            }
            //check down left
            i = 1;
            while (addThreatened(colour, row, col, i, -i, threatened))
            {
                if (board[row + i, col - i].Exists)
                    break;
                i++;
            }
        }

        void queenThreatenHighlight(string colour, int row, int col, List<int[]> threatened, Piece[,] board)
        {
            int i = 1;
            //check down
            while (addThreatened(colour, row, col, i, 0, threatened))
            {
                if (board[row + i, col].Exists)
                    break;
                i++;
            }
            //check up
            i = 1;
            while (addThreatened(colour, row, col, -i, 0, threatened))
            {
                if (board[row - i, col].Exists)
                    break;
                i++;
            }
            //check right
            i = 1;
            while (addThreatened(colour, row, col, 0, i, threatened))
            {
                if (board[row, col + i].Exists)
                    break;
                i++;
            }
            //check left
            i = 1;
            while (addThreatened(colour, row, col, 0, -i, threatened))
            {
                if (board[row, col - i].Exists)
                    break;
                i++;
            }
            i = 1;
            //check down right
            while (addThreatened(colour, row, col, i, i, threatened))
            {
                if (board[row + i, col + i].Exists)
                    break;
                i++;
            }
            //check up right
            i = 1;
            while (addThreatened(colour, row, col, -i, i, threatened))
            {
                if (board[row - i, col + i].Exists)
                    break;
                i++;
            }
            //check up left
            i = 1;
            while (addThreatened(colour, row, col, -i, -i, threatened))
            {
                if (board[row - i, col - i].Exists)
                    break;
                i++;
            }
            //check down left
            i = 1;
            while (addThreatened(colour, row, col, i, -i, threatened))
            {
                if (board[row + i, col - i].Exists)
                    break;
                i++;
            }
        }

        void kingThreatenedHighlight(Piece piece, int row, int col, List<int[]> threatened)
        {
            addThreatened(piece.Colour, row, col, 1, 0, threatened);
            addThreatened(piece.Colour, row, col, 1, 1, threatened);
            addThreatened(piece.Colour, row, col, 0, 1, threatened);
            addThreatened(piece.Colour, row, col, -1, 1, threatened);
            addThreatened(piece.Colour, row, col, -1, 0, threatened);
            addThreatened(piece.Colour, row, col, -1, -1, threatened);
            addThreatened(piece.Colour, row, col, 0, -1, threatened);
            addThreatened(piece.Colour, row, col, 1, -1, threatened);
        }

        bool addThreatened(string colour, int row, int col, int rowOffset, int colOffset, List<int[]> threatened)
        {
            bool added = false;
            int destinationRow = row + rowOffset;
            int destinationCol = col + colOffset;
            if (uf.IsInBounds(destinationRow, destinationCol))
            {
                threatened.Add(new int[] { destinationRow, destinationCol });
                added = true;
            }
            return added;
        }

        void addEnPassant(string colour, int row, int col, int rowOffset, int colOffset, List<int[]> enPassant, Piece[,] board)
        {
            int destinationRow = row + rowOffset;
            int destinationCol = col + colOffset;
            if (destinationRow >= 0 && destinationRow <= 7 && destinationCol >= 0 && destinationCol <= 7 && board[row, col + colOffset].EnPassant)
            {
                enPassant.Add(new int[] { destinationRow, destinationCol });
            }
        }
    }
}
