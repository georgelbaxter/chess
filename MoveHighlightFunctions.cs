using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class MoveHighlightFunctions
    {
        UtilityFunctions uf = new UtilityFunctions();
        CheckFunctions cf = new CheckFunctions();
        const string pawn = "Pawn", rook = "Rook", knight = "Knight", bishop = "Bishop", queen = "Queen", king = "King", white = "White", black = "Black", empty = "empty";
        public void highlightMoveable(Piece piece, Piece[,] board, int row, int col, List<int[]> moveable)
        {
            switch (piece.Type)
            {
                case (pawn):
                    pawnMoveHighlight(piece, board, row, col, moveable);
                    break;
                case (rook):
                    rookMoveHighlight(piece.Colour, row, col, moveable, board);
                    break;
                case (knight):
                    knightMoveHighlight(piece.Colour, row, col, moveable, board);
                    break;
                case (bishop):
                    bishopMoveHighlight(piece.Colour, row, col, moveable, board);
                    break;
                case (queen):
                    queenMoveHighlight(piece.Colour, row, col, moveable, board);
                    break;
                case (king):
                    kingMoveHighlight(piece, row, col, moveable, board);
                    break;
            }
        }

        void pawnMoveHighlight(Piece piece, Piece[,] board, int row, int col, List<int[]> moveable)
        {
            //moving
            if (piece.Colour == black && !board[row + 1, col].Exists)
            {
                addMoveable(piece.Row, piece.Col, 1, 0, moveable, board);
                if (!piece.HasMoved && !board[piece.Row + 2, piece.Col].Exists)
                    addMoveable(piece.Row, piece.Col, 2, 0, moveable, board);
            }
            if (piece.Colour == white && !board[row - 1, col].Exists)
            {
                addMoveable(piece.Row, piece.Col, -1, 0, moveable, board);
                if (!piece.HasMoved && !board[piece.Row - 2, piece.Col].Exists)
                    addMoveable(piece.Row, piece.Col, -2, 0, moveable, board);
            }
        }

        void rookMoveHighlight(string colour, int row, int col, List<int[]> moveable, Piece[,] board)
        {
            bool canContinue = true;
            int i = 1;
            //check down
            while (canContinue)
            {
                addMoveable(row, col, i, 0, moveable, board);
                if (!uf.IsInBounds(row + i) || (uf.IsInBounds(row + i) && board[row + i, col].Exists))
                    canContinue = false;
                i++;
            }
            //check up
            i = 1;
            canContinue = true;
            while (canContinue)
            {
                addMoveable(row, col, -i, 0, moveable, board);
                if (!uf.IsInBounds(row - i) || (uf.IsInBounds(row - i) && board[row - i, col].Exists))
                    canContinue = false;
                i++;
            }
            //check right
            i = 1;
            canContinue = true;
            while (canContinue)
            {
                addMoveable(row, col, 0, i, moveable, board);
                if (!uf.IsInBounds(col + i) || (uf.IsInBounds(col + i) && board[row, col + i].Exists))
                    canContinue = false;
                i++;
            }
            //check left
            i = 1;
            canContinue = true;
            while (canContinue)
            {
                addMoveable(row, col, 0, -i, moveable, board);
                if (!uf.IsInBounds(col - i) || (uf.IsInBounds(col - i) && board[row, col - i].Exists))
                    canContinue = false;
                i++;
            }
        }

        void knightMoveHighlight(string colour, int row, int col, List<int[]> moveable, Piece[,] board)
        {
            //check down right
            addMoveable(row, col, 2, 1, moveable, board);
            //check right down
            addMoveable(row, col, 1, 2, moveable, board);
            //check right up
            addMoveable(row, col, -1, 2, moveable, board);
            //check up right
            addMoveable(row, col, -2, 1, moveable, board);
            //check up left
            addMoveable(row, col, -2, -1, moveable, board);
            //check left up
            addMoveable(row, col, -1, -2, moveable, board);
            //check left down
            addMoveable(row, col, 1, -2, moveable, board);
            //check down left
            addMoveable(row, col, 2, -1, moveable, board);
        }

        void bishopMoveHighlight(string colour, int row, int col, List<int[]> moveable, Piece[,] board)
        {
            int i = 1;
            bool canContinue = true;
            //check down right
            while (canContinue)
            {
                addMoveable(row, col, i, i, moveable, board);
                if (!uf.IsInBounds(row + i, col + i) || (uf.IsInBounds(row + i, col + i) && board[row + i, col + i].Exists))
                    canContinue = false;
                i++;
            }
            //check up right
            i = 1;
            canContinue = true;
            while (canContinue)
            {
                addMoveable(row, col, -i, i, moveable, board);
                if (!uf.IsInBounds(row - i, col + i) || (uf.IsInBounds(row - i, col + i) && board[row - i, col + i].Exists))
                    canContinue = false;
                i++;
            }
            //check up left
            i = 1;
            canContinue = true;
            while (canContinue)
            {
                addMoveable(row, col, -i, -i, moveable, board);
                if (!uf.IsInBounds(row - i, col - i) || (uf.IsInBounds(row - i, col - i) && board[row - i, col - i].Exists))
                    canContinue = false;
                i++;
            }
            //check down left
            i = 1;
            canContinue = true;
            while (canContinue)
            {
                addMoveable(row, col, i, -i, moveable, board);
                if (!uf.IsInBounds(row + i, col - i) || (uf.IsInBounds(row + i, col - i) && board[row + i, col - i].Exists))
                    canContinue = false;
                i++;
            }
        }

        void queenMoveHighlight(string colour, int row, int col, List<int[]> moveable, Piece[,] board)
        {
            //orthogonals
            //check down
            bool canContinue = true;
            int i = 1;
            //check down
            while (canContinue)
            {
                addMoveable(row, col, i, 0, moveable, board);
                if (!uf.IsInBounds(row + i) || (uf.IsInBounds(row + i) && board[row + i, col].Exists))
                    canContinue = false;
                i++;
            }
            //check up
            i = 1;
            canContinue = true;
            while (canContinue)
            {
                addMoveable(row, col, -i, 0, moveable, board);
                if (!uf.IsInBounds(row - i) || (uf.IsInBounds(row - i) && board[row - i, col].Exists))
                    canContinue = false;
                i++;
            }
            //check right
            i = 1;
            canContinue = true;
            while (canContinue)
            {
                addMoveable(row, col, 0, i, moveable, board);
                if (!uf.IsInBounds(col + i) || (uf.IsInBounds(col + i) && board[row, col + i].Exists))
                    canContinue = false;
                i++;
            }
            //check left
            i = 1;
            canContinue = true;
            while (canContinue)
            {
                addMoveable(row, col, 0, -i, moveable, board);
                if (!uf.IsInBounds(col - i) || (uf.IsInBounds(col - i) && board[row, col - i].Exists))
                    canContinue = false;
                i++;
            }
            //diagonals
            i = 1;
            canContinue = true;
            //check down right
            while (canContinue)
            {
                addMoveable(row, col, i, i, moveable, board);
                if (!uf.IsInBounds(row + i, col + i) || (uf.IsInBounds(row + i, col + i) && board[row + i, col + i].Exists))
                    canContinue = false;
                i++;
            }
            //check up right
            i = 1;
            canContinue = true;
            while (canContinue)
            {
                addMoveable(row, col, -i, i, moveable, board);
                if (!uf.IsInBounds(row - i, col + i) || (uf.IsInBounds(row - i, col + i) && board[row - i, col + i].Exists))
                    canContinue = false;
                i++;
            }
            //check up left
            i = 1;
            canContinue = true;
            while (canContinue)
            {
                addMoveable(row, col, -i, -i, moveable, board);
                if (!uf.IsInBounds(row - i, col - i) || (uf.IsInBounds(row - i, col - i) && board[row - i, col - i].Exists))
                    canContinue = false;
                i++;
            }
            //check down left
            i = 1;
            canContinue = true;
            while (canContinue)
            {
                addMoveable(row, col, i, -i, moveable, board);
                if (!uf.IsInBounds(row + i, col - i) || (uf.IsInBounds(row + i, col - i) && board[row + i, col - i].Exists))
                    canContinue = false;
                i++;
            }
        }

        void kingMoveHighlight(Piece piece, int row, int col, List<int[]> moveable, Piece[,] board)
        {
            //check down
            if (!cf.isThreatened(row + 1, col, uf.OtherColour(piece.Colour), board))
                addMoveable(row, col, 1, 0, moveable, board);
            //check down right
            if (!cf.isThreatened(row + 1, col + 1, uf.OtherColour(piece.Colour), board))
                addMoveable(row, col, 1, 1, moveable, board);
            //check right
            if (!cf.isThreatened(row, col + 1, uf.OtherColour(piece.Colour), board))
                addMoveable(row, col, 0, 1, moveable, board);
            //check up right
            if (!cf.isThreatened(row - 1, col + 1, uf.OtherColour(piece.Colour), board))
                addMoveable(row, col, -1, 1, moveable, board);
            //check up
            if (!cf.isThreatened(row - 1, col, uf.OtherColour(piece.Colour), board))
                addMoveable(row, col, -1, 0, moveable, board);
            //check up left
            if (!cf.isThreatened(row - 1, col - 1, uf.OtherColour(piece.Colour), board))
                addMoveable(row, col, -1, -1, moveable, board);
            //check left
            if (!cf.isThreatened(row, col - 1, uf.OtherColour(piece.Colour), board))
                addMoveable(row, col, 0, -1, moveable, board);
            //check down left
            if (!cf.isThreatened(row + 1, col - 1, uf.OtherColour(piece.Colour), board))
                addMoveable(row, col, 1, -1, moveable, board);
            #region castling
            //castling black left
            if (piece.Colour == black)
            {
                if (!piece.HasMoved && !board[0, 0].HasMoved && board[0, 0].Type == rook
                    && !board[0, 1].Exists && !board[0, 2].Exists && !board[0, 3].Exists
                    && !cf.isThreatened(0, 2, white, board) && !cf.isThreatened(0, 3, white, board) && !cf.isThreatened(0, 4, white, board))
                {
                    moveable.Add(new int[] { 0, 2 });
                }
                //castling black right
                if (!piece.HasMoved && !board[0, 7].HasMoved && board[0, 0].Type == rook
                    && !board[0, 5].Exists && !board[0, 6].Exists
                    && !cf.isThreatened(0, 4, white, board) && !cf.isThreatened(0, 5, white, board) && !cf.isThreatened(0, 6, white, board))
                {
                    moveable.Add(new int[] { 0, 6 });
                }
            }
            //castling white left
            if (piece.Colour == white)
            {
                if (!piece.HasMoved && !board[7, 0].HasMoved && board[0, 0].Type == rook
                    && !board[7, 1].Exists && !board[7, 2].Exists && !board[7, 3].Exists
                    && !cf.isThreatened(7, 2, black, board) && !cf.isThreatened(7, 3, black, board) && !cf.isThreatened(7, 4, black, board))
                {
                    moveable.Add(new int[] { 7, 2 });
                }
                //castling white right
                if (!piece.HasMoved && !board[7, 7].HasMoved && board[0, 0].Type == rook
                    && !board[7, 5].Exists && !board[7, 6].Exists
                    && !cf.isThreatened(7, 4, black, board) && !cf.isThreatened(7, 5, black, board) && !cf.isThreatened(7, 6, black, board))
                {
                    moveable.Add(new int[] { 7, 6 });
                }
            }
            #endregion
        }

        bool addMoveable(int row, int col, int rowOffset, int colOffset, List<int[]> moveable, Piece[,] board)
        {
            bool added = false;
            int destinationRow = row + rowOffset;
            int destinationCol = col + colOffset;
            if (uf.IsInBounds(destinationRow, destinationCol))
            {
                if (board[destinationRow, destinationCol].Colour != board[row, col].Colour && !cf.createCheck(row, col, destinationRow, destinationCol, board))
                {
                    moveable.Add(new int[] { destinationRow, destinationCol });
                    added = true;
                }
            }
            return added;
        }
    }
}
