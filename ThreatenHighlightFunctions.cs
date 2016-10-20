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

        public void highlightThreatened(Piece piece, int row, int col, List<int[]> threatened, List<int[]> enPassant, Piece[,] board, bool filtered = true)
        {
            switch (piece.PieceType)
            {
                case (Piece.Type.Pawn):
                    pawnThreatenHighlight(piece, row, col, threatened, board, enPassant);
                    break;
                case (Piece.Type.Rook):
                    rookThreatenHighlight(piece.PieceColour, row, col, threatened, board);
                    break;
                case (Piece.Type.Knight):
                    knightThreatenHighlight(piece.PieceColour, row, col, threatened);
                    break;
                case (Piece.Type.Bishop):
                    bishopThreatenHighlight(piece.PieceColour, row, col, threatened, board);
                    break;
                case (Piece.Type.Queen):
                    queenThreatenHighlight(piece.PieceColour, row, col, threatened, board);
                    break;
                case (Piece.Type.King):
                    kingThreatenedHighlight(piece, row, col, threatened);
                    break;
                default:
                    throw new NotImplementedException("Attempted to move a non existent piece type");
            }
            //filtered list only squares that can be moved to, ie. not pieces of the same colour, (need to add) or threatened pieces of the other colour for kings
            if (filtered)
            {
                List<int[]> toRemove = new List<int[]>();
                foreach (int[] pair in threatened)
                    if (board[row, col].PieceColour == board[pair[0], pair[1]].PieceColour)
                        toRemove.Add(pair);
                //if (piece.Type == king)
                //    foreach (int[] pair in threatened)
                //        if (isThreatened(pair[0], pair[1], uf.OtherColour(piece.Colour), Playfield))
                //            toRemove.Add(pair);
                foreach (int[] pair in toRemove)
                    threatened.Remove(pair);
            }
        }

        void pawnThreatenHighlight(Piece piece, int row, int col, List<int[]> threatened, Piece[,] board, List<int[]> enPassant)
        {
            //capturing
            if (piece.PieceColour == Piece.Colour.Black)
            {
                addThreatened(piece.PieceColour, piece.Row, piece.Col, 1, 1, threatened);
                addThreatened(piece.PieceColour, piece.Row, piece.Col, 1, -1, threatened);
            }
            if (piece.PieceColour == Piece.Colour.White)
            {
                addThreatened(piece.PieceColour, piece.Row, piece.Col, -1, 1, threatened);
                addThreatened(piece.PieceColour, piece.Row, piece.Col, -1, -1, threatened);
            }
            //en passant
            if (piece.PieceColour == Piece.Colour.Black)
            {
                addEnPassant(piece.PieceColour, piece.Row, piece.Col, 1, 1, enPassant, board);
                addEnPassant(piece.PieceColour, piece.Row, piece.Col, 1, -1, enPassant, board);
            }
            if (piece.PieceColour == Piece.Colour.White)
            {
                addEnPassant(piece.PieceColour, piece.Row, piece.Col, -1, 1, enPassant, board);
                addEnPassant(piece.PieceColour, piece.Row, piece.Col, -1, -1, enPassant, board);
            }
        }

        void rookThreatenHighlight(Piece.Colour colour, int row, int col, List<int[]> threatened, Piece[,] board)
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

        void knightThreatenHighlight(Piece.Colour colour, int row, int col, List<int[]> threatened)
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

        void bishopThreatenHighlight(Piece.Colour colour, int row, int col, List<int[]> threatened, Piece[,] board)
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

        void queenThreatenHighlight(Piece.Colour colour, int row, int col, List<int[]> threatened, Piece[,] board)
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
            addThreatened(piece.PieceColour, row, col, 1, 0, threatened);
            addThreatened(piece.PieceColour, row, col, 1, 1, threatened);
            addThreatened(piece.PieceColour, row, col, 0, 1, threatened);
            addThreatened(piece.PieceColour, row, col, -1, 1, threatened);
            addThreatened(piece.PieceColour, row, col, -1, 0, threatened);
            addThreatened(piece.PieceColour, row, col, -1, -1, threatened);
            addThreatened(piece.PieceColour, row, col, 0, -1, threatened);
            addThreatened(piece.PieceColour, row, col, 1, -1, threatened);
        }

        bool addThreatened(Piece.Colour colour, int row, int col, int rowOffset, int colOffset, List<int[]> threatened)
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

        void addEnPassant(Piece.Colour colour, int row, int col, int rowOffset, int colOffset, List<int[]> enPassant, Piece[,] board)
        {
            int destinationRow = row + rowOffset;
            int destinationCol = col + colOffset;
            if (destinationRow >= Board.FirstRow && destinationRow <= Board.LastRow && destinationCol >= Board.FirstCol 
                && destinationCol <= Board.LastCol && board[row, col + colOffset].EnPassant)
            {
                enPassant.Add(new int[] { destinationRow, destinationCol });
            }
        }
    }
}
