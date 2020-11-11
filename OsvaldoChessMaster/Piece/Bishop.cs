namespace OsvaldoChessMaster.Piece
{
    using System;
    using System.Collections.Generic;

    public class Bishop : PieceBase
    {
        public override bool CanJump => false;
        public override int Value => 30;
        public Bishop() { }

        public Bishop(bool color, int PositionX, int PositionY)
            : base(color, PositionX, PositionY) { }

        public override bool IsValidMove(int x1, int y1, int x2, int y2)
        {
            // movimiento en el mismo lugar
            if (x1 == x2 && y1 == y2)
                return false;

            if (Math.Abs(y2 - y1) == (Math.Abs(x2 - x1)))
            {
                return true;
            }

            return false;
        }

        public override HashSet<Position> ValidMoves(Board Board)
        {
            HashSet<Position> bishopMoves = new HashSet<Position>();

            Position bishopPos1 = new Position();
            bishopPos1.x1 = this.Position.PositionX + 1;
            bishopPos1.y1 = this.Position.PositionY + 1;
            if (bishopPos1.x1 >= Constants.ForStart && bishopPos1.x1 < Constants.Size && bishopPos1.y1 >= Constants.ForStart && bishopPos1.y1 < Constants.Size)
            {
                bishopMoves.Add(bishopPos1);
            }
            
            Position bishopPos2 = new Position();
            bishopPos2.x1 = this.Position.PositionX + 2;
            bishopPos2.y1 = this.Position.PositionY + 2;
            if (bishopPos2.x1 >= Constants.ForStart && bishopPos2.x1 < Constants.Size && bishopPos2.y1 >= Constants.ForStart && bishopPos2.y1 < Constants.Size)
            {
                bishopMoves.Add(bishopPos2);
            }

            Position bishopPos3 = new Position();
            bishopPos3.x1 = this.Position.PositionX + 3;
            bishopPos3.y1 = this.Position.PositionY + 3;
            if (bishopPos3.x1 >= Constants.ForStart && bishopPos3.x1 < Constants.Size && bishopPos3.y1 >= Constants.ForStart && bishopPos3.y1 < Constants.Size)
            {
                bishopMoves.Add(bishopPos3);
            }

            Position bishopPos4 = new Position();
            bishopPos4.x1 = this.Position.PositionX + 4;
            bishopPos4.y1 = this.Position.PositionY + 4;
            if (bishopPos4.x1 >= Constants.ForStart && bishopPos4.x1 < Constants.Size && bishopPos4.y1 >= Constants.ForStart && bishopPos4.y1 < Constants.Size)
            {
                bishopMoves.Add(bishopPos4);
            }

            Position bishopPos5 = new Position();
            bishopPos5.x1 = this.Position.PositionX + 5;
            bishopPos5.y1 = this.Position.PositionY + 5;
            if (bishopPos5.x1 >= Constants.ForStart && bishopPos5.x1 < Constants.Size && bishopPos5.y1 >= Constants.ForStart && bishopPos5.y1 < Constants.Size)
            {
                bishopMoves.Add(bishopPos5);
            }

            Position bishopPos6 = new Position();
            bishopPos6.x1 = this.Position.PositionX + 6;
            bishopPos6.y1 = this.Position.PositionY + 6;
            if (bishopPos6.x1 >= Constants.ForStart && bishopPos6.x1 < Constants.Size && bishopPos6.y1 >= Constants.ForStart && bishopPos6.y1 < Constants.Size)
            {
                bishopMoves.Add(bishopPos6);
            }

            Position bishopPos7 = new Position();
            bishopPos7.x1 = this.Position.PositionX + 7;
            bishopPos7.y1 = this.Position.PositionY + 7;
            if (bishopPos7.x1 >= Constants.ForStart && bishopPos7.x1 < Constants.Size && bishopPos7.y1 >= Constants.ForStart && bishopPos7.y1 < Constants.Size)
            {
                bishopMoves.Add(bishopPos7);
            }

            Position bishopPos8 = new Position();
            bishopPos8.x1 = this.Position.PositionX - 1;
            bishopPos8.y1 = this.Position.PositionY - 1;
            if (bishopPos8.x1 >= Constants.ForStart && bishopPos8.x1 < Constants.Size && bishopPos8.y1 >= Constants.ForStart && bishopPos8.y1 < Constants.Size)
            {
                bishopMoves.Add(bishopPos8);
            }

            Position bishopPos9 = new Position();
            bishopPos9.x1 = this.Position.PositionX - 2;
            bishopPos9.y1 = this.Position.PositionY - 2;
            if (bishopPos9.x1 >= Constants.ForStart && bishopPos9.x1 < Constants.Size && bishopPos9.y1 >= Constants.ForStart && bishopPos9.y1 < Constants.Size)
            {
                bishopMoves.Add(bishopPos9);
            }

            Position bishopPos10 = new Position();
            bishopPos10.x1 = this.Position.PositionX - 3;
            bishopPos10.y1 = this.Position.PositionY - 3;
            if (bishopPos10.x1 >= Constants.ForStart && bishopPos10.x1 < Constants.Size && bishopPos10.y1 >= Constants.ForStart && bishopPos10.y1 < Constants.Size)
            {
                bishopMoves.Add(bishopPos10);
            }

            Position bishopPos11 = new Position();
            bishopPos11.x1 = this.Position.PositionX - 4;
            bishopPos11.y1 = this.Position.PositionY - 4;
            if (bishopPos11.x1 >= Constants.ForStart && bishopPos11.x1 < Constants.Size && bishopPos11.y1 >= Constants.ForStart && bishopPos11.y1 < Constants.Size)
            {
                bishopMoves.Add(bishopPos11);
            }

            Position bishopPos12 = new Position();
            bishopPos12.x1 = this.Position.PositionX - 5;
            bishopPos12.y1 = this.Position.PositionY - 5;
            if (bishopPos12.x1 >= Constants.ForStart && bishopPos12.x1 < Constants.Size && bishopPos12.y1 >= Constants.ForStart && bishopPos12.y1 < Constants.Size)
            {
                bishopMoves.Add(bishopPos12);
            }

            Position bishopPos13 = new Position();
            bishopPos13.x1 = this.Position.PositionX - 6;
            bishopPos13.y1 = this.Position.PositionY - 6;
            if (bishopPos13.x1 >= Constants.ForStart && bishopPos13.x1 < Constants.Size && bishopPos13.y1 >= Constants.ForStart && bishopPos13.y1 < Constants.Size)
            {
                bishopMoves.Add(bishopPos13);
            }

            Position bishopPos14 = new Position();
            bishopPos14.x1 = this.Position.PositionX - 7;
            bishopPos14.y1 = this.Position.PositionY - 7;
            if (bishopPos14.x1 >= Constants.ForStart && bishopPos14.x1 < Constants.Size && bishopPos14.y1 >= Constants.ForStart && bishopPos14.y1 < Constants.Size)
            {
                bishopMoves.Add(bishopPos14);
            }

            Position bishopPos15 = new Position();
            bishopPos15.x1 = this.Position.PositionX + 1;
            bishopPos15.y1 = this.Position.PositionY - 1;
            if (bishopPos15.x1 >= Constants.ForStart && bishopPos15.x1 < Constants.Size && bishopPos15.y1 >= Constants.ForStart && bishopPos15.y1 < Constants.Size)
            {
                bishopMoves.Add(bishopPos15);
            }

            Position bishopPos16 = new Position();
            bishopPos16.x1 = this.Position.PositionX + 2;
            bishopPos16.y1 = this.Position.PositionY - 2;
            if (bishopPos16.x1 >= Constants.ForStart && bishopPos16.x1 < Constants.Size && bishopPos16.y1 >= Constants.ForStart && bishopPos2.y1 < Constants.Size)
            {
                bishopMoves.Add(bishopPos16);
            }

            Position bishopPos17 = new Position();
            bishopPos17.x1 = this.Position.PositionX + 3;
            bishopPos17.y1 = this.Position.PositionY - 3;
            if (bishopPos17.x1 >= Constants.ForStart && bishopPos17.x1 < Constants.Size && bishopPos17.y1 >= Constants.ForStart && bishopPos17.y1 < Constants.Size)
            {
                bishopMoves.Add(bishopPos17);
            }

            Position bishopPos18 = new Position();
            bishopPos18.x1 = this.Position.PositionX + 4;
            bishopPos18.y1 = this.Position.PositionY - 4;
            if (bishopPos18.x1 >= Constants.ForStart && bishopPos18.x1 < Constants.Size && bishopPos18.y1 >= Constants.ForStart && bishopPos18.y1 < Constants.Size)
            {
                bishopMoves.Add(bishopPos18);
            }

            Position bishopPos19 = new Position();
            bishopPos19.x1 = this.Position.PositionX + 5;
            bishopPos19.y1 = this.Position.PositionY - 5;
            if (bishopPos19.x1 >= Constants.ForStart && bishopPos19.x1 < Constants.Size && bishopPos19.y1 >= Constants.ForStart && bishopPos19.y1 < Constants.Size)
            {
                bishopMoves.Add(bishopPos19);
            }

            Position bishopPos20 = new Position();
            bishopPos20.x1 = this.Position.PositionX + 6;
            bishopPos20.y1 = this.Position.PositionY - 6;
            if (bishopPos20.x1 >= Constants.ForStart && bishopPos20.x1 < Constants.Size && bishopPos20.y1 >= Constants.ForStart && bishopPos20.y1 < Constants.Size)
            {
                bishopMoves.Add(bishopPos20);
            }

            Position bishopPos21 = new Position();
            bishopPos21.x1 = this.Position.PositionX + 7;
            bishopPos21.y1 = this.Position.PositionY - 7;
            if (bishopPos21.x1 >= Constants.ForStart && bishopPos21.x1 < Constants.Size && bishopPos21.y1 >= Constants.ForStart && bishopPos21.y1 < Constants.Size)
            {
                bishopMoves.Add(bishopPos21);
            }

            Position bishopPos22 = new Position();
            bishopPos22.x1 = this.Position.PositionX - 1;
            bishopPos22.y1 = this.Position.PositionY + 1;
            if (bishopPos22.x1 >= Constants.ForStart && bishopPos22.x1 < Constants.Size && bishopPos22.y1 >= Constants.ForStart && bishopPos22.y1 < Constants.Size)
            {
                bishopMoves.Add(bishopPos22);
            }

            Position bishopPos23 = new Position();
            bishopPos23.x1 = this.Position.PositionX - 2;
            bishopPos23.y1 = this.Position.PositionY + 2;
            if (bishopPos23.x1 >= Constants.ForStart && bishopPos23.x1 < Constants.Size && bishopPos23.y1 >= Constants.ForStart && bishopPos23.y1 < Constants.Size)
            {
                bishopMoves.Add(bishopPos23);
            }

            Position bishopPos24 = new Position();
            bishopPos24.x1 = this.Position.PositionX - 3;
            bishopPos24.y1 = this.Position.PositionY + 3;
            if (bishopPos24.x1 >= Constants.ForStart && bishopPos24.x1 < Constants.Size && bishopPos24.y1 >= Constants.ForStart && bishopPos24.y1 < Constants.Size)
            {
                bishopMoves.Add(bishopPos24);
            }

            Position bishopPos25 = new Position();
            bishopPos25.x1 = this.Position.PositionX - 4;
            bishopPos25.y1 = this.Position.PositionY + 4;
            if (bishopPos25.x1 >= Constants.ForStart && bishopPos25.x1 < Constants.Size && bishopPos25.y1 >= Constants.ForStart && bishopPos25.y1 < Constants.Size)
            {
                bishopMoves.Add(bishopPos25);
            }

            Position bishopPos26 = new Position();
            bishopPos26.x1 = this.Position.PositionX - 5;
            bishopPos26.y1 = this.Position.PositionY + 5;
            if (bishopPos26.x1 >= Constants.ForStart && bishopPos26.x1 < Constants.Size && bishopPos26.y1 >= Constants.ForStart && bishopPos26.y1 < Constants.Size)
            {
                bishopMoves.Add(bishopPos26);
            }
            
            Position bishopPos27 = new Position();
            bishopPos27.x1 = this.Position.PositionX - 6;
            bishopPos27.y1 = this.Position.PositionY + 6;
            if (bishopPos27.x1 >= Constants.ForStart && bishopPos27.x1 < Constants.Size && bishopPos27.y1 >= Constants.ForStart && bishopPos27.y1 < Constants.Size)
            {
                bishopMoves.Add(bishopPos27);
            }

            Position bishopPos28 = new Position();
            bishopPos28.x1 = this.Position.PositionX - 7;
            bishopPos28.y1 = this.Position.PositionY + 7;           
            if (bishopPos28.x1 >= Constants.ForStart && bishopPos28.x1 < Constants.Size && bishopPos28.y1 >= Constants.ForStart && bishopPos28.y1 < Constants.Size)
            {
                bishopMoves.Add(bishopPos28);
            }


            return bishopMoves;
        }

        public override string ToString()
        {
            var color = this.Color ? "w" : "b";

            return $"{color}BISHOP";
        }

        public override object Clone()
        {
            return this.Clone<Bishop>();
        }
    }
}