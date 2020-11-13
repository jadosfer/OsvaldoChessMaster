namespace OsvaldoChessMaster.Piece
{
    using System;
    using System.Collections.Generic;
    public class Rook : PieceBase
    {
        public override bool CanJump => false;
        public override int Value => 50;

        public Rook() { }

        public Rook(bool color, int PositionX, int PositionY)
            : base(color, PositionX, PositionY)
        {
            CanCastling = true;
        }

        /// <summary>
        /// true si la pieza puede moverse a x2,y2 determinado por sus condiciones propias, desconoce obstaculos de otras piezas como quedar en jaque o casillas ocupadas
        /// </summary>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
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

        public override HashSet<Position> ValidMoves(Board Board)
        {
            HashSet<Position> rookMoves = new HashSet<Position>();

            Position rookPos1 = new Position();
            rookPos1.x1 = this.Position.PositionX + 1;
            rookPos1.y1 = this.Position.PositionY;
            if (rookPos1.x1 >= Constants.ForStart && rookPos1.x1 < Constants.Size && rookPos1.y1 >= Constants.ForStart && rookPos1.y1 < Constants.Size)
            {
                rookMoves.Add(rookPos1);
            }

            Position rookPos2 = new Position();
            rookPos2.x1 = this.Position.PositionX + 2;
            rookPos2.y1 = this.Position.PositionY;
            if (rookPos2.x1 >= Constants.ForStart && rookPos2.x1 < Constants.Size && rookPos2.y1 >= Constants.ForStart && rookPos2.y1 < Constants.Size)
            {
                rookMoves.Add(rookPos2);
            }

            Position rookPos3 = new Position();
            rookPos3.x1 = this.Position.PositionX + 3;
            rookPos3.y1 = this.Position.PositionY;
            if (rookPos3.x1 >= Constants.ForStart && rookPos3.x1 < Constants.Size && rookPos3.y1 >= Constants.ForStart && rookPos3.y1 < Constants.Size)
            {
                rookMoves.Add(rookPos3);
            }

            Position rookPos4 = new Position();
            rookPos4.x1 = this.Position.PositionX + 4;
            rookPos4.y1 = this.Position.PositionY;
            if (rookPos4.x1 >= Constants.ForStart && rookPos4.x1 < Constants.Size && rookPos4.y1 >= Constants.ForStart && rookPos4.y1 < Constants.Size)
            {
                rookMoves.Add(rookPos4);
            }

            Position rookPos5 = new Position();
            rookPos5.x1 = this.Position.PositionX + 5;
            rookPos5.y1 = this.Position.PositionY;
            if (rookPos5.x1 >= Constants.ForStart && rookPos5.x1 < Constants.Size && rookPos5.y1 >= Constants.ForStart && rookPos5.y1 < Constants.Size)
            {
                rookMoves.Add(rookPos5);
            }

            Position rookPos6 = new Position();
            rookPos6.x1 = this.Position.PositionX + 6;
            rookPos6.y1 = this.Position.PositionY;
            if (rookPos6.x1 >= Constants.ForStart && rookPos6.x1 < Constants.Size && rookPos6.y1 >= Constants.ForStart && rookPos6.y1 < Constants.Size)
            {
                rookMoves.Add(rookPos6);
            }

            Position rookPos7 = new Position();
            rookPos7.x1 = this.Position.PositionX + 7;
            rookPos7.y1 = this.Position.PositionY;
            if (rookPos7.x1 >= Constants.ForStart && rookPos7.x1 < Constants.Size && rookPos7.y1 >= Constants.ForStart && rookPos7.y1 < Constants.Size)
            {
                rookMoves.Add(rookPos7);
            }

            Position rookPos8 = new Position();
            rookPos8.x1 = this.Position.PositionX - 1;
            rookPos8.y1 = this.Position.PositionY;
            if (rookPos8.x1 >= Constants.ForStart && rookPos8.x1 < Constants.Size && rookPos8.y1 >= Constants.ForStart && rookPos8.y1 < Constants.Size)
            {
                rookMoves.Add(rookPos8);
            }

            Position rookPos9 = new Position();
            rookPos9.x1 = this.Position.PositionX - 2;
            rookPos9.y1 = this.Position.PositionY;
            if (rookPos9.x1 >= Constants.ForStart && rookPos9.x1 < Constants.Size && rookPos9.y1 >= Constants.ForStart && rookPos9.y1 < Constants.Size)
            {
                rookMoves.Add(rookPos9);
            }

            Position rookPos10 = new Position();
            rookPos10.x1 = this.Position.PositionX - 3;
            rookPos10.y1 = this.Position.PositionY;
            if (rookPos10.x1 >= Constants.ForStart && rookPos10.x1 < Constants.Size && rookPos10.y1 >= Constants.ForStart && rookPos10.y1 < Constants.Size)
            {
                rookMoves.Add(rookPos10);
            }

            Position rookPos11 = new Position();
            rookPos11.x1 = this.Position.PositionX - 4;
            rookPos11.y1 = this.Position.PositionY;
            if (rookPos11.x1 >= Constants.ForStart && rookPos11.x1 < Constants.Size && rookPos11.y1 >= Constants.ForStart && rookPos11.y1 < Constants.Size)
            {
                rookMoves.Add(rookPos11);
            }

            Position rookPos12 = new Position();
            rookPos12.x1 = this.Position.PositionX - 5;
            rookPos12.y1 = this.Position.PositionY;
            if (rookPos12.x1 >= Constants.ForStart && rookPos12.x1 < Constants.Size && rookPos12.y1 >= Constants.ForStart && rookPos12.y1 < Constants.Size)
            {
                rookMoves.Add(rookPos12);
            }

            Position rookPos13 = new Position();
            rookPos13.x1 = this.Position.PositionX - 6;
            rookPos13.y1 = this.Position.PositionY;
            if (rookPos13.x1 >= Constants.ForStart && rookPos13.x1 < Constants.Size && rookPos13.y1 >= Constants.ForStart && rookPos13.y1 < Constants.Size)
            {
                rookMoves.Add(rookPos13);
            }

            Position rookPos14 = new Position();
            rookPos14.x1 = this.Position.PositionX - 7;
            rookPos14.y1 = this.Position.PositionY;
            if (rookPos14.x1 >= Constants.ForStart && rookPos14.x1 < Constants.Size && rookPos14.y1 >= Constants.ForStart && rookPos14.y1 < Constants.Size)
            {
                rookMoves.Add(rookPos14);
            }

            Position rookPos15 = new Position();
            rookPos15.x1 = this.Position.PositionX;
            rookPos15.y1 = this.Position.PositionY + 1;
            if (rookPos15.x1 >= Constants.ForStart && rookPos15.x1 < Constants.Size && rookPos15.y1 >= Constants.ForStart && rookPos15.y1 < Constants.Size)
            {
                rookMoves.Add(rookPos15);
            }

            Position rookPos16 = new Position();
            rookPos16.x1 = this.Position.PositionX;
            rookPos16.y1 = this.Position.PositionY + 2;
            if (rookPos16.x1 >= Constants.ForStart && rookPos16.x1 < Constants.Size && rookPos16.y1 >= Constants.ForStart && rookPos16.y1 < Constants.Size)
            {
                rookMoves.Add(rookPos16);
            }

            Position rookPos17 = new Position();
            rookPos17.x1 = this.Position.PositionX;
            rookPos17.y1 = this.Position.PositionY + 3;
            if (rookPos17.x1 >= Constants.ForStart && rookPos17.x1 < Constants.Size && rookPos17.y1 >= Constants.ForStart && rookPos17.y1 < Constants.Size)
            {
                rookMoves.Add(rookPos17);
            }

            Position rookPos18 = new Position();
            rookPos18.x1 = this.Position.PositionX;
            rookPos18.y1 = this.Position.PositionY + 4;
            if (rookPos18.x1 >= Constants.ForStart && rookPos18.x1 < Constants.Size && rookPos18.y1 >= Constants.ForStart && rookPos18.y1 < Constants.Size)
            {
                rookMoves.Add(rookPos18);
            }

            Position rookPos19 = new Position();
            rookPos19.x1 = this.Position.PositionX;
            rookPos19.y1 = this.Position.PositionY + 5;
            if (rookPos19.x1 >= Constants.ForStart && rookPos19.x1 < Constants.Size && rookPos19.y1 >= Constants.ForStart && rookPos19.y1 < Constants.Size)
            {
                rookMoves.Add(rookPos19);
            }

            Position rookPos20 = new Position();
            rookPos20.x1 = this.Position.PositionX;
            rookPos20.y1 = this.Position.PositionY + 6;
            if (rookPos20.x1 >= Constants.ForStart && rookPos20.x1 < Constants.Size && rookPos20.y1 >= Constants.ForStart && rookPos20.y1 < Constants.Size)
            {
                rookMoves.Add(rookPos20);
            }

            Position rookPos21 = new Position();
            rookPos21.x1 = this.Position.PositionX;
            rookPos21.y1 = this.Position.PositionY + 7;
            if (rookPos21.x1 >= Constants.ForStart && rookPos21.x1 < Constants.Size && rookPos21.y1 >= Constants.ForStart && rookPos21.y1 < Constants.Size)
            {
                rookMoves.Add(rookPos21);
            }

            Position rookPos22 = new Position();
            rookPos22.x1 = this.Position.PositionX;
            rookPos22.y1 = this.Position.PositionY - 1;
            if (rookPos22.x1 >= Constants.ForStart && rookPos22.x1 < Constants.Size && rookPos22.y1 >= Constants.ForStart && rookPos22.y1 < Constants.Size)
            {
                rookMoves.Add(rookPos22);
            }

            Position rookPos23 = new Position();
            rookPos23.x1 = this.Position.PositionX;
            rookPos23.y1 = this.Position.PositionY - 2;
            if (rookPos23.x1 >= Constants.ForStart && rookPos23.x1 < Constants.Size && rookPos23.y1 >= Constants.ForStart && rookPos23.y1 < Constants.Size)
            {
                rookMoves.Add(rookPos23);
            }

            Position rookPos24 = new Position();
            rookPos24.x1 = this.Position.PositionX;
            rookPos24.y1 = this.Position.PositionY - 3;
            if (rookPos24.x1 >= Constants.ForStart && rookPos24.x1 < Constants.Size && rookPos24.y1 >= Constants.ForStart && rookPos24.y1 < Constants.Size)
            {
                rookMoves.Add(rookPos24);
            }

            Position rookPos25 = new Position();
            rookPos25.x1 = this.Position.PositionX;
            rookPos25.y1 = this.Position.PositionY - 4;
            if (rookPos25.x1 >= Constants.ForStart && rookPos25.x1 < Constants.Size && rookPos25.y1 >= Constants.ForStart && rookPos25.y1 < Constants.Size)
            {
                rookMoves.Add(rookPos25);
            }

            Position rookPos26 = new Position();
            rookPos26.x1 = this.Position.PositionX;
            rookPos26.y1 = this.Position.PositionY - 5;
            if (rookPos26.x1 >= Constants.ForStart && rookPos26.x1 < Constants.Size && rookPos26.y1 >= Constants.ForStart && rookPos26.y1 < Constants.Size)
            {
                rookMoves.Add(rookPos26);
            }

            Position rookPos27 = new Position();
            rookPos27.x1 = this.Position.PositionX;
            rookPos27.y1 = this.Position.PositionY - 6;
            if (rookPos27.x1 >= Constants.ForStart && rookPos27.x1 < Constants.Size && rookPos27.y1 >= Constants.ForStart && rookPos27.y1 < Constants.Size)
            {
                rookMoves.Add(rookPos27);
            }

            Position rookPos28 = new Position();
            rookPos28.x1 = this.Position.PositionX;
            rookPos28.y1 = this.Position.PositionY - 7;
            if (rookPos28.x1 >= Constants.ForStart && rookPos28.x1 < Constants.Size && rookPos28.y1 >= Constants.ForStart && rookPos28.y1 < Constants.Size)
            {
                rookMoves.Add(rookPos28);
            }


            return rookMoves;
        }

        public override string ToString()
        {
            var color = this.Color ? "w" : "b";

            return $" {color}ROOK ";
        }
        public override object Clone()
        {
            return this.Clone<Rook>();
        }
    }
}