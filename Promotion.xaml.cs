using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Chess
{
    /// <summary>
    /// Interaction logic for Promotion.xaml
    /// </summary>
    public partial class Promotion : Window
    {
        ChessVM cvm;
        Piece.Type promoteTo;
        public Promotion(int row, ref Piece.Type promoteTo)
        {
            cvm = new ChessVM();
            InitializeComponent();
            if (row == 0)
            {
                imgWhiteBishop.Visibility = Visibility.Visible;
                imgWhiteKnight.Visibility = Visibility.Visible;
                imgWhiteQueen.Visibility = Visibility.Visible;
                imgWhiteRook.Visibility = Visibility.Visible;
            }
        }

        public Piece.Type PromoteTo()
        {
            return promoteTo;
        }

        private void imgRook_MouseDown(object sender, MouseButtonEventArgs e)
        {
            promoteTo = Piece.Type.Rook;
            this.Close();
        }

        private void imgKnight_MouseDown(object sender, MouseButtonEventArgs e)
        {
            promoteTo = Piece.Type.Knight;
            this.Close();
        }

        private void imgBishop_MouseDown(object sender, MouseButtonEventArgs e)
        {
            promoteTo = Piece.Type.Bishop;
            this.Close();
        }

        private void imgQueen_MouseDown(object sender, MouseButtonEventArgs e)
        {
            promoteTo = Piece.Type.Queen;
            this.Close();
        }
    }
}
