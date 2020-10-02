using System;
using System.Collections.Generic;
using System.Text;

namespace OsvaldoChessMaster
{
    public class Notation
    {
        public readonly Board _board;        
        Stack<string> StackFullPlay = new Stack<string>();
        private static string movement = ""; // se llena con las dos movidas y luego se reinicia
        private static Dictionary<int, string> columnLetters = new Dictionary<int, string>
        {
            { 0, "a" }, { 1, "b" }, { 2, "c" }, { 3, "d" }, { 4, "e" }, { 5, "f" }, { 6, "g" }, { 7, "h" }
        };

        public Notation(Board board1)
        {
            _board = board1;
        }
        public void WriteMove(int x1, int y1, int x2, int y2, Piece piece1, bool player1, bool turn, int turnNumber)
        {
            int pointer = 0;
            if (player1 == turn) // para escribir el numero de la jugada
            {
                movement = movement.Insert(pointer, turnNumber.ToString());
                movement = movement.Insert(pointer, ".");
                pointer += turnNumber.ToString().Length + 1;
            }
            movement = movement.Insert(pointer++, " ");
            if (piece1.GetType() == typeof(Pawn) && _board.IsPawnCapturing(x1, x2)) //peon comiendo             
            {
                movement = movement.Insert(pointer++, columnLetters[x1]);
            }
            movement = movement.Insert(pointer++, " ");
            if (piece1.GetType() == typeof(Bishop))
            {
                movement = movement.Insert(pointer++, "B");
            }
            if (piece1.GetType() == typeof(Horse))
            {
                movement = movement.Insert(pointer++, "N");
            }
            if (piece1.GetType() == typeof(Queen))
            {
                movement = movement.Insert(pointer++, "Q");
            }
            if (piece1.GetType() == typeof(King))
            {
                movement = movement.Insert(pointer++, "K");
            }
            if (piece1.GetType() == typeof(Bishop))
            {
                movement = movement.Insert(pointer++, "R");
            }

            movement = movement.Insert(pointer++, columnLetters[x2]);
            movement = movement.Insert(pointer++, y2.ToString());
            Console.WriteLine("este es script: " + movement);

            if (player1 != turn) // guarda la juagda completa
            {
                StackFullPlay.Push(movement);
                movement = "";
            }
            Console.WriteLine("este es el StackFullPlay:" + StackFullPlay);
        }
    }
}
