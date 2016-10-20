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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ChessVM cvm;
        UtilityFunctions uf;
        MovementFunctions mf;
        DisplayFunctions df = DisplayFunctions.GetInstance();
        public MainWindow()
        {
            cvm = new ChessVM();
            uf = new UtilityFunctions();
            mf = new MovementFunctions();
            DataContext = df;
            cvm.InitializePlayfield();
            InitializeComponent();
            drawPieces();
        }

        void drawPieces()
        {
            //draw main board
            for (int row = 0; row < Board.Rows; row++)
                for (int col = 0; col < Board.Cols; col++)
                {
                    Image tmpImg = new Image();
                    tmpImg.Source = new BitmapImage(new Uri(uf.GetImageFile(cvm.Playfield[row, col], row, col)));
                    Grid.SetColumn(tmpImg, col);
                    Grid.SetRow(tmpImg, row);
                    gdPlayfield.Children.Add(tmpImg);
                    tmpImg.MouseDown += MouseDownHandler;
                }
            //draw white taken pieces
            for (int row = 0; row < 8; row++)
                for (int col = 0; col < 2; col++)
                {
                    Image tmpImg = new Image();
                    tmpImg.Source = new BitmapImage(new Uri(uf.GetImageFile(cvm.WhiteTaken[row, col], row, col)));
                    Grid.SetColumn(tmpImg, col);
                    Grid.SetRow(tmpImg, row + 1);
                    gdWhitePieces.Children.Add(tmpImg);
                }
            //draw black taken pieces
            for (int row = 0; row < 8; row++)
                for (int col = 0; col < 2; col++)
                {
                    Image tmpImg = new Image();
                    tmpImg.Source = new BitmapImage(new Uri(uf.GetImageFile(cvm.BlackTaken[row, col], row, col)));
                    Grid.SetColumn(tmpImg, col);
                    Grid.SetRow(tmpImg, row + 1);
                    gdBlackPieces.Children.Add(tmpImg);
                }
            lbDisplayText.Content = df.DisplayText;
        }

        void drawBorder(int row, int col, string color)
        {
            {
                Border border = new Border();
                switch (color)
                {
                    case ("yellow"):
                        border.BorderBrush = Brushes.Yellow;
                        break;
                    case ("red"):
                        border.BorderBrush = Brushes.Red;
                        break;
                }
                border.BorderThickness = new Thickness(3);
                Grid.SetRow(border, row);
                Grid.SetColumn(border, col);
                gdPlayfield.Children.Add(border);
            }
        }

        private void MouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            int col = Grid.GetColumn((Image)sender);
            int row = Grid.GetRow((Image)sender);
            List<int[]> moveable = new List<int[]>();
            List<int[]> threatened = new List<int[]>();
            List<int[]> enPassant = new List<int[]>();
            bool promote = false;
            cvm.SelectOrMove(row, col, moveable, threatened, enPassant, ref promote);
            if (promote)
            {
                Piece.Type promoteTo = new Piece.Type();
                while (promoteTo == Piece.Type.Empty)
                {
                    Promotion promo = new Promotion(row, ref promoteTo);
                    promo.ShowDialog();
                    promoteTo = promo.PromoteTo();
                }
                mf.Promote(promoteTo, row, col, cvm.Playfield);
            }
            drawPieces();
            //draw borders for moveable squares

            foreach (int[] pair in moveable)
                drawBorder(pair[0], pair[1], "yellow");
            foreach (int[] pair in threatened)
                //only show red if there's a piece to take
                if (cvm.Playfield[pair[0], pair[1]].Exists && cvm.Highlight.Contains(pair))
                    drawBorder(pair[0], pair[1], "red");
            foreach (int[] pair in enPassant)
                drawBorder(pair[0], pair[1], "red");
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            cvm.InitializePlayfield();
            drawPieces();
        }
    }
}
