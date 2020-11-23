namespace OsvaldoChessMaster.Piece
{
    using System;
    using System.Collections.Generic;

    public class Queen : PieceBase
    {
        public override bool CanJump => false;
        public override int Value => 90;
        public Queen() { }
        public Queen(bool color, int PositionX, int PositionY)
            : base(color, PositionX, PositionY) { }

        /// <summary>
        /// true si la pieza puede moverse a x2,y2 determinado por sus condiciones propias, desconoce obstaculos de otras piezas como quedar en jaque o casillas ocupadas
        /// </summary>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public override bool IsValidMove(int x1, int y1, int x2, int y2, bool turn, Board board)
        {
            //no es tu turno
            if (this.Color != turn)
                return false;

            // movimiento en el mismo lugar
            if (x1 == x2 && y1 == y2)
                return false;

            if (x1 == x2 || y1 == y2)
            {
                return true;
            }

            if (Math.Abs(y2 - y1) == (Math.Abs(x2 - x1)))
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
                        if ((Math.Abs(x2 - x1) == Math.Abs(y2 - y1) && boardLogic.IsDiagonalEmpty(x1, y1, x2, y2, board)))
                        {
                            board.Move(x1, y1, x2, y2);
                            return true;
                        }
                        if ((x2 == x1 || y2 == y1) && boardLogic.IsLineEmpty(x1, y1, x2, y2, board))
                        {
                            board.Move(x1, y1, x2, y2);
                            return true;
                        }
                    }

                    if (!board.IsEmpty(x2, y2) && !boardLogic.IsAlly(x1, y1, x2, y2, board) && !boardLogic.CantMoveIsCheck(x1, y1, x2, y2, board))
                    {
                        if ((Math.Abs(x2 - x1) == Math.Abs(y2 - y1) && boardLogic.IsDiagonalEmpty(x1, y1, x2, y2, board)))
                        {
                            board.Remove(x2, y2);
                            board.Move(x1, y1, x2, y2);
                            return true;
                        }
                        if ((x2 == x1 || y2 == y1) && boardLogic.IsLineEmpty(x1, y1, x2, y2, board))
                        {
                            board.Remove(x2, y2);
                            board.Move(x1, y1, x2, y2);
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        public override HashSet<Position> ValidMoves(Board board)
        {
            HashSet<Position> queenMoves = new HashSet<Position>();

            Position queenLinePos1 = new Position();
            queenLinePos1.x1 = this.Position.PositionX + 1;
            queenLinePos1.y1 = this.Position.PositionY;
            if (queenLinePos1.x1 >= Constants.ForStart && queenLinePos1.x1 < Constants.Size && queenLinePos1.y1 >= Constants.ForStart && queenLinePos1.y1 < Constants.Size)
            {
                queenMoves.Add(queenLinePos1);
            }

            Position queenLinePos2 = new Position();
            queenLinePos2.x1 = this.Position.PositionX + 2;
            queenLinePos2.y1 = this.Position.PositionY;
            if (queenLinePos2.x1 >= Constants.ForStart && queenLinePos2.x1 < Constants.Size && queenLinePos2.y1 >= Constants.ForStart && queenLinePos2.y1 < Constants.Size)
            {
                queenMoves.Add(queenLinePos2);
            }

            Position queenLinePos3 = new Position();
            queenLinePos3.x1 = this.Position.PositionX + 3;
            queenLinePos3.y1 = this.Position.PositionY;
            if (queenLinePos3.x1 >= Constants.ForStart && queenLinePos3.x1 < Constants.Size && queenLinePos3.y1 >= Constants.ForStart && queenLinePos3.y1 < Constants.Size)
            {
                queenMoves.Add(queenLinePos3);
            }

            Position queenLinePos4 = new Position();
            queenLinePos4.x1 = this.Position.PositionX + 4;
            queenLinePos4.y1 = this.Position.PositionY;
            if (queenLinePos4.x1 >= Constants.ForStart && queenLinePos4.x1 < Constants.Size && queenLinePos4.y1 >= Constants.ForStart && queenLinePos4.y1 < Constants.Size)
            {
                queenMoves.Add(queenLinePos4);
            }

            Position queenLinePos5 = new Position();
            queenLinePos5.x1 = this.Position.PositionX + 5;
            queenLinePos5.y1 = this.Position.PositionY;
            if (queenLinePos5.x1 >= Constants.ForStart && queenLinePos5.x1 < Constants.Size && queenLinePos5.y1 >= Constants.ForStart && queenLinePos5.y1 < Constants.Size)
            {
                queenMoves.Add(queenLinePos5);
            }

            Position queenLinePos6 = new Position();
            queenLinePos6.x1 = this.Position.PositionX + 6;
            queenLinePos6.y1 = this.Position.PositionY;
            if (queenLinePos6.x1 >= Constants.ForStart && queenLinePos6.x1 < Constants.Size && queenLinePos6.y1 >= Constants.ForStart && queenLinePos6.y1 < Constants.Size)
            {
                queenMoves.Add(queenLinePos6);
            }

            Position queenLinePos7 = new Position();
            queenLinePos7.x1 = this.Position.PositionX + 7;
            queenLinePos7.y1 = this.Position.PositionY;
            if (queenLinePos7.x1 >= Constants.ForStart && queenLinePos7.x1 < Constants.Size && queenLinePos7.y1 >= Constants.ForStart && queenLinePos7.y1 < Constants.Size)
            {
                queenMoves.Add(queenLinePos7);
            }

            Position queenLinePos8 = new Position();
            queenLinePos8.x1 = this.Position.PositionX - 1;
            queenLinePos8.y1 = this.Position.PositionY;
            if (queenLinePos8.x1 >= Constants.ForStart && queenLinePos8.x1 < Constants.Size && queenLinePos8.y1 >= Constants.ForStart && queenLinePos8.y1 < Constants.Size)
            {
                queenMoves.Add(queenLinePos8);
            }

            Position queenLinePos9 = new Position();
            queenLinePos9.x1 = this.Position.PositionX - 2;
            queenLinePos9.y1 = this.Position.PositionY;
            if (queenLinePos9.x1 >= Constants.ForStart && queenLinePos9.x1 < Constants.Size && queenLinePos9.y1 >= Constants.ForStart && queenLinePos9.y1 < Constants.Size)
            {
                queenMoves.Add(queenLinePos9);
            }

            Position queenLinePos10 = new Position();
            queenLinePos10.x1 = this.Position.PositionX - 3;
            queenLinePos10.y1 = this.Position.PositionY;
            if (queenLinePos10.x1 >= Constants.ForStart && queenLinePos10.x1 < Constants.Size && queenLinePos10.y1 >= Constants.ForStart && queenLinePos10.y1 < Constants.Size)
            {
                queenMoves.Add(queenLinePos10);
            }

            Position queenLinePos11 = new Position();
            queenLinePos11.x1 = this.Position.PositionX - 4;
            queenLinePos11.y1 = this.Position.PositionY;
            if (queenLinePos11.x1 >= Constants.ForStart && queenLinePos11.x1 < Constants.Size && queenLinePos11.y1 >= Constants.ForStart && queenLinePos11.y1 < Constants.Size)
            {
                queenMoves.Add(queenLinePos11);
            }

            Position queenLinePos12 = new Position();
            queenLinePos12.x1 = this.Position.PositionX - 5;
            queenLinePos12.y1 = this.Position.PositionY;
            if (queenLinePos12.x1 >= Constants.ForStart && queenLinePos12.x1 < Constants.Size && queenLinePos12.y1 >= Constants.ForStart && queenLinePos12.y1 < Constants.Size)
            {
                queenMoves.Add(queenLinePos12);
            }

            Position queenLinePos13 = new Position();
            queenLinePos13.x1 = this.Position.PositionX - 6;
            queenLinePos13.y1 = this.Position.PositionY;
            if (queenLinePos13.x1 >= Constants.ForStart && queenLinePos13.x1 < Constants.Size && queenLinePos13.y1 >= Constants.ForStart && queenLinePos13.y1 < Constants.Size)
            {
                queenMoves.Add(queenLinePos13);
            }

            Position queenLinePos14 = new Position();
            queenLinePos14.x1 = this.Position.PositionX - 7;
            queenLinePos14.y1 = this.Position.PositionY;
            if (queenLinePos14.x1 >= Constants.ForStart && queenLinePos14.x1 < Constants.Size && queenLinePos14.y1 >= Constants.ForStart && queenLinePos14.y1 < Constants.Size)
            {
                queenMoves.Add(queenLinePos14);
            }

            Position queenLinePos15 = new Position();
            queenLinePos15.x1 = this.Position.PositionX;
            queenLinePos15.y1 = this.Position.PositionY + 1;
            if (queenLinePos15.x1 >= Constants.ForStart && queenLinePos15.x1 < Constants.Size && queenLinePos15.y1 >= Constants.ForStart && queenLinePos15.y1 < Constants.Size)
            {
                queenMoves.Add(queenLinePos15);
            }

            Position queenLinePos16 = new Position();
            queenLinePos16.x1 = this.Position.PositionX;
            queenLinePos16.y1 = this.Position.PositionY + 2;
            if (queenLinePos16.x1 >= Constants.ForStart && queenLinePos16.x1 < Constants.Size && queenLinePos16.y1 >= Constants.ForStart && queenLinePos16.y1 < Constants.Size)
            {
                queenMoves.Add(queenLinePos16);
            }

            Position queenLinePos17 = new Position();
            queenLinePos17.x1 = this.Position.PositionX;
            queenLinePos17.y1 = this.Position.PositionY + 3;
            if (queenLinePos17.x1 >= Constants.ForStart && queenLinePos17.x1 < Constants.Size && queenLinePos17.y1 >= Constants.ForStart && queenLinePos17.y1 < Constants.Size)
            {
                queenMoves.Add(queenLinePos17);
            }

            Position queenLinePos18 = new Position();
            queenLinePos18.x1 = this.Position.PositionX;
            queenLinePos18.y1 = this.Position.PositionY + 4;
            if (queenLinePos18.x1 >= Constants.ForStart && queenLinePos18.x1 < Constants.Size && queenLinePos18.y1 >= Constants.ForStart && queenLinePos18.y1 < Constants.Size)
            {
                queenMoves.Add(queenLinePos18);
            }

            Position queenLinePos19 = new Position();
            queenLinePos19.x1 = this.Position.PositionX;
            queenLinePos19.y1 = this.Position.PositionY + 5;
            if (queenLinePos19.x1 >= Constants.ForStart && queenLinePos19.x1 < Constants.Size && queenLinePos19.y1 >= Constants.ForStart && queenLinePos19.y1 < Constants.Size)
            {
                queenMoves.Add(queenLinePos19);
            }

            Position queenLinePos20 = new Position();
            queenLinePos20.x1 = this.Position.PositionX;
            queenLinePos20.y1 = this.Position.PositionY + 6;
            if (queenLinePos20.x1 >= Constants.ForStart && queenLinePos20.x1 < Constants.Size && queenLinePos20.y1 >= Constants.ForStart && queenLinePos20.y1 < Constants.Size)
            {
                queenMoves.Add(queenLinePos20);
            }

            Position queenLinePos21 = new Position();
            queenLinePos21.x1 = this.Position.PositionX;
            queenLinePos21.y1 = this.Position.PositionY + 7;
            if (queenLinePos21.x1 >= Constants.ForStart && queenLinePos21.x1 < Constants.Size && queenLinePos21.y1 >= Constants.ForStart && queenLinePos21.y1 < Constants.Size)
            {
                queenMoves.Add(queenLinePos21);
            }

            Position queenLinePos22 = new Position();
            queenLinePos22.x1 = this.Position.PositionX;
            queenLinePos22.y1 = this.Position.PositionY - 1;
            if (queenLinePos22.x1 >= Constants.ForStart && queenLinePos22.x1 < Constants.Size && queenLinePos22.y1 >= Constants.ForStart && queenLinePos22.y1 < Constants.Size)
            {
                queenMoves.Add(queenLinePos22);
            }

            Position queenLinePos23 = new Position();
            queenLinePos23.x1 = this.Position.PositionX;
            queenLinePos23.y1 = this.Position.PositionY - 2;
            if (queenLinePos23.x1 >= Constants.ForStart && queenLinePos23.x1 < Constants.Size && queenLinePos23.y1 >= Constants.ForStart && queenLinePos23.y1 < Constants.Size)
            {
                queenMoves.Add(queenLinePos23);
            }

            Position queenLinePos24 = new Position();
            queenLinePos24.x1 = this.Position.PositionX;
            queenLinePos24.y1 = this.Position.PositionY - 3;
            if (queenLinePos24.x1 >= Constants.ForStart && queenLinePos24.x1 < Constants.Size && queenLinePos24.y1 >= Constants.ForStart && queenLinePos24.y1 < Constants.Size)
            {
                queenMoves.Add(queenLinePos24);
            }

            Position queenLinePos25 = new Position();
            queenLinePos25.x1 = this.Position.PositionX;
            queenLinePos25.y1 = this.Position.PositionY - 4;
            if (queenLinePos25.x1 >= Constants.ForStart && queenLinePos25.x1 < Constants.Size && queenLinePos25.y1 >= Constants.ForStart && queenLinePos25.y1 < Constants.Size)
            {
                queenMoves.Add(queenLinePos25);
            }

            Position queenLinePos26 = new Position();
            queenLinePos26.x1 = this.Position.PositionX;
            queenLinePos26.y1 = this.Position.PositionY - 5;
            if (queenLinePos26.x1 >= Constants.ForStart && queenLinePos26.x1 < Constants.Size && queenLinePos26.y1 >= Constants.ForStart && queenLinePos26.y1 < Constants.Size)
            {
                queenMoves.Add(queenLinePos26);
            }

            Position queenLinePos27 = new Position();
            queenLinePos27.x1 = this.Position.PositionX;
            queenLinePos27.y1 = this.Position.PositionY - 6;
            if (queenLinePos27.x1 >= Constants.ForStart && queenLinePos27.x1 < Constants.Size && queenLinePos27.y1 >= Constants.ForStart && queenLinePos27.y1 < Constants.Size)
            {
                queenMoves.Add(queenLinePos27);
            }

            Position queenLinePos28 = new Position();
            queenLinePos28.x1 = this.Position.PositionX;
            queenLinePos28.y1 = this.Position.PositionY - 7;
            if (queenLinePos28.x1 >= Constants.ForStart && queenLinePos28.x1 < Constants.Size && queenLinePos28.y1 >= Constants.ForStart && queenLinePos28.y1 < Constants.Size)
            {
                queenMoves.Add(queenLinePos28);
            }

            Position queenDiagPos1 = new Position();
            queenDiagPos1.x1 = this.Position.PositionX + 1;
            queenDiagPos1.y1 = this.Position.PositionY + 1;
            if (queenDiagPos1.x1 >= Constants.ForStart && queenDiagPos1.x1 < Constants.Size && queenDiagPos1.y1 >= Constants.ForStart && queenDiagPos1.y1 < Constants.Size)
            {
                queenMoves.Add(queenDiagPos1);
            }

            Position queenDiagPos2 = new Position();
            queenDiagPos2.x1 = this.Position.PositionX + 2;
            queenDiagPos2.y1 = this.Position.PositionY + 2;
            if (queenDiagPos2.x1 >= Constants.ForStart && queenDiagPos2.x1 < Constants.Size && queenDiagPos2.y1 >= Constants.ForStart && queenDiagPos2.y1 < Constants.Size)
            {
                queenMoves.Add(queenDiagPos2);
            }

            Position queenDiagPos3 = new Position();
            queenDiagPos3.x1 = this.Position.PositionX + 3;
            queenDiagPos3.y1 = this.Position.PositionY + 3;
            if (queenDiagPos3.x1 >= Constants.ForStart && queenDiagPos3.x1 < Constants.Size && queenDiagPos3.y1 >= Constants.ForStart && queenDiagPos3.y1 < Constants.Size)
            {
                queenMoves.Add(queenDiagPos3);
            }

            Position queenDiagPos4 = new Position();
            queenDiagPos4.x1 = this.Position.PositionX + 4;
            queenDiagPos4.y1 = this.Position.PositionY + 4;
            if (queenDiagPos4.x1 >= Constants.ForStart && queenDiagPos4.x1 < Constants.Size && queenDiagPos4.y1 >= Constants.ForStart && queenDiagPos4.y1 < Constants.Size)
            {
                queenMoves.Add(queenDiagPos4);
            }

            Position queenDiagPos5 = new Position();
            queenDiagPos5.x1 = this.Position.PositionX + 5;
            queenDiagPos5.y1 = this.Position.PositionY + 5;
            if (queenDiagPos5.x1 >= Constants.ForStart && queenDiagPos5.x1 < Constants.Size && queenDiagPos5.y1 >= Constants.ForStart && queenDiagPos5.y1 < Constants.Size)
            {
                queenMoves.Add(queenDiagPos5);
            }

            Position queenDiagPos6 = new Position();
            queenDiagPos6.x1 = this.Position.PositionX + 6;
            queenDiagPos6.y1 = this.Position.PositionY + 6;
            if (queenDiagPos6.x1 >= Constants.ForStart && queenDiagPos6.x1 < Constants.Size && queenDiagPos6.y1 >= Constants.ForStart && queenDiagPos6.y1 < Constants.Size)
            {
                queenMoves.Add(queenDiagPos6);
            }

            Position queenDiagPos7 = new Position();
            queenDiagPos7.x1 = this.Position.PositionX + 7;
            queenDiagPos7.y1 = this.Position.PositionY + 7;
            if (queenDiagPos7.x1 >= Constants.ForStart && queenDiagPos7.x1 < Constants.Size && queenDiagPos7.y1 >= Constants.ForStart && queenDiagPos7.y1 < Constants.Size)
            {
                queenMoves.Add(queenDiagPos7);
            }

            Position queenDiagPos8 = new Position();
            queenDiagPos8.x1 = this.Position.PositionX - 1;
            queenDiagPos8.y1 = this.Position.PositionY - 1;
            if (queenDiagPos8.x1 >= Constants.ForStart && queenDiagPos8.x1 < Constants.Size && queenDiagPos8.y1 >= Constants.ForStart && queenDiagPos8.y1 < Constants.Size)
            {
                queenMoves.Add(queenDiagPos8);
            }

            Position queenDiagPos9 = new Position();
            queenDiagPos9.x1 = this.Position.PositionX - 2;
            queenDiagPos9.y1 = this.Position.PositionY - 2;
            if (queenDiagPos9.x1 >= Constants.ForStart && queenDiagPos9.x1 < Constants.Size && queenDiagPos9.y1 >= Constants.ForStart && queenDiagPos9.y1 < Constants.Size)
            {
                queenMoves.Add(queenDiagPos9);
            }

            Position queenDiagPos10 = new Position();
            queenDiagPos10.x1 = this.Position.PositionX - 3;
            queenDiagPos10.y1 = this.Position.PositionY - 3;
            if (queenDiagPos10.x1 >= Constants.ForStart && queenDiagPos10.x1 < Constants.Size && queenDiagPos10.y1 >= Constants.ForStart && queenDiagPos10.y1 < Constants.Size)
            {
                queenMoves.Add(queenDiagPos10);
            }

            Position queenDiagPos11 = new Position();
            queenDiagPos11.x1 = this.Position.PositionX - 4;
            queenDiagPos11.y1 = this.Position.PositionY - 4;
            if (queenDiagPos11.x1 >= Constants.ForStart && queenDiagPos11.x1 < Constants.Size && queenDiagPos11.y1 >= Constants.ForStart && queenDiagPos11.y1 < Constants.Size)
            {
                queenMoves.Add(queenDiagPos11);
            }

            Position queenDiagPos12 = new Position();
            queenDiagPos12.x1 = this.Position.PositionX - 5;
            queenDiagPos12.y1 = this.Position.PositionY - 5;
            if (queenDiagPos12.x1 >= Constants.ForStart && queenDiagPos12.x1 < Constants.Size && queenDiagPos12.y1 >= Constants.ForStart && queenDiagPos12.y1 < Constants.Size)
            {
                queenMoves.Add(queenDiagPos12);
            }

            Position queenDiagPos13 = new Position();
            queenDiagPos13.x1 = this.Position.PositionX - 6;
            queenDiagPos13.y1 = this.Position.PositionY - 6;
            if (queenDiagPos13.x1 >= Constants.ForStart && queenDiagPos13.x1 < Constants.Size && queenDiagPos13.y1 >= Constants.ForStart && queenDiagPos13.y1 < Constants.Size)
            {
                queenMoves.Add(queenDiagPos13);
            }

            Position queenDiagPos14 = new Position();
            queenDiagPos14.x1 = this.Position.PositionX - 7;
            queenDiagPos14.y1 = this.Position.PositionY - 7;
            if (queenDiagPos14.x1 >= Constants.ForStart && queenDiagPos14.x1 < Constants.Size && queenDiagPos14.y1 >= Constants.ForStart && queenDiagPos14.y1 < Constants.Size)
            {
                queenMoves.Add(queenDiagPos14);
            }

            Position queenDiagPos15 = new Position();
            queenDiagPos15.x1 = this.Position.PositionX - 1;
            queenDiagPos15.y1 = this.Position.PositionY + 1;
            if (queenDiagPos15.x1 >= Constants.ForStart && queenDiagPos15.x1 < Constants.Size && queenDiagPos15.y1 >= Constants.ForStart && queenDiagPos15.y1 < Constants.Size)
            {
                queenMoves.Add(queenDiagPos15);
            }

            Position queenDiagPos16 = new Position();
            queenDiagPos16.x1 = this.Position.PositionX - 2;
            queenDiagPos16.y1 = this.Position.PositionY + 2;
            if (queenDiagPos16.x1 >= Constants.ForStart && queenDiagPos16.x1 < Constants.Size && queenDiagPos16.y1 >= Constants.ForStart && queenDiagPos2.y1 < Constants.Size)
            {
                queenMoves.Add(queenDiagPos16);
            }

            Position queenDiagPos17 = new Position();
            queenDiagPos17.x1 = this.Position.PositionX - 3;
            queenDiagPos17.y1 = this.Position.PositionY + 3;
            if (queenDiagPos17.x1 >= Constants.ForStart && queenDiagPos17.x1 < Constants.Size && queenDiagPos17.y1 >= Constants.ForStart && queenDiagPos17.y1 < Constants.Size)
            {
                queenMoves.Add(queenDiagPos17);
            }

            Position queenDiagPos18 = new Position();
            queenDiagPos18.x1 = this.Position.PositionX - 4;
            queenDiagPos18.y1 = this.Position.PositionY + 4;
            if (queenDiagPos18.x1 >= Constants.ForStart && queenDiagPos18.x1 < Constants.Size && queenDiagPos18.y1 >= Constants.ForStart && queenDiagPos18.y1 < Constants.Size)
            {
                queenMoves.Add(queenDiagPos18);
            }

            Position queenDiagPos19 = new Position();
            queenDiagPos19.x1 = this.Position.PositionX - 5;
            queenDiagPos19.y1 = this.Position.PositionY + 5;
            if (queenDiagPos19.x1 >= Constants.ForStart && queenDiagPos19.x1 < Constants.Size && queenDiagPos19.y1 >= Constants.ForStart && queenDiagPos19.y1 < Constants.Size)
            {
                queenMoves.Add(queenDiagPos19);
            }

            Position queenDiagPos20 = new Position();
            queenDiagPos20.x1 = this.Position.PositionX - 6;
            queenDiagPos20.y1 = this.Position.PositionY + 6;
            if (queenDiagPos20.x1 >= Constants.ForStart && queenDiagPos20.x1 < Constants.Size && queenDiagPos20.y1 >= Constants.ForStart && queenDiagPos20.y1 < Constants.Size)
            {
                queenMoves.Add(queenDiagPos20);
            }

            Position queenDiagPos21 = new Position();
            queenDiagPos21.x1 = this.Position.PositionX - 7;
            queenDiagPos21.y1 = this.Position.PositionY + 7;
            if (queenDiagPos21.x1 >= Constants.ForStart && queenDiagPos21.x1 < Constants.Size && queenDiagPos21.y1 >= Constants.ForStart && queenDiagPos21.y1 < Constants.Size)
            {
                queenMoves.Add(queenDiagPos21);
            }

            Position queenDiagPos22 = new Position();
            queenDiagPos22.x1 = this.Position.PositionX + 1;
            queenDiagPos22.y1 = this.Position.PositionY - 1;
            if (queenDiagPos22.x1 >= Constants.ForStart && queenDiagPos22.x1 < Constants.Size && queenDiagPos22.y1 >= Constants.ForStart && queenDiagPos22.y1 < Constants.Size)
            {
                queenMoves.Add(queenDiagPos22);
            }

            Position queenDiagPos23 = new Position();
            queenDiagPos23.x1 = this.Position.PositionX + 2;
            queenDiagPos23.y1 = this.Position.PositionY - 2;
            if (queenDiagPos23.x1 >= Constants.ForStart && queenDiagPos23.x1 < Constants.Size && queenDiagPos23.y1 >= Constants.ForStart && queenDiagPos23.y1 < Constants.Size)
            {
                queenMoves.Add(queenDiagPos23);
            }

            Position queenDiagPos24 = new Position();
            queenDiagPos24.x1 = this.Position.PositionX + 3;
            queenDiagPos24.y1 = this.Position.PositionY - 3;
            if (queenDiagPos24.x1 >= Constants.ForStart && queenDiagPos24.x1 < Constants.Size && queenDiagPos24.y1 >= Constants.ForStart && queenDiagPos24.y1 < Constants.Size)
            {
                queenMoves.Add(queenDiagPos24);
            }

            Position queenDiagPos25 = new Position();
            queenDiagPos25.x1 = this.Position.PositionX + 4;
            queenDiagPos25.y1 = this.Position.PositionY - 4;
            if (queenDiagPos25.x1 >= Constants.ForStart && queenDiagPos25.x1 < Constants.Size && queenDiagPos25.y1 >= Constants.ForStart && queenDiagPos25.y1 < Constants.Size)
            {
                queenMoves.Add(queenDiagPos25);
            }

            Position queenDiagPos26 = new Position();
            queenDiagPos26.x1 = this.Position.PositionX + 5;
            queenDiagPos26.y1 = this.Position.PositionY - 5;
            if (queenDiagPos26.x1 >= Constants.ForStart && queenDiagPos26.x1 < Constants.Size && queenDiagPos26.y1 >= Constants.ForStart && queenDiagPos26.y1 < Constants.Size)
            {
                queenMoves.Add(queenDiagPos26);
            }

            Position queenDiagPos27 = new Position();
            queenDiagPos27.x1 = this.Position.PositionX + 6;
            queenDiagPos27.y1 = this.Position.PositionY - 6;
            if (queenDiagPos27.x1 >= Constants.ForStart && queenDiagPos27.x1 < Constants.Size && queenDiagPos27.y1 >= Constants.ForStart && queenDiagPos27.y1 < Constants.Size)
            {
                queenMoves.Add(queenDiagPos27);
            }

            Position queenDiagPos28 = new Position();
            queenDiagPos28.x1 = this.Position.PositionX + 7;
            queenDiagPos28.y1 = this.Position.PositionY - 7;
            if (queenDiagPos28.x1 >= Constants.ForStart && queenDiagPos28.x1 < Constants.Size && queenDiagPos28.y1 >= Constants.ForStart && queenDiagPos28.y1 < Constants.Size)
            {
                queenMoves.Add(queenDiagPos28);
            }

            return queenMoves;
        }


        public override string ToString()
        {
            var color = this.Color ? "w" : "b";

            return $"{color}QUEEN ";
        }
        public override object Clone()
        {
            return this.Clone<Queen>();
        }
    }
}