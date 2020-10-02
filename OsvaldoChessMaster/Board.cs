using System;
using System.Collections.Generic;
using System.Text;

namespace OsvaldoChessMaster
{
    public class Board
    {
        int pointer = 0;
        public int turnNumber;
        public bool turn = true; //turn=true es el turno del player1
        private const int Size = 8;
        public Piece[,] ChessBoard;
        private bool player1;
        Stack<string> StackFullPlay = new Stack<string>();
        private string movement = ""; // se llena con las dos movidas y luego se reinicia
        private static Dictionary<int, string> columnLetters = new Dictionary<int, string>
        {
            { 0, "a" }, { 1, "b" }, { 2, "c" }, { 3, "d" }, { 4, "e" }, { 5, "f" }, { 6, "g" }, { 7, "h" }
        };

        public Board(bool player1)
        {               
            turnNumber = 1;
            this.player1 = player1;
            ChessBoard = new Piece[Size, Size];
                       
            ChessBoard[0, 1] = new Pawn(player1); 
            ChessBoard[1, 1] = new Pawn(player1);
            ChessBoard[2, 1] = new Pawn(player1);
            ChessBoard[3, 1] = new Pawn(player1);
            ChessBoard[4, 1] = new Pawn(player1);
            ChessBoard[5, 1] = new Pawn(player1);
            ChessBoard[6, 1] = new Pawn(player1);
            ChessBoard[7, 1] = new Pawn(player1);

            ChessBoard[0, 6] = new Pawn(!player1);
            ChessBoard[1, 6] = new Pawn(!player1);
            ChessBoard[2, 6] = new Pawn(!player1);
            ChessBoard[3, 6] = new Pawn(!player1);
            ChessBoard[4, 6] = new Pawn(!player1);
            ChessBoard[5, 6] = new Pawn(!player1);
            ChessBoard[6, 6] = new Pawn(!player1);
            ChessBoard[7, 6] = new Pawn(!player1);

            ChessBoard[0, 0] = new Rook(player1);
            ChessBoard[7, 0] = new Rook(player1);
            ChessBoard[0, 7] = new Rook(!player1);
            ChessBoard[7, 7] = new Rook(!player1);

            ChessBoard[1, 0] = new Horse(player1);
            ChessBoard[6, 0] = new Horse(player1);
            ChessBoard[1, 7] = new Horse(!player1);
            ChessBoard[6, 7] = new Horse(!player1);
 
            ChessBoard[2, 0] = new Bishop(player1);
            ChessBoard[5, 0] = new Bishop(player1);
            ChessBoard[2, 7] = new Bishop(!player1);
            ChessBoard[5, 7] = new Bishop(!player1);

            ChessBoard[4, 0] = new King(player1);
            ChessBoard[4, 7] = new King(!player1);

            ChessBoard[3, 0] = new Queen(player1);
            ChessBoard[3, 7] = new Queen(!player1);

        }
        public Piece GetPiece(int x, int y) => ChessBoard[x, y];

        public bool IsAlly(int x1, int y1, int x2, int y2) //solo es true si hay otra pieza del mismo color, sino false siempre
        {
            Piece piece1 = GetPiece(x1, y1);
            Piece piece2 = GetPiece(x2, y2);

            if (piece1.Color == piece2.Color)
            {
                return true;
            }
            return false;
        }

        public bool IsInRange(int x1, int y1, int x2, int y2)
        {
            if (!(x1 < 0) && !(x1 > 7) && !(y1 < 0) && !(y2 > 7) && !(x2 < 0) && !(x2 > 7) && !(y2 < 0) && !(y2 > 7))
            {
                return true;
            }
            Console.WriteLine("Out of range");
            return false;
        }

        public bool IsColorTurn(Piece piece1)
        {
            if (piece1.Color != turn)
            {
                Console.WriteLine("Is not your turn");
                return false;
            }
            return true;
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
            {
                return true;
            }
            return false;
        }

        public bool IsHorse(Piece piece)
        {
            if (piece.GetType() == typeof(Horse))
            {
                return true;
            }
            return false;
        }

        public bool IsPawnCapturing(int x1, int x2)// true si se mueve en diagonal
        {
            if (x1 != x2)
            {
                return true;
            }
            return false;
        }

        public void MovePawn(int x1, int y1, int x2, int y2, bool player1)
        {
            try
            {
                Piece piece1 = GetPiece(x1, y1);
                if (!IsInRange(x1, y1, x2, y2) || !IsPawn(piece1))
                {
                    return;
                }
                Console.WriteLine(piece1.GetType() + " es la pieza reconocida para esta movida");

                if (player1 == turn && y2 > y1 && piece1.IsValidMove(x1, y1, x2, y2)) // El peon sube
                {
                    if (IsPawnCapturing(x1, x2))
                    {
                        if (!IsAlly(x1, y1, x2, y2) && !IsEmpty(x2, y2))
                        {
                            MovePromotion(x1, y1, x2, y2, piece1);
                        }
                    }
                    else //no come
                    {
                        
                        if (Math.Abs(y2 - y1) == 1 && IsEmpty(x2, y2)) //sube 1 casillero
                        {                            
                            MovePromotion(x1, y1, x2, y2, piece1);
                        }
                        if (IsEmpty(x2, y2 - 1) && IsEmpty(x2, y2)) //sube 2 casilleros chequea 2 vacios
                        {                         
                            MovePromotion(x1, y1, x2, y2, piece1);
                        }
                    }
                }

                if (player1 != turn && y2 < y1 && piece1.IsValidMove(x1, y1, x2, y2)) //el peon baja
                {

                    if (IsPawnCapturing(x1, x2))
                    {

                        if (!IsAlly(x1, y1, x2, y2) && !IsEmpty(x2, y2))
                        {
                            MovePromotion(x1, y1, x2, y2, piece1, false);
                        }
                    }
                    else
                    {
                        if (Math.Abs(y2 - y1) == 1 && IsEmpty(x2, y2)) //baja 1 casillero
                        {
                            MovePromotion(x1, y1, x2, y2, piece1, false);
                        }
                        if (IsEmpty(x2, y2 + 1) && IsEmpty(x2, y2)) //baja 2 casilleros chequea 2 vacios
                        {
                            MovePromotion(x1, y1, x2, y2, piece1, false);
                        }
                    }
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("No se pudo escoger la pieza");
                return;
            }
        }

        /// <summary>
        /// resume el metodo move de peones donde se promueve!!
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="turn"></param>
        /// <param name="piece1"></param>
        /// <param name="moveUp"></param>
        private void MovePromotion(int x1, int y1, int x2, int y2, Piece piece1, bool moveUp = true)
        {
            FinallyMove(x1, y1, x2, y2, piece1, player1);
            var limit = moveUp ? 7 : 0;
            if (y2 == limit)
            {
                Piece piece2 = new Queen(turn);
                ChessBoard[x2, y2] = piece2;
            }
        }

        private void FinallyMove(int x1, int y1, int x2, int y2, Piece piece1, bool player1)
        {
            ChessBoard[x2, y2] = piece1;
            ChessBoard[x1, y1] = null;
            WriteMove(x1, y1, x2, y2, piece1, player1);
            Console.WriteLine("turn y player: " + turn + player1 + "....................");
            if (turn==player1)
            {                   
                Console.WriteLine("turnNumber: " + turnNumber);
                turnNumber++;
                Console.WriteLine("turnNumber: " + turnNumber);
            }            
            TurnChange();
        }

        public bool IsLineEmpty(int x1, int y1, int x2, int y2) // chequea 
        {
            bool abort = false;
            if (x1 == x2)
            {
                if (Math.Abs(y1 - y2) > 1)
                {
                    for (int i = 1; i < Math.Abs(y1 - y2); i++)
                    {
                        if (y1 < y2)
                        {
                            if (!IsEmpty(x1, y1 + i))
                            {
                                abort = true;
                            }
                        }
                        else
                        {
                            if (!IsEmpty(x1, y1 - i))
                            {
                                abort = true;
                            }
                        }
                    }
                }
            }

            if (y1 == y2)
            {
                for (int i = 1; i < Math.Abs(x1 - x2); i++)
                {
                    if (x1 < x2)
                    {
                        if (!IsEmpty(x1 + i, y1))
                        {
                            abort = true;
                        }
                    }
                    else
                    {
                        if (!IsEmpty(x1, y1 - i))
                        {
                            abort = true;
                        }
                    }
                }
            }

            return !abort;
        }

        public bool IsDiagonalEmpty(int x1, int y1, int x2, int y2)
        {
            bool abort = false;
            if (Math.Abs(y1 - y2) == Math.Abs(x1 - x2)) //quizás sea redundante esta línea
            {
                if (Math.Abs(y1 - y2) > 1)
                {
                    for (int i = 1; i < Math.Abs(y1 - y2); i++)
                    {
                        if (y1 < y2 && x1 < x2)
                        {
                            if (!IsEmpty(x1 + i, y1 + i))
                            {
                                abort = true;
                            }
                        }
                        if (y1 > y2 && x1 < x2)
                        {
                            if (!IsEmpty(x1 + i, y1 - i))
                            {
                                abort = true;
                            }
                        }
                        if (y1 < y2 && x1 > x2)
                        {
                            if (!IsEmpty(x1 - i, y1 + i))
                            {
                                abort = true;
                            }
                        }
                        if (y1 > y2 && x1 > x2)
                        {
                            if (!IsEmpty(x1 - i, y1 - i))
                            {
                                abort = true;
                            }
                        }
                    }
                }
            }
            return !abort;
        }


        public void MovePiece(int x1, int y1, int x2, int y2, bool player1)
        {
            try
            {
                Piece piece1 = GetPiece(x1, y1);
                Console.WriteLine(piece1.GetType() + " es la pieza reconocida para esta movida");

                if (!IsInRange(x1, y1, x2, y2) || !IsColorTurn(piece1))
                {
                    return;
                }

                if (IsPawn(piece1))
                {
                    MovePawn(x1, y1, x2, y2, player1);
                }
                else
                {

                    if (piece1.CanJump)
                    {
                        FinallyMove(x1, y1, x2, y2, piece1, player1);
                    }

                    if (!piece1.CanJump && (x1 == x2 || y1 == y2) && piece1.IsValidMove(x1, y1, x2, y2) && IsLineEmpty(x1, y1, x2, y2))
                    {
                        FinallyMove(x1, y1, x2, y2, piece1, player1);
                    }

                    if (!piece1.CanJump && x1 != x2 && y1 != y2 && piece1.IsValidMove(x1, y1, x2, y2) && IsDiagonalEmpty(x1, y1, x2, y2))
                    {
                        FinallyMove(x1, y1, x2, y2, piece1, player1);

                    }
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("No se pudo escoger la pieza");
            }
        }

        public void WriteMove(int x1, int y1, int x2, int y2, Piece piece1, bool player1)
        {            
            if (player1 == turn) // para escribir el numero de la jugada
            {
                movement = movement.Insert(pointer, ".");
                movement = movement.Insert(pointer, turnNumber.ToString());                
                pointer += turnNumber.ToString().Length + 1;
            }
            movement = movement.Insert(pointer++, " ");
            if (piece1.GetType() == typeof(Pawn) && IsPawnCapturing(x1, x2)) //peon comiendo             
            {
                movement = movement.Insert(pointer++, columnLetters[x1]);
            }
            movement = movement.Insert(pointer++, " ");
            if (piece1.GetType() == typeof(Bishop))
            {
                movement = movement.Insert(pointer++, "B");
            }
            if (piece1.GetType() == typeof(Horse))
            {
                movement = movement.Insert(pointer++, "N");
            }
            if (piece1.GetType() == typeof(Queen))
            {
                movement = movement.Insert(pointer++, "Q");
            }
            if (piece1.GetType() == typeof(King))
            {
                movement = movement.Insert(pointer++, "K");
            }
            if (piece1.GetType() == typeof(Bishop))
            {
                movement = movement.Insert(pointer++, "R");
            }

            movement = movement.Insert(pointer++, columnLetters[x2]);
            movement = movement.Insert(pointer++, (y2+1).ToString());            

            if (player1 != turn) // guarda la jugada completa
            {
                StackFullPlay.Push(movement);
                pointer = 0;
                Console.WriteLine("este es script: " + movement);
                Console.WriteLine("este es el StackFullPlay:" + StackFullPlay);
                movement = "";
            }   
        }
        public void TurnChange()
        {
            turn = !turn;
            Console.WriteLine("turn: " + turn + "       cambio de turno -----------------------");
        }
    }    
}
