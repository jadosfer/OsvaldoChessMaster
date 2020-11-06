namespace OsvaldoChessMaster.Piece
{
    using System;

    public class Bishop : PieceBase
    {
        public override bool CanJump => false;
        public override int Value => 30;
        public Bishop() { }

        public Bishop(bool color, int PositionX, int PositionY)
            : base(color, PositionX, PositionY) { }

        public override bool IsValidMove(int x2, int y2)
        {
            // movimiento en el mismo lugar
            if (Position.PositionX == x2 && Position.PositionY == y2)
                return false;

            if (Math.Abs(y2 - Position.PositionY) == (Math.Abs(x2 - Position.PositionX)))
            {
                return true;
            }

            return false;
        }
        
        public override string ToString()
        {
            var color = this.Color ? "w" : "b";

            return $"{color}_BISHOP_";
        }

        public override object Clone()
        {
            return this.Clone<Bishop>();
        }
    }
}