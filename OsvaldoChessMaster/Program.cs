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

            //------------------------------- movida CERO:            
            Console.WriteLine("movida CERO: (3, 0, 3, 6) muevo Dama");            
            board1.MovePiece(3, 0, 3, 6, player1); //muevo peon blancas una casilla para arriba          
 
            Program.PrintBoard(board1);
            
            
            board1.MovePiece(4, 1, 4, 3, player1); 
            PrintBoard(board1);
            board1.MovePiece(4, 6, 4, 4, player1);
            PrintBoard(board1);
            board1.MovePiece(1, 7, 2, 5, player1);
            PrintBoard(board1);
            board1.MovePiece(1, 0, 0, 2, player1);
            PrintBoard(board1);
            board1.MovePiece(1, 6, 2, 5, player1);
            PrintBoard(board1);


        }
        public static void PrintBoard(Board board1)
        {
            Console.WriteLine();
            Console.Write(board1.ChessBoard[0, 7] + " "); Console.Write(board1.ChessBoard[1, 7] + " "); Console.Write(board1.ChessBoard[2, 7] + " "); Console.Write(board1.ChessBoard[3, 7] + " "); Console.Write(board1.ChessBoard[4, 7] + " "); Console.Write(board1.ChessBoard[5, 7] + " "); Console.Write(board1.ChessBoard[6, 7] + " "); Console.WriteLine(board1.ChessBoard[7, 7]);
            Console.Write(board1.ChessBoard[0, 6] + " "); Console.Write(board1.ChessBoard[1, 6] + " "); Console.Write(board1.ChessBoard[2, 6] + " "); Console.Write(board1.ChessBoard[3, 6] + " "); Console.Write(board1.ChessBoard[4, 6] + " "); Console.Write(board1.ChessBoard[5, 6] + " "); Console.Write(board1.ChessBoard[6, 6] + " "); Console.WriteLine(board1.ChessBoard[7, 6]);
            Console.Write(board1.ChessBoard[0, 5] + " "); Console.Write(board1.ChessBoard[1, 5] + " "); Console.Write(board1.ChessBoard[2, 5] + " "); Console.Write(board1.ChessBoard[3, 5] + " "); Console.Write(board1.ChessBoard[4, 5] + " "); Console.Write(board1.ChessBoard[5, 5] + " "); Console.Write(board1.ChessBoard[6, 5] + " "); Console.WriteLine(board1.ChessBoard[7, 5]);
            Console.Write(board1.ChessBoard[0, 4] + " "); Console.Write(board1.ChessBoard[1, 4] + " "); Console.Write(board1.ChessBoard[2, 4] + " "); Console.Write(board1.ChessBoard[3, 4] + " "); Console.Write(board1.ChessBoard[4, 4] + " "); Console.Write(board1.ChessBoard[5, 4] + " "); Console.Write(board1.ChessBoard[6, 4] + " "); Console.WriteLine(board1.ChessBoard[7, 4]);
            Console.Write(board1.ChessBoard[0, 3] + " "); Console.Write(board1.ChessBoard[1, 3] + " "); Console.Write(board1.ChessBoard[2, 3] + " "); Console.Write(board1.ChessBoard[3, 3] + " "); Console.Write(board1.ChessBoard[4, 3] + " "); Console.Write(board1.ChessBoard[5, 3] + " "); Console.Write(board1.ChessBoard[6, 3] + " "); Console.WriteLine(board1.ChessBoard[7, 3]);
            Console.Write(board1.ChessBoard[0, 2] + " "); Console.Write(board1.ChessBoard[1, 2] + " "); Console.Write(board1.ChessBoard[2, 2] + " "); Console.Write(board1.ChessBoard[3, 2] + " "); Console.Write(board1.ChessBoard[4, 2] + " "); Console.Write(board1.ChessBoard[5, 2] + " "); Console.Write(board1.ChessBoard[6, 2] + " "); Console.WriteLine(board1.ChessBoard[7, 2]);
            Console.Write(board1.ChessBoard[0, 1] + " "); Console.Write(board1.ChessBoard[1, 1] + " "); Console.Write(board1.ChessBoard[2, 1] + " "); Console.Write(board1.ChessBoard[3, 1] + " "); Console.Write(board1.ChessBoard[4, 1] + " "); Console.Write(board1.ChessBoard[5, 1] + " "); Console.Write(board1.ChessBoard[6, 1] + " "); Console.WriteLine(board1.ChessBoard[7, 1]);
            Console.Write(board1.ChessBoard[0, 0] + " "); Console.Write(board1.ChessBoard[1, 0] + " "); Console.Write(board1.ChessBoard[2, 0] + " "); Console.Write(board1.ChessBoard[3, 0] + " "); Console.Write(board1.ChessBoard[4, 0] + " "); Console.Write(board1.ChessBoard[5, 0] + " "); Console.Write(board1.ChessBoard[6, 0] + " "); Console.WriteLine(board1.ChessBoard[7, 0]);
            Console.WriteLine();
        }        
    }
}
