namespace OsvaldoChessMaster
{
    using System;
    using System.Collections.Generic;
    using OsvaldoChessMaster.Piece;

    public class Board
    {
        private const int Size = 9;
        private int pointer;
        private bool player1;
        private Stack<string> stackFullPlay;
        private string movement = string.Empty; // se llena con las dos movidas y luego se reinicia

        public int TurnNumber { get; set; }
        public bool Turn { get; set; } //turn=true es el turno del player1

        public PieceBase[,] ChessBoard { get; set; }                

        private static Dictionary<int, string> columnLetters = new Dictionary<int, string>
        {
            { 1, "a" }, { 2, "b" }, { 3, "c" }, { 4, "d" }, { 5, "e" }, { 6, "f" }, { 7, "g" }, { 8, "h" }
        };

        public Board(bool player1)
        {
            // Inicializacion de variables
            this.Turn = true;
            this.stackFullPlay = new Stack<string>();

            TurnNumber = 1;
            this.player1 = player1;
            ChessBoard = new PieceBase[Size, Size];

            ChessBoard[1, 2] = new Pawn(player1);
            ChessBoard[2, 2] = new Pawn(player1);
            ChessBoard[3, 2] = new Pawn(player1);
            ChessBoard[4, 2] = new Pawn(player1);
            ChessBoard[5, 2] = new Pawn(player1);
            ChessBoard[6, 2] = new Pawn(player1);
            ChessBoard[7, 2] = new Pawn(player1);
            ChessBoard[8, 2] = new Pawn(player1);

            ChessBoard[1, 7] = new Pawn(!player1);
            ChessBoard[2, 7] = new Pawn(!player1);
            ChessBoard[3, 7] = new Pawn(!player1);
            ChessBoard[4, 7] = new Pawn(!player1);
            ChessBoard[5, 7] = new Pawn(!player1);
            ChessBoard[6, 7] = new Pawn(!player1);
            ChessBoard[7, 7] = new Pawn(!player1);
            ChessBoard[8, 7] = new Pawn(!player1);

            ChessBoard[1, 1] = new Rook(player1);
            ChessBoard[8, 1] = new Rook(player1);
            ChessBoard[1, 8] = new Rook(!player1);
            ChessBoard[8, 8] = new Rook(!player1);

            ChessBoard[2, 1] = new Knight(player1);
            ChessBoard[7, 1] = new Knight(player1);
            ChessBoard[2, 8] = new Knight(!player1);
            ChessBoard[7, 8] = new Knight(!player1);

            ChessBoard[3, 1] = new Bishop(player1);
            ChessBoard[6, 1] = new Bishop(player1);
            ChessBoard[3, 8] = new Bishop(!player1);
            ChessBoard[6, 8] = new Bishop(!player1);

            ChessBoard[5, 1] = new King(player1);
            ChessBoard[5, 8] = new King(!player1);

            ChessBoard[4, 1] = new Queen(player1);
            ChessBoard[4, 8] = new Queen(!player1);

            for (int i = 1; i < 9; i++)
            {
                for (int j = 3; j < 7; j++)
                {
                    ChessBoard[i, j] = new EmptyPiece(player1);
                }
            }

        }
        public PieceBase GetPiece(int x, int y)
        {
            return ChessBoard[x, y] ?? new EmptyPiece(player1);
        }

        public bool IsAlly(int x1, int y1, int x2, int y2) //solo es true si hay otra pieza del mismo color, sino false siempre
        {
            var piece1 = GetPiece(x1, y1);
            var piece2 = GetPiece(x2, y2);

            if (piece1.Color == piece2.Color)
            {
                return true;
            }

            return false;
        }

        public bool IsInRange(int x1, int y1, int x2, int y2)
        {
            if (!(x1 < 1) && !(x1 > 8) && !(y1 < 1) && !(y2 > 8) && !(x2 < 1) && !(x2 > 8) && !(y2 < 1) && !(y2 > 8))
            {
                return true;
            }
            Console.WriteLine("Out of range");
            return false;
        }

        public bool IsColorTurn(PieceBase piece1)
        {
            if (piece1.Color != Turn)
            {
                //Console.WriteLine("Is not your turn");
                return false;
            }
            return true;
        }

        public bool IsEmpty(int x2, int y2)
        {
            try
            {
                var piece1 = GetPiece(x2, y2);

                if (piece1.GetType() == typeof(EmptyPiece))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        public bool IsPawn(PieceBase piece) => piece.GetType() == typeof(Pawn);

        public bool IsHorse(PieceBase piece) => piece.GetType() == typeof(Knight);

        // true si se mueve en diagonal
        public bool IsPawnCapturing(int x1, int x2) => x1 != x2;

        public bool CanMovePawn(int x1, int y1, int x2, int y2, bool player1, bool testing)
        {
            try
            {
                var piece1 = GetPiece(x1, y1);

                if (!IsInRange(x1, y1, x2, y2) || !IsPawn(piece1))
                {
                    return false;
                }

                // El peon sube
                if (player1 == Turn && y2 > y1 && piece1.IsValidMove(x1, y1, x2, y2))
                {
                    if (IsPawnCapturing(x1, x2))
                    {
                        if (!IsAlly(x1, y1, x2, y2) && !IsEmpty(x2, y2))
                        {
                            if (!testing)
                            {
                                MovePromotion(x1, y1, x2, y2, piece1);
                            }
                            return true;
                        }
                        if (IsPawn(GetPiece(x2, y2 - 1)) && !IsAlly(x1, y1, x2, y2 - 1))
                        {
                            if (GetPiece(x2, y2 - 1).GetCapturableByTheWay() && TurnNumber - GetPiece(x2, y2 - 1).GetturnNumberCapturableByTheWay() < 2)
                            {
                                // FinallyMove(x1, y1, x2, y2, piece1, player1);
                                if (!testing)
                                {
                                    //come al paso
                                    ChessBoard[x2, y2 - 1] = new EmptyPiece(player1);
                                }
                                return true;
                            }
                        }
                    }
                    else //no come
                    {
                        if (Math.Abs(y2 - y1) == 1 && IsEmpty(x2, y2)) //sube 1 casillero
                        {
                            if (!testing)
                            {
                                MovePromotion(x1, y1, x2, y2, piece1);
                            }
                            return true;
                        }
                        if (IsEmpty(x2, y2 - 1) && IsEmpty(x2, y2)) //sube 2 casilleros chequea 2 vacios
                        {
                            //FinallyMove(x1, y1, x2, y2, piece1, player1);
                            if (!testing)
                            {
                                piece1.SetCapturableByTheWay(true, TurnNumber);
                            }
                            return true;
                        }
                    }
                }

                if (player1 != Turn && y2 < y1 && piece1.IsValidMove(x1, y1, x2, y2)) //el peon baja
                {

                    if (IsPawnCapturing(x1, x2))
                    {

                        if (!IsAlly(x1, y1, x2, y2) && !IsEmpty(x2, y2))
                        {
                            if (!testing)
                            {
                                MovePromotion(x1, y1, x2, y2, piece1, false);
                            }

                            return true;
                        }

                        if (!IsEmpty(x2, y2 + 1) && !IsAlly(x1, y1, x2, y2 + 1))
                        {
                            if (GetPiece(x2, y2 + 1).GetCapturableByTheWay() && TurnNumber - GetPiece(x2, y2 + 1).GetturnNumberCapturableByTheWay() < 2)
                            {
                                //FinallyMove(x1, y1, x2, y2, piece1, player1);
                                if (!testing)
                                {
                                    ChessBoard[x2, y2 + 1] = new EmptyPiece(true); //come al paso
                                }
                                return true;
                            }
                        }
                    }
                    else
                    {
                        if (Math.Abs(y2 - y1) == 1 && IsEmpty(x2, y2)) //baja 1 casillero
                        {
                            if (!testing)
                            {
                                MovePromotion(x1, y1, x2, y2, piece1, false);
                            }
                            return true;
                        }
                        if (IsEmpty(x2, y2 + 1) && IsEmpty(x2, y2)) //baja 2 casilleros chequea 2 vacios
                        {
                            //FinallyMove(x1, y1, x2, y2, piece1, player1);
                            if (!testing)
                            {
                                piece1.SetCapturableByTheWay(true, TurnNumber); //solucionar esto en los chequeos de bloqueo de jaque                            
                            }
                            return true;
                        }
                        return false;
                    }

                }
                if (!piece1.IsValidMove(x1, y1, x2, y2))
                {
                    Console.WriteLine("Not valid move");
                    return false;
                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("No se pudo escoger la pieza");
                return false;
            }
            return false;
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
        private void MovePromotion(int x1, int y1, int x2, int y2, PieceBase piece1, bool moveUp = true)
        {            
            var limit = moveUp ? 8 : 1;
            if (y2 == limit)
            {
                var piece2 = new Queen(Turn);
                ChessBoard[x2, y2] = piece2;
            }
        }

        public void FinallyMove(int x1, int y1, int x2, int y2, bool player1)
        {
            if (CanMovePiece(x1, y1, x2, y2, player1, false))
            {
                var piece1 = GetPiece(x1, y1);
                ChessBoard[x2, y2] = piece1;
                ChessBoard[x1, y1] = new EmptyPiece(true);
                //piece1.PositionX = x2;
                //piece1.PositionY = y2;

                if (piece1.GetType() == typeof(King) && piece1.CanCastling)
                {
                    piece1.CanCastling = false; // si movió king no podrá enrocar jamás!
                    Console.WriteLine("Ya no puede enrocar porque movio rey");
                }

                if (piece1.GetType() == typeof(Rook) && piece1.CanCastling)
                {
                    piece1.CanCastling = false; // si movió torre no podrá enrocar jamás!
                    Console.WriteLine("Ya no puede enrocar porque movió la torre");
                }

                WriteMove(x1, y1, x2, y2, piece1, player1);
                int XKING = XKing(!piece1.Color); //coordenadas del rey del player que mueve
                int YKING = YKing(!piece1.Color);


                if (IsCheckmate(XKING, YKING, !piece1.Color))  // jaque mate
                {
                    Console.WriteLine("CHECKMATE: JAQUE MATE!!!!!!!!!!!!   --------fin de juego----------");
                    return;
                }
                // Ahogado
                if (!IsSquareCheck(XKING, YKING, !piece1.Color) && IsAllSquaresAroundCheckorBlocked(XKING, YKING, !piece1.Color) && CanAllyBlock(XKING, YKING, !piece1.Color) && CanOtherPieceMove(!piece1.Color))
                {
                    Console.WriteLine("DRAW: AHOGADO!!!!!!!!!!!!  TABLES: TABLAS  -------fin de juego------");
                    return;
                }
                // Jaque
                if (IsSquareCheck(XKING, YKING, !piece1.Color))
                {
                    Console.WriteLine("x,y " + XKING + YKING + " JAQUE AL REY!!!!!!!!!!!!");
                }

                if (Turn == player1)
                {
                    Console.WriteLine("turnNumber: " + TurnNumber);
                    TurnNumber++;
                }
                TurnChange();
            }
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

        public bool CanMovePiece(int x1, int y1, int x2, int y2, bool player1, bool testing)
        {
            try
            {
                var piece1 = GetPiece(x1, y1);

                if (!piece1.IsValidMove(x1, y1, x2, y2) && !(piece1.GetType() == typeof(King) && piece1.GetType() == typeof(Rook) && x1 == 5 && x2 == 3 && y1 == y2 && (y1 == 1 || y1 == 8) && IsEmpty(x2 - 1, y2) && IsEmpty(x2, y2) && IsEmpty(x2 + 1, y2) && piece1.CanCastling && !IsSquareCheck(2, y1, piece1.Color) && !IsSquareCheck(3, y1, piece1.Color) && !IsSquareCheck(4, y1, piece1.Color)) && !(x1 == 5 && x2 == 7 && y1 == y2 && (y1 == 1 || y1 == 8) && IsEmpty(x2 - 1, y2) && IsEmpty(x2, y2) && piece1.CanCastling && !IsSquareCheck(6, y1, piece1.Color) && !IsSquareCheck(7, y1, piece1.Color)))
                {
                    return false;
                }
                //Console.WriteLine(piece1.GetType() + " es la pieza reconocida para esta movida");

                if (!IsInRange(x1, y1, x2, y2) || !IsColorTurn(piece1))
                {
                    return false;
                }
                if (piece1.CanJump && piece1.IsValidMove(x1, y1, x2, y2))
                {
                    //FinallyMove(x1, y1, x2, y2, piece1, player1);
                    return true;
                }

                if (IsPawn(piece1))
                {
                    return CanMovePawn(x1, y1, x2, y2, player1, testing);
                }
                else
                {
                    // enroque largo          mov horiz   ---arriba o abajo---    -----------casilleros vacios entre rey y torre?-------------    fue movido el rey antes?    -------------estan jaqueadas las casillas entre torre y rey???-----------------------------------------------                            
                    if (piece1.GetType() == typeof(King) && piece1.GetType() == typeof(Rook) && x1 == 5 && x2 == 3 && y1 == y2 && (y1 == 1 || y1 == 8) && IsEmpty(x2 - 1, y2) && IsEmpty(x2, y2) && IsEmpty(x2 + 1, y2) && piece1.CanCastling && !IsSquareCheck(2, y1, piece1.Color) && !IsSquareCheck(3, y1, piece1.Color) && !IsSquareCheck(4, y1, piece1.Color))
                    {
                        var piece2 = GetPiece(x2, y2);

                        if (piece2.CanCastling) //me fijo se se habia movido la torre antes
                        {
                            if (!testing)
                            {
                                piece1.CanCastling = false; // ya no va a poder enrocar mas (a la torre no hace falta)
                                piece1.LCastling = true; // esto es para escribir la jugada                              
                            }

                            int XKING = XKing(piece1.Color); //coordenadas del rey del player que mueve
                            int YKING = YKing(piece1.Color);
                            if (IsSquareCheck(XKING, YKING, piece1.Color))
                            {
                                if (!testing)
                                {
                                    Console.WriteLine("Movimiento Invalido, SU REY QUEDA EN JAQUE!!!!!!!!!!!!");
                                }
                                return false;
                            }
                            else
                            {
                                //FinallyMove(x1, y1, x2, y2, piece1, player1); 
                                if (!testing)
                                {
                                    ChessBoard[x2 + 1, y2] = new Rook(true); //termino de mover la torre
                                    ChessBoard[1, y1] = new EmptyPiece(true);
                                }
                                return true;
                            }
                        }
                    }

                    // enroque corto

                    if (x1 == 5 && x2 == 7 && y1 == y2 && (y1 == 1 || y1 == 8) && IsEmpty(x2 - 1, y2) && IsEmpty(x2, y2) && piece1.CanCastling && !IsSquareCheck(6, y1, piece1.Color) && !IsSquareCheck(7, y1, piece1.Color))
                    {
                        var piece2 = GetPiece(x2, y2);

                        if (piece2.CanCastling) //me fijo se se habia movido la torre antes
                        {
                            if (!testing)
                            {
                                piece1.CanCastling = false; // ya no va a poder enrocar mas
                                piece1.SCastling = true; // esto es para escribir la jugada
                            }

                            int XKING = XKing(piece1.Color); //coordenadas del rey del player que mueve
                            int YKING = YKing(piece1.Color);
                            if (IsSquareCheck(XKING, YKING, piece1.Color))
                            {
                                if (!testing)
                                {
                                    Console.WriteLine("Movimiento Invalido, SU REY ESTA EN JAQUE!!!!!!!!!!!!");
                                }
                                return false;
                            }
                            else
                            {
                                //FinallyMove(x1, y1, x2, y2, piece1, player1);
                                if (!testing)
                                {
                                    ChessBoard[x1 + 1, y2] = new Rook(true); //termino de mover la torre
                                    ChessBoard[8, y1] = new EmptyPiece(true);
                                }
                                return true;
                            }
                        }
                    }


                    if (!piece1.CanJump && (x1 == x2 || y1 == y2) && piece1.IsValidMove(x1, y1, x2, y2) && IsLineEmpty(x1, y1, x2, y2))
                    {
                        //FinallyMove(x1, y1, x2, y2, piece1, player1);
                        return true;
                    }

                    if (!piece1.CanJump && x1 != x2 && y1 != y2 && piece1.IsValidMove(x1, y1, x2, y2) && IsDiagonalEmpty(x1, y1, x2, y2))
                    {

                        //FinallyMove(x1, y1, x2, y2, piece1, player1);
                        return true;

                    }
                    return false;
                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("No se pudo escoger la pieza");
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("No piece there!!!");
                return false;
            }
        }

        public void WriteMove(int x1, int y1, int x2, int y2, PieceBase piece1, bool player1)
        {
            if (player1 == Turn) // para escribir el numero de la jugada
            {
                movement = movement.Insert(pointer, ".");
                movement = movement.Insert(pointer, TurnNumber.ToString());
                pointer += TurnNumber.ToString().Length + 1;
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
            if (piece1.GetType() == typeof(Knight))
            {
                movement = movement.Insert(pointer++, "N");
            }
            if (piece1.GetType() == typeof(Queen))
            {
                movement = movement.Insert(pointer++, "Q");
            }
            if (piece1.GetType() == typeof(Rook))
            {
                movement = movement.Insert(pointer++, "R");
            }
            if (piece1.GetType() == typeof(King))
            {
                if (piece1.SCastling)
                {
                    movement = movement.Insert(pointer, "0-0");
                    pointer += 3;
                    return;
                }
                if (piece1.SCastling)
                {
                    movement = movement.Insert(pointer, "0-0-0");
                    pointer += 5;
                    return;
                }
                movement = movement.Insert(pointer++, "K");
            }
            movement = movement.Insert(pointer++, columnLetters[x2]);
            movement = movement.Insert(pointer++, (y2).ToString());

            if (player1 != Turn) // guarda la jugada completa
            {
                stackFullPlay.Push(movement);
                pointer = 0;
                Console.WriteLine("--------- Esta fue la jugada: " + movement);
                movement = "";
            }
        }

        public void TurnChange()
        {
            Turn = !Turn;
            //Console.WriteLine("turn: " + turn + "       cambio de turno -----------------------");
        }

        public bool IsSquareCheck(int x, int y, bool targetColor) //Color es el color del rey (el opuesto del atacante)
        {
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    if (IsPieceChecking(i, j, x, y, targetColor))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsPieceChecking(int xPiece, int yPiece, int xTarget, int yTarget, bool targetColor)
        {
            var piece1 = GetPiece(xPiece, yPiece);

            if (piece1.Color != targetColor && piece1.IsValidMove(xPiece, yPiece, xTarget, yTarget))
            {
                if (IsPawn(piece1))
                {
                    if (IsPawnCapturing(xPiece, xTarget))
                    {
                        return true;
                    }
                    return false;
                }
                if (Math.Abs(xTarget - xPiece) == Math.Abs(yTarget - yPiece) && IsDiagonalEmpty(xPiece, yPiece, xTarget, yTarget))
                {
                    return true;
                }
                if ((xTarget == xPiece || yTarget == yPiece) && IsLineEmpty(xPiece, yPiece, xTarget, yTarget))
                {
                    return true;
                }
                if (piece1.CanJump)
                {
                    return true;
                }
                return false;
            }

            else
            {
                return false;
            }
        }

        public int XKing(bool Color) //Color es el color del Rey que busca casilla por casilla
        {
            int XKing = 0;
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    var piece1 = GetPiece(i, j);

                    if (piece1.GetType() == typeof(King) && piece1.Color == Color)
                    {
                        XKing = i;
                    }
                }
            }
            return XKing;
        }
        public int YKing(bool Color) //Color es el color del Rey que busca casilla por casilla
        {
            int YKing = 0;

            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    var piece1 = GetPiece(i, j);

                    if (piece1.GetType() == typeof(King) && piece1.Color == Color)
                    {
                        YKing = j;
                    }
                }
            }

            return YKing;
        }
        public bool IsCheckmate(int Xking, int YKing, bool KingColor)
        {
            if (IsSquareCheck(Xking, YKing, KingColor) && IsAllSquaresAroundCheckorBlocked(Xking, YKing, KingColor) && CanAllyBlock(Xking, YKing, KingColor))
            { 
                return true; 
            }
            else
            { 
                return false; 
            }
        }

        public bool IsAllSquaresAroundCheckorBlocked(int Xking, int YKing, bool KingColor)
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (!IsSquareCheck(Xking + i, YKing + j, KingColor) && i != Xking && j != YKing && IsEmpty(Xking + i, YKing + j))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool CanAllyBlock(int Xking, int Yking, bool KingColor)
        {
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    var pieceA = GetPiece(i, j);

                    if (pieceA.Color == KingColor)
                    {
                        for (int k = 1; k < 9; k++)
                        {
                            for (int l = 1; l < 9; l++)
                            {
                                var auxPiece = GetPiece(k, l);
                                //MovePiece(i, j, k, l, player1);
                                ChessBoard[k, l] = pieceA;
                                ChessBoard[i, j] = new EmptyPiece(player1);

                                //MovePiece(i, j, k, l, player1);
                                if (!IsSquareCheck(Xking, Yking, KingColor))
                                {
                                    ChessBoard[i, j] = pieceA;
                                    ChessBoard[k, l] = auxPiece;
                                    return true;
                                }
                                ChessBoard[i, j] = pieceA;
                                ChessBoard[k, l] = auxPiece;
                            }
                        }
                    }

                }
            }

            return false;
        }

        public bool CanOtherPieceMove(bool KingColor)
        {
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    var piece1 = GetPiece(i, j);
                    if (piece1.GetType() != typeof(King) && piece1.Color == KingColor)
                    {
                        if (CanPieceMoveOrDraw(i, j))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public bool CanPieceMoveOrDraw(int x, int y)
        {
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    if (CanMovePiece(x, y, i, j, player1, true)) //va true porque solo quiere saber si se puede mover
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}