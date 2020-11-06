using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace OsvaldoChessMaster.Piece
{
    public class PieceStatus : ICloneable, IEquatable<PieceStatus>
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public PieceStatus()
        {
        }

        public PieceStatus(int positionX, int positionY)
        {
            this.PositionX = positionX;
            this.PositionY = positionY;
        }

        public bool Equals([AllowNull] PieceStatus other)
        {
            if (other.PositionX != this.PositionX)
                return false;
            if (other.PositionY != this.PositionY)
                return false;

            return true;
        }

        public object Clone()
        {
            return new PieceStatus(PositionX, PositionY);
        }
    }
}
