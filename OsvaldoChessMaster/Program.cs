using System;

namespace OsvaldoChessMaster
{
    class Program
    {        
        static void Main(string[] args)
        {            
            // Game game1 = new Game(true); 
            Board board1 = new Board(true);
            bool turn = true; // true si mueven blancas
            bool player1 = true; //true para blancas abajo y negras arriba

            Console.WriteLine("turno de: " + turn + " (True para blancas)");

            //------------------------------- movida CERO:
            Console.WriteLine("movida CERO: (3, 0, 3, 6) muevo Dama");
            //Console.WriteLine(board1.IsEmpty(1, 2)+" que está vacía");
            board1.MovePiece(3, 0, 3, 6, player1, turn); //muevo peon blancas una casilla para arriba            


            try
            {
                Piece piece1 = board1.GetPiece(3, 6);
                Console.WriteLine(piece1.GetType() + " ya está en (3,6)");                
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("No se pudo escoger la pieza");
            }
            try
            {                
                Piece piece1 = board1.GetPiece(3, 0);                
                Console.WriteLine(piece1.GetType() + " está aun en (3,0)");
                Console.WriteLine("Jugada Fallida");

            }
            catch (NullReferenceException e)
            {                
                turn = !turn; //cambia el turno
            }           

            Console.WriteLine("turno de: " + turn + " (True para blancas)");


            //-------------------------------1ra movida:
            Console.WriteLine("primera movida: (1, 1, 1, 2) muevo peon blancas una casilla para arriba");
            //Console.WriteLine(board1.IsEmpty(1, 2)+" que está vacía");
            board1.MovePiece(1, 1, 1, 2, player1, turn); //muevo peon blancas una casilla para arriba            
            
            
            try
            {
                Piece piece1 = board1.GetPiece(1, 2);
                Console.WriteLine(piece1.GetType() + " ya está en (1,2)");                
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("No se pudo escoger la pieza");
            }
            try
            {                
                Piece piece1 = board1.GetPiece(1, 1);
                Console.WriteLine(piece1.GetType());
                Console.WriteLine("Jugada Fallida");
            }
            catch (NullReferenceException e)
            {
                turn = !turn; //cambia el turno                
            }
            Console.WriteLine("turno de: " + turn + " (True para blancas)");

            //-------------------------------2da movida:
            Console.WriteLine("segunda movida: (1, 0, 2, 2) muevo caballo de abajo pero toca a negras(arriba)");            
            board1.MovePiece(1, 0, 2, 2, player1, turn);// muevo caballo de abajo pero toca a negras 

            Console.WriteLine("chequeo si hay algo en el casillero destino: deberia estar vacío");
            try
            {                
                Piece piece1 = board1.GetPiece(2, 2);
                Console.WriteLine(piece1.GetType() + " ya está en (2,2)");                
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("casillero vacio");
            }
            try
            {
                Piece piece1 = board1.GetPiece(1, 0);
                Console.WriteLine(piece1.GetType());
                Console.WriteLine("Jugada Fallida");                
            }
            catch (NullReferenceException e)
            {
                turn = !turn; //cambia el turno
            Console.WriteLine("turno de: " + turn + " (True para blancas)");

            //-------------------------------3ra movida:

            Console.WriteLine("3ra movida: (1, 7, 2, 5) muevo el caballo negro de arriba");
            board1.MovePiece(1, 7, 2, 5, player1, turn);//muevo caballo negro (arriba)

            try
            {
                Piece piece1 = board1.GetPiece(2, 5);
                Console.WriteLine(piece1.GetType() + " ya está en (2,5)");                
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Empty box3");
            }
            try
            {
                Piece piece1 = board1.GetPiece(1, 7);
                Console.WriteLine(piece1.GetType());
                Console.WriteLine("Jugada Fallida");
                
            }
            catch (NullReferenceException e)
            {
                turn = !turn; //cambia el turno
            }
            Console.WriteLine("turno de: " + turn + " (True para blancas)");

            //-------------------------------4a movida:

            Console.WriteLine("4a movida: (1, 0, 0, 2) muevo el caballo blanco");
            board1.MovePiece(1, 0, 0, 2, player1, turn);//muevo caballo blanco de abajo

            try
            {
                Piece piece1 = board1.GetPiece(0, 2);
                Console.WriteLine(piece1.GetType() + " ya está en (0, 2)");                
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Empty box3");
            }
            try
            {
                Piece piece1 = board1.GetPiece(1, 0);
                Console.WriteLine(piece1.GetType());
                Console.WriteLine("Jugada Fallida");                
            }
            catch (NullReferenceException e)
            {
                turn = !turn; //cambia el turno
            }
            Console.WriteLine("turno de: " + turn + " (True para blancas)");







        }
    }
}
