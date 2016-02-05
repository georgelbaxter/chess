using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Piece
    {
        string colour = "empty";
        public string Colour
        {
            get { return colour; }
            set { colour = value; }
        }
        string type = "empty";
        public string Type
        {
            get { return type; }
            set { type = value; }
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

        public Piece(Piece piece)
        {
            Colour = piece.Colour;
            Type = piece.Type;
            Exists = piece.Exists;
            HasMoved = piece.HasMoved;
            EnPassant = piece.EnPassant;
            Row = piece.Row;
            Col = piece.Col;
        }

        public Piece()
        {
        }
    }
}
