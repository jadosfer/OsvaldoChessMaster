namespace OsvaldoChessMaster.Piece
{
    using System;

    public class Pawn : PieceBase
    {
        public override bool CanJump => false;
        private bool CapturableByTheWay;
        private int turnNumberCapturableByTheWay;

        public Pawn() { }
        public Pawn(bool color, int PositionX, int PositionY) 
            : base(color, PositionX, PositionY)
        {            
        }

        public override bool IsValidMove(int x2, int y2)
        {
            // movimiento en el mismo lugar
            if (Position.PositionX == x2 && Position.PositionY == y2)
                return false;

            // si quiere moverse en la misma columna
            if (Position.PositionX == x2)
            {
                if ((Math.Abs(y2 - Position.PositionY) == 1) || (Math.Abs(y2 - Position.PositionY) == 2 && (Position.PositionY == 2 || Position.PositionY == 7)))
                {
                    return true;
                }
            }

            // si quiere moverse a las columnas de al lado            
            if ((Math.Abs(x2 - Position.PositionX) == 1) && Math.Abs(y2 - Position.PositionY) == 1)
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

            return $"{color}  PAWN  ";
        }
        public override object Clone()
        {
            return this.Clone<Pawn>();
        }
    }
}