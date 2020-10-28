namespace OsvaldoChessMaster.Piece
{
    public class Rook : PieceBase
    {
        public override bool CanJump => false;

        public Rook(bool color) 
            : base(color)
        {
            CanCastling = true;
        }

        public override bool IsValidMove(int x1, int y1, int x2, int y2)
        {
            // movimiento en el mismo lugar
            if (x1 == x2 && y1 == y2)
                return false;

            if (x1 == x2 || y1 == y2)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            var color = this.Color ? "w" : "b";

            return $"{color}_(_ROOK_)_";
        }
    }
}