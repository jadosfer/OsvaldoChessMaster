using System;
using System.Collections.Generic;
using System.Text;

namespace OsvaldoChessMaster
{
    class Notation
    {        
        private static Dictionary<int, string> columnLetters = new Dictionary<int, string> 
        { 
            { 0, "a" }, { 1, "b" }, { 2, "c" }, { 3, "d" }, { 4, "e" }, { 5, "f" }, { 6, "g" }, { 7, "h" } 
        };
        private static Dictionary<int, string> rowsToString = new Dictionary<int, string>
        {
            { 0, "0" }, { 1, "1" }, { 2, "2" }, { 3, "3" }, { 4, "4" }, { 5, "5" }, { 6, "6" }, { 7, "7" }
        };

        public static void WriteMove(int x1, int y1, int x2, int y2, Piece piece1)
        {
            string script = "";
            if (piece1.GetType() == typeof(Pawn))
            {                
                script = script.Insert(0, columnLetters[y2]);
                script = script.Insert(1, rowsToString[y2]);
                Console.WriteLine("este es script: " + script);
            }
            
                      

        }
    }
}
