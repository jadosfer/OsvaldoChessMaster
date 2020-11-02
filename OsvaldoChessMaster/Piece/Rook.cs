namespace OsvaldoChessMaster.Piece
{
    public class Rook : PieceBase
    {
        public override bool CanJump => false;

        public Rook() { }

        public Rook(bool color, int PositionX, int PositionY)
            : base(color, PositionX, PositionY)
        {
            CanCastling = true;
        }

        public override bool IsValidMove(int x2, int y2)
        {
            // movimiento en el mismo lugar
            if (Position.PositionX == x2 && Position.PositionY == y2)
                return false;

            if (Position.PositionX == x2 || Position.PositionY == y2)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            var color = this.Color ? "w" : "b";

            return $"{color}__ROOK__";
        }
        public override object Clone()
        {
            return this.Clone<Rook>();
        }
    }
}