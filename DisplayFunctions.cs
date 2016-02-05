using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
//deals with text to say check, checkmate {colour}, and stalemate
//also deals with displaying images of taken pieces on the side
namespace Chess
{
    class DisplayFunctions : INotifyPropertyChanged
    {
        private DisplayFunctions()
        {
            DisplayText = "Welcome";
        }
        static DisplayFunctions displayFunctions;
        public static DisplayFunctions GetInstance()
        {
            if(displayFunctions == null)
                displayFunctions = new DisplayFunctions();
            return displayFunctions;
        }

        string displayText;
        public string DisplayText
        {
            get { return displayText; }
            set { displayText = value; OnPropertyChanged(); }
        }
        string white = "White", black = "Black";
        public void AddTaken(Piece piece, Piece[,] whiteTaken, Piece[,] blackTaken, int[] whiteTakenIndeces, int[] blackTakenIndeces)
        {
            if (piece.Colour == white)
            {
                whiteTaken[whiteTakenIndeces[0], whiteTakenIndeces[1]] = piece;
                whiteTakenIndeces[1]++;
                whiteTakenIndeces[0] += whiteTakenIndeces[1] / 2;
                whiteTakenIndeces[1] %= 2;
            }
            if (piece.Colour == black)
            {
                blackTaken[blackTakenIndeces[0], blackTakenIndeces[1]] = piece;
                blackTakenIndeces[1]++;
                blackTakenIndeces[0] += blackTakenIndeces[1] / 2;
                blackTakenIndeces[1] %= 2;
            }
        }

        public void Check()
        {
            DisplayText = "Check!";
        }

        public void Checkmate(string colour)
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
