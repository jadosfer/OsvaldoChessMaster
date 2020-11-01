namespace OsvaldoChessMaster.Piece
{
    public abstract class PieceBase
    {
        public abstract bool CanJump { get; }
        //public int PositionX { get; set; }
        //public int PositionY { get; set; }
        public virtual bool CanCastling { get; set; }
        public bool LCastling { get; set; }
        public bool SCastling { get; set; }
        public bool Color { get; set; }

        public PieceBase(bool color)
        {
            this.Color = color;
        }

        public abstract bool IsValidMove(int x1, int y1, int x2, int y2);
        public virtual bool GetCapturableByTheWay() => true;
        public virtual void SetCapturableByTheWay(bool CapturableByTheWay, int turnNumber) { }
        public virtual int GetturnNumberCapturableByTheWay() => 0;
    }
}