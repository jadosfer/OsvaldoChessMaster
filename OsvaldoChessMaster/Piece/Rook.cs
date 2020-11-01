namespace OsvaldoChessMaster.Piece
{
    public class Rook : PieceBase
    {
        public override bool CanJump => false;
        

        public Rook(bool color, int PositionX, int PositionY)
            : base(color, PositionX, PositionY)
        {
            CanCastling = true;
        }

        public override bool IsValidMove(int x2, int y2)
        {
            // movimiento en el mismo lugar
            if (PositionX == x2 && PositionY == y2)
                return false;

            if (PositionX == x2 || PositionY == y2)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            var color = this.Color ? "w" : "b";

            return $"{color}_(R)_";
        }
    }
}