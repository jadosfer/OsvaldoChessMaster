using System;
using OsvaldoChessMaster;
using OsvaldoChessMaster.Piece;
using System.Collections.Generic;
using System.Diagnostics;

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

            Move[,] MovesArray = new Move[2, 2];


            board.FinallyMove(4, 1, 4, 3); //blancas            
            PrintBoard(board);
            //board.FinallyMove(artificInt.BestComputerMoveDepth4(board).x1, artificInt.BestComputerMoveDepth4(board).y1, artificInt.BestComputerMoveDepth4(board).x2, artificInt.BestComputerMoveDepth4(board).y2);
            //PrintBoard(board);
            //board.FinallyMove(5, 2, 5, 4); //blancas
            //PrintBoard(board);
            //board.FinallyMove(artificInt.BestComputerMoveDepth4(board).x1, artificInt.BestComputerMoveDepth4(board).y1, artificInt.BestComputerMoveDepth4(board).x2, artificInt.BestComputerMoveDepth4(board).y2);
            //PrintBoard(board);

            board.FinallyMove(4, 6, 4, 4);
            PrintBoard(board);
            board.FinallyMove(5, 0, 2, 3); //blancas
            PrintBoard(board);
            board.FinallyMove(5, 6, 5, 5);
            PrintBoard(board);
            board.FinallyMove(0, 1, 0, 2); //blancas
            PrintBoard(board);
            board.FinallyMove(6, 7, 7, 5);
            PrintBoard(board);
            board.FinallyMove(6, 0, 7, 2); //blancas
            PrintBoard(board);
            board.FinallyMove(5, 7, 0, 2);
            PrintBoard(board);
            board.FinallyMove(4, 0, 6, 0); //blancas
            PrintBoard(board);
            board.FinallyMove(4, 7, 6, 7); // enroque no permitido
            PrintBoard(board);
            board.FinallyMove(3, 6, 3, 4);
            PrintBoard(board);


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

            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            //artificInt.BestResponse(board);
            //stopwatch.Stop();
            //Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
        }

        public static void PrintBoard(Board board)
        {
            Console.WriteLine();
            Console.Write(board.ChessBoard[0, 7] + " "); Console.Write(board.ChessBoard[1, 7] + " "); Console.Write(board.ChessBoard[2, 7] + " "); Console.Write(board.ChessBoard[3, 7] + " "); Console.Write(board.ChessBoard[4, 7] + " "); Console.Write(board.ChessBoard[5, 7] + " "); Console.Write(board.ChessBoard[6, 7] + " "); Console.WriteLine(board.ChessBoard[7, 7] + " ");
            Console.Write(board.ChessBoard[0, 6] + " "); Console.Write(board.ChessBoard[1, 6] + " "); Console.Write(board.ChessBoard[2, 6] + " "); Console.Write(board.ChessBoard[3, 6] + " "); Console.Write(board.ChessBoard[4, 6] + " "); Console.Write(board.ChessBoard[5, 6] + " "); Console.Write(board.ChessBoard[6, 6] + " "); Console.WriteLine(board.ChessBoard[7, 6] + " ");
            Console.Write(board.ChessBoard[0, 5] + " "); Console.Write(board.ChessBoard[1, 5] + " "); Console.Write(board.ChessBoard[2, 5] + " "); Console.Write(board.ChessBoard[3, 5] + " "); Console.Write(board.ChessBoard[4, 5] + " "); Console.Write(board.ChessBoard[5, 5] + " "); Console.Write(board.ChessBoard[6, 5] + " "); Console.WriteLine(board.ChessBoard[7, 5] + " ");
            Console.Write(board.ChessBoard[0, 4] + " "); Console.Write(board.ChessBoard[1, 4] + " "); Console.Write(board.ChessBoard[2, 4] + " "); Console.Write(board.ChessBoard[3, 4] + " "); Console.Write(board.ChessBoard[4, 4] + " "); Console.Write(board.ChessBoard[5, 4] + " "); Console.Write(board.ChessBoard[6, 4] + " "); Console.WriteLine(board.ChessBoard[7, 4] + " ");
            Console.Write(board.ChessBoard[0, 3] + " "); Console.Write(board.ChessBoard[1, 3] + " "); Console.Write(board.ChessBoard[2, 3] + " "); Console.Write(board.ChessBoard[3, 3] + " "); Console.Write(board.ChessBoard[4, 3] + " "); Console.Write(board.ChessBoard[5, 3] + " "); Console.Write(board.ChessBoard[6, 3] + " "); Console.WriteLine(board.ChessBoard[7, 3] + " ");
            Console.Write(board.ChessBoard[0, 2] + " "); Console.Write(board.ChessBoard[1, 2] + " "); Console.Write(board.ChessBoard[2, 2] + " "); Console.Write(board.ChessBoard[3, 2] + " "); Console.Write(board.ChessBoard[4, 2] + " "); Console.Write(board.ChessBoard[5, 2] + " "); Console.Write(board.ChessBoard[6, 2] + " "); Console.WriteLine(board.ChessBoard[7, 2] + " ");
            Console.Write(board.ChessBoard[0, 1] + " "); Console.Write(board.ChessBoard[1, 1] + " "); Console.Write(board.ChessBoard[2, 1] + " "); Console.Write(board.ChessBoard[3, 1] + " "); Console.Write(board.ChessBoard[4, 1] + " "); Console.Write(board.ChessBoard[5, 1] + " "); Console.Write(board.ChessBoard[6, 1] + " "); Console.WriteLine(board.ChessBoard[7, 1] + " ");
            Console.Write(board.ChessBoard[0, 0] + " "); Console.Write(board.ChessBoard[1, 0] + " "); Console.Write(board.ChessBoard[2, 0] + " "); Console.Write(board.ChessBoard[3, 0] + " "); Console.Write(board.ChessBoard[4, 0] + " "); Console.Write(board.ChessBoard[5, 0] + " "); Console.Write(board.ChessBoard[6, 0] + " "); Console.WriteLine(board.ChessBoard[7, 0] + " ");
            Console.WriteLine();
        }
    }
}
