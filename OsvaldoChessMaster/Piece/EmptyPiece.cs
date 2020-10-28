namespace OsvaldoChessMaster.Piece
{
    public class EmptyPiece : PieceBase
    {
        public override bool CanJump => false;

        public EmptyPiece(bool color) 
            : base(color) { }

        public override bool IsValidMove(int x1, int y1, int x2, int y2)
        {
            return false;
        }

        public override string ToString()
        {
            return "___________";
        }
    }
}