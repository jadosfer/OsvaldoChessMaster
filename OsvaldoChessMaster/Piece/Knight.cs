namespace OsvaldoChessMaster.Piece
{
    using System;

    public class Knight : PieceBase
    {
        public override bool CanJump => true;
        public Knight(bool color, int PositionX, int PositionY)
            : base(color, PositionX, PositionY) { }

        public override bool IsValidMove(int x2, int y2)
        {
            // movimiento en el mismo lugar
            if (PositionX == x2 && PositionY == y2)
                return false;

            if ((Math.Abs(y2 - PositionY) == 2 && (Math.Abs(x2 - PositionX) == 1)) || (Math.Abs(y2 - y1) == 1 && (Math.Abs(x2 - x1) == 2)))
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            var color = this.Color ? "w" : "b";

            return $"{color}_(N)_";
        }
    }
}