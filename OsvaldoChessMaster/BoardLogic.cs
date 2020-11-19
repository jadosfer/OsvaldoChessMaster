namespace OsvaldoChessMaster
{
    using System;
    using System.Collections.Generic;
    using OsvaldoChessMaster.Piece;

    public class BoardLogic
    {
        private int pointer;
        private Stack<string> stackFullPlay;
        private string movement = string.Empty; // se llena con las dos movidas y luego se reinicia

        public Stack<Move> pastMoves = new Stack<Move>();
        public Stack<Move> futureMoves = new Stack<Move>();

        bool IsCheckFlagBU;
        bool IsCheckmateFlagBU;
        bool IsCantMoveCheckFlagBU;

        private static Dictionary<int, string> columnLetters = new Dictionary<int, string>
        {
            { 0, "a" }, { 1, "b" }, { 2, "c" }, { 3, "d" }, { 4, "e" }, { 5, "f" }, { 6, "g" }, { 7, "h" }
        };

        public BoardLogic(bool player1)
        {
            // Inicializacion de variables
            this.stackFullPlay = new Stack<string>();  
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

        public bool FinallyMove(int x1, int y1, int x2, int y2, Board board, bool print = true)
        {
            if (LogicMove(x1, y1, x2, y2, board))
            {
                TurnChange(board);
                Move move = new Move();
                move.x1 = x1;
                move.y1 = y1;
                move.x2 = x2;
                move.y2 = y2;
                pastMoves.Push(move);
                if (print) GameDisplay.PrintBoard(board, this, x1, y1, x2, y2);
                return true;
            }
            return false;
        }

        public Board GoTo(int targetTurn, Board board)
        {
            int head = board.TurnNumber;
            while (head != 0)
            {
                futureMoves.Push(pastMoves.Pop());
                head -= 1;
            }
            board = new Board(board.player1);
            while (head != targetTurn)
            {
                Move move = futureMoves.Pop();                
                FinallyMove(move.x1, move.y1, move.x2, move.y2, board, false);//aca se hace un push en pastMoves
                head += 1;
            }
            return board;
        }
       

        public bool LogicMove(int x1, int y1, int x2, int y2, Board board)
        {
            if (IsInRange(x1, y1, x2, y2))
            {
                PieceBase piece = board.GetPiece(x1, y1);
                if (!board.IsEmpty(x1, y1) && piece.IsValidMove(x1, y1, x2, y2, board.Turn, board))
                {
                    //es Knight
                    if (piece.GetType() == typeof(Knight))
                    {
                        if (board.IsEmpty(x2, y2) && !CantMoveIsCheck(x1, y1, x2, y2, board))
                        {
                            board.Move(x1, y1, x2, y2);
                            return true;
                        }
                        if (!board.IsEmpty(x2, y2) && !CantMoveIsCheck(x1, y1, x2, y2, board))
                        {
                            board.Remove(x2, y2);
                            board.Move(x1, y1, x2, y2);
                            return true;
                        }
                    }
                    else
                    //no es Knight
                    {
                        if (board.IsEmpty(x2, y2) && !IsCastling(x1, y1, x2, board) && !CantMoveIsCheck(x1, y1, x2, y2, board) && !board.IsPawn(x1, y1))
                        {
                            if ((Math.Abs(x2 - x1) == Math.Abs(y2 - y1) && IsDiagonalEmpty(x1, y1, x2, y2, board)) || IsLineEmpty(x1, y1, x2, y2, board))
                            {
                                board.Move(x1, y1, x2, y2);
                                CastlingChanges(x1, y1, x2, y2, piece);
                                UpDateEnPassant(y1, y2, piece, board);
                                return true;
                            }
                        }

                        if (!board.IsEmpty(x2, y2) && !IsAlly(x1, y1, x2, y2, board) && !IsCastling(x1, y1, x2, board) && !CantMoveIsCheck(x1, y1, x2, y2, board) && !board.IsPawn(x1, y1))
                        {
                            if ((Math.Abs(x2 - x1) == Math.Abs(y2 - y1) && IsDiagonalEmpty(x1, y1, x2, y2, board)) || IsLineEmpty(x1, y1, x2, y2, board))
                            {
                                board.Remove(x2, y2);
                                board.Move(x1, y1, x2, y2);
                                CastlingChanges(x1, y1, x2, y2, piece);
                                UpDateEnPassant(y1, y2, piece, board);
                                return true;
                            }
                        }

                        if (board.IsEmpty(x2, y2) && IsCastling(x1, y1, x2, board) && CanCastling(x1, y1, x2, board))
                        {
                            board.Move(x1, y1, x2, y2);
                            piece.CanCastling = false;
                            MoveRookCastling(x1, y1, x2, board);
                            return true;
                        }

                        if (IsPawn(piece) && IsPawnCapturing(x1, x2) && !board.IsEmpty(x2, y2) && !CantMoveIsCheck(x1, y1, x2, y2, board))
                        {
                            board.Remove(x2, y2);
                            board.Move(x1, y1, x2, y2);
                            if (Promoting(y2))
                            {
                                board.Promotion(x2, y2, board.Turn);
                            }
                            return true;
                        }

                        if (IsPawn(piece) && IsPawnCapturing(x1, x2) && EnPassant(x1, y1, x2, y2, board) && !CantMoveIsCheck(x1, y1, x2, y2, board))
                        {
                            board.RemoveCapturePassant(x1, y1, x2, y2);
                            board.Move(x1, y1, x2, y2);
                            return true;
                        }

                        if (IsPawn(piece) && !IsPawnCapturing(x1, x2) && board.IsEmpty(x2, y2) && IsLineEmpty(x1, y1, x2, y2, board) && !CantMoveIsCheck(x1, y1, x2, y2, board))
                        {
                            board.Move(x1, y1, x2, y2);
                            if (Promoting(y2))
                            {
                                board.Promotion(x2, y2, board.Turn);
                            }
                            return true;
                        }
                    }
                }
            }           
                    
            return false;
        }

        public bool IsCastling(int x1, int y1, int x2, Board board)
        {
            if (board.GetPiece(x1, y1).GetType() == typeof(King))
            {
                return Math.Abs(x2 - x1) == 2;
            }            
            return false;
        }

        //parto de la base de que es king porque pasó Iscastling primero
        public bool CanCastling(int x1, int y1, int x2, Board board)
        {
            if (x2 > x1 && y1 == 0 && Board.P1RRookCanCastling && board.GetPiece(x1, y1).CanCastling)
            {
                if (!IsSquareCheck(x1, y1, x1 + 1, y1, board.Turn, board) && !IsSquareCheck(x1 + 2, y1, board.Turn, board))
                {
                    return true;
                }                
            }
            if (x2 < x1 && y1 == 0 && Board.P1LRookCanCastling && board.GetPiece(x1, y1).CanCastling)
            {
                if (!IsSquareCheck(x1, y1, x1 - 1, y1, board.Turn, board) && !IsSquareCheck(x1 - 2, y1, board.Turn, board))
                {
                    return true;
                }
            }
            if (x2 > x1 && y1 == 7 && Board.P2RRookCanCastling && board.GetPiece(x1, y1).CanCastling)
            {
                if (!IsSquareCheck(x1, y1, x1 + 1, y1, board.Turn, board) && !IsSquareCheck(x1 + 2, y1, board.Turn, board))
                {
                    return true;
                }
            }
            if (x2 < x1 && y1 == 7 && Board.P2LRookCanCastling && board.GetPiece(x1, y1).CanCastling)
            {
                if (!IsSquareCheck(x1, y1, x1 - 1, y1, board.Turn, board) && !IsSquareCheck(x1 - 2, y1, board.Turn, board))
                {
                    return true;
                }
            }
            return false;
        }

        //imposibilita enrocar, una vez que se mueve el rey o las torres
        private void CastlingChanges(int x1, int y1, int x2, int y2, PieceBase piece)
        {
            if (piece.GetType() == typeof(King) && piece.CanCastling)
            {
                piece.CanCastling = false;
            }
            if (piece.GetType() == typeof(Rook) && x1 == 0 && y1 == 0)
            {
                Board.P1LRookCanCastling = false;
            }
            if (piece.GetType() == typeof(Rook) && x1 == 7 && y1 == 0)
            {
                Board.P1RRookCanCastling = false;
            }
            if (piece.GetType() == typeof(Rook) && x1 == 0 && y1 == 7)
            {
                Board.P2LRookCanCastling = false;
            }
            if (piece.GetType() == typeof(Rook) && x1 == 7 && y1 == 7)
            {
                Board.P2RRookCanCastling = false;
            }
        }

        public bool CantMoveIsCheck(int x1, int y1, int x2, int y2, Board board)
        {            
            PieceBase pieceAux = board.GetPiece(x2, y2);
            if (pieceAux != null)
            {
                board.Remove(x2, y2);
            }
            board.Move(x1, y1, x2, y2);

            //moví pero no cambió el turno, quiero saber si hay jaque
            bool result = IsSquareCheck(XKing(board.Turn, board), YKing(board.Turn, board), board.Turn, board);
            UndoMove(x1, y1, x2, y2, pieceAux, board);
            if (result == true)
            {
                board.IsCantMoveCheckFlag = true;
                //Console.WriteLine("you can't move there, is check");
            }
                
            return result;
        }
                

        private void MoveRookCastling(int x1, int y1, int x2, Board board)
        {
            if (x2 > x1)
            {
                board.Move(Constants.Size - 1, y1, x2 - 1, y1);
            }
            if (x2 < x1)
            {
                board.Move(Constants.ForStart, y1, x2 + 1, y1);
            }
        }

        //ya doy por sabido que está comiendo y me dice si voy a comer al paso
        public bool EnPassant(int x1, int y1, int x2, int y2, Board board)
        {            
            if (y2 == 2 && board.IsEmpty(x2, y2))
            {
                PieceBase piece = board.GetPiece(x2, y2 + 1);
                if (piece != null && piece.EnPassantAllowed && board.TurnNumber == piece.EnPassantTurnNumber)
                {
                    return true;
                }
            }
            if (y2 == 6 && board.IsEmpty(x2, y2))
            {
                PieceBase piece = board.GetPiece(x2, y2 - 1);
                if (piece != null && piece.EnPassantAllowed && board.TurnNumber == piece.EnPassantTurnNumber)
                {
                    return true;
                }
            }
            return false;
        }

        // pone un flag en el peon para avisar que puede ser comido y anota el turno
        private void UpDateEnPassant(int y1, int y2, PieceBase piece, Board board)
        {
            if (piece.GetType() == typeof(Pawn) && Math.Abs(y2 - y1) == 2) 
            {
                piece.EnPassantAllowed = true;
                piece.EnPassantTurnNumber = board.TurnNumber;
            }
        }

        public bool IsInRange(int x1, int y1, int x2, int y2)
        {
            return (x1 >= Constants.ForStart && x1 < Constants.Size && y1 >= Constants.ForStart && y1 < Constants.Size && x2 >= Constants.ForStart && x2 < Constants.Size && y2 >= Constants.ForStart && y2 < Constants.Size);
        }

        public bool IsColorTurn(PieceBase piece1, Board board) => piece1.Color == board.Turn;
        

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
   
        private bool Promoting(int y2)
        {
            return y2 == 0 || y2 == 7;
        }

        public void TurnChange(Board board)
        {
            IsCheckFlagBU = board.IsCheckFlag;
            board.IsCheckFlag = false;
            board.Turn = !board.Turn;
            board.TurnNumber += 1;
            board.FullMoveNumber = board.TurnNumber / 2;
            IsCheckmateFlagBU = board.IsCheckmateFlag;
            IsCheckFlagBU = board.IsCheckFlag;
            IsCantMoveCheckFlagBU = board.IsCantMoveCheckFlag;

            if (IsCheckmate(XKing(board.Turn, board), YKing(board.Turn, board), board.Turn, board))
            {
                board.IsCheckmateFlag = true;
                //Console.WriteLine("Game Over, check mate");
            }
            if (IsSquareCheck(XKing(board.Turn, board), YKing(board.Turn, board), board.Turn, board))
            {
                board.IsCheckFlag = true;
                //Console.WriteLine("Check!");
            }
            board.IsCantMoveCheckFlag = false;
        }

        public void TurnReverse(Board board)
        {
            board.IsCheckFlag = IsCheckFlagBU;
            board.Turn = !board.Turn;
            board.TurnNumber -= 1;
            board.FullMoveNumber = board.TurnNumber / 2;
            board.IsCheckmateFlag = IsCheckmateFlagBU;
            board.IsCheckFlag = IsCheckFlagBU;
            board.IsCantMoveCheckFlag = IsCantMoveCheckFlagBU;
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

       

        public void UndoMove(int x1, int y1, int x2, int y2, PieceBase removedPiece, Board board)
        {
            board.Move(x2, y2, x1, y1);
            board.PutPiece(removedPiece, x2, y2);
            UndoRookCastling(x1, x2, y2, board);
        }

        public void UndoRookCastling(int x1, int x2, int y2, Board board)
        {
            PieceBase piece = board.GetPiece(x2, y2);
            if (piece != null && piece.GetType() == typeof(King) && Math.Abs(x2 - x1) == 2)
            {
                if (x1 > x2)
                {
                    board.Move(x2 + 1, y2, 0, y2);
                }
                if (x1 < x2)
                {
                    board.Move(x2 - 1, y2, 7, y2);
                }
            }
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
            bool turnoBorrar = board.Turn;
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
            bool turnoBorrar = board.Turn;
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
            bool turnoBorrar = board.Turn;
            var piece1 = board.GetPiece(xPiece, yPiece);

            if (piece1 != null && piece1.Color != targetColor && piece1.IsValidMove(xPiece, yPiece, xTarget, yTarget, !board.Turn, board))
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
            if (IsSquareCheck(Xking, YKing, KingColor, board) && IsAllSquaresAroundCheckorBlocked(Xking, YKing, KingColor, board) && !CanAllyBlock(Xking, YKing, KingColor, board))
            {
                return true;
            }
            return false;
        }

        public bool IsDrawn(int Xking, int YKing, bool KingColor, Board board)
        {
            HashSet<PieceBase> wp = board.WhitePieces; // borrar!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            HashSet<PieceBase> bp = board.BlackPieces; //
            if (board.Turn == KingColor && !IsSquareCheck(Xking, YKing, KingColor, board) && IsAllSquaresAroundCheckorBlocked(Xking, YKing, KingColor, board) && !CanOtherPieceMove(KingColor, board))
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
                foreach (Position position in piece.ValidMoves(board))
                {
                    int k = position.x1;
                    int l = position.y1;
                    if (IsInRange(x1, y1, k, l))
                    {
                        var auxPiece = board.GetPiece(k, l);
                        if (LogicMove(x1, y1, k, l, board)) 
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

            foreach (Position position in piece.ValidMoves(board))
            {
                int i = position.x1;
                int j = position.y1;
                PieceBase pieceRemoved = board.GetPiece(i, j);
                if (LogicMove(x, y, i, j, board)) //va true (osea testea) porque solo quiere saber si se puede mover
                {
                    UndoMove(x, y, i, j, pieceRemoved, board);
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
        public void WriteMove(int x1, int y1, int x2, int y2, PieceBase piece1, bool player1, Board board)
        {
            if (player1 == board.Turn) // para escribir el numero de la jugada
            {
                movement = movement.Insert(pointer, ".");
                movement = movement.Insert(pointer, board.FullMoveNumber.ToString());
                pointer += board.FullMoveNumber.ToString().Length + 1;
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

            if (player1 != board.Turn) // guarda la jugada completa
            {
                stackFullPlay.Push(movement);
                pointer = 0;
                //Console.WriteLine("--------- Esta fue la jugada: " + movement);
                movement = "";
            }
        }
    }
}
