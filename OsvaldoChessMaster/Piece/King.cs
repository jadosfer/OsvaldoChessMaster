namespace OsvaldoChessMaster.Piece
{
    using System;

    public class King : PieceBase
    {
        public override bool CanJump => false;
        public override bool CanCastling { get; set; }

        public King(bool color) 
            : base(color)
        {
            this.CanCastling = true;
        }

        public override bool IsValidMove(int x1, int y1, int x2, int y2)
        {
            if (Math.Sqrt((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1)) < 2)
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