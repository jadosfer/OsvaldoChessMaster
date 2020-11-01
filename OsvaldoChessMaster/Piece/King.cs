namespace OsvaldoChessMaster.Piece
{
    using System;

    public class King : PieceBase
    {
        public override bool CanJump => false;
        public override bool CanCastling { get; set; }

        public King(bool color, int PositionX, int PositionY)
            : base(color, PositionX, PositionY)
        {
            this.CanCastling = true;
        }

        public override bool IsValidMove(int x2, int y2)
        {
            // movimiento en el mismo lugar
            if (PositionX == x2 && PositionY == y2)
                return false;

            if (Math.Sqrt((y2 - PositionY) * (y2 - PositionY) + (x2 - PositionX) * (x2 - PositionX)) < 2)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            var color = this.Color ? "w" : "b";

            return $"{color}_(K)_";
        }
    }
}