using System;
using System.Collections.Generic;
using System.Text;

namespace OsvaldoChessMaster
{
    public class Board
    {
        public bool turn = true; //turn=true es el turno del player1
        private const int size = 8;        
        private Piece[,] ChessBoard;
        
        public Board(bool player1)
        {
            ChessBoard = new Piece[size,size];

            Piece pawn1 = new Pawn(player1); //creamos todas las Pieces segun posicion
            Piece pawn2 = new Pawn(player1);
            Piece pawn8 = new Pawn(player1);
            Piece pawn3 = new Pawn(player1);
            Piece pawn4 = new Pawn(player1);
            Piece pawn5 = new Pawn(player1);
            Piece pawn6 = new Pawn(player1);
            Piece pawn7 = new Pawn(player1);

            Piece pawn9 = new Pawn(!player1);
            Piece pawn10 = new Pawn(!player1);
            Piece pawn11 = new Pawn(!player1);
            Piece pawn12 = new Pawn(!player1);
            Piece pawn13 = new Pawn(!player1);
            Piece pawn14 = new Pawn(!player1);
            Piece pawn15 = new Pawn(!player1);
            Piece pawn16 = new Pawn(!player1);
            
            ChessBoard[0, 1] = pawn1;
            ChessBoard[1, 1] = pawn2;
            ChessBoard[2, 1] = pawn3;
            ChessBoard[3, 1] = pawn4;
            ChessBoard[4, 1] = pawn5;
            ChessBoard[5, 1] = pawn6;
            ChessBoard[6, 1] = pawn7;
            ChessBoard[7, 1] = pawn8;

            ChessBoard[0, 6] = pawn9;
            ChessBoard[1, 6] = pawn10;
            ChessBoard[2, 6] = pawn11;
            ChessBoard[3, 6] = pawn12;
            ChessBoard[4, 6] = pawn13;
            ChessBoard[5, 6] = pawn14;
            ChessBoard[6, 6] = pawn15;
            ChessBoard[7, 6] = pawn16;

            Piece rook1 = new Rook(player1);
            Piece rook2 = new Rook(player1);
            Piece rook3 = new Rook(!player1);
            Piece rook4 = new Rook(!player1);
            ChessBoard[0, 0] = rook1;
            ChessBoard[7, 0] = rook2;
            ChessBoard[0, 7] = rook3;
            ChessBoard[7, 7] = rook4;

            Piece horse1 = new Horse(player1);
            Piece horse2 = new Horse(player1);
            Piece horse3 = new Horse(!player1);
            Piece horse4 = new Horse(!player1);
            ChessBoard[1, 0] = horse1;
            ChessBoard[6, 0] = horse2;
            ChessBoard[1, 7] = horse3;
            ChessBoard[6, 7] = horse4;

            Piece bishop1 = new Bishop(player1);
            Piece bishop2 = new Bishop(player1);
            Piece bishop3 = new Bishop(!player1);
            Piece bishop4 = new Bishop(!player1);
            ChessBoard[2, 0] = bishop1;
            ChessBoard[5, 0] = bishop2;
            ChessBoard[2, 7] = bishop3;
            ChessBoard[5, 7] = bishop4;

            Piece king1 = new King(player1);
            Piece king2 = new King(!player1);
            ChessBoard[4, 0] = king1;
            ChessBoard[4, 7] = king2;

            Piece queen1 = new Queen(player1);
            Piece queen2 = new Queen(!player1);
            ChessBoard[3, 0] = queen1;
            ChessBoard[3, 7] = queen2;
        }
        public Piece GetPiece(int x, int y) => ChessBoard[x,y];

        public bool IsAlly(int x1, int y1, int x2, int y2)
        {
            Piece piece1 = GetPiece(x1, y1);
            Piece piece2 = GetPiece(x2, y2);

            if (piece1.Color == piece2.Color)
            {
                return false;
            }            
            return false;
        }

        public bool IsInRange(int x1, int y1, int x2, int y2)
        {
            if (!(x1 < 0) && !(x1 > 8) && !(y1 < 0) && !(y2 > 8) && !(x2 < 0) && !(x2 > 8) && !(y2 < 0) && !(y2 > 8))
            {
                return true;
            }
            return false;
        }

        public bool IsEmpty(int x2, int y2)
        {
            try
            {
                Piece piece1 = GetPiece(x2, y2);
                piece1.GetType();
                return false; 
            }
            catch (NullReferenceException e)
            {
                return true;
            }            
        }

        public bool IsPawn(Piece piece)
        {
            if (piece.GetType() == typeof(Pawn))
            //if (piece.GetType().Equals(typeof(Pawn)))
            {
                return true;
            }
            return false;
        }

        public bool IsPawnEating(int x1, int x2)// true si se mueve en diagonal
        {
            if (x1 != x2)            
            {
                return true;
            }
            return false;
        }



        public void MovePiece(int x1, int y1, int x2, int y2, bool player1, bool turn)
        {
            Piece piece1 = GetPiece(x1, y1);
            Console.WriteLine("aca");
            Console.WriteLine(piece1.GetType() + " es la pieza correctamente escogida en esta movida");
            Console.WriteLine("aca");
            if (!IsInRange(x1, y1, x2, y2))
            {
                Console.WriteLine("Out of range");
                return;
            }            
            if (piece1.Color != turn)
            {                
                Console.WriteLine("Is not your turn");
                return;
            }

            if (IsInRange(x1, y1, x2, y2) && piece1.Color == turn)
            {                
                if (IsPawn(piece1)) //caso especial Peon. Si player 1 es true, las blancas suben
                {                      
                    if (player1 == turn && y2 > y1 && piece1.IsValidMove(x1, y1, x2, y2))
                    {
                        if (IsPawnEating(x1, x2)) //!IsEmpty(x2, y2)
                        {
                            if (!IsAlly(x1, y1, x2, y2))
                            {
                                ChessBoard[x2, y2] = piece1;
                                ChessBoard[x1, y1] = null;
                            }

                        }
                    }                    
                
                    if (player1 != turn && y2 < y1 && piece1.IsValidMove(x1, y1, x2, y2))
                    {
                        if (IsPawnEating(x1, x2) == (!IsEmpty(x2, y2) && !IsAlly(x1, y1, x2, y2)))
                        {
                            ChessBoard[x2, y2] = piece1;
                            ChessBoard[x1, y1] = null;
                        }
                    }                 
                }

                else if (piece1.IsValidMove(x1, y1, x2, y2))
                {                    
                    ChessBoard[x2, y2] = piece1;
                    ChessBoard[x1, y1] = null;                    
                } 
            }
        }

    }
}
