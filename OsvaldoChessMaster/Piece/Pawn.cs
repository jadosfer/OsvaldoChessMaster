namespace OsvaldoChessMaster.Piece
{
    using System;
    using System.Collections.Generic;

    public class Pawn : PieceBase
    {
        public override bool CanJump => false;
        public override int Value => 10;
        public override bool CapturableByTheWay { get; set; }
        public override int turnNumberCapturableByTheWay { get; set; }

        public Pawn() { }
        public Pawn(bool color, int PositionX, int PositionY) 
            : base(color, PositionX, PositionY)
        {            
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

            // si quiere moverse en la misma columna
            if (x1 == x2)
            {
                if ((Math.Abs(y2 - y1) == 1) || (Math.Abs(y2 - y1) == 2 && (y1 == 1 || y1 == 6)))
                {
                    return true;
                }
            }

            // si quiere moverse a las columnas de al lado            
            if ((Math.Abs(x2 - x1) == 1) && Math.Abs(y2 - y1) == 1)
            {
                return true;
            }
            return false;
        }

        public override HashSet<Position> ValidMoves(Board Board)
        {
            HashSet<Position> pawnMoves = new HashSet<Position>();

            if (Board.Turn == Board.player1)
            {
                if (this.Position.PositionY == 1)
                {
                    Position pawnPos1 = new Position();
                    pawnPos1.x1 = this.Position.PositionX - 1;
                    pawnPos1.y1 = this.Position.PositionY + 1;
                    pawnMoves.Add(pawnPos1);
                    Position pawnPos2 = new Position();
                    pawnPos2.x1 = this.Position.PositionX + 1;
                    pawnPos2.y1 = this.Position.PositionY + 1;
                    pawnMoves.Add(pawnPos2);
                    Position pawnPos3 = new Position();
                    pawnPos3.x1 = this.Position.PositionX;
                    pawnPos3.y1 = this.Position.PositionY + 1;
                    pawnMoves.Add(pawnPos3);
                    Position pawnPos4 = new Position();
                    pawnPos4.x1 = this.Position.PositionX;
                    pawnPos4.y1 = this.Position.PositionY + 2;
                    pawnMoves.Add(pawnPos4);
                }
                else
                {
                    Position pawnPos1 = new Position();
                    pawnPos1.x1 = this.Position.PositionX - 1;
                    pawnPos1.y1 = this.Position.PositionY + 1;
                    pawnMoves.Add(pawnPos1);
                    Position pawnPos2 = new Position();
                    pawnPos2.x1 = this.Position.PositionX + 1;
                    pawnPos2.y1 = this.Position.PositionY + 1;
                    pawnMoves.Add(pawnPos2);
                    Position pawnPos3 = new Position();
                    pawnPos3.x1 = this.Position.PositionX;
                    pawnPos3.y1 = this.Position.PositionY + 1;
                    pawnMoves.Add(pawnPos3);
                }
            }
            else
            {
                if (this.Position.PositionY == 7)
                {
                    Position pawnPos5 = new Position();
                    pawnPos5.x1 = this.Position.PositionX - 1;
                    pawnPos5.y1 = this.Position.PositionY - 1;
                    pawnMoves.Add(pawnPos5);
                    Position pawnPos6 = new Position();
                    pawnPos6.x1 = this.Position.PositionX + 1;
                    pawnPos6.y1 = this.Position.PositionY - 1;
                    pawnMoves.Add(pawnPos6);
                    Position pawnPos7 = new Position();
                    pawnPos7.x1 = this.Position.PositionX;
                    pawnPos7.y1 = this.Position.PositionY - 1;
                    pawnMoves.Add(pawnPos7);
                    Position pawnPos8 = new Position();
                    pawnPos8.x1 = this.Position.PositionX;
                    pawnPos8.y1 = this.Position.PositionY - 2;
                    pawnMoves.Add(pawnPos8);
                }
                else
                {
                    Position pawnPos5 = new Position();
                    pawnPos5.x1 = this.Position.PositionX - 1;
                    pawnPos5.y1 = this.Position.PositionY - 1;
                    pawnMoves.Add(pawnPos5);
                    Position pawnPos6 = new Position();
                    pawnPos6.x1 = this.Position.PositionX + 1;
                    pawnPos6.y1 = this.Position.PositionY - 1;
                    pawnMoves.Add(pawnPos6);
                    Position pawnPos7 = new Position();
                    pawnPos7.x1 = this.Position.PositionX;
                    pawnPos7.y1 = this.Position.PositionY - 1;
                    pawnMoves.Add(pawnPos7);
                }              
            }
            return pawnMoves;
        }
        


        public override string ToString()
        {
            var color = this.Color ? "w" : "b";

            return $"{color}PAWN  ";
        }
        public override object Clone()
        {
            return this.Clone<Pawn>();
        }
    }
}