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

        public void highlightMoveable(Piece piece, Piece[,] board, int row, int col, List<int[]> moveable)
        {
            switch (piece.PieceType)
            {
                case (Piece.Type.Pawn):
                    pawnMoveHighlight(piece, row, col, moveable, board);
                    break;
                case (Piece.Type.Rook):
                    rookMoveHighlight(piece.PieceColour, row, col, moveable, board);
                    break;
                case (Piece.Type.Knight):
                    knightMoveHighlight(piece.PieceColour, row, col, moveable, board);
                    break;
                case (Piece.Type.Bishop):
                    bishopMoveHighlight(piece.PieceColour, row, col, moveable, board);
                    break;
                case (Piece.Type.Queen):
                    queenMoveHighlight(piece.PieceColour, row, col, moveable, board);
                    break;
                case (Piece.Type.King):
                    kingMoveHighlight(piece, row, col, moveable, board);
                    break;
            }
        }

        void pawnMoveHighlight(Piece piece, int row, int col, List<int[]> moveable, Piece[,] board)
        {
            //moving
            if (piece.PieceColour == Piece.Colour.Black && !board[row + 1, col].Exists)
            {
                addMoveable(piece.Row, piece.Col, 1, 0, moveable, board);
                if (!piece.HasMoved && !board[piece.Row + 2, piece.Col].Exists)
                    addMoveable(piece.Row, piece.Col, 2, 0, moveable, board);
            }
            if (piece.PieceColour == Piece.Colour.White && !board[row - 1, col].Exists)
            {
                addMoveable(piece.Row, piece.Col, -1, 0, moveable, board);
                if (!piece.HasMoved && !board[piece.Row - 2, piece.Col].Exists)
                    addMoveable(piece.Row, piece.Col, -2, 0, moveable, board);
            }
        }

        void rookMoveHighlight(Piece.Colour colour, int row, int col, List<int[]> moveable, Piece[,] board)
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

        void knightMoveHighlight(Piece.Colour colour, int row, int col, List<int[]> moveable, Piece[,] board)
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

        void bishopMoveHighlight(Piece.Colour colour, int row, int col, List<int[]> moveable, Piece[,] board)
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

        void queenMoveHighlight(Piece.Colour colour, int row, int col, List<int[]> moveable, Piece[,] board)
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
            if (!cf.isThreatened(row + 1, col, uf.OtherColour(piece.PieceColour), board))
                addMoveable(row, col, 1, 0, moveable, board);
            //check down right
            if (!cf.isThreatened(row + 1, col + 1, uf.OtherColour(piece.PieceColour), board))
                addMoveable(row, col, 1, 1, moveable, board);
            //check right
            if (!cf.isThreatened(row, col + 1, uf.OtherColour(piece.PieceColour), board))
                addMoveable(row, col, 0, 1, moveable, board);
            //check up right
            if (!cf.isThreatened(row - 1, col + 1, uf.OtherColour(piece.PieceColour), board))
                addMoveable(row, col, -1, 1, moveable, board);
            //check up
            if (!cf.isThreatened(row - 1, col, uf.OtherColour(piece.PieceColour), board))
                addMoveable(row, col, -1, 0, moveable, board);
            //check up left
            if (!cf.isThreatened(row - 1, col - 1, uf.OtherColour(piece.PieceColour), board))
                addMoveable(row, col, -1, -1, moveable, board);
            //check left
            if (!cf.isThreatened(row, col - 1, uf.OtherColour(piece.PieceColour), board))
                addMoveable(row, col, 0, -1, moveable, board);
            //check down left
            if (!cf.isThreatened(row + 1, col - 1, uf.OtherColour(piece.PieceColour), board))
                addMoveable(row, col, 1, -1, moveable, board);
            #region castling
            //castling black left
            if (piece.PieceColour == Piece.Colour.Black)
            {
                //Check that the king hasn't moved
                if (!piece.HasMoved 
                    //and that the left rook hasn't moved
                    && !board[Board.FirstRow, Board.FirstCol].HasMoved 
                    //and that it is a rook
                    && board[Board.FirstRow, Board.FirstCol].PieceType == Piece.Type.Rook
                    //and that there are no pieces between them
                    && !board[Board.FirstRow, Board.FirstCol + 1].Exists 
                    && !board[Board.FirstRow, Board.FirstCol + 2].Exists 
                    && !board[Board.FirstRow, Board.FirstCol + 3].Exists
                    //and that king doesn't move through tiles that are threatened by white
                    && !cf.isThreatened(Board.FirstRow, Board.FirstCol + 2, Piece.Colour.White, board) 
                    && !cf.isThreatened(Board.FirstRow, Board.FirstCol + 3, Piece.Colour.White, board) 
                    && !cf.isThreatened(Board.FirstRow, Board.FirstCol + 4, Piece.Colour.White, board))
                {
                    moveable.Add(new int[] { Board.FirstRow, Board.FirstCol + 2 });
                }
                //castling black right
                if (!piece.HasMoved 
                    && !board[Board.FirstRow, Board.LastCol].HasMoved 
                    && board[Board.FirstRow, Board.LastCol].PieceType == Piece.Type.Rook
                    && !board[Board.FirstRow, Board.LastCol - 1].Exists 
                    && !board[Board.FirstRow, Board.LastCol - 2].Exists
                    && !cf.isThreatened(Board.FirstRow, Board.LastCol - 3, Piece.Colour.White, board) 
                    && !cf.isThreatened(Board.FirstRow, Board.LastCol - 2, Piece.Colour.White, board) 
                    && !cf.isThreatened(Board.FirstRow, Board.LastCol - 1, Piece.Colour.White, board))
                {
                    moveable.Add(new int[] { Board.FirstRow, Board.LastCol - 1 });
                }
            }
            //castling white left
            if (piece.PieceColour == Piece.Colour.White)
            {
                if (!piece.HasMoved 
                    && !board[Board.LastRow, Board.FirstCol].HasMoved 
                    && board[Board.LastRow, Board.FirstCol].PieceType == Piece.Type.Rook
                    && !board[Board.LastRow, Board.FirstCol + 1].Exists 
                    && !board[Board.LastRow, Board.FirstCol + 2].Exists 
                    && !board[Board.LastRow, Board.FirstCol + 3].Exists
                    && !cf.isThreatened(Board.LastRow, Board.FirstCol + 2, Piece.Colour.Black, board) 
                    && !cf.isThreatened(Board.LastRow, Board.FirstCol + 3, Piece.Colour.Black, board) 
                    && !cf.isThreatened(Board.LastRow, Board.FirstCol + 4, Piece.Colour.Black, board))
                {
                    moveable.Add(new int[] { Board.LastRow, Board.FirstCol + 2 });
                }
                //castling white right
                if (!piece.HasMoved 
                    && !board[Board.LastRow, Board.LastCol].HasMoved 
                    && board[Board.LastRow, Board.LastCol].PieceType == Piece.Type.Rook
                    && !board[Board.LastRow, Board.LastCol - 2].Exists 
                    && !board[Board.LastRow, Board.LastCol - 1].Exists
                    && !cf.isThreatened(Board.LastRow, Board.LastCol - 3, Piece.Colour.Black, board) 
                    && !cf.isThreatened(Board.LastRow, Board.LastCol - 2, Piece.Colour.Black, board) 
                    && !cf.isThreatened(Board.LastRow, Board.LastCol - 1, Piece.Colour.Black, board))
                {
                    moveable.Add(new int[] { Board.LastRow, Board.LastCol - 1 });
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
                if (board[destinationRow, destinationCol].PieceColour != board[row, col].PieceColour 
                    && !cf.createCheck(row, col, destinationRow, destinationCol, board))
                {
                    moveable.Add(new int[] { destinationRow, destinationCol });
                    added = true;
                }
            }
            return added;
        }
    }
}
