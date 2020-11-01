namespace OsvaldoChessMaster.Piece
{
    public abstract class PieceBase
    {
        public abstract bool CanJump { get; }        
        public virtual bool CanCastling { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public bool LongCastling { get; set; }
        public bool ShortCastling { get; set; }
        public bool Color { get; set; }

        public PieceBase(bool color, int PositionX, int PositionY)
        {
            this.Color = color;
            this.PositionX = PositionX;
            this.PositionY = PositionY;
        }

        public abstract bool IsValidMove(int x2, int y2);
        public virtual bool GetCapturableByTheWay() => true;
        public virtual void SetCapturableByTheWay(bool CapturableByTheWay, int turnNumber) { }
        public virtual int GetturnNumberCapturableByTheWay() => 0;

        public bool Equals([AllowNull] PieceBase other)
        {
            if (other == null)
                return false;

            if (other.CanJump != this.CanJump)
                return false;

            if (other.CanCastling != this.CanCastling)
                return false;

            if (other.LCastling != this.LCastling)
                return false;

            if (other.SCastling != this.SCastling)
                return false;

            if (other.Color != this.Color)
                return false;

            return true;
        }

        public abstract object Clone();

        protected T Clone<T>()
            where T : PieceBase, new()
        {
            var clone = new T()
            {
                CanCastling = this.CanCastling,
                LCastling = this.LCastling,
                SCastling = this.SCastling,
                Color = this.Color
            };

            return clone;
        }
    }
}