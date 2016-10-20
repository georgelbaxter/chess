using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
//deals with text to say check, checkmate <colour>, and stalemate
//and deals with displaying images of the taken pieces on the side
namespace Chess
{
    class DisplayFunctions : INotifyPropertyChanged
    {
        static DisplayFunctions displayFunctions;

        //Constructors
        private DisplayFunctions()
        {
            DisplayText = "Welcome";
        }

        public static DisplayFunctions GetInstance()
        {
            if(displayFunctions == null)
                displayFunctions = new DisplayFunctions();
            return displayFunctions;
        }

        //Property
        string displayText;
        public string DisplayText
        {
            get { return displayText; }
            set { displayText = value; OnPropertyChanged(); }
        }

        //Methods
        public void AddTaken(Piece piece, Piece[,] whiteTaken, Piece[,] blackTaken, int[] whiteTakenIndeces, int[] blackTakenIndeces)
        {
            if (piece.PieceColour == Piece.Colour.White)
            {
                addTaken(piece, whiteTaken, whiteTakenIndeces);
            }
            if (piece.PieceColour == Piece.Colour.Black)
            {
                addTaken(piece, blackTaken, blackTakenIndeces);
            }
        }

        void addTaken(Piece piece, Piece[,] takenGrid, int[] takenIndeces)
        {
            takenGrid[takenIndeces[0], takenIndeces[1]] = piece;
            takenIndeces[1]++;
            takenIndeces[0] += takenIndeces[1] / 2;
            takenIndeces[1] %= 2;
        }

        public void Check()
        {
            DisplayText = "Check!";
        }

        public void Checkmate(Piece.Colour colour)
        {
            DisplayText = "Checkmate " + colour + "!";
        }

        public void Stalemate()
        {
            DisplayText = "Stalemate.";
        }

        public void Clear()
        {
            DisplayText = string.Empty;
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
