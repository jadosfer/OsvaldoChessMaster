using System;

namespace OsvaldoChessMaster
{
    class Program
    {
        private bool turn;
        private bool player1;
        static void Main(string[] args)
        {            
            // Game game1 = new Game(true); 
            Board board1 = new Board(true); //true para blancas abajo y negras arriba
            bool turn = true;
            bool player1 = true;

            Console.WriteLine("turno de: " + turn + " (True para blancas)");
            //-------------------------------1ra movida:
            Console.WriteLine("primera movida: (1, 1, 1, 2) muevo peon blancas una casilla para arriba");
            board1.MovePiece(1, 1, 1, 2, player1, turn); //muevo peon blancas una casilla para arriba            
            turn = !turn; //cambia el turno, ahora toca a negras
            
            try
            {
                Piece piece1 = board1.GetPiece(1, 2);                
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("No se pudo escoger la pieza");
            }
            Console.WriteLine("turno de: " + turn + " (True para blancas)");
            //-------------------------------2da movida:
            Console.WriteLine("segunda movida: (1, 0, 2, 2) muevo caballo de abajo pero toca a negras(arriba)");            
            board1.MovePiece(1, 0, 2, 2, player1, turn);// muevo caballo de abajo pero toca a negras 

            Console.WriteLine("chequeo si hay algo en el casillero destino: deberia estar vacío");
            try
            {                
                Piece piece1 = board1.GetPiece(2, 2);
                Console.WriteLine(piece1.GetType());
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("casillero vacio");
            }
            Console.WriteLine("turno de: " + turn + " (True para blancas)");
            //-------------------------------3ra movida:

            Console.WriteLine("3ra movida: (1, 7, 2, 5) muevo el caballo negro de arriba");
            board1.MovePiece(1, 7, 2, 5, player1, turn);//muevo caballo negro (arriba)

            try
            {
                Piece piece1 = board1.GetPiece(2, 5);                
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Empty box3");
            }

            try
            {
                Piece piece1 = board1.GetPiece(1, 1);
                Console.WriteLine(piece1.GetType());
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("casillero vacio");
            }
            try
            {
                Piece piece1 = board1.GetPiece(1, 0);
                Console.WriteLine(piece1.GetType());
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("casillero vacio");
            }
            try
            {
                Piece piece1 = board1.GetPiece(1, 7);
                Console.WriteLine(piece1.GetType());
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("casillero vacio");
            }








        }
    }
}
