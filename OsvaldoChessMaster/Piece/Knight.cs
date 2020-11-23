namespace OsvaldoChessMaster.Piece
{
    using System;
    using System.Collections.Generic;

    public class Knight : PieceBase
    {
        public override bool CanJump => true;
        public override int Value => 30;
        public Knight() { }
        public Knight(bool color, int PositionX, int PositionY)
            : base(color, PositionX, PositionY) { }


        public override bool IsValidMove(int x1, int y1, int x2, int y2, bool turn, Board board)
        {
            //no es tu turno
            if (this.Color != turn)
                return false;

            // movimiento en el mismo lugar
            if (x1 == x2 && y1 == y2)
                return false;

            if ((Math.Abs(y2 - y1) == 2 && (Math.Abs(x2 - x1) == 1)) || (Math.Abs(y2 - y1) == 1 && (Math.Abs(x2 - x1) == 2)))
            {
                return true;
            }

            return false;
        }

        public override bool LogicMove(int x1, int y1, int x2, int y2, Board board, BoardLogic boardLogic)
        {
            if (boardLogic.IsInRange(x1, y1, x2, y2))
            {
                PieceBase piece = board.GetPiece(x1, y1);
                if (!board.IsEmpty(x1, y1) && piece.IsValidMove(x1, y1, x2, y2, board.Turn, board))
                {
                    if (board.IsEmpty(x2, y2) && !boardLogic.CantMoveIsCheck(x1, y1, x2, y2, board))
                    {
                        board.Move(x1, y1, x2, y2);
                        return true;
                    }
                    if (!board.IsEmpty(x2, y2) && !boardLogic.CantMoveIsCheck(x1, y1, x2, y2, board))
                    {
                        board.Remove(x2, y2);
                        board.Move(x1, y1, x2, y2);
                        return true;
                    }

                }
            }

            return false;
        }

        public override HashSet<Position> ValidMoves(Board board)
        {
            HashSet<Position> knightMoves = new HashSet<Position>();

            Position knightPos1 = new Position();
            knightPos1.x1 = this.Position.PositionX - 1;
            knightPos1.y1 = this.Position.PositionY + 2;
            if (knightPos1.x1 >= Constants.ForStart && knightPos1.x1 < Constants.Size && knightPos1.y1 >= Constants.ForStart && knightPos1.y1 < Constants.Size)
            {
                knightMoves.Add(knightPos1);
            };
            Position knightPos2 = new Position();
            knightPos2.x1 = this.Position.PositionX + 1;
            knightPos2.y1 = this.Position.PositionY + 2;
            if (knightPos2.x1 >= Constants.ForStart && knightPos2.x1 < Constants.Size && knightPos2.y1 >= Constants.ForStart && knightPos2.y1 < Constants.Size)
            {
                knightMoves.Add(knightPos2);
            }
            Position knightPos3 = new Position();
            knightPos3.x1 = this.Position.PositionX - 1;
            knightPos3.y1 = this.Position.PositionY - 2;
            if (knightPos3.x1 >= Constants.ForStart && knightPos3.x1 < Constants.Size && knightPos3.y1 >= Constants.ForStart && knightPos3.y1 < Constants.Size)
            {
                knightMoves.Add(knightPos3);
            }
            Position knightPos4 = new Position();
            knightPos4.x1 = this.Position.PositionX + 1;
            knightPos4.y1 = this.Position.PositionY - 2;
            if (knightPos4.x1 >= Constants.ForStart && knightPos4.x1 < Constants.Size && knightPos4.y1 >= Constants.ForStart && knightPos4.y1 < Constants.Size)
            {
                knightMoves.Add(knightPos4);
            }
            Position knightPos5 = new Position();
            knightPos5.x1 = this.Position.PositionX - 2;
            knightPos5.y1 = this.Position.PositionY + 1;
            if (knightPos5.x1 >= Constants.ForStart && knightPos5.x1 < Constants.Size && knightPos5.y1 >= Constants.ForStart && knightPos5.y1 < Constants.Size)
            {
                knightMoves.Add(knightPos5);
            }
            Position knightPos6 = new Position();
            knightPos6.x1 = this.Position.PositionX + 2;
            knightPos6.y1 = this.Position.PositionY + 1;
            if (knightPos6.x1 >= Constants.ForStart && knightPos6.x1 < Constants.Size && knightPos6.y1 >= Constants.ForStart && knightPos6.y1 < Constants.Size)
            {
                knightMoves.Add(knightPos6);
            }
            Position knightPos7 = new Position();
            knightPos7.x1 = this.Position.PositionX - 2;
            knightPos7.y1 = this.Position.PositionY - 1;
            if (knightPos7.x1 >= Constants.ForStart && knightPos7.x1 < Constants.Size && knightPos7.y1 >= Constants.ForStart && knightPos7.y1 < Constants.Size)
            {
                knightMoves.Add(knightPos7);
            }
            Position knightPos8 = new Position();
            knightPos8.x1 = this.Position.PositionX + 2;
            knightPos8.y1 = this.Position.PositionY - 1;
            if (knightPos8.x1 >= Constants.ForStart && knightPos8.x1 < Constants.Size && knightPos8.y1 >= Constants.ForStart && knightPos8.y1 < Constants.Size)
            {
                knightMoves.Add(knightPos8);
            }

            return knightMoves;
        }





        public override string ToString()
        {
            var color = this.Color ? "w" : "b";

            return $"{color}KNIGHT";
        }
        public override object Clone()
        {
            return this.Clone<Knight>();
        }
    }
}