namespace OsvaldoChessMaster.Piece
{
    using System;

    public class Bishop : PieceBase
    {
        public override bool CanJump => false;

        public Bishop() { }

        public Bishop(bool color) : base(color) { }

        public override bool IsValidMove(int x1, int y1, int x2, int y2)
        {
            // movimiento en el mismo lugar
            if (x1 == x2 && y1 == y2)
                return false;

            if (Math.Abs(y2 - y1) == (Math.Abs(x2 - x1)))
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            var color = this.Color ? "w" : "b";

            return $"{color}_(BISHOP)_";
        }

        public override object Clone()
        {
            return this.Clone<Bishop>();
        }
    }
}