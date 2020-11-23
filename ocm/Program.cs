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
            BoardLogic boardLogic = new BoardLogic(player1);
            Board board = new Board(player1);
            ArtificialIntelligence artificInt = new ArtificialIntelligence(board);

            Move[,] MovesArray = new Move[2, 2];

            boardLogic.FinallyMove(4, 1, 4, 3, board, boardLogic); //blancas            

            double[] response = artificInt.Minimize(board, boardLogic, 0);
            boardLogic.FinallyMove((int)response[0], (int)response[1], (int)response[2], (int)response[3], board, boardLogic);


            //Move move1 = artificInt.BestComputerMoveDepth4(board, boardLogic);
            //boardLogic.FinallyMove(move1.x1, move1.y1, move1.x2, move1.y2, board, boardLogic);

            //boardLogic.FinallyMove(3, 0, 6, 3, board, boardLogic); //blancas            

            //move1 = artificInt.BestComputerMoveDepth4(board, boardLogic);
            //boardLogic.FinallyMove(move1.x1, move1.y1, move1.x2, move1.y2, board, boardLogic);

            //boardLogic.FinallyMove(6, 3, 5, 2, board, boardLogic); //blancas            

            //move1 = artificInt.BestComputerMoveDepth4(board, boardLogic);
            //boardLogic.FinallyMove(move1.x1, move1.y1, move1.x2, move1.y2, board, boardLogic);

            //boardLogic.FinallyMove(5, 0, 2, 3, board, boardLogic); //blancas enroque

            //move1 = artificInt.BestComputerMoveDepth4(board, boardLogic);
            //boardLogic.FinallyMove(move1.x1, move1.y1, move1.x2, move1.y2, board, boardLogic);

            //boardLogic.FinallyMove(1, 0, 2, 2, board, boardLogic); //blancas 

            //move1 = artificInt.BestComputerMoveDepth4(board, boardLogic);
            //boardLogic.FinallyMove(move1.x1, move1.y1, move1.x2, move1.y2, board, boardLogic);

            //boardLogic.FinallyMove(2, 1, 2, 3, board, boardLogic); //blancas 

            //move1 = artificInt.BestComputerMoveDepth4(board, boardLogic);
            //boardLogic.FinallyMove(move1.x1, move1.y1, move1.x2, move1.y2, board, boardLogic);

            ////boardLogic.FinallyMove(5, 2, 4, 4, board, boardLogic); //blancas 

            //move1 = artificInt.BestComputerMoveDepth4(board, boardLogic);
            //boardLogic.FinallyMove(move1.x1, move1.y1, move1.x2, move1.y2, board, boardLogic);

            //boardLogic.FinallyMove(2, 3, 3, 4, board, boardLogic); //blancas 

            //boardLogic.FinallyMove(4, 7, 6, 7, board, boardLogic); //negras enroque            


            //boardLogic.FinallyMove(4, 4, 5, 6, board, boardLogic); //blancas 

            //move1 = artificInt.BestComputerMoveDepth4(board, boardLogic);
            //boardLogic.FinallyMove(move1.x1, move1.y1, move1.x2, move1.y2, board, boardLogic);

            //boardLogic.FinallyMove(4, 4, 5, 6, board, boardLogic); //blancas 












            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            //artificInt.BestResponse(board);
            //stopwatch.Stop();
            //Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
        }

        public static void PrintBoard(Board board)
        {
            Console.WriteLine();
            PrintElement(0, 7, board); PrintElement(1, 7, board); PrintElement(2, 7, board); PrintElement(3, 7, board); PrintElement(4, 7, board); PrintElement(5, 7, board); PrintElement(6, 7, board); PrintElement(7, 7, board); Console.WriteLine(" ");
            PrintElement(0, 6, board); PrintElement(1, 6, board); PrintElement(2, 6, board); PrintElement(3, 6, board); PrintElement(4, 6, board); PrintElement(5, 6, board); PrintElement(6, 6, board); PrintElement(7, 6, board); Console.WriteLine(" ");
            PrintElement(0, 5, board); PrintElement(1, 5, board); PrintElement(2, 5, board); PrintElement(3, 5, board); PrintElement(4, 5, board); PrintElement(5, 5, board); PrintElement(6, 5, board); PrintElement(7, 5, board); Console.WriteLine(" ");
            PrintElement(0, 4, board); PrintElement(1, 4, board); PrintElement(2, 4, board); PrintElement(3, 4, board); PrintElement(4, 4, board); PrintElement(5, 4, board); PrintElement(6, 4, board); PrintElement(7, 4, board); Console.WriteLine(" ");
            PrintElement(0, 3, board); PrintElement(1, 3, board); PrintElement(2, 3, board); PrintElement(3, 3, board); PrintElement(4, 3, board); PrintElement(5, 3, board); PrintElement(6, 3, board); PrintElement(7, 3, board); Console.WriteLine(" ");
            PrintElement(0, 2, board); PrintElement(1, 2, board); PrintElement(2, 2, board); PrintElement(3, 2, board); PrintElement(4, 2, board); PrintElement(5, 2, board); PrintElement(6, 2, board); PrintElement(7, 2, board); Console.WriteLine(" ");
            PrintElement(0, 1, board); PrintElement(1, 1, board); PrintElement(2, 1, board); PrintElement(3, 1, board); PrintElement(4, 1, board); PrintElement(5, 1, board); PrintElement(6, 1, board); PrintElement(7, 1, board); Console.WriteLine(" ");
            PrintElement(0, 0, board); PrintElement(1, 0, board); PrintElement(2, 0, board); PrintElement(3, 0, board); PrintElement(4, 0, board); PrintElement(5, 0, board); PrintElement(6, 0, board); PrintElement(7, 0, board); Console.WriteLine(" ");                        
            Console.WriteLine();
            Console.WriteLine();

        }
        public static void PrintElement(int x, int y, Board board)
        {
            if (board.ChessBoard[x, y] == null)
            {
                Console.Write("   -    ");
            }
            else
            {
                Console.Write(board.ChessBoard[x, y] + " ");
            }
        }
    }
}
