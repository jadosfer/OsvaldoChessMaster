namespace OsvaldoChessMaster.Piece
{
    using System;
    using System.Collections.Generic;

    public class King : PieceBase
    {
        public override bool CanJump => false;
        public override int Value => 900;
        public override bool CanCastling { get; set; }

        public King() { }

        public King(bool color, int PositionX, int PositionY)
            : base(color, PositionX, PositionY)
        {
            this.CanCastling = true;
        }

        /// <summary>
        /// true si la pieza puede moverse a x2,y2 determinado por sus condiciones propias, desconoce quedar en jaque o casillas ocupadas
        /// </summary>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public override bool IsValidMove(int x1, int y1, int x2, int y2)
        {
            // movimiento en el mismo lugar
            if (x1 == x2 && y1 == y2)
                return false;

            if (Math.Sqrt((y2 - y1) * (y2 - y1) + 
                (x2 - x1) * (x2 - x1)) < 2)
            {
                return true;
            }

            return false;
        }

        public override HashSet<Position> ValidMoves(Board Board)
        {
            HashSet<Position> kingMoves = new HashSet<Position>();

            Position kingPos1 = new Position();
            kingPos1.x1 = this.Position.PositionX + 1;
            kingPos1.y1 = this.Position.PositionY;
            if (kingPos1.x1 >= Constants.ForStart && kingPos1.x1 < Constants.Size && kingPos1.y1 >= Constants.ForStart && kingPos1.y1 < Constants.Size)
            {
                kingMoves.Add(kingPos1);
            }
            Position kingPos2 = new Position();
            kingPos2.x1 = this.Position.PositionX - 1;
            kingPos2.y1 = this.Position.PositionY;
            if (kingPos2.x1 >= Constants.ForStart && kingPos2.x1 < Constants.Size && kingPos2.y1 >= Constants.ForStart && kingPos2.y1 < Constants.Size)
            {
                kingMoves.Add(kingPos1);
            }
            Position kingPos3 = new Position();
            kingPos3.x1 = this.Position.PositionX + 1;
            kingPos3.y1 = this.Position.PositionY + 1;
            if (kingPos3.x1 >= Constants.ForStart && kingPos3.x1 < Constants.Size && kingPos3.y1 >= Constants.ForStart && kingPos3.y1 < Constants.Size)
            {
                kingMoves.Add(kingPos1);
            }
            Position kingPos4 = new Position();
            kingPos4.x1 = this.Position.PositionX - 1;
            kingPos4.y1 = this.Position.PositionY + 1;
            if (kingPos4.x1 >= Constants.ForStart && kingPos4.x1 < Constants.Size && kingPos4.y1 >= Constants.ForStart && kingPos4.y1 < Constants.Size)
            {
                kingMoves.Add(kingPos1);
            }
            Position kingPos5 = new Position();
            kingPos5.x1 = this.Position.PositionX + 1;
            kingPos5.y1 = this.Position.PositionY - 1;
            if (kingPos5.x1 >= Constants.ForStart && kingPos5.x1 < Constants.Size && kingPos5.y1 >= Constants.ForStart && kingPos5.y1 < Constants.Size)
            {
                kingMoves.Add(kingPos1);
            }
            Position kingPos6 = new Position();
            kingPos6.x1 = this.Position.PositionX - 1;
            kingPos6.y1 = this.Position.PositionY - 1;
            if (kingPos6.x1 >= Constants.ForStart && kingPos6.x1 < Constants.Size && kingPos6.y1 >= Constants.ForStart && kingPos6.y1 < Constants.Size)
            {
                kingMoves.Add(kingPos1);
            }
            Position kingPos7 = new Position();
            kingPos7.x1 = this.Position.PositionX;
            kingPos7.y1 = this.Position.PositionY + 1;
            if (kingPos7.x1 >= Constants.ForStart && kingPos7.x1 < Constants.Size && kingPos7.y1 >= Constants.ForStart && kingPos7.y1 < Constants.Size)
            {
                kingMoves.Add(kingPos1);
            }
            Position kingPos8 = new Position();
            kingPos8.x1 = this.Position.PositionX;
            kingPos8.y1 = this.Position.PositionY - 1;
            if (kingPos8.x1 >= Constants.ForStart && kingPos8.x1 < Constants.Size && kingPos8.y1 >= Constants.ForStart && kingPos8.y1 < Constants.Size)
            {
                kingMoves.Add(kingPos1);
            }

            return kingMoves;
        }




        public override string ToString()
        {
            var color = this.Color ? "w" : "b";

            return $"{color} KING ";
        }
        public override object Clone()
        {
            return this.Clone<King>();
        }
    }
}