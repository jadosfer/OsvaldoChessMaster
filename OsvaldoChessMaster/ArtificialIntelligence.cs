using System;
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

        public ArtificialIntelligence(Board board)
        {
            pawnPositionValues = PawnPositionValues();
            knightPositionValues = KnightPositionValues();
            bishopPositionValues = BishopPositionValues();
            rookPositionValues = RookPositionValues();
            queenPositionValues = QueenPositionValues();
            kingPositionValues = KingPositionValues();
        }

        

        /// <summary>
        /// devuelve un array con todas las movidas posibles que mejoran el puntaje de un estado del tablero
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public List<Move> AllPosiblePlays(Board board)
        {
            double actualValue = EvaluateBoard(board);

            List<Move> AllMoves = new List<Move>();   
            

            for (int i = Constants.ForStart; i < Constants.Size; i++)            
            {                
                for (int j = Constants.ForStart; j < Constants.Size; j++) 
                {
                    var piece1 = board.GetPiece(i, j);

                    if (piece1.Color == !board.player1) // o sino si es == computerColor
                    {
                        AllMoves = AllPosiblePiecePlays(board, i, j, AllMoves, actualValue);
                    }
                }
            }                        
            if (AllMoves != null)
            {
                return AllMoves;
            }
            AllMoves.Add(BestResponse(board));
            return AllMoves;

        }

        /// <summary>
        /// devuelve un array con todas las movidas posibles que tiene una pieza ubicada en i,j que mejoran el puntaje de un estado del tablero
        /// </summary>
        /// <param name="board"></param>
        /// <param name="i"></param> coordenada x de la pieza
        /// <param name="j"></param> coordenada y de la pieza
        /// <param name="AllMoves"></param>
        /// <param name="actualValue"></param>
        /// <returns></returns>
        public List<Move> AllPosiblePiecePlays(Board board, int i, int j, List<Move> AllMoves, double actualValue)
        {            

            for (int k = Constants.ForStart; k < Constants.Size; k++)
            {
                for (int l = Constants.ForStart; l < Constants.Size; l++)
                {
                    PieceBase[,] BackupBoard2 = board.CloneChessBoard();
                    bool TurnShow = board.Turn;
                                        
                    if (board.FinallyMove(i, j, k, l))
                    {                        
                        // solo lo guardo si no empeora la situacion (cuanto ams negativo mejor para la pc
                        double Eval = EvaluateBoard(board);
                        
                        if (Eval <= actualValue)
                        {
                            //actualValue = EvaluateBoard(board);
                            AllMoves.Add(new Move { x1 = i, y1 = j, x2 = k, y2 = l });
                        }
                        // back to previous turn
                        board.ChessBoard = BackupBoard2; //vuelve al board clonado                      
                        board.TurnChange();                        
                    }                    
                }
            }                        
            return AllMoves;           
        }       


        /// <summary>
        /// recibe una lista de movidas y devuelve otra lista de movidas que son la mejor respuesta para cada una de las movidas que recibió.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="previousMoves"></param>
        /// <returns></returns>
        public List<Move> BestResponses(Board board, List<Move> previousMoves)
        {
            List<Move> responses = new List<Move>();            

            foreach (Move previousMove in previousMoves)
            {
                PieceBase[,] ChessBoardAux = board.CloneChessBoard();
                board.FinallyMove(previousMove.x1, previousMove.y1, previousMove.x2, previousMove.y2);
                responses.Add(BestResponse(board));
                // back to previous board
                board.ChessBoard = ChessBoardAux;
                // back to previous turn
                board.TurnChange();
            }

            return responses;
        }
        /// <summary>
        /// devuelve la movida que mas aumenta el puntaje de un estado del tablero (o si es negativo que lo hace menos negativo) Es un metodo para simular la movida del humano
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public Move BestResponse(Board board)
        {
            
            double actualValue = EvaluateBoard(board);
            Move response = new Move();
            Move responseNotNull = new Move();

            for (int i = Constants.ForStart; i < Constants.Size; i++)
            {
                for (int j = Constants.ForStart; j < Constants.Size; j++)
                {                    
                    var piece1 = board.GetPiece(i, j);

                    if (piece1.Color == board.Turn && piece1.GetType() != typeof(EmptyPiece))
                    {
                        for (int k = Constants.ForStart; k < Constants.Size; k++)
                        {
                            for (int l = Constants.ForStart; l < Constants.Size; l++)
                            {
                                var ChessBoardAux = board.CloneChessBoard();
                                if (board.FinallyMove(i, j, k, l))
                                {                                    
                                    responseNotNull.x1 = i;
                                    responseNotNull.y1 = j;
                                    responseNotNull.x2 = k;
                                    responseNotNull.y2 = l;

                                    // si mueve el de abajo
                                    if (board.player1 != board.Turn) // va != porque el finally me lo acaba de cambiar a turn
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
                                    
                                    // back to previous turn                                    
                                    board.TurnChange();                                    
                                }
                            }
                        }
                    }
                }
            }
            if (response != null)
            {
                return response;
            }
            return responseNotNull; 

        }

        /// <summary>
        /// devuelve la mejor jugada que debe hacer la compu tras probar 4 movidas hacia adelante. Metodo minimax de los rústicos
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public Move BestComputerMoveDepth4(Board board)
        {           
            double value = EvaluateBoard(board);
            Console.WriteLine("calculó EvaluateBoard");
            Move bestMove = null;
            List<Move> allMove1 = AllPosiblePlays(board);
            Console.WriteLine("calculó las AllPosiblePlays");
            foreach (Move move1 in allMove1)
            {
                PieceBase[,] ChessBoardAux = board.CloneChessBoard();
                board.FinallyMove(move1.x1, move1.y1, move1.x2, move1.y2);
                board.FinallyMove(BestResponse(board).x1, BestResponse(board).y1, BestResponse(board).x2, BestResponse(board).y2);
                Console.WriteLine("movio y calculo la Best Response");
                List<Move> allMove3 = AllPosiblePlays(board);
                Console.WriteLine("calculó las AllPosiblePlays luego de mover y repsonder");

                foreach (Move move3 in allMove3)
                {
                    bool TurnShow2 = board.Turn;
                    PieceBase[,] ChessBoardAux2 = board.CloneChessBoard();
                    board.FinallyMove(move3.x1, move3.y1, move3.x2, move3.y2);
                    board.FinallyMove(BestResponse(board).x1, BestResponse(board).y1, BestResponse(board).x2, BestResponse(board).y2);
                    double Eval = EvaluateBoard(board);
                    if (Eval < value)
                    {
                        value = EvaluateBoard(board);
                        bestMove = move1; //me quedo con la move1 que produce la menor bestresponse
                    }
                    // back to previous board
                    board.ChessBoard = ChessBoardAux2;
                }
                board.ChessBoard = ChessBoardAux;
            }
            if (bestMove != null)
            {
                return bestMove;
            }
            return BestResponse(board);
        }

        /// <summary>
        /// calcual el puntaje de un estado del tablero (player1 positivo, compu negativo)
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public double EvaluateBoard(Board board)
        {            
            double sum = 0;

            for (int i = Constants.ForStart; i < Constants.Size; i++)
            {
                for (int j = Constants.ForStart; j < Constants.Size; j++)
                {
                    var piece1 = board.GetPiece(i, j);


                    switch (piece1.GetType().Name)
                    {
                        case nameof(Pawn):
                            if (piece1.Color == board.player1)
                            {
                                sum += 10 + pawnPositionValues[i, j];
                            }
                            else
                            {
                                sum -= 10 + pawnPositionValues[i, 7 - j];
                            }
                            break;
                        case nameof(Knight):
                            if (piece1.Color == board.player1)
                            {
                                sum += 30 + knightPositionValues[i, j];
                            }
                            else
                            {
                                sum -= 30 + knightPositionValues[i, 7 - j];
                            }
                            break;
                        case nameof(Bishop):
                            if (piece1.Color == board.player1)
                            {
                                sum += 30 + bishopPositionValues[i, j];
                            }
                            else
                            {
                                sum -= 30 + bishopPositionValues[i, 7 - j];
                            }
                            break;
                        case nameof(Rook):
                            if (piece1.Color == board.player1)
                            {
                                sum += 50 + rookPositionValues[i, j];
                            }
                            else
                            {
                                sum -= 50 + rookPositionValues[i, 7 - j];
                            }
                            break;
                        case nameof(Queen):
                            if (piece1.Color == board.player1)
                            {
                                sum += 90 + queenPositionValues[i, j];
                            }
                            else
                            {
                                sum -= 90 + queenPositionValues[i, 7 - j];
                            }
                            break;
                        case nameof(King):
                            if (piece1.Color == board.player1)
                            {
                                sum += 900 + kingPositionValues[i, j];
                            }
                            else
                            {
                                sum -= 900 + kingPositionValues[i, 7 - j];
                            }
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
            double[] ValuesFile1 = {0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
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