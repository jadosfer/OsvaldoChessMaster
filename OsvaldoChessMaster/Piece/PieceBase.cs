using System;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;


namespace OsvaldoChessMaster.Piece
{
    public abstract class PieceBase : ICloneable, IEquatable<PieceBase>
    {
        
        public abstract bool CanJump { get; }
        public abstract int Value{ get; }
        public virtual bool CanCastling { get; set; }
        public virtual bool CapturableByTheWay { get; set; }
        public virtual int turnNumberCapturableByTheWay { get; set; }
        public PieceStatus Position { get; set; }


        public bool LongCastling { get; set; }
        public bool ShortCastling { get; set; }
        public bool Color { get; set; }

        public PieceBase(bool color = false, int PositionX = 0, int PositionY = 0)
        {
            this.Position = new PieceStatus(PositionX, PositionY);
            this.Color = color;            
        }

        public abstract bool IsValidMove(int x1, int y1, int x2, int y2);

        public abstract HashSet<Position> ValidMoves(Board board);        
        

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

            if (other.Position.PositionX != this.Position.PositionX)
                return false;
            if (other.Position.PositionY != this.Position.PositionY)
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
                LongCastling = this.LongCastling,
                ShortCastling = this.ShortCastling,
                Color = this.Color,
                Position = new PieceStatus(this.Position.PositionX, this.Position.PositionY)


            };

            return clone;
        }
    }
}