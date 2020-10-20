namespace OsvaldoChessMaster.Piece
{
    using System;

    public class Bishop : PieceBase
    {
        public override bool CanJump => false;
        public Bishop(bool color) : base(color) { }

        public override bool IsValidMove(int x1, int y1, int x2, int y2)
        {
            if (Math.Abs(y2 - y1) == (Math.Abs(x2 - x1)))
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