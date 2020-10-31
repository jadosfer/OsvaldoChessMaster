using System;
using System.Collections.Generic;
using System.Text;
using OsvaldoChessMaster.Piece;

namespace OsvaldoChessMaster
{
    class ArtificialIntelligence
    {
        public int x1;
        public int y1;
        public int x2;
        public int y2;
        public int x3;
        public int y3;
        public int x4;
        public int y4;
        public int x5;
        public int y5;
        public int x6;
        public int y6;
        public int[] move;
        public double[,] moves;

        public ArtificialIntelligence(Board board, bool computerColor)
        {
        }

        public int[] ValuesMovesAllowed(Board board, bool computerColor, double actualValue)
        {
            double actualValue2;
            double actualValue3 = 9999;
            //double maxPositionValue = -9999;
            move = new int[4];
            int count = 0;
            var ChessBoardAux = board.ChessBoard;
            var ChessBoardAux3 = board.ChessBoard; 
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    board.ChessBoard = ChessBoardAux;
                    var piece1 = board.GetPiece(i, j);
                    if (piece1.Color == board.Turn)
                    {
                        for (int k = 1; k < 9; k++)
                        {
                            for (int l = 1; l < 9; l++)
                            {
                                board.ChessBoard = ChessBoardAux;
                                var auxPiece = board.GetPiece(k, l);
                                if (board.FinallyMove(i, j, k, l, !computerColor))
                                {
                                    // solo lo guardo si no empeora la situacion (cuanto ams negativo mejor para la pc
                                    if (EvaluateBoard(board, computerColor) <= actualValue)
                                    {
                                        actualValue2 = EvaluateBoard(board, computerColor);
                                        var ChessBoardAux2 = board.ChessBoard;
                                        for (int m = 1; m < 9; m++)
                                        {
                                            for (int n = 1; n < 9; n++)
                                            {
                                                board.ChessBoard = ChessBoardAux2;
                                                var piece2 = board.GetPiece(m, n);
                                                if (piece2.Color = board.Turn)
                                                {
                                                    for (int o = 1; o < 9; o++)
                                                    {
                                                        for (int p = 1; p < 9; p++)
                                                        {
                                                            board.ChessBoard = ChessBoardAux2;
                                                            var auxPiece2 = board.GetPiece(o, p);
                                                            if (board.FinallyMove(m, n, o, p, !computerColor))
                                                            {
                                                                // solo lo guardo si no empeora la situacion
                                                                if (EvaluateBoard(board, computerColor) >= actualValue2)                                                                    
                                                                {
                                                                    actualValue2 = EvaluateBoard(board, computerColor);                                                                 
                                                                    ChessBoardAux3 = board.ChessBoard;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        for (int q = 1; q < 9; q++)
                                        {
                                            for (int r = 1; r < 9; r++)
                                            {
                                                board.ChessBoard = ChessBoardAux3;
                                                var piece3 = board.GetPiece(q, r);
                                                if (piece3.Color = board.Turn)
                                                {
                                                    for (int s = 1; s < 9; s++)
                                                    {
                                                        for (int t = 1; t < 9; t++)
                                                        {
                                                            board.ChessBoard = ChessBoardAux3;
                                                            var auxPiece3 = board.GetPiece(s, t);
                                                            if (board.FinallyMove(q, r, s, t, !computerColor))
                                                            {
                                                                // solo lo guardo si no empeora la situacion
                                                                if (EvaluateBoard(board, computerColor) <= actualValue2)
                                                                {
                                                                    actualValue2 = EvaluateBoard(board, computerColor);
                                                                    if (actualValue2 < actualValue3)
                                                                    {
                                                                        actualValue3 = actualValue2;
                                                                        x1 = i;
                                                                        y1 = j;
                                                                        x2 = k;
                                                                        y2 = l;
                                                                        //x3 = m;
                                                                        //y3 = n;
                                                                        //x4 = o;
                                                                        //y4 = p;
                                                                        x5 = q;
                                                                        y5 = r;
                                                                        x6 = s;
                                                                        y6 = t;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }                                                   

                                                }
                                            }
                                        }
                                    }                              
                                    //UndoRookCastling(board, computerColor, auxPiece, i, j, k, l);
                                    ////revierto cambio de turno de finallymove
                                    //board.TurnChange();

                                    //if (EvaluateBoard(board, computerColor) > maxPositionValue)
                                    //{
                                    //    maxPositionValue = EvaluateBoard(board, computerColor);
                                    //    x1 = i;
                                    //    y1 = j;
                                    //    x2 = k;
                                    //    y2 = l;
                                    //}
                                }
                            }
                        }
                    }
                }
            }

            move[0] = x1;
            move[1] = y1;
            move[2] = x2;
            move[3] = y2;
            return move;
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

        public double EvaluateBoard(Board board, bool computerColor)
        {
            double sum = 0;

            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    var piece1 = board.GetPiece(i, j);
                    if (piece1.Color == computerColor)
                    {
                        switch (piece1.GetType().Name)
                        {
                            case nameof(Pawn):
                                sum += 10 + PawnPositionValues(i, j);
                                break;
                            case nameof(Knight):
                                sum += 30 + KnightPositionValues(i, j);
                                break;
                            case nameof(Bishop):
                                sum += 30 + BishopPositionValues(i, j);
                                break;
                            case nameof(Rook):
                                sum += 50 + RookPositionValues(i, j);
                                break;
                            case nameof(Queen):
                                sum += 90 + QueenPositionValues(i, j);
                                break;
                            case nameof(King):
                                sum += 900 + KingPositionValues(i, j);
                                break;

                        }
                    }
                    else
                    {
                        switch (piece1.GetType().Name)
                        {
                            case nameof(Pawn):
                                sum -= 10 - PawnPositionValues(8-i, j);
                                break;
                            case nameof(Knight):
                                sum -= 30 - KnightPositionValues(8-i, j);
                                break;
                            case nameof(Bishop):
                                sum -= 30 - BishopPositionValues(8 - i, j);
                                break;
                            case nameof(Rook):
                                sum -= 50 - RookPositionValues(8 - i, j);
                                break;
                            case nameof(Queen):
                                sum -= 90 - QueenPositionValues(8 - i, j);
                                break;
                            case nameof(King):
                                sum -= 900 - KingPositionValues(8 - i, j);
                                break;
                        }

                    }
                }
            }
            return sum;
        }

        public double PawnPositionValues(int x, int y)
        {
            double[] pawnValuesFile1 = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
            double[] pawnValuesFile2 = { 0.5, 1.0, 1.0, -2.0, -2.0, 1.0, 1.0, 0.5 };
            double[] pawnValuesFile3 = { 0.5, -0.5, -1.0, 0.0, 0.0, -1.0, -0.5, 0.5 };
            double[] pawnValuesFile4 = { 0.0, 0.0, 0.0, 2.0, 2.0, 0.0, 0.0, 0.0 };
            double[] pawnValuesFile5 = { 0.5, 0.5, 1.0, 2.5, 2.5, 1.0, 0.5, 0.5 };
            double[] pawnValuesFile6 = { 1.0, 1.0, 2.0, 3.0, 3.0, 2.0, 1.0, 1.0 };
            double[] pawnValuesFile7 = { 5.0, 5.0, 5.0, 5.0, 5.0, 5.0, 5.0, 5.0 };
            double[] pawnValuesFile8 = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
            double[,] PawnPositionValues = new double[8, 8];
            for (int i = 1; i < 9; i++)
            {
                PawnPositionValues[1, i] = pawnValuesFile1[i];
                PawnPositionValues[2, i] = pawnValuesFile2[i];
                PawnPositionValues[3, i] = pawnValuesFile3[i];
                PawnPositionValues[4, i] = pawnValuesFile4[i];
                PawnPositionValues[5, i] = pawnValuesFile5[i];
                PawnPositionValues[6, i] = pawnValuesFile6[i];
                PawnPositionValues[7, i] = pawnValuesFile7[i];
                PawnPositionValues[8, i] = pawnValuesFile8[i];
            }
            return PawnPositionValues[x, y];
        }

        public double KnightPositionValues(int x, int y)
        {
            double[] ValuesFile1 = { -5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0 };
            double[] ValuesFile2 = { -4.0, -2.0, 0.0, 0.5, 0.5, 0.0, -2.0, -4.0 };
            double[] ValuesFile3 = { -3.0, 0.0, 1.0, 1.5, 1.5, 1.0, 0.0, -3.0 };
            double[] ValuesFile4 = { -3.0, 0.5, 1.5, 2.0, 2.0, 1.5, 0.5, -3.0 };
            double[] ValuesFile5 = { -3.0, 0.5, 1.5, 2.0, 2.0, 1.5, 0.5, -3.0 };
            double[] ValuesFile6 = { -3.0, 0.0, 1.0, 1.5, 1.5, 1.0, 0.0, -3.0 };
            double[] ValuesFile7 = { -4.0, -2.0, 0.0, 0.5, 0.5, 0.0, -2.0, -4.0 };
            double[] ValuesFile8 = { -5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0 };
            double[,] KnightPositionValues = new double[8, 8];
            for (int i = 1; i < 9; i++)
            {
                KnightPositionValues[1, i] = ValuesFile1[i];
                KnightPositionValues[2, i] = ValuesFile2[i];
                KnightPositionValues[3, i] = ValuesFile3[i];
                KnightPositionValues[4, i] = ValuesFile4[i];
                KnightPositionValues[5, i] = ValuesFile5[i];
                KnightPositionValues[6, i] = ValuesFile6[i];
                KnightPositionValues[7, i] = ValuesFile7[i];
                KnightPositionValues[8, i] = ValuesFile8[i];
            }
            return KnightPositionValues[x, y];
        }

        public double BishopPositionValues(int x, int y)
        {
            double[] ValuesFile1 = { -2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0 };
            double[] ValuesFile2 = { -1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.0 };
            double[] ValuesFile3 = { -1.0, 0.0, 0.5, 1.0, 1.0, 0.5, 0.0, -1.0 };
            double[] ValuesFile4 = { -1.0, 0.0, 1.0, 1.0, 1.0, 1.0, 0.0, -1.0 };
            double[] ValuesFile5 = { -1.0, 0.5, 0.5, 1.0, 1.0, 0.5, 0.5, -1.0 };
            double[] ValuesFile6 = { -1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, -1.0 };
            double[] ValuesFile7 = { -1.0, 0.5, 0.0, 0.0, 0.0, 0.0, 0.5, -1.0 };
            double[] ValuesFile8 = { -2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0 };
            double[,] BishopPositionValues = new double[8, 8];
            for (int i = 1; i < 9; i++)
            {
                BishopPositionValues[1, i] = ValuesFile1[i];
                BishopPositionValues[2, i] = ValuesFile2[i];
                BishopPositionValues[3, i] = ValuesFile3[i];
                BishopPositionValues[4, i] = ValuesFile4[i];
                BishopPositionValues[5, i] = ValuesFile5[i];
                BishopPositionValues[6, i] = ValuesFile6[i];
                BishopPositionValues[7, i] = ValuesFile7[i];
                BishopPositionValues[8, i] = ValuesFile8[i];
            }
            return BishopPositionValues[x, y];
        }

        public double RookPositionValues(int x, int y)
        {
            double[] ValuesFile1 = { 0.0, 0.0, 0.0, 0.5, 0.5, 0.0, 0.0, 0.0 };
            double[] ValuesFile2 = { -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 };
            double[] ValuesFile3 = { -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 };
            double[] ValuesFile4 = { -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 };
            double[] ValuesFile5 = { -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 };
            double[] ValuesFile6 = { -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 };
            double[] ValuesFile7 = { 0.5, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 0.5 };
            double[] ValuesFile8 = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
            double[,] RookPositionValues = new double[8, 8];
            for (int i = 1; i < 9; i++)
            {
                RookPositionValues[1, i] = ValuesFile1[i];
                RookPositionValues[2, i] = ValuesFile2[i];
                RookPositionValues[3, i] = ValuesFile3[i];
                RookPositionValues[4, i] = ValuesFile4[i];
                RookPositionValues[5, i] = ValuesFile5[i];
                RookPositionValues[6, i] = ValuesFile6[i];
                RookPositionValues[7, i] = ValuesFile7[i];
                RookPositionValues[8, i] = ValuesFile8[i];
            }
            return RookPositionValues[x, y];
        }

        public double QueenPositionValues(int x, int y)
        {
            double[] ValuesFile1 = { -2.0, -1.0, -1.0, -0.5, -0.5, -1.0, -1.0, -2.0 };
            double[] ValuesFile2 = { -1.0, 0.0, 0.5, 0.0, 0.0, 0.0, 0.0, -1.0 };
            double[] ValuesFile3 = { -1.0, 0.5, 0.5, 0.5, 0.5, 0.5, 0.0, -1.0 };
            double[] ValuesFile4 = { 0.0, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, -0.5 };
            double[] ValuesFile5 = { -0.5, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, -0.5 };
            double[] ValuesFile6 = { -1.0, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, -1.0 };
            double[] ValuesFile7 = { -1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.0 };
            double[] ValuesFile8 = { -2.0, -1.0, -1.0, -0.5, -0.5, -1.0, -1.0, -2.0 };
            double[,] QueenPositionValues = new double[8, 8];
            for (int i = 1; i < 9; i++)
            {
                QueenPositionValues[1, i] = ValuesFile1[i];
                QueenPositionValues[2, i] = ValuesFile2[i];
                QueenPositionValues[3, i] = ValuesFile3[i];
                QueenPositionValues[4, i] = ValuesFile4[i];
                QueenPositionValues[5, i] = ValuesFile5[i];
                QueenPositionValues[6, i] = ValuesFile6[i];
                QueenPositionValues[7, i] = ValuesFile7[i];
                QueenPositionValues[8, i] = ValuesFile8[i];
            }
            return QueenPositionValues[x, y];
        }

        public double KingPositionValues(int x, int y)
        {
            double[] ValuesFile1 = { 2.0, 3.0, 1.0, 0.0, 0.0, 1.0, 3.0, 2.0 };
            double[] ValuesFile2 = { 2.0, 2.0, 0.0, 0.0, 0.0, 0.0, 2.0, 2.0 };
            double[] ValuesFile3 = { -1.0, -2.0, -2.0, -2.0, -2.0, -2.0, -2.0, -1.0 };
            double[] ValuesFile4 = { -2.0, -3.0, -3.0, -4.0, -4.0, -3.0, -3.0, -2.0 };
            double[] ValuesFile5 = { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0 };
            double[] ValuesFile6 = { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0 };
            double[] ValuesFile7 = { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0 };
            double[] ValuesFile8 = { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0 };
            double[,] KingPositionValues = new double[8, 8];
            for (int i = 1; i < 9; i++)
            {
                KingPositionValues[1, i] = ValuesFile1[i];
                KingPositionValues[2, i] = ValuesFile2[i];
                KingPositionValues[3, i] = ValuesFile3[i];
                KingPositionValues[4, i] = ValuesFile4[i];
                KingPositionValues[5, i] = ValuesFile5[i];
                KingPositionValues[6, i] = ValuesFile6[i];
                KingPositionValues[7, i] = ValuesFile7[i];
                KingPositionValues[8, i] = ValuesFile8[i];
            }
            return KingPositionValues[x, y];
        }


    }
}
