using System;
using System.Collections.Generic;
using System.Text;

namespace OsvaldoChessMaster
{
    public class GameDisplay
    {
        //public GameDisplay(board)
        //{
        //    // Inicializacion de variables            
        //    player1 = true;
        //    Turn = true;
        //    TurnNumber = 0;
        //    FullMoveNumber = 0;
        //    this.stackFullPlay = new Stack<string>();
        //    FullMoveNumber = 1;
        //    Board board = new Board(player1);
        //}
        public static void PrintBoard(Board board, BoardLogic boardLogic, int x1, int y1, int x2, int y2)
        {
            Console.WriteLine();
            Console.WriteLine("----- MOVE: " + " x1 = " + x1 + ", y1 = " + y1 + ", x2 = " + x2 + ", y2 = " + y2 + " -----");
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
            
            if (board.IsCheckmateFlag)
            {
                Console.WriteLine("------------Game Over - CHECK MATE------------");
            }
            else if (board.IsCheckFlag && !board.IsCantMoveCheckFlag)
            {
                Console.WriteLine("------------Be Carefull - IS CHECK------------");
            }
            else if (board.IsCheckFlag && board.IsCantMoveCheckFlag)
            {
                Console.WriteLine("------------Can't move there and is Still CHECK------------");
            }
            else if (!board.IsCheckFlag && board.IsCantMoveCheckFlag)
            {
                Console.WriteLine("------------Can't move there, is CHECK------------");
            }
            string colorTurn = board.Turn ? "WhiteArmy" : "BlackArmy";
            Console.WriteLine("---------------- TURN: " + colorTurn + " ----------------");
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
