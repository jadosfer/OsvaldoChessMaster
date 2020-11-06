namespace OsvaldoChessMaster.Piece
{
    using System;

    public class Pawn : PieceBase
    {
        public override bool CanJump => false;
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
        public override bool IsValidMove(int x2, int y2)
        {
            // movimiento en el mismo lugar
            if (Position.PositionX == x2 && Position.PositionY == y2)
                return false;

            // si quiere moverse en la misma columna
            if (Position.PositionX == x2)
            {
                if ((Math.Abs(y2 - Position.PositionY) == 1) || (Math.Abs(y2 - Position.PositionY) == 2 && (Position.PositionY == 1 || Position.PositionY == 6)))
                {
                    return true;
                }
            }

            // si quiere moverse a las columnas de al lado            
            if ((Math.Abs(x2 - Position.PositionX) == 1) && Math.Abs(y2 - Position.PositionY) == 1)
            {
                return true;
            }
            return false;
        }
       

        public override string ToString()
        {
            var color = this.Color ? "w" : "b";

            return $"{color}  PAWN  ";
        }
        public override object Clone()
        {
            return this.Clone<Pawn>();
        }
    }
}