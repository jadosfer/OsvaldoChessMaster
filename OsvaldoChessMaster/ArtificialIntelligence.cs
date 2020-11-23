﻿using System;
using System.Collections.Generic;
using OsvaldoChessMaster.Piece;

namespace OsvaldoChessMaster
{
    public class ArtificialIntelligence
    {
        public double[,] pawnPositionValues = new double[Constants.Size, Constants.Size];
        public double[,] knightPositionValues = new double[Constants.Size, Constants.Size];
        public double[,] bishopPositionValues = new double[Constants.Size, Constants.Size];
        public double[,] rookPositionValues = new double[Constants.Size, Constants.Size];
        public double[,] queenPositionValues = new double[Constants.Size, Constants.Size];
        public double[,] kingPositionValues = new double[Constants.Size, Constants.Size];

        PieceBase[,] ChessBoardBU; //backups para restore
        HashSet<PieceBase> WhitePiecesBU;
        HashSet<PieceBase> BlackPiecesBU;
        bool IsCheckFlagBU;
        bool IsCheckmateFlagBU;
        bool IIsCantMoveCheckFlagBU;

        PieceBase[,] ChessBoardBU2; //backups para restore2
        HashSet<PieceBase> WhitePiecesBU2;
        HashSet<PieceBase> BlackPiecesBU2;
        bool IsCheckFlagBU2;
        bool IsCheckmateFlagBU2;
        bool IIsCantMoveCheckFlagBU2;

        public ArtificialIntelligence(Board board)
        {
            pawnPositionValues = PawnPositionValues();
            knightPositionValues = KnightPositionValues();
            bishopPositionValues = BishopPositionValues();
            rookPositionValues = RookPositionValues();
            queenPositionValues = QueenPositionValues();
            kingPositionValues = KingPositionValues();
        }
        // devuelve una movida que maximiza el score y el score
        public double[] Maximize(Board board, BoardLogic boardLogic, int depth) 
        {            
            double maxScore = -10000;
            double[] result = new double[] { 0, 0, 0, 0, EvaluateBoard(board) };
            if (Math.Abs(EvaluateBoard(board)) > 500 || depth > Constants.depthLimit)
            {
                return result;
            }
            HashSet<PieceBase> BackupWhitePiecesAPP = board.CloneWhitePieces();
            HashSet<PieceBase> BackupBlackPiecesAPP = board.CloneBlackPieces();
            var hashSet = board.Turn ? BackupWhitePiecesAPP : BackupBlackPiecesAPP;
            foreach (PieceBase piece in hashSet)
            {
                board = boardLogic.GoTo(board.TurnNumber, board);
                int x = piece.Position.PositionX;
                int y = piece.Position.PositionY;
                foreach (Position position in piece.ValidMoves(board))
                {
                    if (boardLogic.FinallyMove(x, y, position.x1, position.y1, board, boardLogic, false))
                    {
                        double[] minimized = Minimize(board, boardLogic, depth + 1);
                        double Eval = minimized[4];                        
                        if (Eval >= maxScore)
                        {
                            maxScore = Eval;
                            result[0] = x;
                            result[1] = y;
                            result[2] = position.x1;
                            result[3] = position.y1;
                            result[4] = maxScore;
                        }
                        // back to previous board
                        board = boardLogic.GoTo(board.TurnNumber - 1, board);
                        boardLogic.futureMoves.Clear();
                    }                    
                }
            }
            return result;
        }

        // devuelve una movida que minimiza el score y el score
        public double[] Minimize(Board board, BoardLogic boardLogic, int depth)
        {            
            double minScore = 10000;
            double[] result = new double[] { 0, 0, 0, 0, EvaluateBoard(board) };
            if (Math.Abs(EvaluateBoard(board)) > 500 || depth > Constants.depthLimit)
            {
                result = new double[] { 0, 0, 0, 0, EvaluateBoard(board) };
                return result;
            }
            HashSet<PieceBase> BackupWhitePiecesAPP = board.CloneWhitePieces();
            HashSet<PieceBase> BackupBlackPiecesAPP = board.CloneBlackPieces();
            var hashSet = board.Turn ? BackupWhitePiecesAPP : BackupBlackPiecesAPP;
            foreach (PieceBase piece in hashSet)
            {
                board = boardLogic.GoTo(board.TurnNumber, board);
                int x = piece.Position.PositionX;
                int y = piece.Position.PositionY;
                foreach (Position position in piece.ValidMoves(board))
                {
                    if (boardLogic.FinallyMove(x, y, position.x1, position.y1, board, boardLogic, false))
                    {
                        double[] maximized = Maximize(board, boardLogic, depth + 1);
                        double Eval = maximized[4];
                        if (Eval <= minScore)
                        {
                            minScore = Eval;
                            result[0] = x;
                            result[1] = y;
                            result[2] = position.x1;
                            result[3] = position.y1;
                            result[4] = minScore;
                        }
                        // back to previous board
                        board = boardLogic.GoTo(board.TurnNumber - 1, board);
                        boardLogic.futureMoves.Clear();
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// devuelve un array con todas las movidas posibles que mejoran el puntaje de un estado del tablero
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public List<Move> AllPosiblePlays(Board board, BoardLogic boardLogic)
        {
            double actualValue = EvaluateBoard(board);
            List<Move> AllMoves = new List<Move>();

            HashSet<PieceBase> BackupWhitePiecesAPP = board.CloneWhitePieces();
            HashSet<PieceBase> BackupBlackPiecesAPP = board.CloneBlackPieces();
            var hashSet = board.Turn ? BackupWhitePiecesAPP : BackupBlackPiecesAPP;
            foreach (PieceBase piece in hashSet)
            {
                bool TurnShow = board.Turn;
                AllMoves = AllPosiblePiecePlays(board, boardLogic, piece, AllMoves, actualValue);
            }
            if (AllMoves != null)
            {
                return AllMoves;
            }
            Console.WriteLine("Algo raro, falla AllPosiblePlays");
            AllMoves.Add(BestResponse(board, boardLogic));

            return AllMoves;
        }

        public List<Move> AllPosiblePiecePlays(Board board, BoardLogic boardLogic, PieceBase piece, List<Move> AllMoves, double actualValue)
        {
            board = boardLogic.GoTo(board.TurnNumber, board);            
            int x = piece.Position.PositionX;
            int y = piece.Position.PositionY;

            foreach (Position position in piece.ValidMoves(board))
            {
                if (boardLogic.FinallyMove(x, y, position.x1, position.y1, board, boardLogic, false))
                {
                    // solo lo guardo si no empeora la situacion (cuanto ams negativo mejor para la pc
                    double Eval = EvaluateBoard(board);

                    if (Eval <= actualValue)
                    {
                        //actualValue = EvaluateBoard(board);
                        AllMoves.Add(new Move { x1 = x, y1 = y, x2 = position.x1, y2 = position.y1 });
                    }
                    // back to previous board
                    board = boardLogic.GoTo(board.TurnNumber - 1, board);
                    boardLogic.futureMoves.Clear();
                }
                //}
            }
            return AllMoves;
        }


        /// <summary>
        /// devuelve la movida que mas aumenta el puntaje de un estado del tablero (o si es negativo que lo hace menos negativo) Es un metodo para simular la movida del humano
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public Move BestResponse(Board board, BoardLogic boardLogic)
        {
            bool Turno = board.Turn;
            board = boardLogic.GoTo(board.TurnNumber, board);
            double actualValue = EvaluateBoard(board);
            Move response = new Move();
            Move responseNotNull = new Move();

            HashSet<PieceBase> BackupWhitePieces = board.CloneWhitePieces();
            HashSet<PieceBase> BackupBlackPieces = board.CloneBlackPieces();
            var hashSet = board.Turn ? BackupWhitePieces : BackupBlackPieces;
            foreach (PieceBase piece in hashSet)
            {
                int i = piece.Position.PositionX;
                int j = piece.Position.PositionY;
                foreach (Position position in piece.ValidMoves(board))
                {

                    var removedPiece = board.GetPiece(position.x1, position.y1);                    
                    if (boardLogic.FinallyMove(i, j, position.x1, position.y1, board, boardLogic, false))                        
                    {                        
                        responseNotNull.x1 = i;
                        responseNotNull.y1 = j;
                        responseNotNull.x2 = position.x1;
                        responseNotNull.y2 = position.y1;

                        // si movió el de abajo
                        if (board.player1 != board.Turn) // va != porque el finally me lo acaba de cambiar a turn
                        {   // guardo la mejor movida
                            double Eval = EvaluateBoard(board);
                            if (Eval >= actualValue)
                            {
                                actualValue = Eval;
                                response.x1 = i;
                                response.y1 = j;
                                response.x2 = position.x1;
                                response.y2 = position.y1;
                            }
                        }
                        else
                        {   // mueve el de arriba
                            if (EvaluateBoard(board) <= actualValue)
                            {
                                actualValue = EvaluateBoard(board);
                                response.x1 = i;
                                response.y1 = j;
                                response.x2 = position.x1;
                                response.y2 = position.y1;
                            }
                        }
                        // back to previous board
                        board = boardLogic.GoTo(board.TurnNumber - 1, board);
                        boardLogic.futureMoves.Clear();
                    }
                }
            }

            if (response != null)
            {
                return response;
            }
            Console.WriteLine("Algo raro, falla bestResponse");
            return responseNotNull;
        }

        /// <summary>
        /// devuelve la mejor jugada que debe hacer la compu tras probar 4 movidas hacia adelante. Metodo minimax de los rústicos
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public Move BestComputerMoveDepth4(Board board, BoardLogic boardLogic)
        {
            bool Turno = board.Turn;
            board = boardLogic.GoTo(board.TurnNumber, board);            
            double value = EvaluateBoard(board);
            Console.WriteLine("calculó EvaluateBoard");
            Move BestComputerMove = null;
            List<Move> allMove1 = AllPosiblePlays(board, boardLogic);
            Console.WriteLine("calculó las AllPosiblePlays");
            foreach (Move move1 in allMove1)
            {                
                if (boardLogic.FinallyMove(move1.x1, move1.y1, move1.x2, move1.y2, board, boardLogic, false))
                {
                    Move moveResponse = BestResponse(board, boardLogic);
                    boardLogic.FinallyMove(moveResponse.x1, moveResponse.y1, moveResponse.x2, moveResponse.y2, board, boardLogic, false);

                    Console.WriteLine("movio y calculo la Best Response");
                    List<Move> allMove3 = AllPosiblePlays(board, boardLogic);
                    Console.WriteLine("calculó las AllPosiblePlays luego de mover y repsonder");

                    foreach (Move move3 in allMove3)
                    {
                        if (boardLogic.FinallyMove(move3.x1, move3.y1, move3.x2, move3.y2, board, boardLogic, false))
                        {
                            Move moveResponse2 = BestResponse(board, boardLogic);
                            boardLogic.FinallyMove(moveResponse2.x1, moveResponse2.y1, moveResponse2.x2, moveResponse2.y2, board, boardLogic, false);

                            double Eval = EvaluateBoard(board);
                            if (Eval <= value)
                            {
                                value = EvaluateBoard(board);
                                BestComputerMove = move1; //me quedo con la move1 que produce la menor bestresponse
                            }
                            // back to previous board
                            board = boardLogic.GoTo(board.TurnNumber - 2, board);
                            boardLogic.futureMoves.Clear();
                        }
                    }

                    board = boardLogic.GoTo(board.TurnNumber - 2, board);
                    boardLogic.futureMoves.Clear();
                }
            }
            if (BestComputerMove != null)
            {
                return BestComputerMove;
            }
            Console.WriteLine("Algo raro, falla BestComputerMoveDepth4");
            return BestResponse(board, boardLogic);
        }

        /// <summary>
        /// calcual el puntaje de un estado del tablero (player1 positivo, compu negativo)
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public double EvaluateBoard(Board board)
        {
            double sum = 0;
            //var hashSet = KingColor ? WhitePieces : BlackPieces;
            foreach (var piece in board.WhitePieces)
            {
                int i = piece.Position.PositionX;
                int j = piece.Position.PositionY;

                if (piece != null)
                {
                    switch (piece.GetType().Name)
                    {
                        case nameof(Pawn):
                            sum += 10 + pawnPositionValues[i, j];
                            break;
                        case nameof(Knight):
                            sum += 30 + knightPositionValues[i, j];
                            break;
                        case nameof(Bishop):
                            sum += 30 + bishopPositionValues[i, j];
                            break;
                        case nameof(Rook):
                            sum += 50 + rookPositionValues[i, j];
                            break;
                        case nameof(Queen):
                            sum += 90 + queenPositionValues[i, j];
                            break;
                        case nameof(King):
                            sum += 900 + kingPositionValues[i, j];
                            break;
                    }
                }
            }
            foreach (var piece in board.BlackPieces)
            {
                int i = piece.Position.PositionX;
                int j = piece.Position.PositionY;

                if (piece != null)
                {
                    switch (piece.GetType().Name)
                    {
                        case nameof(Pawn):
                            sum -= 10 + pawnPositionValues[i, 7 - j];
                            break;
                        case nameof(Knight):
                            sum -= 30 + knightPositionValues[i, 7 - j];
                            break;
                        case nameof(Bishop):
                            sum -= 30 + bishopPositionValues[i, 7 - j];
                            break;
                        case nameof(Rook):
                            sum -= 50 + rookPositionValues[i, 7 - j];
                            break;
                        case nameof(Queen):
                            sum -= 90 + queenPositionValues[i, 7 - j];
                            break;
                        case nameof(King):
                            sum -= 900 + kingPositionValues[i, 7 - j];
                            break;
                    }
                }

            }
            return sum;
        }





        /// <summary>
        /// se usa en el constructor, crea el array de puntajes segun posicion del pawn
        /// </summary>
        /// <returns></returns>
        public double[,] PawnPositionValues()
        {
            double[] ValuesFile1 = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
            double[] ValuesFile2 = { 0.5, 1.0, 1.0, -2.0, -2.0, 1.0, 1.0, 0.5 };
            double[] ValuesFile3 = { 0.5, -0.5, -1.0, 0.0, 0.0, -1.0, -0.5, 0.5 };
            double[] ValuesFile4 = { 0.0, 0.0, 0.0, 2.0, 2.0, 0.0, 0.0, 0.0 };
            double[] ValuesFile5 = { 0.5, 0.5, 1.0, 2.5, 2.5, 1.0, 0.5, 0.5 };
            double[] ValuesFile6 = { 1.0, 1.0, 2.0, 3.0, 3.0, 2.0, 1.0, 1.0 };
            double[] ValuesFile7 = { 5.0, 5.0, 5.0, 5.0, 5.0, 5.0, 5.0, 5.0 };
            double[] ValuesFile8 = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };

            for (int i = Constants.ForStart; i < Constants.Size; i++)
            {
                pawnPositionValues[i, 0] = ValuesFile1[i];
                pawnPositionValues[i, 1] = ValuesFile2[i];
                pawnPositionValues[i, 2] = ValuesFile3[i];
                pawnPositionValues[i, 3] = ValuesFile4[i];
                pawnPositionValues[i, 4] = ValuesFile5[i];
                pawnPositionValues[i, 5] = ValuesFile6[i];
                pawnPositionValues[i, 6] = ValuesFile7[i];
                pawnPositionValues[i, 7] = ValuesFile8[i];
            }

            return pawnPositionValues;
        }
        /// <summary>
        /// se usa en el constructor, crea el array de puntajes segun posicion del knight
        /// </summary>
        /// <returns></returns>
        public double[,] KnightPositionValues()
        {
            double[] ValuesFile1 = { -5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0 };
            double[] ValuesFile2 = { -4.0, -2.0, 0.0, 0.5, 0.5, 0.0, -2.0, -4.0 };
            double[] ValuesFile3 = { -3.0, 0.0, 1.0, 1.5, 1.5, 1.0, 0.0, -3.0 };
            double[] ValuesFile4 = { -3.0, 0.5, 1.5, 2.0, 2.0, 1.5, 0.5, -3.0 };
            double[] ValuesFile5 = { -3.0, 0.5, 1.5, 2.0, 2.0, 1.5, 0.5, -3.0 };
            double[] ValuesFile6 = { -3.0, 0.0, 1.0, 1.5, 1.5, 1.0, 0.0, -3.0 };
            double[] ValuesFile7 = { -4.0, -2.0, 0.0, 0.5, 0.5, 0.0, -2.0, -4.0 };
            double[] ValuesFile8 = { -5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0 };


            for (int i = Constants.ForStart; i < Constants.Size; i++)
            {
                knightPositionValues[i, 0] = ValuesFile1[i];
                knightPositionValues[i, 1] = ValuesFile2[i];
                knightPositionValues[i, 2] = ValuesFile3[i];
                knightPositionValues[i, 3] = ValuesFile4[i];
                knightPositionValues[i, 4] = ValuesFile5[i];
                knightPositionValues[i, 5] = ValuesFile6[i];
                knightPositionValues[i, 6] = ValuesFile7[i];
                knightPositionValues[i, 7] = ValuesFile8[i];
            }

            return knightPositionValues;
        }
        /// <summary>
        /// se usa en el constructor, crea el array de puntajes segun posicion del bishop
        /// </summary>
        /// <returns></returns>
        public double[,] BishopPositionValues()
        {
            double[] ValuesFile1 = { -2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0 };
            double[] ValuesFile2 = { -1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.0 };
            double[] ValuesFile3 = { -1.0, 0.0, 0.5, 1.0, 1.0, 0.5, 0.0, -1.0 };
            double[] ValuesFile4 = { -1.0, 0.0, 1.0, 1.0, 1.0, 1.0, 0.0, -1.0 };
            double[] ValuesFile5 = { -1.0, 0.5, 0.5, 1.0, 1.0, 0.5, 0.5, -1.0 };
            double[] ValuesFile6 = { -1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, -1.0 };
            double[] ValuesFile7 = { -1.0, 0.5, 0.0, 0.0, 0.0, 0.0, 0.5, -1.0 };
            double[] ValuesFile8 = { -2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0 };

            for (int i = Constants.ForStart; i < Constants.Size; i++)
            {
                bishopPositionValues[i, 0] = ValuesFile1[i];
                bishopPositionValues[i, 1] = ValuesFile2[i];
                bishopPositionValues[i, 2] = ValuesFile3[i];
                bishopPositionValues[i, 3] = ValuesFile4[i];
                bishopPositionValues[i, 4] = ValuesFile5[i];
                bishopPositionValues[i, 5] = ValuesFile6[i];
                bishopPositionValues[i, 6] = ValuesFile7[i];
                bishopPositionValues[i, 7] = ValuesFile8[i];
            }

            return bishopPositionValues;
        }

        /// <summary>
        /// se usa en el constructor, crea el array de puntajes segun posicion del rook
        /// </summary>
        /// <returns></returns>
        public double[,] RookPositionValues()
        {
            double[] ValuesFile1 = { 0.0, 0.0, 0.0, 0.5, 0.5, 0.0, 0.0, 0.0 };
            double[] ValuesFile2 = { -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 };
            double[] ValuesFile3 = { -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 };
            double[] ValuesFile4 = { -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 };
            double[] ValuesFile5 = { -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 };
            double[] ValuesFile6 = { -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 };
            double[] ValuesFile7 = { 0.5, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 0.5 };
            double[] ValuesFile8 = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };

            for (int i = Constants.ForStart; i < Constants.Size; i++)
            {
                rookPositionValues[i, 0] = ValuesFile1[i];
                rookPositionValues[i, 1] = ValuesFile2[i];
                rookPositionValues[i, 2] = ValuesFile3[i];
                rookPositionValues[i, 3] = ValuesFile4[i];
                rookPositionValues[i, 4] = ValuesFile5[i];
                rookPositionValues[i, 5] = ValuesFile6[i];
                rookPositionValues[i, 6] = ValuesFile7[i];
                rookPositionValues[i, 7] = ValuesFile8[i];
            }

            return rookPositionValues;
        }
        /// <summary>
        /// /// se usa en el constructor, crea el array de puntajes segun posicion del queen
        /// </summary>
        /// <returns></returns>

        public double[,] QueenPositionValues()
        {
            double[] ValuesFile1 = { -2.0, -1.0, -1.0, -0.5, -0.5, -1.0, -1.0, -2.0 };
            double[] ValuesFile2 = { -1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.0 };
            double[] ValuesFile3 = { -1.0, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, -1.0 };
            double[] ValuesFile4 = { 0.0, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, 0.0 };
            double[] ValuesFile5 = { -0.5, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, -0.5 };
            double[] ValuesFile6 = { -1.0, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, -1.0 };
            double[] ValuesFile7 = { -1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.0 };
            double[] ValuesFile8 = { -2.0, -1.0, -1.0, -0.5, -0.5, -1.0, -1.0, -2.0 };

            for (int i = Constants.ForStart; i < Constants.Size; i++)
            {
                queenPositionValues[i, 0] = ValuesFile1[i];
                queenPositionValues[i, 1] = ValuesFile2[i];
                queenPositionValues[i, 2] = ValuesFile3[i];
                queenPositionValues[i, 3] = ValuesFile4[i];
                queenPositionValues[i, 4] = ValuesFile5[i];
                queenPositionValues[i, 5] = ValuesFile6[i];
                queenPositionValues[i, 6] = ValuesFile7[i];
                queenPositionValues[i, 7] = ValuesFile8[i];
            }

            return queenPositionValues;
        }
        /// <summary>
        /// se usa en el constructor, crea el array de puntajes segun posicion del king
        /// </summary>
        /// <returns></returns>

        public double[,] KingPositionValues()
        {
            double[] ValuesFile1 = { 2.0, 3.0, 1.0, 0.0, 0.0, 1.0, 3.0, 2.0 };
            double[] ValuesFile2 = { 2.0, 2.0, 0.0, 0.0, 0.0, 0.0, 2.0, 2.0 };
            double[] ValuesFile3 = { -1.0, -2.0, -2.0, -2.0, -2.0, -2.0, -2.0, -1.0 };
            double[] ValuesFile4 = { -2.0, -3.0, -3.0, -4.0, -4.0, -3.0, -3.0, -2.0 };
            double[] ValuesFile5 = { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0 };
            double[] ValuesFile6 = { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0 };
            double[] ValuesFile7 = { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0 };
            double[] ValuesFile8 = { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0 };

            for (int i = Constants.ForStart; i < Constants.Size; i++)
            {
                kingPositionValues[i, 0] = ValuesFile1[i];
                kingPositionValues[i, 1] = ValuesFile2[i];
                kingPositionValues[i, 2] = ValuesFile3[i];
                kingPositionValues[i, 3] = ValuesFile4[i];
                kingPositionValues[i, 4] = ValuesFile5[i];
                kingPositionValues[i, 5] = ValuesFile6[i];
                kingPositionValues[i, 6] = ValuesFile7[i];
                kingPositionValues[i, 7] = ValuesFile8[i];
            }

            return kingPositionValues;
        }
    }
}