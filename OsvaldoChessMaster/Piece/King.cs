﻿namespace OsvaldoChessMaster.Piece
{
    using System;

    public class King : PieceBase
    {
        public override bool CanJump => false;
        public override bool CanCastling { get; set; }

        public King() { }

        public King(bool color, int PositionX, int PositionY)
            : base(color, PositionX, PositionY)
        {
            this.CanCastling = true;
        }

        public override bool IsValidMove(int x2, int y2)
        {
            // movimiento en el mismo lugar
            if (Position.PositionX == x2 && Position.PositionY == y2)
                return false;

            if (Math.Sqrt((y2 - Position.PositionY) * (y2 - Position.PositionY) + 
                (x2 - Position.PositionX) * (x2 - Position.PositionX)) < 2)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            var color = this.Color ? "w" : "b";

            return $"{color}__KING__";
        }
        public override object Clone()
        {
            return this.Clone<King>();
        }
    }
}