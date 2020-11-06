namespace OsvaldoChessMaster
{
    using System;
    using System.Collections.Generic;
    using OsvaldoChessMaster.Piece;

    public class Board
    {
        private int pointer;
        public bool player1 { get; set; }
        private Stack<string> stackFullPlay;
        private string movement = string.Empty; // se llena con las dos movidas y luego se reinicia

        public int TurnNumber { get; set; }
        public bool Turn { get; set; } //turn=true es el turno del player1

        public PieceBase[,] ChessBoard { get; set; }

        public bool IsCheckmateFlag { get; private set; }

        public bool IsCheckFlag { get; private set; }
        public bool IsCantMoveCheckFlag { get; private set; }
        public bool IsCastlingFlag { get; set; }

        private static Dictionary<int, string> columnLetters = new Dictionary<int, string>
        {
            { 0, "a" }, { 1, "b" }, { 2, "c" }, { 3, "d" }, { 4, "e" }, { 5, "f" }, { 6, "g" }, { 7, "h" }
        };

        public Board(bool player1)
        {
            // Inicializacion de variables            
            this.Turn = true;
            this.stackFullPlay = new Stack<string>();

            TurnNumber = 1;
            this.player1 = player1;
            ChessBoard = new PieceBase[Constants.Size, Constants.Size];

            ChessBoard[0, 1] = new Pawn(player1, 0, 1);
            ChessBoard[1, 1] = new Pawn(player1, 1, 1);
            ChessBoard[2, 1] = new Pawn(player1, 2, 1);
            ChessBoard[3, 1] = new Pawn(player1, 3, 1);
            ChessBoard[4, 1] = new Pawn(player1, 4, 1);
            ChessBoard[5, 1] = new Pawn(player1, 5, 1);
            ChessBoard[6, 1] = new Pawn(player1, 6, 1);
            ChessBoard[7, 1] = new Pawn(player1, 7, 1);

            ChessBoard[0, 6] = new Pawn(!player1, 0, 6);
            ChessBoard[1, 6] = new Pawn(!player1, 1, 6);
            ChessBoard[2, 6] = new Pawn(!player1, 2, 6);
            ChessBoard[3, 6] = new Pawn(!player1, 3, 6);
            ChessBoard[4, 6] = new Pawn(!player1, 4, 6);
            ChessBoard[5, 6] = new Pawn(!player1, 5, 6);
            ChessBoard[6, 6] = new Pawn(!player1, 6, 6);
            ChessBoard[7, 6] = new Pawn(!player1, 7, 6);

            ChessBoard[0, 0] = new Rook(player1, 1, 0);
            ChessBoard[7, 0] = new Rook(player1, 8, 0);
            ChessBoard[0, 7] = new Rook(!player1, 1, 7);
            ChessBoard[7, 7] = new Rook(!player1, 8, 7);

            ChessBoard[1, 0] = new Knight(player1, 1, 0);
            ChessBoard[6, 0] = new Knight(player1, 6, 0);
            ChessBoard[1, 7] = new Knight(!player1, 1, 7);
            ChessBoard[6, 7] = new Knight(!player1, 6, 7);

            ChessBoard[2, 0] = new Bishop(player1, 2, 0);
            ChessBoard[5, 0] = new Bishop(player1, 5, 0);
            ChessBoard[2, 7] = new Bishop(!player1, 2, 7);
            ChessBoard[5, 7] = new Bishop(!player1, 5, 7);

            ChessBoard[4, 0] = new King(player1, 4, 0);
            ChessBoard[4, 7] = new King(!player1, 4, 7);

            ChessBoard[3, 0] = new Queen(player1, 3, 0);
            ChessBoard[3, 7] = new Queen(!player1, 3, 7);

            for (int i = Constants.ForStart; i < Constants.Size; i++)
            {
                for (int j = Constants.EmptyPieceStartLine; j < Constants.EmptyPieceEndLine; j++)
                {
                    ChessBoard[i, j] = new EmptyPiece(player1, i, j);
                }
            }

        }
        public PieceBase GetPiece(int x, int y)
        {
            return ChessBoard[x, y] ?? new EmptyPiece(player1, x, y);
        }


        public bool IsAlly(int x1, int y1, int x2, int y2) //solo es true si hay otra pieza del mismo color, sino false siempre
        {
            var piece1 = GetPiece(x1, y1);
            var piece2 = GetPiece(x2, y2);
            return piece1.Color == piece2.Color;
        }

        public bool IsInRange(int x1, int y1, int x2, int y2)
        {
            return (x1 >= Constants.ForStart && x1 < Constants.Size && y1 >= Constants.ForStart && y1 < Constants.Size && x2 >= Constants.ForStart && x2 < Constants.Size && y2 >= Constants.ForStart && y2 < Constants.Size);
        }

        public bool IsColorTurn(PieceBase piece1) => piece1.Color == Turn;


        public bool IsEmpty(int x2, int y2)
        {
            try
            {
                var piece1 = GetPiece(x2, y2);
                return piece1.GetType() == typeof(EmptyPiece);
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

        /// <summary>
        /// Chequea si es un movimiento valido para el peón, asegura que si come haya una pieza enemiga en destino o si avanza que no haya nada adelante y demas
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>       
        /// <param name="testing"></param> si es falso quiere decir que no tiene que revertir, ya que va a quedar asi
        /// <returns></returns>
        public bool CanMovePawn(int x1, int y1, int x2, int y2, bool testing)
        {
            try
            {
                var piece1 = GetPiece(x1, y1);

                if (!IsInRange(x1, y1, x2, y2) || !IsPawn(piece1))
                {
                    return false;
                }

                // El peon sube
                if (player1 == Turn && y2 > y1 && piece1.IsValidMove(x2, y2))
                {
                    if (IsPawnCapturing(x1, x2))
                    {
                        if (!IsAlly(x1, y1, x2, y2) && !IsEmpty(x2, y2))
                        {
                            if (!testing)
                            {
                                MovePromotion(x2, y2);
                            }
                            return true;
                        }
                        if (IsPawn(GetPiece(x2, y2 - 1)) && !IsAlly(x1, y1, x2, y2 - 1))
                        {
                            if (GetPiece(x2, y2 - 1).CapturableByTheWay && TurnNumber - GetPiece(x2, y2 - 1).turnNumberCapturableByTheWay < 2)
                            {
                                if (!testing)
                                {
                                    //come al paso
                                    ChessBoard[x2, y2 - 1] = GetPiece(x2, y2);// pongo la pieza vacía
                                    ChessBoard[x2, y2 - 1].Position.PositionX = x2;
                                    ChessBoard[x2, y2 - 1].Position.PositionX = y2 - 1;
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
                                MovePromotion(x2, y2);
                            }
                            return true;
                        }
                        if (IsEmpty(x2, y2 - 1) && IsEmpty(x2, y2)) //sube 2 casilleros chequea 2 vacios
                        {
                            if (!testing)
                            {
                                piece1.CapturableByTheWay = true;
                                piece1.turnNumberCapturableByTheWay = TurnNumber;
                            }
                            return true;
                        }
                    }
                }
                //el peon baja
                if (player1 != Turn && y2 < y1 && piece1.IsValidMove(x2, y2))
                {

                    if (IsPawnCapturing(x1, x2))
                    {

                        if (!IsAlly(x1, y1, x2, y2) && !IsEmpty(x2, y2))
                        {
                            if (!testing)
                            {
                                MovePromotion(x2, y2);
                            }

                            return true;
                        }

                        if (!IsEmpty(x2, y2 + 1) && !IsAlly(x1, y1, x2, y2 + 1))
                        {
                            if (GetPiece(x2, y2 + 1).CapturableByTheWay && TurnNumber - GetPiece(x2, y2 + 1).turnNumberCapturableByTheWay < 2)
                            {
                                if (!testing)
                                {
                                    //come al paso
                                    ChessBoard[x2, y2 + 1] = GetPiece(x2, y2); //pongo pieza vacia
                                    ChessBoard[x2, y2 + 1].Position.PositionX = x2;
                                    ChessBoard[x2, y2 + 1].Position.PositionX = y2 + 1;
                                }
                                return true;
                            }
                        }
                    }
                    //no come
                    else
                    {
                        if (Math.Abs(y2 - y1) == 1 && IsEmpty(x2, y2)) //baja 1 casillero
                        {
                            if (!testing)
                            {
                                MovePromotion(x2, y2);
                            }
                            return true;
                        }
                        if (IsEmpty(x2, y2 + 1) && IsEmpty(x2, y2)) //baja 2 casilleros chequea 2 vacios
                        {
                            if (!testing)
                            {
                                piece1.CapturableByTheWay = true;
                                piece1.turnNumberCapturableByTheWay = TurnNumber;
                            }
                            return true;
                        }
                        return false;
                    }

                }
                if (!piece1.IsValidMove(x2, y2))
                {
                    //Console.WriteLine("Not valid move");
                    return false;
                }
            }
            catch (NullReferenceException)
            {
                //Console.WriteLine("No se pudo escoger la pieza");
                return false;
            }
            return false;
        }

        /// <summary>
        /// resume el metodo move de peones donde se promueve y convierte en dama
        /// </summary>       
        /// <param name="Turn"></param> Turn es el color de la dama que creo y del peon
        /// <param name="piece1"></param> es el peon que llega al otro lado       
        private void MovePromotion(int x2, int y2)
        {
            if (y2 == Constants.LowerFile || y2 == Constants.UpperFile)
            {
                ChessBoard[x2, y2] = new Queen(Turn, x2, y2);
            }
        }

        /// <summary>
        /// Hace todas las comprobaciones y mueve la pieza en x1,y1 al destino x2,y2. Si el movimiento fue de un rey a enrocar, agrega ademas el movimiento respectivo de la torre
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public bool FinallyMove(int x1, int y1, int x2, int y2)
        {
            
            if (CanMovePiece(x1, y1, x2, y2, false)) //false porque no está testeando, sino que hay orden de mover
            {
                //baja 3 flags
                IsCheckFlag = false;
                IsCantMoveCheckFlag = false;
                IsCheckmateFlag = false;


                var piece1 = GetPiece(x1, y1);
                PieceBase auxEmptyPiece = GetPiece(x2, y2);
                ChessBoard[x2, y2] = piece1; //muevo piece1

                if (auxEmptyPiece.GetType() == typeof(EmptyPiece))
                {
                    ChessBoard[x1, y1] = auxEmptyPiece; //pongo pieza vacia
                    auxEmptyPiece.Position.PositionX = x1;
                    auxEmptyPiece.Position.PositionY = y1;
                }
                else
                {
                    ChessBoard[x1, y1] = new EmptyPiece(true, x1, y1);
                }

                piece1.Position.PositionX = x2;
                piece1.Position.PositionY = y2;

                if (piece1.GetType() == typeof(King) && piece1.CanCastling)
                {
                    piece1.CanCastling = false;
                    //Console.WriteLine("Ya no puede enrocar porque movio rey");
                    TurnChange();
                    return true;
                }

                if (piece1.GetType() == typeof(Rook) && piece1.CanCastling)
                {
                    piece1.CanCastling = false;
                    //Console.WriteLine("Ya no puede enrocar porque movió la torre");
                    TurnChange();
                    return true;
                }

                //WriteMove(x1, y1, x2, y2, piece1, player1);

                //coordenadas del rey del player que no mueve
                int XKING = XKing(!piece1.Color);
                int YKING = YKing(!piece1.Color);
                
                if (IsCheckmate(XKING, YKING, !piece1.Color))  // jaque mate
                {
                    //Console.WriteLine("CHECKMATE: JAQUE MATE!!!!!!!!!!!!   --------fin de juego----------");
                    IsCheckmateFlag = true;
                    return true;
                }
                // Ahogado
                else
                {
                    TurnChange();
                    if (IsAllSquaresAroundCheckorBlocked(XKING, YKING, !piece1.Color) && !CanOtherPieceMove(!piece1.Color))
                    {
                        TurnChange();
                        //Console.WriteLine("DRAW: AHOGADO!!!!!!!!!!!!  TABLES: TABLAS  -------fin de juego------");
                        return false;
                    }
                    TurnChange();
                }                

                if (Turn == player1)
                {
                    TurnNumber++; //TurnNumber se compone de dos movidas
                }
                TurnChange();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// chequea si la recta horizonal o vertical entre origen y destino está vacia
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public bool IsLineEmpty(int x1, int y1, int x2, int y2) // chequea 
        {
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
                                return false;
                            }
                        }
                        else
                        {
                            if (!IsEmpty(x1, y1 - i))
                            {
                                return false;
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
                            return false;
                        }
                    }
                    else
                    {
                        if (!IsEmpty(x1 - i, y1))
                        {
                            return false;
                        }
                    }
                }
            }
            if (x1 != x2 && y1 != y2)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// /// chequea si la diagonal entre origen y destino está vacia
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public bool IsDiagonalEmpty(int x1, int y1, int x2, int y2)
        {
            bool abort = false;
            if (Math.Abs(y1 - y2) == Math.Abs(x1 - x2)) //de mas? solo lo uso para queen y bishop
            {
                if (Math.Abs(y1 - y2) > 1) //si es igual a uno el movimiento es de una casilla y devuelvo true
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
            else
            {
                return false; //no es una diagonal
            }
            return !abort;
        }

        /// <summary>
        /// chequea si la pieza en x1,y1 puede moverse a x2,y2.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>        
        /// <param name="testing"></param> El método finallymove pasa este parametro como true antes de mover. Para saber si efectivamente puede mover. Cuando está en true (cuando chequeda ahogado) revierte todos los movimientos que hace. 
        /// <returns></returns>
        public bool CanMovePiece(int x1, int y1, int x2, int y2, bool testing)
        {
            try
            {
                var piece1 = GetPiece(x1, y1);
                if (!IsInRange(x1, y1, x2, y2) || !IsColorTurn(piece1))
                {
                    return false;
                }


                var piece2 = GetPiece(x2, y2);
                if (piece1.Color == piece2.Color && piece2.GetType() != typeof(EmptyPiece))
                {
                    return false;
                }

                if (piece1.CanJump && piece1.IsValidMove(x2, y2))
                {
                    return true;
                }

                // esto y el if son para ver si quedas en jaque
                MovePieceAndPutEmpty(x1, y1, x2, y2);

                if (IsSquareCheck(XKing(piece1.Color), YKing(piece1.Color), piece1.Color))
                {
                    //vuelvo atras el movimiento
                    ChessBoard[x2, y2] = piece2;
                    piece2.Position.PositionX = x2;
                    piece2.Position.PositionY = y2;

                    ChessBoard[x1, y1] = piece1;
                    piece1.Position.PositionX = x1;
                    piece1.Position.PositionY = y1;

                    IsCantMoveCheckFlag = true;

                    //quedas en jaque entonces es invalido el movimiento
                    return false;
                }
                else
                {   //vuelvo atras el movimiento
                    ChessBoard[x2, y2] = piece2;
                    piece2.Position.PositionX = x2;
                    piece2.Position.PositionY = y2;

                    ChessBoard[x1, y1] = piece1;
                    piece1.Position.PositionX = x1;
                    piece1.Position.PositionY = y1;
                }

                if (IsPawn(piece1))
                {
                    return CanMovePawn(x1, y1, x2, y2, testing);
                }
                else
                {
                    if (piece1.GetType() == typeof(King) && IsSquareCheck(x2, y2, piece1.Color)) // no lo deja mover al lugar en jaque
                    {
                        //Console.WriteLine("No podes moverte ahí, quedas en jaque");
                        IsCantMoveCheckFlag = true;
                        return false;
                    }

                    // enroque largo                                                                                 mov horiz   ---arriba o abajo---    -----------casilleros vacios entre rey y torre?-------------    fue movido el rey antes?    -------------estan jaqueadas las casillas entre torre y rey??? x2 = 2 (a donde va el rey ya se que no, porque  lo chequeo antes)
                    if (piece1.GetType() == typeof(King) && x1 == 4 && x2 == 2 && y1 == y2 && (y1 == Constants.LowerFile || y1 == Constants.UpperFile) && IsEmpty(x2 - 1, y2) && IsEmpty(x2, y2) && IsEmpty(x2 + 1, y2) && piece1.CanCastling && !IsSquareCheck(1, y1, 3, y1, piece1.Color))
                    {
                        piece2 = GetPiece(x2 - 2, y2);
                        if (piece2.CanCastling) //me fijo se se habia movido la torre antes
                        {
                            if (!testing)
                            {
                                piece1.CanCastling = false; // ya no va a poder enrocar mas (a la torre no hace falta)
                                piece1.LongCastling = true; // esto es para escribir la jugada                             
                            }

                            if (!testing)
                            {
                                MovePieceAndPutEmpty(0, y1, x1 + 1, y2);
                                //PieceBase AuxEmptyPiece = GetPiece(x2 + 1, y2);
                                //ChessBoard[x1 + 1, y2] = GetPiece(0, y1); //pongo torre a la der del rey
                                //GetPiece(x1 + 1, y2).Position.PositionX = x1 + 1;
                                //GetPiece(x1 + 1, y2).Position.PositionY = y2;

                                //ChessBoard[0, y1] = AuxEmptyPiece; //pone empty donde estaba la torre
                                //GetPiece(0, y1).Position.PositionX = 0;
                                //GetPiece(0, y1).Position.PositionY = y1;

                                IsCastlingFlag = true;
                            }
                            return true;

                        }
                    }

                    // enroque corto

                    if (piece1.GetType() == typeof(King) && x1 == 4 && x2 == 6 && y1 == y2 && (y1 == Constants.LowerFile || y1 == Constants.UpperFile) && IsEmpty(x2 - 1, y2) && IsEmpty(x2, y2) && piece1.CanCastling && !IsSquareCheck(5, y1, piece1.Color)) // la posicion x2=6 es donde va el rey y ya se que no va a haber jaque(ya chequeado arriba)
                    {
                        piece2 = GetPiece(x2 + 1, y2);
                        if (piece2.CanCastling) //me fijo se se habia movido la torre antes
                        {
                            if (!testing)
                            {
                                piece1.CanCastling = false; // ya no va a poder enrocar mas
                                piece1.ShortCastling = true; // esto es para escribir la jugada
                            }

                            int XKING = XKing(piece1.Color); //coordenadas del rey del player que mueve
                            int YKING = YKing(piece1.Color);
                            if (IsSquareCheck(XKING, YKING, piece1.Color))
                            {
                                if (!testing)
                                {
                                    //Console.WriteLine("Movimiento Invalido, SU REY ESTA EN JAQUE!!!!!!!!!!!!");
                                }
                                return false;
                            }
                            else
                            {
                                if (!testing)
                                {
                                    PieceBase AuxEmptyPiece = GetPiece(x2 + 1, y2);
                                    ChessBoard[x1 + 1, y2] = GetPiece(7, y1); //pongo torre a la der del rey
                                    GetPiece(x1 + 1, y2).Position.PositionX = x1 + 1;
                                    GetPiece(x1 + 1, y2).Position.PositionY = y2;

                                    ChessBoard[7, y1] = AuxEmptyPiece; //pone empty donde estaba la torre
                                    GetPiece(7, y1).Position.PositionX = 7;
                                    GetPiece(7, y1).Position.PositionY = y1;
                                }
                                return true;
                            }
                        }
                    }

                    if (!piece1.CanJump && (x1 == x2 || y1 == y2) && piece1.IsValidMove(x2, y2) && IsLineEmpty(x1, y1, x2, y2))
                    {
                        return true;
                    }

                    return (!piece1.CanJump && x1 != x2 && y1 != y2 && piece1.IsValidMove(x2, y2) && IsDiagonalEmpty(x1, y1, x2, y2));
                }
            }
            catch (NullReferenceException e)
            {
                return false;
            }
            catch (IndexOutOfRangeException e)
            {
                return false;
            }
        }
        /// <summary>
        /// Guarda en una lista las jugadas, pero lo tengo que rever
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="piece1"></param>
        /// <param name="player1"></param>
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
                if (piece1.ShortCastling)
                {
                    movement = movement.Insert(pointer, "0-0");
                    pointer += 3;
                    return;
                }
                if (piece1.ShortCastling)
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
                //Console.WriteLine("--------- Esta fue la jugada: " + movement);
                movement = "";
            }
        }

        public void TurnChange()
        {
            Turn = !Turn;
        }

        /// <summary>
        /// chequea si una casilla esta siendo jaqueda por el bando de color targetColor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="targetColor"></param> bando bajo posible jaque
        /// <returns></returns>

        public bool IsSquareCheck(int x1, int y1, bool targetColor) //Color es el color del rey (el opuesto del atacante)
        {
            for (int i = Constants.ForStart; i < Constants.Size; i++)
            {
                for (int j = Constants.ForStart; j < Constants.Size; j++)
                {
                    if (IsPieceChecking(i, j, x1, y1, targetColor))
                    {
                        return true;
                    }


                }
            }
            return false;
        }

        public bool IsSquareCheck(int x1, int y1, int x2, int y2, bool targetColor) //Color es el color del rey (el opuesto del atacante)
        {

            for (int i = Constants.ForStart; i < Constants.Size; i++)
            {
                for (int j = Constants.ForStart; j < Constants.Size; j++)
                {
                    if (IsPieceChecking(i, j, x1, y1, targetColor) || IsPieceChecking(i, j, x2, y2, targetColor))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// chequea si Piece puede hacer jaque a Target, según el caso mira si las diagonales y lineas están libres
        /// </summary>
        /// <param name="xPiece"></param>
        /// <param name="yPiece"></param>
        /// <param name="xTarget"></param>
        /// <param name="yTarget"></param>
        /// <param name="targetColor"></param>
        /// <returns></returns>
        public bool IsPieceChecking(int xPiece, int yPiece, int xTarget, int yTarget, bool targetColor)
        {
            var piece1 = GetPiece(xPiece, yPiece);

            if (piece1.Color != targetColor && piece1.IsValidMove(xTarget, yTarget))
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

        /// <summary>
        /// busca en todo el tablero la cordenada x del rey del color pedido
        /// </summary>
        /// <param name="Color"></param> color del rey buscado
        /// <returns></returns>
        public int XKing(bool Color) //Color es el color del Rey que busca casilla por casilla
        {
            int XKing = 0;
            for (int i = Constants.ForStart; i < Constants.Size; i++)
            {
                for (int j = Constants.ForStart; j < Constants.Size; j++)
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
        /// <summary>
        /// busca en todo el tablero la cordenada y del rey del color pedido
        /// </summary>
        /// <param name="Color"></param> color del rey buscado
        /// <returns></returns>
        public int YKing(bool Color) //Color es el color del Rey que busca casilla por casilla
        {
            int YKing = 0;

            for (int i = Constants.ForStart; i < Constants.Size; i++)
            {
                for (int j = Constants.ForStart; j < Constants.Size; j++)
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
        /// <summary>
        /// Chequea si hay jaque mate
        /// </summary>
        /// <param name="Xking"></param>
        /// <param name="YKing"></param>
        /// <param name="KingColor"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Chequea si las casillas alrededor del rey estan en jaque o con fichas aliadas (es para ver el mate o ahogado)
        /// </summary>
        /// <param name="Xking"></param>
        /// <param name="YKing"></param>
        /// <param name="KingColor"></param>
        /// <returns></returns>
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

        /// <summary>
        /// chequea si alguna ficha aliada puede bloquear el jaque
        /// </summary>
        /// <param name="Xking"></param>
        /// <param name="Yking"></param>
        /// <param name="KingColor"></param>
        /// <returns></returns>
        public bool CanAllyBlock(int Xking, int Yking, bool KingColor)
        {
            for (int i = Constants.ForStart; i < Constants.Size; i++)
            {
                for (int j = Constants.ForStart; j < Constants.Size; j++)
                {
                    var pieceA = GetPiece(i, j);

                    if (pieceA.Color == KingColor)
                    {
                        for (int k = Constants.ForStart; k < Constants.Size; k++)
                        {
                            for (int l = Constants.ForStart; l < Constants.Size; l++)
                            {
                                var auxPiece = GetPiece(k, l);
                                MovePieceAndPutEmpty(i, j, k, l);

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

        /// <summary>
        /// si el rey no esta en jaque pero no tiene opciones válidas necesito saber si alguna ficha aliada se puede mover, sino es ahogado
        /// </summary>
        /// <param name="KingColor"></param>
        /// <returns></returns>
        public bool CanOtherPieceMove(bool KingColor)
        {
            for (int i = Constants.ForStart; i < Constants.Size; i++)
            {
                for (int j = Constants.ForStart; j < Constants.Size; j++)
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

        /// <summary>
        /// true si la pieza en x,y tiene al menos un movimiento válido (es para saber si no es ahogado)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool CanPieceMoveOrDraw(int x, int y)
        {
            for (int i = Constants.ForStart; i < Constants.Size; i++)
            {
                for (int j = Constants.ForStart; j < Constants.Size; j++)
                {
                    if (CanMovePiece(x, y, i, j, true)) //va true (osea testea) porque solo quiere saber si se puede mover
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// hace un movimiento de pieza de x1,y1 a x2,y2 y coloca una pieza vacia en x1,y1 (si en x2,y2 habia una vacia coloca eso, sino instancia una emptyPiece nueva)
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>

        public void MovePieceAndPutEmpty(int x1, int y1, int x2, int y2)
        {
            PieceBase auxPosibleEmptyPiece = GetPiece(x2, y2);

            ChessBoard[x2, y2] = ChessBoard[x1, y1]; //pongo pieza de x1,y1 en x2, y2
            ChessBoard[x2, y2].Position.PositionX = x2;
            ChessBoard[x2, y2].Position.PositionY = y2;

            if (auxPosibleEmptyPiece.GetType() == typeof(EmptyPiece))
            {
                ChessBoard[x1, y1] = auxPosibleEmptyPiece; //pongo pieza vacia
                auxPosibleEmptyPiece.Position.PositionX = x1;
                auxPosibleEmptyPiece.Position.PositionY = y1;
            }
            else
            {
                ChessBoard[x1, y1] = new EmptyPiece(true, x1, y1);
            }

        }

        /// <summary>
        /// Method to perform deep clone/copy of a Chess Board.
        /// </summary>
        /// <returns></returns>
        public PieceBase[,] CloneChessBoard()
        {
            var clonedChessBoard = new PieceBase[Constants.Size, Constants.Size];

            for (int i = Constants.ForStart; i < Constants.Size; i++)
            {
                for (int j = Constants.ForStart; j < Constants.Size; j++)
                {
                    if (this.ChessBoard[i, j] != null)
                        clonedChessBoard[i, j] = this.ChessBoard[i, j].Clone() as PieceBase;
                }
            }
            return clonedChessBoard;
        }


        public PieceBase[,] CloneChessBoard2()
        {
            var clonedChessBoard = new PieceBase[Constants.Size, Constants.Size];

            for (int i = Constants.ForStart; i < Constants.Size; i++)
            {
                for (int j = Constants.ForStart; j < Constants.Size; j++)
                {
                    if (this.ChessBoard[i, j] != null)
                    {

                        switch (this.ChessBoard[i, j].GetType().Name)
                        {
                            case nameof(Rook):
                                clonedChessBoard[i, j] = new Rook(ChessBoard[i, j].Color, i, j);
                                break;
                            case nameof(Pawn):
                                clonedChessBoard[i, j] = new Pawn(ChessBoard[i, j].Color, i, j);
                                break;
                            case nameof(Knight):
                                clonedChessBoard[i, j] = new Knight(ChessBoard[i, j].Color, i, j);
                                break;
                            case nameof(Bishop):
                                clonedChessBoard[i, j] = new Bishop(ChessBoard[i, j].Color, i, j);
                                break;

                            case nameof(King):
                                clonedChessBoard[i, j] = new King(ChessBoard[i, j].Color, i, j);
                                break;
                            case nameof(Queen):
                                clonedChessBoard[i, j] = new Queen(ChessBoard[i, j].Color, i, j);
                                break;
                            case nameof(EmptyPiece):
                                clonedChessBoard[i, j] = new EmptyPiece(ChessBoard[i, j].Color, i, j);
                                break;
                        }

                    }
                }
            }
            return clonedChessBoard;
        }

    }
}