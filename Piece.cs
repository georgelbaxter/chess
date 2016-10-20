using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Piece
    {
        Colour pieceColour = Colour.Empty;
        public Colour PieceColour
        {
            get { return pieceColour; }
            set { pieceColour = value; }
        }
        Type Piecetype = Type.Empty;
        public Type PieceType
        {
            get { return Piecetype; }
            set { Piecetype = value; }
        }
        bool exists = false;
        public bool Exists
        {
            get { return exists; }
            set { exists = value; }
        }
        bool hasMoved = false;
        public bool HasMoved
        {
            get { return hasMoved; }
            set { hasMoved = value; }
        }
        bool enPassant = false;
        public bool EnPassant
        {
            get { return enPassant; }
            set { enPassant = value; }
        }
        int row;
        public int Row
        {
            get { return row; }
            set { row = value; }
        }
        int col;
        public int Col
        {
            get { return col; }
            set { col = value; }
        }

        public enum Colour
        {
            Empty,
            Black,
            White
        }

        public enum Type
        {
            Empty,
            Pawn,
            Rook,
            Knight,
            Bishop,
            Queen,
            King
        }

        public Piece(Piece piece)
        {
            PieceColour = piece.PieceColour;
            PieceType = piece.PieceType;
            Exists = piece.Exists;
            HasMoved = piece.HasMoved;
            EnPassant = piece.EnPassant;
            Row = piece.Row;
            Col = piece.Col;
        }

        public Piece()
        { }
    }
}
