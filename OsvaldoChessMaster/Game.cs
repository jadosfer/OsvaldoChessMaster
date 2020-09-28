using System;
using System.Collections.Generic;
using System.Text;

namespace OsvaldoChessMaster
{
    public class Game
    { 
        bool playing = false;
        public Game(bool player1)
        {
            //Console.WriteLine("Ingrese -n para nueva partida, -c para cargar una partida guardada, o -s para salir");
            //Console.ReadLine();
            NewGame(player1);
        }

        public void NewGame(bool player1) 
        {
            Board board1 = new Board(player1);
        }
        public void ReiniciarPartida() { }
        public void GuardarPartida() { }

    



    }
}
