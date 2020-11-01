namespace OsvaldoChessMaster.Piece
{
    using System;

    public class Pawn : PieceBase
    {
        public override bool CanJump => false;
        private bool CapturableByTheWay;
        private int turnNumberCapturableByTheWay;

        public Pawn(bool color, int PositionX, int PositionY) 
            : base(color, PositionX, PositionY)
        {            
        }

        public override bool IsValidMove(int x2, int y2)
        {
            // movimiento en el mismo lugar
            if (PositionX == x2 && PositionY == y2)
                return false;

            // si quiere moverse en la misma columna
            if (PositionX == x2)
            {
                if ((Math.Abs(y2 - PositionY) == 1) || (Math.Abs(y2 - PositionY) == 2 && (y1 == 2 || y1 == 7)))
                {
                    return true;
                }
            }

            // si quiere moverse a las columnas de al lado            
            if ((Math.Abs(x2 - PositionX) == 1) && Math.Abs(y2 - PositionY) == 1)
            {
                return true;
            }
            return false;
        }

        public override bool GetCapturableByTheWay()
        {
            return CapturableByTheWay;
        }

        public override int GetturnNumberCapturableByTheWay()
        {
            return turnNumberCapturableByTheWay;
        }

        public override void SetCapturableByTheWay(bool CapturableByTheWay, int turnNumber)
        {
            this.CapturableByTheWay = CapturableByTheWay;
            this.turnNumberCapturableByTheWay = turnNumber;
        }

        public override string ToString()
        {
            var color = this.Color ? "w" : "b";

            return $"{color}_(P)_";
        }
    }
}