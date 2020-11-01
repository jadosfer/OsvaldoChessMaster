namespace OsvaldoChessMaster.Piece
{
    using System;

    public class King : PieceBase
    {
        public override bool CanJump => false;

        public King()
            : this(false)
        {
        }

        public King(bool color) 
            : base(color)
        {
            this.CanCastling = true;
        }

        public override bool IsValidMove(int x1, int y1, int x2, int y2)
        {
            // movimiento en el mismo lugar
            if (x1 == x2 && y1 == y2)
                return false;

            if (Math.Sqrt((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1)) < 2)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            var color = this.Color ? "w" : "b";

            return $"{color}_(_KING_)_";
        }

        public override object Clone()
        {
            return this.Clone<King>();
        }
    }
}