namespace OsvaldoChessMaster
{
    using System;
    using System.Collections.Generic;
    using OsvaldoChessMaster.Piece;

    public class BoardLogic
    {
        private int pointer;
        public bool player1 { get; set; }
        public bool PromotionFlag { get; private set; }

        private Stack<string> stackFullPlay;
        private string movement = string.Empty; // se llena con las dos movidas y luego se reinicia

        public int TurnNumber { get; set; }
        public bool Turn { get; set; } //turn=true es el turno del player1        

        public bool IsCheckmateFlag { get; private set; }

        public bool IsCheckFlag { get; private set; }
        public bool IsCantMoveCheckFlag { get; private set; }
        public bool IsCastlingFlag { get; set; }

        private static Dictionary<int, string> columnLetters = new Dictionary<int, string>
        {
            { 0, "a" }, { 1, "b" }, { 2, "c" }, { 3, "d" }, { 4, "e" }, { 5, "f" }, { 6, "g" }, { 7, "h" }
        };

        public BoardLogic(bool player1)
        {
            // Inicializacion de variables            
            this.Turn = true;
            this.stackFullPlay = new Stack<string>();
            TurnNumber = 1;
            this.player1 = player1;

            Board board = new Board(player1);
        }      


        public bool IsAlly(int x1, int y1, int x2, int y2, Board board) //solo es true si hay otra pieza del mismo color, sino false siempre
        {
            var piece1 = board.GetPiece(x1, y1);
            var piece2 = board.GetPiece(x2, y2);
            if (piece1 != null && piece2 != null)
            {
                return piece1.Color == piece2.Color;
            }
            return false;

        }

        public bool IsInRange(int x1, int y1, int x2, int y2)
        {
            return (x1 >= Constants.ForStart && x1 < Constants.Size && y1 >= Constants.ForStart && y1 < Constants.Size && x2 >= Constants.ForStart && x2 < Constants.Size && y2 >= Constants.ForStart && y2 < Constants.Size);
        }

        public bool IsColorTurn(PieceBase piece1) => piece1.Color == Turn;
        

        public bool IsPawn(PieceBase piece)
        {
            if (piece != null)
            {
                return piece.GetType() == typeof(Pawn);
            }
            return false;
        }

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
        private bool CanMovePawn(int x1, int y1, int x2, int y2, bool testing, Board board)
        {
            try
            {
                var piece1 = board.GetPiece(x1, y1);

                // El peon sube
                if (player1 == Turn && y2 > y1 && piece1.IsValidMove(x1, y1, x2, y2))
                {
                    if (IsPawnCapturing(x1, x2))
                    {
                        if (!IsAlly(x1, y1, x2, y2, board) && !board.IsEmpty(x2, y2))
                        {
                            if (!testing)
                            {
                                PromotionFlag = true;
                            }
                            return true;
                        }
                        if (IsPawn(board.GetPiece(x2, y2 - 1)) && !IsAlly(x1, y1, x2, y2 - 1, board))
                        {
                            if (board.GetPiece(x2, y2 - 1) != null && board.GetPiece(x2, y2 - 1).CapturableByTheWay && TurnNumber - board.GetPiece(x2, y2 - 1).turnNumberCapturableByTheWay < 2)
                            {
                                if (!testing)
                                {
                                    //come al paso
                                    board.PutEmpty(x2, y2 - 1); // pongo la pieza vacía                                    
                                }
                                return true;
                            }
                        }
                    }
                    else //no come
                    {
                        if (Math.Abs(y2 - y1) == 1 && board.IsEmpty(x2, y2)) //sube 1 casillero
                        {
                            if (!testing)
                            {
                                PromotionFlag = true;
                            }
                            return true;
                        }
                        if (board.IsEmpty(x2, y2 - 1) && board.IsEmpty(x2, y2)) //sube 2 casilleros chequea 2 vacios
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
                if (player1 != Turn && y2 < y1 && piece1.IsValidMove(x1, y1, x2, y2))
                {

                    if (IsPawnCapturing(x1, x2))
                    {

                        if (!IsAlly(x1, y1, x2, y2, board) && !board.IsEmpty(x2, y2))
                        {
                            if (!testing)
                            {
                                board.MovePromotion(x2, y2, Turn);
                            }

                            return true;
                        }

                        if (!board.IsEmpty(x2, y2 + 1) && !IsAlly(x1, y1, x2, y2 + 1, board))
                        {
                            if (board.GetPiece(x2, y2 + 1) != null && board.GetPiece(x2, y2 + 1).CapturableByTheWay && TurnNumber - board.GetPiece(x2, y2 + 1).turnNumberCapturableByTheWay < 2)
                            {
                                if (!testing)
                                {
                                    //come al paso
                                    board.PutEmpty(x2, y2 + 1); //pongo pieza vacia                                    
                                }
                                return true;
                            }
                        }
                    }
                    //no come
                    else
                    {
                        if (Math.Abs(y2 - y1) == 1 && board.IsEmpty(x2, y2)) //baja 1 casillero
                        {
                            if (!testing)
                            {
                                board.MovePromotion(x2, y2, Turn);
                            }
                            return true;
                        }
                        if (board.IsEmpty(x2, y2 + 1) && board.IsEmpty(x2, y2)) //baja 2 casilleros chequea 2 vacios
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
                if (!piece1.IsValidMove(x1, y1, x2, y2))
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
        /// Hace todas las comprobaciones y mueve la pieza en x1,y1 al destino x2,y2. Si el movimiento fue de un rey a enrocar, agrega ademas el movimiento respectivo de la torre
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public bool FinallyMove(int x1, int y1, int x2, int y2, Board board)
        {
            //int Prueba = ChessBoard[0, 6].Position.PositionY;
            HashSet<PieceBase> wp = board.WhitePieces;  // borrar!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            HashSet<PieceBase> bp = board.BlackPieces;  // borrar!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            if (CanMovePiece(x1, y1, x2, y2, false, board)) //false porque no está testeando, sino que hay orden de mover
            {
                //baja 3 flags
                IsCheckFlag = false;
                IsCantMoveCheckFlag = false;
                IsCheckmateFlag = false;

                var piece1 = board.GetPiece(x1, y1);
                MovePieceAndPutEmpty(x1, y1, x2, y2, board);
                if (PromotionFlag)
                {
                    board.MovePromotion(x2, y2, Turn);
                    PromotionFlag = false;
                }

                if (IsPieceChecking(x2, y2, XKing(Turn, board), YKing(Turn, board), Turn, board))
                {
                    IsCheckFlag = true;
                    ScriptBoard.flag3 = true;
                }

                if (piece1.GetType() == typeof(King) && piece1.CanCastling)
                {
                    piece1.CanCastling = false;
                    //Console.WriteLine("Ya no puede enrocar porque movio rey");                    
                    return true;
                }

                if (piece1.GetType() == typeof(Rook) && piece1.CanCastling)
                {
                    piece1.CanCastling = false;
                    //Console.WriteLine("Ya no puede enrocar porque movió la torre");                    
                    return true;
                }

                //WriteMove(x1, y1, x2, y2, piece1, player1);

                //coordenadas del rey del player que no mueve
                int XKING = XKing(!piece1.Color, board);
                int YKING = YKing(!piece1.Color, board);
                if (IsCheckmate(XKING, YKING, !piece1.Color, board))  // jaque mate
                {
                    //Console.WriteLine("CHECKMATE: JAQUE MATE!!!!!!!!!!!!   --------fin de juego----------");
                    IsCheckmateFlag = true;
                    ScriptBoard.flag5 = true;
                    return true;
                }
                // Ahogado veo si despues de mover el otro queda ahogado
                else
                {
                    return !IsDrawn(XKING, YKING, !piece1.Color, board);
                }

                if (Turn == player1)
                {
                    TurnNumber++; //TurnNumber se compone de dos movidas
                }
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
        public bool IsLineEmpty(int x1, int y1, int x2, int y2, Board board) // chequea 
        {
            if (x1 == x2)
            {
                if (Math.Abs(y1 - y2) > 1)
                {
                    for (int i = 1; i < Math.Abs(y1 - y2); i++)
                    {
                        if (y1 < y2)
                        {
                            if (!board.IsEmpty(x1, y1 + i))
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (!board.IsEmpty(x1, y1 - i))
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
                        if (!board.IsEmpty(x1 + i, y1))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (!board.IsEmpty(x1 - i, y1))
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
        public bool IsDiagonalEmpty(int x1, int y1, int x2, int y2, Board board)
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
                            if (!board.IsEmpty(x1 + i, y1 + i))
                            {
                                abort = true;
                            }
                        }
                        if (y1 > y2 && x1 < x2)
                        {
                            if (!board.IsEmpty(x1 + i, y1 - i))
                            {
                                abort = true;
                            }
                        }
                        if (y1 < y2 && x1 > x2)
                        {
                            if (!board.IsEmpty(x1 - i, y1 + i))
                            {
                                abort = true;
                            }
                        }
                        if (y1 > y2 && x1 > x2)
                        {
                            if (!board.IsEmpty(x1 - i, y1 - i))
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
        public bool CanMovePiece(int x1, int y1, int x2, int y2, bool testing, Board board)
        {
            try
            {
                var piece1 = board.GetPiece(x1, y1);

                if (piece1 == null || !IsInRange(x1, y1, x2, y2) || !IsColorTurn(piece1))
                {
                    return false;
                }

                var piece2 = board.GetPiece(x2, y2);
                if (piece2 != null && piece1.Color == piece2.Color)
                {
                    return false;
                }

                // esto y el if son para ver si quedas en jaque  
                BoardLogic boardPruebaAnterior = this;
                MovePieceAndPutEmpty(x1, y1, x2, y2, board);

                if (IsSquareCheck(XKing(piece1.Color, board), YKing(piece1.Color, board), piece1.Color, board))
                {
                    ScriptBoard.flag4 = true;
                    //vuelvo atras el movimiento
                    UndoMove(x1, y1, x2, y2, piece2, board);

                    IsCantMoveCheckFlag = true;
                    //Console.WriteLine("Cant Move, is check");

                    //quedas en jaque entonces es invalido el movimiento
                    return false;
                }
                else
                {   //igualmente vuelvo atras el movimiento
                    UndoMove(x1, y1, x2, y2, piece2, board);

                }

                if (piece1.CanJump && piece1.IsValidMove(x1, y1, x2, y2))
                {
                    return true;
                }

                if (IsPawn(piece1))
                {
                    return CanMovePawn(x1, y1, x2, y2, testing, board);
                }
                else
                {
                    if (piece1.GetType() == typeof(King) && IsSquareCheck(x2, y2, piece1.Color, board)) // no lo deja mover al lugar en jaque
                    {
                        //Console.WriteLine("No podes moverte ahí, quedas en jaque");
                        IsCantMoveCheckFlag = true;
                        return false;
                    }

                    // enroque largo                                                                                 mov horiz   ---arriba o abajo---    -----------casilleros vacios entre rey y torre?-------------    fue movido el rey antes?    -------------estan jaqueadas las casillas entre torre y rey??? x2 = 2 (a donde va el rey ya se que no, porque  lo chequeo antes)
                    if (piece1.GetType() == typeof(King) && x1 == 4 && x2 == 2 && y1 == y2 && (y1 == Constants.LowerRow || y1 == Constants.UpperRow) && board.IsEmpty(x2 - 1, y2) && board.IsEmpty(x2, y2) && board.IsEmpty(x2 + 1, y2) && piece1.CanCastling && !IsSquareCheck(1, y1, 3, y1, piece1.Color, board) && board.ChessBoard[x2 - 2, y2] != null && board.ChessBoard[x2 - 2, y2].CanCastling)
                    {
                        if (!testing)
                        {
                            piece1.CanCastling = false; // ya no va a poder enrocar mas (a la torre no hace falta)
                            piece1.LongCastling = true; // esto es para escribir la jugada                             
                        }

                        int XKING = XKing(piece1.Color, board); //coordenadas del rey del player que mueve
                        int YKING = YKing(piece1.Color, board);
                        if (IsSquareCheck(XKING, YKING, piece1.Color, board))
                        {
                            return false;
                        }

                        if (!testing)
                        {
                            MovePieceAndPutEmpty(0, y1, x2 + 1, y2, board);
                            IsCastlingFlag = true;
                            TurnChange();
                        }
                        return true;
                    }

                    // enroque corto

                    if (piece1.GetType() == typeof(King) && x1 == 4 && x2 == 6 && y1 == y2 && (y1 == Constants.LowerRow || y1 == Constants.UpperRow) && board.IsEmpty(x2 - 1, y2) && board.IsEmpty(x2, y2) && piece1.CanCastling && !IsSquareCheck(5, y1, piece1.Color, board) && board.ChessBoard[x2 + 1, y2] != null && board.ChessBoard[x2 + 1, y2].CanCastling) // la posicion x2=6 es donde va el rey y ya se que no va a haber jaque(ya chequeado arriba)
                    {

                        if (!testing)
                        {
                            piece1.CanCastling = false; // ya no va a poder enrocar mas
                            piece1.ShortCastling = true; // esto es para escribir la jugada
                        }

                        int XKING = XKing(piece1.Color, board); //coordenadas del rey del player que mueve
                        int YKING = YKing(piece1.Color, board);
                        if (IsSquareCheck(XKING, YKING, piece1.Color, board))
                        {
                            if (!testing)
                            {
                                //Console.WriteLine("Movimiento Invalido, SU REY ESTA EN JAQUE!!!!!!!!!!!!");
                                //Console.WriteLine("Movimiento Invalido, SU REY ESTA EN JAQUE!!!!!!!!!!!!");
                            }
                            return false;
                        }

                        if (!testing)
                        {
                            MovePieceAndPutEmpty(7, y1, x1 + 1, y2, board);
                            IsCastlingFlag = true;
                            TurnChange();
                        }
                        return true;
                    }

                    if (!piece1.CanJump && (x1 == x2 || y1 == y2) && piece1.IsValidMove(x1, y1, x2, y2) && IsLineEmpty(x1, y1, x2, y2, board))
                    {
                        return true;
                    }

                    return (!piece1.CanJump && x1 != x2 && y1 != y2 && piece1.IsValidMove(x1, y1, x2, y2) && IsDiagonalEmpty(x1, y1, x2, y2, board));
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
        /// hace un movimiento de pieza de x1,y1 a x2,y2 y coloca una pieza vacia en x1,y1 (si en x2,y2 habia una vacia coloca eso, sino instancia una emptyPiece nueva)
        /// </summary>           
        public void MovePieceAndPutEmpty(int x1, int y1, int x2, int y2, Board board)
        {
            if (IsInRange(x1, y1, x2, y2) && board.ChessBoard[x1, y1] != null)// en rango y piece1 no nula
            {
                if (board.ChessBoard[x2, y2] != null && board.ChessBoard[x2, y2].Color != board.ChessBoard[x1, y1].Color)
                {
                    //capturo una blanca
                    if (board.ChessBoard[x2, y2].Color)
                    {
                        foreach (PieceBase piece in board.WhitePieces)
                        {
                            if (piece.Position.PositionX == x2 && piece.Position.PositionY == y2)
                            {
                                int count1 = board.WhitePieces.Count;
                                board.WhitePieces.Remove(piece);
                                int count2 = board.WhitePieces.Count;
                                break;
                            }
                        }
                    }
                    //capturo una negra
                    else if (board.ChessBoard[x2, y2] != null)
                    {
                        foreach (PieceBase piece in board.WhitePieces)
                        {
                            if (piece.Position.PositionX == x2 && piece.Position.PositionY == y2)
                            {
                                int count1 = board.BlackPieces.Count;
                                board.BlackPieces.Remove(piece);
                                int count2 = board.BlackPieces.Count;
                                break;
                            }
                        }                                       
                    }
                }
                board.ChessBoard[x2, y2] = board.ChessBoard[x1, y1]; //pongo pieza de x1,y1 en x2, y2                

                if (board.ChessBoard[x2, y2] != null && board.ChessBoard[x2, y2].Color)
                {
                    foreach (PieceBase piece in board.WhitePieces)
                    {
                        if (piece.Position.PositionX == x1 && piece.Position.PositionY == y1)
                        {
                            board.WhitePieces.Remove(piece);
                            board.ChessBoard[x2, y2].Position.PositionX = x2;
                            board.ChessBoard[x2, y2].Position.PositionY = y2;
                            board.WhitePieces.Add(board.ChessBoard[x2, y2]);
                            break;
                        }
                    }
                }
                else if (board.ChessBoard[x2, y2] != null)
                {
                    foreach (PieceBase piece in board.BlackPieces)
                    {
                        if (piece.Position.PositionX == x1 && piece.Position.PositionY == y1)
                        {
                            board.BlackPieces.Remove(piece);
                            board.ChessBoard[x2, y2].Position.PositionX = x2;
                            board.ChessBoard[x2, y2].Position.PositionY = y2;
                            board.BlackPieces.Add(board.ChessBoard[x2, y2]);
                            break;
                        }
                    }
                }

                board.ChessBoard[x1, y1] = null;
                TurnChange();
            }
        }

        public void UndoMove(int x1, int y1, int x2, int y2, PieceBase auxPiece, Board board)
        {
            PieceBase borrarPiecePrueba = board.ChessBoard[x1, y1];
            PieceBase borrarPiecePrueba2 = board.ChessBoard[x2, y2];

            var hashSetW = board.WhitePieces;
            var hashSetB = board.BlackPieces;
            if (IsInRange(x1, y1, x2, y2))
            {
                if (board.ChessBoard[x2, y2] != null)
                {
                    BoardLogic Auxil = this;
                    board.ChessBoard[x1, y1] = board.ChessBoard[x2, y2];
                    board.ChessBoard[x1, y1].Position.PositionX = x1;
                    board.ChessBoard[x1, y1].Position.PositionY = y1;
                    board.ChessBoard[x2, y2] = auxPiece;

                    if (auxPiece != null)
                    {
                        board.ChessBoard[x2, y2].Position.PositionX = x2;
                        board.ChessBoard[x2, y2].Position.PositionY = y2;
                        var hashSet = auxPiece.Color ? board.WhitePieces : board.BlackPieces;
                        PieceBase PruebaPiece = board.ChessBoard[x2, y2];
                        if (hashSet.Count < 16) //esto hay que corregirlo. lo hago porque me agrega piezas de mas indefinidamente y no encuentro cuando
                        {
                            hashSet.Add(auxPiece);
                        }
                    }
                    TurnChange();
                }
                else
                {
                    Console.WriteLine("esatba nula x2,y2 en el undo, error" + " x1:" + x1 + " y1:" + y1 + " x2:" + x2 + " y2:" + y2);
                }
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
        public bool IsSquareCheck(int x1, int y1, bool targetColor, Board board) //Color es el color del rey (el opuesto del atacante)
        {
            HashSet<PieceBase> BackupWhitePieces = board.CloneWhitePieces();
            HashSet<PieceBase> BackupBlackPieces = board.CloneBlackPieces();
            var hashSet = !targetColor ? BackupWhitePieces : BackupBlackPieces;
            foreach (PieceBase piece in hashSet)
            {
                int i = piece.Position.PositionX;
                int j = piece.Position.PositionY;
                if (IsPieceChecking(i, j, x1, y1, targetColor, board))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsSquareCheck(int x1, int y1, int x2, int y2, bool targetColor, Board board) //Color es el color del rey (el opuesto del atacante) hay dos pares porque puede chequear dos a la vez
        {
            HashSet<PieceBase> BackupWhitePieces = board.CloneWhitePieces();
            HashSet<PieceBase> BackupBlackPieces = board.CloneBlackPieces();
            var hashSet = !targetColor ? BackupWhitePieces : BackupBlackPieces;
            foreach (PieceBase piece in hashSet)
            {
                int i = piece.Position.PositionX;
                int j = piece.Position.PositionY;
                if (IsPieceChecking(i, j, x1, y1, targetColor, board) || IsPieceChecking(i, j, x2, y2, targetColor, board))
                {
                    return true;
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
        public bool IsPieceChecking(int xPiece, int yPiece, int xTarget, int yTarget, bool targetColor, Board board)
        {
            var piece1 = board.GetPiece(xPiece, yPiece);

            if (piece1 != null && piece1.Color != targetColor && piece1.IsValidMove(xPiece, yPiece, xTarget, yTarget))
            {
                if (IsPawn(piece1))
                {
                    if (IsPawnCapturing(xPiece, xTarget))
                    {
                        return true;
                    }
                    return false;
                }
                if (Math.Abs(xTarget - xPiece) == Math.Abs(yTarget - yPiece) && IsDiagonalEmpty(xPiece, yPiece, xTarget, yTarget, board))
                {
                    return true;
                }
                if ((xTarget == xPiece || yTarget == yPiece) && IsLineEmpty(xPiece, yPiece, xTarget, yTarget, board))
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
        /// busca en todo el hashSet la cordenada "x" del rey del color pedido
        /// </summary>
        /// <param name="Color"></param> color del rey buscado
        /// <returns></returns>


        public int XKing(bool Color, Board board) //Color es el color del Rey que busca casilla por casilla
        {
            HashSet<PieceBase> BackupWhitePieces = board.CloneWhitePieces();
            HashSet<PieceBase> BackupBlackPieces = board.CloneBlackPieces();
            var hashSet = Color ? BackupWhitePieces : BackupBlackPieces;


            foreach (PieceBase piece in hashSet)
            {
                if (piece.GetType() == typeof(King))
                {
                    return piece.Position.PositionX;
                }
            }
            return -1;
        }

        /// <summary>
        /// busca en todo el hashSet la cordenada "y" del rey del color pedido
        /// </summary>
        /// <param name="Color"></param> color del rey buscado
        /// <returns></returns>
        public int YKing(bool Color, Board board) //Color es el color del Rey que busca casilla por casilla
        {
            HashSet<PieceBase> BackupWhitePieces = board.CloneWhitePieces();
            HashSet<PieceBase> BackupBlackPieces = board.CloneBlackPieces();
            var hashSet = Color ? BackupWhitePieces : BackupBlackPieces;

            foreach (PieceBase piece in hashSet)
            {
                if (piece.GetType() == typeof(King))
                {
                    return piece.Position.PositionY;
                }
            }
            return -1;
        }
        /// <summary>
        /// Chequea si hay jaque mate
        /// </summary>
        /// <param name="Xking"></param>
        /// <param name="YKing"></param>
        /// <param name="KingColor"></param>
        /// <returns></returns>
        public bool IsCheckmate(int Xking, int YKing, bool KingColor, Board board)
        {
            if (IsSquareCheck(Xking, YKing, KingColor, board) && IsAllSquaresAroundCheckorBlocked(Xking, YKing, KingColor, board) && CanAllyBlock(Xking, YKing, KingColor, board))
            {
                return true;
            }
            return false;
        }

        public bool IsDrawn(int Xking, int YKing, bool KingColor, Board board)
        {
            HashSet<PieceBase> wp = board.WhitePieces; // borrar!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            HashSet<PieceBase> bp = board.BlackPieces; //
            if (Turn == KingColor && !IsSquareCheck(Xking, YKing, KingColor, board) && IsAllSquaresAroundCheckorBlocked(Xking, YKing, KingColor, board) && !CanOtherPieceMove(KingColor, board))
            {
                Console.WriteLine("Is Drawn");
                return true;
            }
            return false;
        }

        /// <summary>
        /// Chequea si las casillas alrededor del rey estan en jaque o con fichas aliadas (es para ver el mate o ahogado)
        /// </summary>
        /// <param name="Xking"></param>
        /// <param name="YKing"></param>
        /// <param name="KingColor"></param>
        /// <returns></returns>
        public bool IsAllSquaresAroundCheckorBlocked(int Xking, int YKing, bool KingColor, Board board)
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (!IsSquareCheck(Xking + i, YKing + j, KingColor, board) && i != Xking && j != YKing && board.IsEmpty(Xking + i, YKing + j))
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
        public bool CanAllyBlock(int Xking, int Yking, bool KingColor, Board board)
        {
            HashSet<PieceBase> BackupWhitePieces = board.CloneWhitePieces();
            HashSet<PieceBase> BackupBlackPieces = board.CloneBlackPieces();
            var hashSet = KingColor ? BackupWhitePieces : BackupBlackPieces;
            foreach (PieceBase piece in hashSet)
            {
                int x1 = piece.Position.PositionX;
                int y1 = piece.Position.PositionY;
                foreach (Position position in piece.ValidMoves(this))
                {
                    int k = position.x1;
                    int l = position.y1;
                    if (IsInRange(x1, y1, k, l))
                    {
                        var auxPiece = board.GetPiece(k, l);
                        if (FinallyMove(x1, y1, k, l, board))
                        {
                            if (!IsSquareCheck(Xking, Yking, KingColor, board))
                            {
                                UndoMove(x1, y1, k, l, auxPiece, board);
                                return true;
                            }
                            UndoMove(x1, y1, k, l, auxPiece, board);
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
        public bool CanOtherPieceMove(bool KingColor, Board board)
        {
            HashSet<PieceBase> BackupWhitePieces = board.CloneWhitePieces();
            HashSet<PieceBase> BackupBlackPieces = board.CloneBlackPieces();
            var hashSet = KingColor ? BackupWhitePieces : BackupBlackPieces;
            foreach (PieceBase piece in hashSet)
            {
                int x1 = piece.Position.PositionX;
                int y1 = piece.Position.PositionY;

                // el if es importante porque quizas fue comida esa pieza y ahí ahora hay otra

                if (piece.GetType() != typeof(King))
                {
                    if (CanPieceMoveOrDraw(x1, y1, piece, board))
                    {
                        return true;
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
        public bool CanPieceMoveOrDraw(int x, int y, PieceBase piece, Board board)
        {

            foreach (Position position in piece.ValidMoves(this))
            {
                int i = position.x1;
                int j = position.y1;
                if (CanMovePiece(x, y, i, j, true, board)) //va true (osea testea) porque solo quiere saber si se puede mover
                {
                    return true;
                }
            }
            return false;
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
    }
}
