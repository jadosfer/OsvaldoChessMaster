using System;
using System.Diagnostics.CodeAnalysis;

namespace OsvaldoChessMaster.Piece
{
    public abstract class PieceBase : ICloneable, IEquatable<PieceBase>
    {
        public abstract bool CanJump { get; }        
        public virtual bool CanCastling { get; set; }
        public PieceStatus Position{ get; set; }

    
        public bool LongCastling { get; set; }
        public bool ShortCastling { get; set; }
        public bool Color { get; set; }

        public PieceBase(bool color = false, int PositionX = 0, int PositionY = 0)
        {
            this.Position = new PieceStatus(PositionX, PositionY);
            this.Color = color;            
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

            if (other.LongCastling != this.LongCastling)
                return false;

            if (other.ShortCastling != this.ShortCastling)
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
                Position = this.Position,
                LongCastling = this.LongCastling,
                ShortCastling = this.ShortCastling,
                Color = this.Color



            };

            return clone;
        }
    }
}