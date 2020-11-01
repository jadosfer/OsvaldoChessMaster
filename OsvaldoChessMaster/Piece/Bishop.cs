namespace OsvaldoChessMaster.Piece
{
    using System;

    public class Bishop : PieceBase
    {
        public override bool CanJump => false;
        public Bishop(bool color, int PositionX, int PositionY)
            : base(color, PositionX, PositionY) { }

        public override bool IsValidMove(int x2, int y2)
        {
            // movimiento en el mismo lugar
            if (PositionX == x2 && PositionY == y2)
                return false;

            if (Math.Abs(y2 - PositionY) == (Math.Abs(x2 - PositionX)))
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            var color = this.Color ? "w" : "b";

            return $"{color}_(B)_";
        }
    }
}