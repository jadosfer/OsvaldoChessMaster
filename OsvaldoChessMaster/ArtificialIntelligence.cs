using System;
using System.Collections.Generic;
using System.Text;
using OsvaldoChessMaster.Piece;

namespace OsvaldoChessMaster
{
    public class ArtificialIntelligence
    {
       
        public int[] move;
        public double[,] moves;
        public double[,] pawnPositionValues = new double[9, 9];
        public double[,] knightPositionValues = new double[9, 9];
        public double[,] bishopPositionValues = new double[9, 9];
        public double[,] rookPositionValues = new double[9, 9];
        public double[,] queenPositionValues = new double[9, 9];
        public double[,] kingPositionValues = new double[9, 9];

        public double[,] PawnPositionValues(int x, int y) => pawnPositionValues;
        public double[,] KnightPositionValues(int x, int y) => knightPositionValues;
        public double[,] BishopPositionValues(int x, int y) => bishopPositionValues;
        public double[,] RookPositionValues(int x, int y) => rookPositionValues;
        public double[,] QueenPositionValues(int x, int y) => queenPositionValues;
        public double[,] KingPositionValues(int x, int y) => kingPositionValues;
        public ArtificialIntelligence(Board board)
        {
            pawnPositionValues = PawnPositionValues();
            knightPositionValues = KnightPositionValues();
            bishopPositionValues = BishopPositionValues();
            rookPositionValues = RookPositionValues();
            queenPositionValues = QueenPositionValues();
            kingPositionValues = KingPositionValues();
        }

        public List<Move> AllPosiblePlays(Board board)
        {
            double actualValue = EvaluateBoard(board);
            List<Move> AllMoves = new List<Move>();
            //int count = 0;
            PieceBase[,] ChessBoardAux = (PieceBase[,]) board.ChessBoard.Clone();
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    var piece1 = board.GetPiece(i, j);
                    if (piece1.Color == board.Turn) // o sino si es == computerColor
                    {
                        for (int k = 1; k < 9; k++)
                        {
                            for (int l = 1; l < 9; l++)
                            {
                                
                                if (board.FinallyMove(i, j, k, l, board.player1))
                                {                                    
                                    // solo lo guardo si no empeora la situacion (cuanto ams negativo mejor para la pc
                                    if (EvaluateBoard(board) <= actualValue)
                                    {
                                        actualValue = EvaluateBoard(board);
                                        AllMoves.Add(new Move { x1 = i, y1 = j, x2 = k, y2 = l });
                                    }
                                    // back to previous board
                                    board.ChessBoard = ChessBoardAux;
                                    // back to previous turn
                                    board.TurnChange();                                    
                                }
                            }
                        }
                    }
                }
            }
            return AllMoves;
        }

        public List<Move> BestResponses(Board board, List<Move> previousMoves)
        {
            List<Move> responses = new List<Move>();            
            PieceBase[,] ChessBoardAux = (PieceBase[,])board.ChessBoard.Clone();
            foreach (Move previousMove in previousMoves)
            {
                board.FinallyMove(previousMove.x1, previousMove.y1, previousMove.x2, previousMove.y2, board.player1);
                responses.Add(BestResponse(board));
                // back to previous board
                board.ChessBoard = ChessBoardAux;
                // back to previous turn
                board.TurnChange();
            }
            return responses;
        }

        public Move BestResponse(Board board)
        {
            var ChessBoardAux = CloneBoard(board);
            //PrintBoard(board);
            //Console.WriteLine("Print arriba despeus de clonar");
            //PieceBase[,] ChessBoardAux = (PieceBase[,]) board.ChessBoard.Clone();
            double actualValue = EvaluateBoard(board);
            Move response = new Move();            

            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    
                    var piece1 = board.GetPiece(i, j);
                    if (piece1.Color == board.Turn && piece1.GetType() != typeof(EmptyPiece))
                    {
                        for (int k = 1; k < 9; k++)
                        {
                            for (int l = 1; l < 9; l++)
                            {
                                if (board.FinallyMove(i, j, k, l, board.player1))
                                {
                                    //PrintBoard(board);
                                    //Console.WriteLine("primer print arriba. despues del finally");
                                    // si mueve el de abajo
                                    if (board.player1)
                                    {   // guardo la mejor movida
                                        if (EvaluateBoard(board) >= actualValue)
                                        {
                                            actualValue = EvaluateBoard(board);
                                            response.x1 = i;
                                            response.y1 = j;
                                            response.x2 = k;
                                            response.y2 = l;
                                        }
                                    }
                                    else
                                    {   // mueve el de arriba
                                        if (EvaluateBoard(board) <= actualValue)
                                        {
                                            actualValue = EvaluateBoard(board);
                                            response.x1 = i;
                                            response.y1 = j;
                                            response.x2 = k;
                                            response.y2 = l;
                                        }
                                    }
                                    // back to previous board
                                    board.ChessBoard = ChessBoardAux;
                                    PrintChessBoard(ChessBoardAux);
                                    Console.WriteLine("arriba imprimo el Aux");
                                    // back to previous turn                                    
                                    board.TurnChange();
                                    PrintBoard(board);
                                    Console.WriteLine("arriba El board undo");
                                    //Console.WriteLine("" + response.x1 + response.y1 + response.x2 + response.y2);
                                }
                            }
                        }
                    }
                }
            }                       
            return response;
        }

        public PieceBase[,] CloneBoard(Board board)
        {
            PieceBase[,] clonedBoard = new PieceBase[9, 9];
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {                    
                    switch (board.GetPiece(i, j).GetType().Name)
                    {
                        case nameof(Pawn):
                            clonedBoard[i, j] = new Pawn(board.player1);
                            break;
                        case nameof(Knight):
                            clonedBoard[i, j] = new Knight(board.player1);
                            break;
                        case nameof(Bishop):
                            clonedBoard[i, j] = new Bishop(board.player1);
                            break;
                        case nameof(Rook):
                            clonedBoard[i, j] = new Rook(board.player1);
                            break;
                        case nameof(Queen):
                            clonedBoard[i, j] = new Queen(board.player1);
                            break;
                        case nameof(King):
                            clonedBoard[i, j] = new King(board.player1);
                            break;
                        case nameof(EmptyPiece):
                            clonedBoard[i, j] = new EmptyPiece(board.player1);
                            break;
                    }
                }
            }
            return clonedBoard;
        }

    
        public Move BestComputerMoveDepth4(Board board)
        {
            PieceBase[,] ChessBoardAux = (PieceBase[,])board.ChessBoard.Clone();
            double value = EvaluateBoard(board);
            Move bestMove = null;
            List<Move> allMove1 = AllPosiblePlays(board);
            foreach (Move move1 in allMove1)
            {
                board.FinallyMove(move1.x1, move1.y1, move1.x2, move1.y2, board.player1);
                board.FinallyMove(BestResponse(board).x1, BestResponse(board).y1, BestResponse(board).x2, BestResponse(board).y2, board.player1);
                List<Move> allMove3 = AllPosiblePlays(board);
                PieceBase[,] ChessBoardAux2 = (PieceBase[,])board.ChessBoard.Clone();                
                foreach (Move move3 in allMove3)
                {
                    board.FinallyMove(move1.x1, move1.y1, move1.x2, move1.y2, board.player1);
                    board.FinallyMove(BestResponse(board).x1, BestResponse(board).y1, BestResponse(board).x2, BestResponse(board).y2, board.player1);
                    if (EvaluateBoard(board) < value)
                    {
                        value = EvaluateBoard(board);
                        bestMove = move1; //me quedo con la move1 que produce la menor bestresponse
                    }
                    // back to previous board
                    board.ChessBoard = ChessBoardAux2;
                }
                // back to previous board
                board.ChessBoard = ChessBoardAux;
            }
            return bestMove;
        }

        public void UndoRookCastling(Board board, bool computerColor, PieceBase auxPiece, int i, int j, int k, int l)
        {
            //undo rook castling
            if (board.isCastling)
            {   //if white is below  && move to right
                if (!computerColor && i < k)
                {   //copy rook on x=6 to x=8
                    board.ChessBoard[8, j] = board.ChessBoard[6, j];
                    //if was Castling auxPiece is emptyPiece    
                    board.ChessBoard[6, j] = auxPiece;
                }
                //if white is below  && move to left
                if (!computerColor && i > k)
                {   //copy rook on x=4 to x=1
                    board.ChessBoard[1, j] = board.ChessBoard[4, j];
                    //if was Castling auxPiece is emptyPiece    
                    board.ChessBoard[4, j] = auxPiece;
                }
                //if black is below  && move to right
                if (computerColor && i < k)
                {   //copy rook on x=5 to x=8
                    board.ChessBoard[8, j] = board.ChessBoard[5, j];
                    //if was Castling auxPiece is emptyPiece    
                    board.ChessBoard[5, j] = auxPiece;
                }
                //if black is below  && move to left
                if (computerColor && i > k)
                {   //copy rook on x=4 to x=1
                    board.ChessBoard[1, j] = board.ChessBoard[3, j];
                    //if was Castling auxPiece is emptyPiece    
                    board.ChessBoard[3, j] = auxPiece;
                }
                board.isCastling = false;
            }
        }

        public double EvaluateBoard(Board board)
        {
            double sum = 0;

            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    var piece1 = board.GetPiece(i, j);
                    if (piece1.Color == board.player1) //piezas del jugador suman y de pc restan
                    {
                        switch (piece1.GetType().Name)
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
                    else
                    {
                        switch (piece1.GetType().Name)
                        {
                            case nameof(Pawn):
                                sum -= 10 + pawnPositionValues[i, 9 - j];
                                break;
                            case nameof(Knight):
                                sum -= 30 + knightPositionValues[i, 9 - j];
                                break;
                            case nameof(Bishop):
                                sum -= 30 + bishopPositionValues[i, 9 - j];
                                break;
                            case nameof(Rook):
                                sum -= 50 + rookPositionValues[i, 9 - j];
                                break;
                            case nameof(Queen):
                                sum -= 90 + queenPositionValues[i, 9 - j];
                                break;
                            case nameof(King):
                                sum -= 900 + kingPositionValues[i, 9 - j];
                                break;
                        }

                    }
                }
            }
            return sum;
        }

        public double[,] PawnPositionValues()
        {
            double[] ValuesFile1 = { 0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
            double[] ValuesFile2 = { 0, 0.5, 1.0, 1.0, -2.0, -2.0, 1.0, 1.0, 0.5 };
            double[] ValuesFile3 = { 0, 0.5, -0.5, -1.0, 0.0, 0.0, -1.0, -0.5, 0.5 };
            double[] ValuesFile4 = { 0, 0.0, 0.0, 0.0, 2.0, 2.0, 0.0, 0.0, 0.0 };
            double[] ValuesFile5 = { 0, 0.5, 0.5, 1.0, 2.5, 2.5, 1.0, 0.5, 0.5 };
            double[] ValuesFile6 = { 0, 1.0, 1.0, 2.0, 3.0, 3.0, 2.0, 1.0, 1.0 };
            double[] ValuesFile7 = { 0, 5.0, 5.0, 5.0, 5.0, 5.0, 5.0, 5.0, 5.0 };
            double[] ValuesFile8 = { 0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };

            for (int i = 1; i < 9; i++)
            {
                pawnPositionValues[i, 1] = ValuesFile1[i];
                pawnPositionValues[i, 2] = ValuesFile2[i];
                pawnPositionValues[i, 3] = ValuesFile3[i];
                pawnPositionValues[i, 4] = ValuesFile4[i];
                pawnPositionValues[i, 5] = ValuesFile5[i];
                pawnPositionValues[i, 6] = ValuesFile6[i];
                pawnPositionValues[i, 7] = ValuesFile7[i];
                pawnPositionValues[i, 8] = ValuesFile8[i];
            }
            return pawnPositionValues;
        }

        public double[,] KnightPositionValues()
        {
            double[] ValuesFile1 = { 0, -5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0 };
            double[] ValuesFile2 = { 0, -4.0, -2.0, 0.0, 0.5, 0.5, 0.0, -2.0, -4.0 };
            double[] ValuesFile3 = { 0, -3.0, 0.0, 1.0, 1.5, 1.5, 1.0, 0.0, -3.0 };
            double[] ValuesFile4 = { 0, -3.0, 0.5, 1.5, 2.0, 2.0, 1.5, 0.5, -3.0 };
            double[] ValuesFile5 = { 0, -3.0, 0.5, 1.5, 2.0, 2.0, 1.5, 0.5, -3.0 };
            double[] ValuesFile6 = { 0, -3.0, 0.0, 1.0, 1.5, 1.5, 1.0, 0.0, -3.0 };
            double[] ValuesFile7 = { 0, -4.0, -2.0, 0.0, 0.5, 0.5, 0.0, -2.0, -4.0 };
            double[] ValuesFile8 = { 0, -5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0 };
            double[,] knightPositionValues = new double[9, 9];
            for (int i = 1; i < 9; i++)
            {
                knightPositionValues[i, 1] = ValuesFile1[i];
                knightPositionValues[i, 2] = ValuesFile2[i];
                knightPositionValues[i, 3] = ValuesFile3[i];
                knightPositionValues[i, 4] = ValuesFile4[i];
                knightPositionValues[i, 5] = ValuesFile5[i];
                knightPositionValues[i, 6] = ValuesFile6[i];
                knightPositionValues[i, 7] = ValuesFile7[i];
                knightPositionValues[i, 8] = ValuesFile8[i];
            }
            return knightPositionValues;
        }

        public double[,] BishopPositionValues()
        {
            double[] ValuesFile1 = { 0, -2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0 };
            double[] ValuesFile2 = { 0, -1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.0 };
            double[] ValuesFile3 = { 0, -1.0, 0.0, 0.5, 1.0, 1.0, 0.5, 0.0, -1.0 };
            double[] ValuesFile4 = { 0, -1.0, 0.0, 1.0, 1.0, 1.0, 1.0, 0.0, -1.0 };
            double[] ValuesFile5 = { 0, -1.0, 0.5, 0.5, 1.0, 1.0, 0.5, 0.5, -1.0 };
            double[] ValuesFile6 = { 0, -1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, -1.0 };
            double[] ValuesFile7 = { 0, -1.0, 0.5, 0.0, 0.0, 0.0, 0.0, 0.5, -1.0 };
            double[] ValuesFile8 = { 0, -2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0 };
            
            for (int i = 1; i < 9; i++)
            {
                bishopPositionValues[i, 1] = ValuesFile1[i];
                bishopPositionValues[i, 2] = ValuesFile2[i];
                bishopPositionValues[i, 3] = ValuesFile3[i];
                bishopPositionValues[i, 4] = ValuesFile4[i];
                bishopPositionValues[i, 5] = ValuesFile5[i];
                bishopPositionValues[i, 6] = ValuesFile6[i];
                bishopPositionValues[i, 7] = ValuesFile7[i];
                bishopPositionValues[i, 8] = ValuesFile8[i];
            }
            return bishopPositionValues;
        }

        public double[,] RookPositionValues()
        {
            double[] ValuesFile1 = { 0, 0.0, 0.0, 0.0, 0.5, 0.5, 0.0, 0.0, 0.0 };
            double[] ValuesFile2 = { 0, -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 };
            double[] ValuesFile3 = { 0, -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 };
            double[] ValuesFile4 = { 0, -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 };
            double[] ValuesFile5 = { 0, -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 };
            double[] ValuesFile6 = { 0, -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 };
            double[] ValuesFile7 = { 0, 0.5, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 0.5 };
            double[] ValuesFile8 = { 0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };            
            for (int i = 1; i < 9; i++)
            {
                rookPositionValues[i, 1] = ValuesFile1[i];
                rookPositionValues[i, 2] = ValuesFile2[i];
                rookPositionValues[i, 3] = ValuesFile3[i];
                rookPositionValues[i, 4] = ValuesFile4[i];
                rookPositionValues[i, 5] = ValuesFile5[i];
                rookPositionValues[i, 6] = ValuesFile6[i];
                rookPositionValues[i, 7] = ValuesFile7[i];
                rookPositionValues[i, 8] = ValuesFile8[i];
            }
            return rookPositionValues;
        }

        public double[,] QueenPositionValues()
        {
            double[] ValuesFile1 = { 0, -2.0, -1.0, -1.0, -0.5, -0.5, -1.0, -1.0, -2.0 };
            double[] ValuesFile2 = { 0, -1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.0 };
            double[] ValuesFile3 = { 0, -1.0, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, -1.0 };
            double[] ValuesFile4 = { 0, 0.0, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, 0.0 };
            double[] ValuesFile5 = { 0, -0.5, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, -0.5 };
            double[] ValuesFile6 = { 0, -1.0, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, -1.0 };
            double[] ValuesFile7 = { 0, -1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.0 };
            double[] ValuesFile8 = { 0, -2.0, -1.0, -1.0, -0.5, -0.5, -1.0, -1.0, -2.0 };
            
            for (int i = 1; i < 9; i++)
            {
                queenPositionValues[i, 1] = ValuesFile1[i];
                queenPositionValues[i, 2] = ValuesFile2[i];
                queenPositionValues[i, 3] = ValuesFile3[i];
                queenPositionValues[i, 4] = ValuesFile4[i];
                queenPositionValues[i, 5] = ValuesFile5[i];
                queenPositionValues[i, 6] = ValuesFile6[i];
                queenPositionValues[i, 7] = ValuesFile7[i];
                queenPositionValues[i, 8] = ValuesFile8[i];
            }
            return queenPositionValues;
        }

        public double[,] KingPositionValues()
        {
            double[] ValuesFile1 = { 0, 2.0, 3.0, 1.0, 0.0, 0.0, 1.0, 3.0, 2.0 };
            double[] ValuesFile2 = { 0, 2.0, 2.0, 0.0, 0.0, 0.0, 0.0, 2.0, 2.0 };
            double[] ValuesFile3 = { 0, -1.0, -2.0, -2.0, -2.0, -2.0, -2.0, -2.0, -1.0 };
            double[] ValuesFile4 = { 0, -2.0, -3.0, -3.0, -4.0, -4.0, -3.0, -3.0, -2.0 };
            double[] ValuesFile5 = { 0, -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0 };
            double[] ValuesFile6 = { 0, -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0 };
            double[] ValuesFile7 = { 0, -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0 };
            double[] ValuesFile8 = { 0, -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0 };
            
            for (int i = 1; i < 9; i++)
            {
                kingPositionValues[i, 1] = ValuesFile1[i];
                kingPositionValues[i, 2] = ValuesFile2[i];
                kingPositionValues[i, 3] = ValuesFile3[i];
                kingPositionValues[i, 4] = ValuesFile4[i];
                kingPositionValues[i, 5] = ValuesFile5[i];
                kingPositionValues[i, 6] = ValuesFile6[i];
                kingPositionValues[i, 7] = ValuesFile7[i];
                kingPositionValues[i, 8] = ValuesFile8[i];
            }
            return kingPositionValues;
        }

        public static void PrintBoard(Board board)
        {
            Console.WriteLine();
            Console.Write(board.ChessBoard[1, 8] + " "); Console.Write(board.ChessBoard[2, 8] + " "); Console.Write(board.ChessBoard[3, 8] + " "); Console.Write(board.ChessBoard[4, 8] + " "); Console.Write(board.ChessBoard[5, 8] + " "); Console.Write(board.ChessBoard[6, 8] + " "); Console.Write(board.ChessBoard[7, 8] + " "); Console.WriteLine(board.ChessBoard[8, 8] + " ");
            Console.Write(board.ChessBoard[1, 7] + " "); Console.Write(board.ChessBoard[2, 7] + " "); Console.Write(board.ChessBoard[3, 7] + " "); Console.Write(board.ChessBoard[4, 7] + " "); Console.Write(board.ChessBoard[5, 7] + " "); Console.Write(board.ChessBoard[6, 7] + " "); Console.Write(board.ChessBoard[7, 7] + " "); Console.WriteLine(board.ChessBoard[8, 7] + " ");
            Console.Write(board.ChessBoard[1, 6] + " "); Console.Write(board.ChessBoard[2, 6] + " "); Console.Write(board.ChessBoard[3, 6] + " "); Console.Write(board.ChessBoard[4, 6] + " "); Console.Write(board.ChessBoard[5, 6] + " "); Console.Write(board.ChessBoard[6, 6] + " "); Console.Write(board.ChessBoard[7, 6] + " "); Console.WriteLine(board.ChessBoard[8, 6] + " ");
            Console.Write(board.ChessBoard[1, 5] + " "); Console.Write(board.ChessBoard[2, 5] + " "); Console.Write(board.ChessBoard[3, 5] + " "); Console.Write(board.ChessBoard[4, 5] + " "); Console.Write(board.ChessBoard[5, 5] + " "); Console.Write(board.ChessBoard[6, 5] + " "); Console.Write(board.ChessBoard[7, 5] + " "); Console.WriteLine(board.ChessBoard[8, 5] + " ");
            Console.Write(board.ChessBoard[1, 4] + " "); Console.Write(board.ChessBoard[2, 4] + " "); Console.Write(board.ChessBoard[3, 4] + " "); Console.Write(board.ChessBoard[4, 4] + " "); Console.Write(board.ChessBoard[5, 4] + " "); Console.Write(board.ChessBoard[6, 4] + " "); Console.Write(board.ChessBoard[7, 4] + " "); Console.WriteLine(board.ChessBoard[8, 4] + " ");
            Console.Write(board.ChessBoard[1, 3] + " "); Console.Write(board.ChessBoard[2, 3] + " "); Console.Write(board.ChessBoard[3, 3] + " "); Console.Write(board.ChessBoard[4, 3] + " "); Console.Write(board.ChessBoard[5, 3] + " "); Console.Write(board.ChessBoard[6, 3] + " "); Console.Write(board.ChessBoard[7, 3] + " "); Console.WriteLine(board.ChessBoard[8, 3] + " ");
            Console.Write(board.ChessBoard[1, 2] + " "); Console.Write(board.ChessBoard[2, 2] + " "); Console.Write(board.ChessBoard[3, 2] + " "); Console.Write(board.ChessBoard[4, 2] + " "); Console.Write(board.ChessBoard[5, 2] + " "); Console.Write(board.ChessBoard[6, 2] + " "); Console.Write(board.ChessBoard[7, 2] + " "); Console.WriteLine(board.ChessBoard[8, 2] + " ");
            Console.Write(board.ChessBoard[1, 1] + " "); Console.Write(board.ChessBoard[2, 1] + " "); Console.Write(board.ChessBoard[3, 1] + " "); Console.Write(board.ChessBoard[4, 1] + " "); Console.Write(board.ChessBoard[5, 1] + " "); Console.Write(board.ChessBoard[6, 1] + " "); Console.Write(board.ChessBoard[7, 1] + " "); Console.WriteLine(board.ChessBoard[8, 1] + " ");
            Console.WriteLine();
        }

        public static void PrintChessBoard(PieceBase[,] ChessBoard)
        {
            Console.WriteLine();
            Console.Write(ChessBoard[1, 8] + " "); Console.Write(ChessBoard[2, 8] + " "); Console.Write(ChessBoard[3, 8] + " "); Console.Write(ChessBoard[4, 8] + " "); Console.Write(ChessBoard[5, 8] + " "); Console.Write(ChessBoard[6, 8] + " "); Console.Write(ChessBoard[7, 8] + " "); Console.WriteLine(ChessBoard[8, 8] + " ");
            Console.Write(ChessBoard[1, 7] + " "); Console.Write(ChessBoard[2, 7] + " "); Console.Write(ChessBoard[3, 7] + " "); Console.Write(ChessBoard[4, 7] + " "); Console.Write(ChessBoard[5, 7] + " "); Console.Write(ChessBoard[6, 7] + " "); Console.Write(ChessBoard[7, 7] + " "); Console.WriteLine(ChessBoard[8, 7] + " ");
            Console.Write(ChessBoard[1, 6] + " "); Console.Write(ChessBoard[2, 6] + " "); Console.Write(ChessBoard[3, 6] + " "); Console.Write(ChessBoard[4, 6] + " "); Console.Write(ChessBoard[5, 6] + " "); Console.Write(ChessBoard[6, 6] + " "); Console.Write(ChessBoard[7, 6] + " "); Console.WriteLine(ChessBoard[8, 6] + " ");
            Console.Write(ChessBoard[1, 5] + " "); Console.Write(ChessBoard[2, 5] + " "); Console.Write(ChessBoard[3, 5] + " "); Console.Write(ChessBoard[4, 5] + " "); Console.Write(ChessBoard[5, 5] + " "); Console.Write(ChessBoard[6, 5] + " "); Console.Write(ChessBoard[7, 5] + " "); Console.WriteLine(ChessBoard[8, 5] + " ");
            Console.Write(ChessBoard[1, 4] + " "); Console.Write(ChessBoard[2, 4] + " "); Console.Write(ChessBoard[3, 4] + " "); Console.Write(ChessBoard[4, 4] + " "); Console.Write(ChessBoard[5, 4] + " "); Console.Write(ChessBoard[6, 4] + " "); Console.Write(ChessBoard[7, 4] + " "); Console.WriteLine(ChessBoard[8, 4] + " ");
            Console.Write(ChessBoard[1, 3] + " "); Console.Write(ChessBoard[2, 3] + " "); Console.Write(ChessBoard[3, 3] + " "); Console.Write(ChessBoard[4, 3] + " "); Console.Write(ChessBoard[5, 3] + " "); Console.Write(ChessBoard[6, 3] + " "); Console.Write(ChessBoard[7, 3] + " "); Console.WriteLine(ChessBoard[8, 3] + " ");
            Console.Write(ChessBoard[1, 2] + " "); Console.Write(ChessBoard[2, 2] + " "); Console.Write(ChessBoard[3, 2] + " "); Console.Write(ChessBoard[4, 2] + " "); Console.Write(ChessBoard[5, 2] + " "); Console.Write(ChessBoard[6, 2] + " "); Console.Write(ChessBoard[7, 2] + " "); Console.WriteLine(ChessBoard[8, 2] + " ");
            Console.Write(ChessBoard[1, 1] + " "); Console.Write(ChessBoard[2, 1] + " "); Console.Write(ChessBoard[3, 1] + " "); Console.Write(ChessBoard[4, 1] + " "); Console.Write(ChessBoard[5, 1] + " "); Console.Write(ChessBoard[6, 1] + " "); Console.Write(ChessBoard[7, 1] + " "); Console.WriteLine(ChessBoard[8, 1] + " ");
            Console.WriteLine();
        }

    }

    public class Move
    {
        public int x1 { get; set; }
        public int y1 { get; set; }
        public int x2 { get; set; }
        public int y2 { get; set; }
    }

    public class MoveAndResponse
    {
        public Move move1 { get; set; }
        public Move move2 { get; set; }

        //public int x1 { get; set; }
        //public int y1 { get; set; }
        //public int x2 { get; set; }
        //public int y2 { get; set; }
        //public int x3 { get; set; }
        //public int y3 { get; set; }
        //public int x4 { get; set; }
        //public int y4 { get; set; }         
    }

}
