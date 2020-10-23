using System;

namespace OsvaldoChessMaster
{
    class Program
    {
        
        static void Main(string[] args)
        {
            // Game game1 = new Game(true);             
            Board board1 = new Board(true);            
            //bool turn = true; // true si mueven blancas
            bool player1 = true; //true para blancas abajo y negras arriba
            
            Program.PrintBoard(board1);

            board1.FinallyMove(5, 2, 5, 4, player1); //blancas
            PrintBoard(board1);
            board1.FinallyMove(5, 7, 5, 5, player1);
            PrintBoard(board1);            
            board1.FinallyMove(6, 1, 3, 4, player1); //blancas
            PrintBoard(board1);
            board1.FinallyMove(6, 7, 6, 6, player1);
            PrintBoard(board1);
            board1.FinallyMove(1, 2, 1, 3, player1); //blancas
            PrintBoard(board1);
            board1.FinallyMove(7, 8, 8, 6, player1);
            PrintBoard(board1);
            board1.FinallyMove(7, 1, 8, 3, player1); //blancas
            PrintBoard(board1);
            board1.FinallyMove(6, 8, 1, 3, player1);
            PrintBoard(board1);
            board1.FinallyMove(2, 2, 1, 3, player1); //blancas
            PrintBoard(board1);
            board1.FinallyMove(5, 8, 7, 8, player1); // enroque no permitido
            PrintBoard(board1);
            board1.FinallyMove(4, 7, 4, 5, player1);
            PrintBoard(board1);
            board1.FinallyMove(1, 3, 1, 4, player1); //blancas
            PrintBoard(board1);
            board1.FinallyMove(1, 7, 1, 6, player1);
            PrintBoard(board1);
            board1.FinallyMove(3, 1, 1, 3, player1); //blancas
            PrintBoard(board1);
            board1.FinallyMove(4, 8, 4, 6, player1);
            PrintBoard(board1);
            board1.FinallyMove(1, 4, 1, 5, player1); //blancas
            PrintBoard(board1);
            board1.FinallyMove(8, 8, 7, 8, player1); // muevo torre
            PrintBoard(board1);
            board1.FinallyMove(7, 2, 7, 3, player1); //blancas
            PrintBoard(board1);
            board1.FinallyMove(7, 8, 8, 8, player1); // muevo torre
            PrintBoard(board1);
            board1.FinallyMove(7, 3, 7, 4, player1); //blancas
            PrintBoard(board1);
            board1.FinallyMove(5, 8, 7, 8, player1); // enroque no permitido
            PrintBoard(board1);
            board1.FinallyMove(5, 8, 6, 8, player1); 
            PrintBoard(board1);
            board1.FinallyMove(1, 3, 4, 6, player1); //blancas
            PrintBoard(board1);
            board1.FinallyMove(6, 8, 7, 8, player1);
            PrintBoard(board1);
            board1.FinallyMove(3, 4, 4, 5, player1); //blancas
            PrintBoard(board1);





        }
        public static void PrintBoard(Board board1)
        {
            Console.WriteLine();
            Console.Write(board1.ChessBoard[1, 8] + " "); Console.Write(board1.ChessBoard[2, 8] + " "); Console.Write(board1.ChessBoard[3, 8] + " "); Console.Write(board1.ChessBoard[4, 8] + " "); Console.Write(board1.ChessBoard[5, 8] + " "); Console.Write(board1.ChessBoard[6, 8] + " "); Console.Write(board1.ChessBoard[7, 8] + " "); Console.WriteLine(board1.ChessBoard[8, 8] + " ");
            Console.Write(board1.ChessBoard[1, 7] + " "); Console.Write(board1.ChessBoard[2, 7] + " "); Console.Write(board1.ChessBoard[3, 7] + " "); Console.Write(board1.ChessBoard[4, 7] + " "); Console.Write(board1.ChessBoard[5, 7] + " "); Console.Write(board1.ChessBoard[6, 7] + " "); Console.Write(board1.ChessBoard[7, 7] + " "); Console.WriteLine(board1.ChessBoard[8, 7] + " ");
            Console.Write(board1.ChessBoard[1, 6] + " "); Console.Write(board1.ChessBoard[2, 6] + " "); Console.Write(board1.ChessBoard[3, 6] + " "); Console.Write(board1.ChessBoard[4, 6] + " "); Console.Write(board1.ChessBoard[5, 6] + " "); Console.Write(board1.ChessBoard[6, 6] + " "); Console.Write(board1.ChessBoard[7, 6] + " "); Console.WriteLine(board1.ChessBoard[8, 6] + " ");
            Console.Write(board1.ChessBoard[1, 5] + " "); Console.Write(board1.ChessBoard[2, 5] + " "); Console.Write(board1.ChessBoard[3, 5] + " "); Console.Write(board1.ChessBoard[4, 5] + " "); Console.Write(board1.ChessBoard[5, 5] + " "); Console.Write(board1.ChessBoard[6, 5] + " "); Console.Write(board1.ChessBoard[7, 5] + " "); Console.WriteLine(board1.ChessBoard[8, 5] + " ");
            Console.Write(board1.ChessBoard[1, 4] + " "); Console.Write(board1.ChessBoard[2, 4] + " "); Console.Write(board1.ChessBoard[3, 4] + " "); Console.Write(board1.ChessBoard[4, 4] + " "); Console.Write(board1.ChessBoard[5, 4] + " "); Console.Write(board1.ChessBoard[6, 4] + " "); Console.Write(board1.ChessBoard[7, 4] + " "); Console.WriteLine(board1.ChessBoard[8, 4] + " ");
            Console.Write(board1.ChessBoard[1, 3] + " "); Console.Write(board1.ChessBoard[2, 3] + " "); Console.Write(board1.ChessBoard[3, 3] + " "); Console.Write(board1.ChessBoard[4, 3] + " "); Console.Write(board1.ChessBoard[5, 3] + " "); Console.Write(board1.ChessBoard[6, 3] + " "); Console.Write(board1.ChessBoard[7, 3] + " "); Console.WriteLine(board1.ChessBoard[8, 3] + " ");
            Console.Write(board1.ChessBoard[1, 2] + " "); Console.Write(board1.ChessBoard[2, 2] + " "); Console.Write(board1.ChessBoard[3, 2] + " "); Console.Write(board1.ChessBoard[4, 2] + " "); Console.Write(board1.ChessBoard[5, 2] + " "); Console.Write(board1.ChessBoard[6, 2] + " "); Console.Write(board1.ChessBoard[7, 2] + " "); Console.WriteLine(board1.ChessBoard[8, 2] + " ");
            Console.Write(board1.ChessBoard[1, 1] + " "); Console.Write(board1.ChessBoard[2, 1] + " "); Console.Write(board1.ChessBoard[3, 1] + " "); Console.Write(board1.ChessBoard[4, 1] + " "); Console.Write(board1.ChessBoard[5, 1] + " "); Console.Write(board1.ChessBoard[6, 1] + " "); Console.Write(board1.ChessBoard[7, 1] + " "); Console.WriteLine(board1.ChessBoard[8, 1] + " ");
            Console.WriteLine();
        }        
    }
}
