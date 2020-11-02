namespace OsvaldoChessMaster.Piece
{
    using System;

    public class Knight : PieceBase
    {
        public override bool CanJump => true;
        public Knight() { }
        public Knight(bool color, int PositionX, int PositionY)
            : base(color, PositionX, PositionY) { }

        public override bool IsValidMove(int x2, int y2)
        {
            // movimiento en el mismo lugar
            if (Position.PositionX == x2 && Position.PositionY == y2)
                return false;

            if ((Math.Abs(y2 - Position.PositionY) == 2 && (Math.Abs(x2 - Position.PositionX) == 1)) || (Math.Abs(y2 - Position.PositionY) == 1 && (Math.Abs(x2 - Position.PositionX) == 2)))
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            var color = this.Color ? "w" : "b";

            return $"{color}_KNIGHT_";
        }
        public override object Clone()
        {
            return this.Clone<Knight>();
        }
    }
}