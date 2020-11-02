using System;
using System.Collections.Generic;
using System.Text;

namespace OsvaldoChessMaster.Piece
{
    public class PieceStatus
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public PieceStatus()
        {            
        }

        public PieceStatus(int positionX, int positionY)
        {
            this.PositionX = positionX;
            this.PositionY = positionY;
        }
    }
}
