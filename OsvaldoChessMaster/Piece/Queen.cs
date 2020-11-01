namespace OsvaldoChessMaster.Piece
{
    using System;

    public class Queen : PieceBase
    {
        public override bool CanJump => false;

        public Queen() { }

        public Queen(bool color) 
            : base(color) { }

        public override bool IsValidMove(int x1, int y1, int x2, int y2)
        {
            // movimiento en el mismo lugar
            if (x1 == x2 && y1 == y2)
                return false;

            if (x1 == x2 || y1 == y2)
            {
                return true;
            }

            if (Math.Abs(y2 - y1) == (Math.Abs(x2 - x1)))
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            var color = this.Color ? "w" : "b";

            return $"{color}_(QUEEN_)_";
        }

        public override object Clone()
        {
            return this.Clone<Queen>();
        }
    }
}