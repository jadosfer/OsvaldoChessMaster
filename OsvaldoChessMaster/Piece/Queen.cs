namespace OsvaldoChessMaster.Piece
{
    using System;

    public class Queen : PieceBase
    {
        public override bool CanJump => false;

        public Queen(bool color, int PositionX, int PositionY)
            : base(color, PositionX, PositionY) { }

    public override bool IsValidMove(int x2, int y2)
        {
            // movimiento en el mismo lugar
            if (PositionX == x2 && PositionY == y2)
                return false;

            if (PositionX == x2 || PositionY == y2)
            {
                return true;
            }

            if (Math.Abs(y2 - PositionY) == (Math.Abs(x2 - PositionX)))
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            var color = this.Color ? "w" : "b";

            return $"{color}_(Q)_";
        }
    }
}