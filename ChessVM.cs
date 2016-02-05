using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
/* Feb 5
 * re-added check display functionality
 * Feb 1
 * Began splitting ChessVM into different classes to separate different functions
 * done: UtilityFunctions, DisplayFunctions, MovementFunctions, ThreatenedHighlight, checkmate
 * todo: put check notification back in, filter king threatened highlight (seems to work)
 *  if (isThreatened(uf.FindKing(uf.OtherColour(selectedPiece.Colour), Playfield), selectedPiece.Colour, Playfield))
        DisplayText = "Check!";
    else
        DisplayText = string.Empty;
 * Jan 26
 * Changed order for threatened and movement highlighting to fix highlighting issues
 * added board parameter for findKing function
 * Chess seems functional now?
 * Jan 25
 * minor fix in castling
 * moved Checkmate() to checkCheck and from drawPieces to after movePiece() and added label for Checkmate notification
 * Checkmate() triggers if the only valid move is to take a piece. -> fixed
 * Bishop, rook, queen movement misbehaves with check if the piece would have to move more than one space to block
 * fixed, need to fix threatened highlighting and king movement on check not allowed
 * todo: notification on check (maybe done?)
 * Jan 22
 * adding pieces taken display
 * fixed stalemate triggering after checkmate
 * checkmate no longer triggers on just check
 * checkmate does not trigger reliably
 * Jan 21
 * finished check and checkmate
 * stalemate triggers on the next click
 * todo: stalemate, show taken pieces, highlight check
 * Jan 19
 * turn order is enforced
 * Check is becoming a mess. Should add a king locator function or keep track of kings
 * Jan 18
 * Kings can no longer take protected pieces, filtered added to highlightThreatened
 * added rook movement to castling, castling should be complete
 * enPassant now captures, should no longer persist
 * Pawn promotion added
 * todo: check, checkmate, enforce turn order, show taken pieces?
 * Jan 12
 * splitting highlightMove into highlightMove and highlightThreaten
 * castling checks correctly and moves king
 * still need en passant, and rook movement for castling
 * Jan 11
 * created addMoveable and addThreatened functions to simplify the highlightMoveable code
 * bishop, knight, rooks, pawns updated, still missing en passant
 * Jan 8
 * Lists are passes by reference, so just updating them will work
 * Jan 7
 * Changing moveable to output two lists, threatened and moveable
 * need to figure out the out parameter
 * Jan 5
 * started threatened check
 * isThreatened calls highlight which calls isThreatened etc.
 * Jan 2
 * finished rook movement function
 * added other movement functions
 * missing castling
 * need threatened check
 * Dec 18
 * Added switch case for movement options
 * added pawn movement highlight function
 * Missing En Passant
 * Dec 10
 * created chess using elements from checkers, primarily image drawing
 * selection now adds a border instead of changing the image
 */
namespace Chess
{
    class ChessVM
    {
        #region variables and other function classes
        bool WhiteTurn = true, gameDone = false;
        public Piece[,] Playfield = new Piece[8, 8];
        public Piece[,] WhiteTaken = new Piece[8, 2];
        public Piece[,] BlackTaken = new Piece[8, 2];
        Piece selectedPiece = new Piece();
        int[] selectedIndeces, whiteTakenIndeces = { 0, 0 }, blackTakenIndeces = { 0, 0 };
        const string pawn = "Pawn", rook = "Rook", knight = "Knight", bishop = "Bishop", queen = "Queen", king = "King", white = "White", black = "Black", empty = "empty";
        public List<int[]> Highlight = new List<int[]>();
        
        //function classes
        UtilityFunctions uf = new UtilityFunctions();
        DisplayFunctions df = DisplayFunctions.GetInstance();
        MovementFunctions mf = new MovementFunctions();
        MoveHighlightFunctions mhf = new MoveHighlightFunctions();
        ThreatenHighlightFunctions thf = new ThreatenHighlightFunctions();
        CheckFunctions cf = new CheckFunctions();
        CheckDisplayFunctions cdf = new CheckDisplayFunctions();
        #endregion
        public void InitializePlayfield()
        {
            uf.InitializePlayfield(ref WhiteTurn, ref gameDone, Highlight, Playfield, WhiteTaken, BlackTaken, ref whiteTakenIndeces, ref blackTakenIndeces);
        }

        public void SelectOrMove(int row, int col, List<int[]> moveable, List<int[]> threatened, List<int[]> enPassant, ref bool promote)
        {
            bool doSelect = true;
            //enforce turn order for move
            if ((WhiteTurn && selectedPiece.Colour == white) || (!WhiteTurn && selectedPiece.Colour == black))
                foreach (int[] pair in Highlight)
                    if (pair[0] == row && pair[1] == col)
                    {
                        move(row, col, ref promote, ref doSelect);
                        break;
                    }
            if (doSelect)
                select(row, col, moveable, threatened, enPassant);
        }

        void select(int row, int col, List<int[]> moveable, List<int[]> threatened, List<int[]> enPassant)
        {
            Highlight.Clear();
            {
                //enforce turn order for select
                if ((WhiteTurn && Playfield[row, col].Colour == white) || (!WhiteTurn && Playfield[row, col].Colour == black))
                {
                    selectedPiece = Playfield[row, col];
                    selectedIndeces = new int[2] { row, col };
                    mhf.highlightMoveable(Playfield[row, col], Playfield, row, col, moveable);
                    thf.highlightThreatened(Playfield[row, col], row, col, threatened, enPassant, Playfield);
                    foreach (int[] pair in moveable)
                        if (!cf.createCheck(row, col, pair[0], pair[1], Playfield))
                            Highlight.AddRange(moveable);
                    foreach (int[] pair in threatened)
                        if (Playfield[pair[0], pair[1]].Exists && !cf.createCheck(row, col, pair[0], pair[1], Playfield))
                            Highlight.Add(pair);
                    foreach (int[] pair in enPassant)
                        if (!cf.createCheck(row, col, pair[0], pair[1], Playfield))
                            Highlight.AddRange(enPassant);
                }
            }
        }

        void move(int row, int col, ref bool promote, ref bool doSelect)
        {
            Highlight.Clear();
            if (Playfield[selectedIndeces[0], selectedIndeces[1]].Type == king && Math.Abs(selectedIndeces[1] - col) == 2)
                mf.castle(col, Playfield, selectedIndeces);
            if (Playfield[selectedIndeces[0], selectedIndeces[1]].Type == pawn && (Playfield[3, col].EnPassant || Playfield[4, col].EnPassant))
                mf.performEnPassant(row, col, Playfield);
            //castling and En Passant can't create an opportunity for en passant, so we can remove all the en passant flags here
            for (int rows = 0; rows < 8; rows++)
            {
                for (int cols = 0; cols < 8; cols++)
                    Playfield[rows, cols].EnPassant = false;
            }
            //move piece happens after the en passant flag clearing because it can raise en passant flags
            mf.movePiece(selectedPiece, Playfield, WhiteTaken, BlackTaken, whiteTakenIndeces, blackTakenIndeces, selectedIndeces, row, col);
            cdf.checkForCheck(Playfield, selectedPiece.Colour);
            cdf.checkForCheckmate(Playfield, selectedPiece.Colour, gameDone);
            if (selectedPiece.Type == pawn && (row == 0 || row == 7))
                promote = true;
            //switch turns
            WhiteTurn = !WhiteTurn;
            doSelect = false;
        }
    }
}
