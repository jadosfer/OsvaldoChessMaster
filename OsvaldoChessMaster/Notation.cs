using System;
using System.Collections.Generic;
using System.Text;

namespace OsvaldoChessMaster
{
    class Notation
    {
        public Notation(Board board1) 
        {

        }
        
        private int moveNumber; // se compone de la suma de los movement de ambos jugadores
        private static Dictionary<int, string> columnLetters = new Dictionary<int, string>
        {
            { 0, "a" }, { 1, "b" }, { 2, "c" }, { 3, "d" }, { 4, "e" }, { 5, "f" }, { 6, "g" }, { 7, "h" }
        };

        public static void WriteMovePawnMoving(int x1, int y1, int x2, int y2, Piece piece1)
        {
            string movement = "";
            if (piece1.GetType() == typeof(Pawn))
            {
                if (!Board.IsPawnCapturing(x1, x2))
                {
                    movement = movement.Insert(0, columnLetters[y2]);
                    movement = movement.Insert(1, y2.ToString());
                    Console.WriteLine("este es script: " + script);
                }
            }
        }

        public static void WriteMovePawnCapturing(int x1, int y1, int x2, int y2, Piece piece1)
        {            
            string movement = "";
            if (piece1.GetType() == typeof(Pawn))
            {
                if (Board.IsPawnCapturing(x1, x2))
                {
                    movement = movement.Insert(0, columnLetters[y2]);
                    movement = movement.Insert(1, rowsToString[y2]);
                    Console.WriteLine("este es script: " + movement);
                }
            }
        }
        public Stack<string> StackMovements(string movement)
        {
            Stack<string> movements = new Stack<string>();
            movements.Push(movement);
            return movements;
        }
    }
}
