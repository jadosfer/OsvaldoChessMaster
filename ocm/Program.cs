using System;
using OsvaldoChessMaster;
using OsvaldoChessMaster.Piece;
using System.Collections.Generic;

namespace ocm
{
    class Program
    {

        static void Main(string[] args)
        {
            
            //bool turn = true; // true si mueven blancas
            bool player1 = true; //true para blancas abajo y negras arriba                        
            Board board = new Board(player1);
            ArtificialIntelligence artificInt = new ArtificialIntelligence(board);            
            
            PieceBase[,] BackupBoard = board.CloneChessBoard();
            List<Move> AllMoves = new List<Move>();            
            //Program.PrintBoard(board);

            board.FinallyMove(5, 2, 5, 4); //blancas
            PrintBoard(board);
            
            Console.WriteLine("almoves =" + artificInt.AllPosiblePiecePlays(board, BackupBoard, 2, 8, AllMoves, 4));

            //board.FinallyMove(artificInt.BestComputerMoveDepth4(board).x1, artificInt.BestComputerMoveDepth4(board).y1, artificInt.BestComputerMoveDepth4(board).x2, artificInt.BestComputerMoveDepth4(board).y2);
            PrintBoard(board);

            //board.FinallyMove(5, 2, 5, 4); //blancas
            //PrintBoard(board);
            //board.FinallyMove(5, 7, 5, 5);
            //PrintBoard(board);
            //board.FinallyMove(6, 1, 3, 4); //blancas
            //PrintBoard(board);
            //board.FinallyMove(6, 7, 6, 6);
            //PrintBoard(board);
            //board.FinallyMove(1, 2, 1, 3); //blancas
            //PrintBoard(board);
            //board.FinallyMove(7, 8, 8, 6);
            //PrintBoard(board);
            //board.FinallyMove(7, 1, 8, 3); //blancas
            //PrintBoard(board);
            //board.FinallyMove(6, 8, 1, 3);
            //PrintBoard(board);
            //board.FinallyMove(2, 2, 1, 3); //blancas
            //PrintBoard(board);
            //board.FinallyMove(5, 8, 7, 8); // enroque no permitido
            //PrintBoard(board);
            //board.FinallyMove(4, 7, 4, 5);
            //PrintBoard(board);
            //board.FinallyMove(1, 3, 1, 4); //blancas
            //PrintBoard(board);
            //board.FinallyMove(1, 7, 1, 6);
            //PrintBoard(board);
            //board.FinallyMove(3, 1, 1, 3); //blancas
            //PrintBoard(board);
            //board.FinallyMove(4, 8, 4, 6);
            //PrintBoard(board);
            //board.FinallyMove(1, 4, 1, 5); //blancas
            //PrintBoard(board);
            //board.FinallyMove(8, 8, 7, 8); // muevo torre
            //PrintBoard(board);

            //board.FinallyMove(7, 2, 7, 3); //blancas
            //PrintBoard(board);
            //board.FinallyMove(7, 8, 8, 8); // muevo torre
            //PrintBoard(board);
            //board.FinallyMove(7, 3, 7, 4); //blancas
            //PrintBoard(board);
            //board.FinallyMove(5, 8, 7, 8); // enroque no permitido
            //PrintBoard(board);
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
    }
}
