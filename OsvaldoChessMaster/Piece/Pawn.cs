﻿namespace OsvaldoChessMaster.Piece
{
    using System;
    using System.Collections.Generic;

    public class Pawn : PieceBase
    {
        public override bool CanJump => false;
        public override bool EnPassantAllowed { get; set; }
        public override int EnPassantTurnNumber { get; set; }

        public override int Value => 10;
        public override bool CapturableByTheWay { get; set; }
        public override int FullMoveNumberCapturableByTheWay { get; set; }

        public Pawn() { }
        public Pawn(bool color, int PositionX, int PositionY)
            : base(color, PositionX, PositionY)
        {
            EnPassantAllowed = false;
        }

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

            // si quiere moverse en la misma columna
            if (x1 == x2)
            {
                if (this.Color == board.player1 && ((y2 - y1) == 1 || (y2 - y1 == 2 && y1 == 1)))
                {
                    return true;
                }
                if (this.Color != board.player1 && ((y1 - y2) == 1 || (y1 - y2 == 2 && y1 == 6)))
                {
                    return true;
                }
            }

            // si quiere moverse a las columnas de al lado            
            if (this.Color == board.player1 && (Math.Abs(x2 - x1) == 1 && y2 - y1 == 1))
            {
                return true;
            }
            if (this.Color != board.player1 && (Math.Abs(x2 - x1) == 1 && y1 - y2 == 1))
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

                    if (boardLogic.IsPawnCapturing(x1, x2) && !board.IsEmpty(x2, y2) && !boardLogic.CantMoveIsCheck(x1, y1, x2, y2, board))
                    {
                        board.Remove(x2, y2);
                        board.Move(x1, y1, x2, y2);
                        if (boardLogic.Promoting(y2))
                        {
                            board.Promotion(x2, y2, board.Turn);
                        }
                        return true;
                    }

                    if (boardLogic.IsPawnCapturing(x1, x2) && boardLogic.EnPassant(x1, y1, x2, y2, board) && !boardLogic.CantMoveIsCheck(x1, y1, x2, y2, board))
                    {
                        board.RemoveCapturePassant(x1, y1, x2, y2);
                        board.Move(x1, y1, x2, y2);
                        return true;
                    }

                    if (!boardLogic.IsPawnCapturing(x1, x2) && board.IsEmpty(x2, y2) && boardLogic.IsLineEmpty(x1, y1, x2, y2, board) && !boardLogic.CantMoveIsCheck(x1, y1, x2, y2, board))
                    {
                        board.Move(x1, y1, x2, y2);
                        boardLogic.UpDateEnPassant(y1, y2, piece, board);
                        if (boardLogic.Promoting(y2))
                        {
                            board.Promotion(x2, y2, board.Turn);
                        }
                        return true;
                    }
                }
            }

            return false;
        }

        public override HashSet<Position> ValidMoves(Board board)
        {
            HashSet<Position> pawnMoves = new HashSet<Position>();

            if (board.Turn == board.player1)
            {
                if (this.Position.PositionY == 1)
                {
                    Position pawnPos1 = new Position();
                    pawnPos1.x1 = this.Position.PositionX - 1;
                    pawnPos1.y1 = this.Position.PositionY + 1;
                    if (pawnPos1.x1 >= Constants.ForStart && pawnPos1.x1 < Constants.Size && pawnPos1.y1 >= Constants.ForStart && pawnPos1.y1 < Constants.Size)
                    {
                        pawnMoves.Add(pawnPos1);
                    }
                    Position pawnPos2 = new Position();
                    pawnPos2.x1 = this.Position.PositionX + 1;
                    pawnPos2.y1 = this.Position.PositionY + 1;
                    if (pawnPos2.x1 >= Constants.ForStart && pawnPos2.x1 < Constants.Size && pawnPos2.y1 >= Constants.ForStart && pawnPos2.y1 < Constants.Size)
                    {
                        pawnMoves.Add(pawnPos2);
                    }
                    Position pawnPos3 = new Position();
                    pawnPos3.x1 = this.Position.PositionX;
                    pawnPos3.y1 = this.Position.PositionY + 1;
                    if (pawnPos3.x1 >= Constants.ForStart && pawnPos3.x1 < Constants.Size && pawnPos3.y1 >= Constants.ForStart && pawnPos3.y1 < Constants.Size)
                    {
                        pawnMoves.Add(pawnPos3);
                    }
                    Position pawnPos4 = new Position();
                    pawnPos4.x1 = this.Position.PositionX;
                    pawnPos4.y1 = this.Position.PositionY + 2;
                    if (pawnPos4.x1 >= Constants.ForStart && pawnPos4.x1 < Constants.Size && pawnPos4.y1 >= Constants.ForStart && pawnPos4.y1 < Constants.Size)
                    {
                        pawnMoves.Add(pawnPos4);
                    }
                }
                else
                {
                    Position pawnPos1 = new Position();
                    pawnPos1.x1 = this.Position.PositionX - 1;
                    pawnPos1.y1 = this.Position.PositionY + 1;
                    if (pawnPos1.x1 >= Constants.ForStart && pawnPos1.x1 < Constants.Size && pawnPos1.y1 >= Constants.ForStart && pawnPos1.y1 < Constants.Size)
                    {
                        pawnMoves.Add(pawnPos1);
                    }
                    Position pawnPos2 = new Position();
                    pawnPos2.x1 = this.Position.PositionX + 1;
                    pawnPos2.y1 = this.Position.PositionY + 1;
                    if (pawnPos2.x1 >= Constants.ForStart && pawnPos2.x1 < Constants.Size && pawnPos2.y1 >= Constants.ForStart && pawnPos2.y1 < Constants.Size)
                    {
                        pawnMoves.Add(pawnPos2);
                    }
                    Position pawnPos3 = new Position();
                    pawnPos3.x1 = this.Position.PositionX;
                    pawnPos3.y1 = this.Position.PositionY + 1;
                    if (pawnPos3.x1 >= Constants.ForStart && pawnPos3.x1 < Constants.Size && pawnPos3.y1 >= Constants.ForStart && pawnPos3.y1 < Constants.Size)
                    {
                        pawnMoves.Add(pawnPos3);
                    }
                }
            }
            else
            {
                if (this.Position.PositionY == 6)
                {
                    Position pawnPos5 = new Position();
                    pawnPos5.x1 = this.Position.PositionX - 1;
                    pawnPos5.y1 = this.Position.PositionY - 1;
                    if (pawnPos5.x1 >= Constants.ForStart && pawnPos5.x1 < Constants.Size && pawnPos5.y1 >= Constants.ForStart && pawnPos5.y1 < Constants.Size)
                    {
                        pawnMoves.Add(pawnPos5);
                    }
                    Position pawnPos6 = new Position();
                    pawnPos6.x1 = this.Position.PositionX + 1;
                    pawnPos6.y1 = this.Position.PositionY - 1;
                    if (pawnPos6.x1 >= Constants.ForStart && pawnPos6.x1 < Constants.Size && pawnPos6.y1 >= Constants.ForStart && pawnPos6.y1 < Constants.Size)
                    {
                        pawnMoves.Add(pawnPos6);
                    }
                    Position pawnPos7 = new Position();
                    pawnPos7.x1 = this.Position.PositionX;
                    pawnPos7.y1 = this.Position.PositionY - 1;
                    if (pawnPos7.x1 >= Constants.ForStart && pawnPos7.x1 < Constants.Size && pawnPos7.y1 >= Constants.ForStart && pawnPos7.y1 < Constants.Size)
                    {
                        pawnMoves.Add(pawnPos7);
                    }
                    Position pawnPos8 = new Position();
                    pawnPos8.x1 = this.Position.PositionX;
                    pawnPos8.y1 = this.Position.PositionY - 2;
                    if (pawnPos8.x1 >= Constants.ForStart && pawnPos8.x1 < Constants.Size && pawnPos8.y1 >= Constants.ForStart && pawnPos8.y1 < Constants.Size)
                    {
                        pawnMoves.Add(pawnPos8);
                    }
                }
                else
                {
                    Position pawnPos5 = new Position();
                    pawnPos5.x1 = this.Position.PositionX - 1;
                    pawnPos5.y1 = this.Position.PositionY - 1;
                    if (pawnPos5.x1 >= Constants.ForStart && pawnPos5.x1 < Constants.Size && pawnPos5.y1 >= Constants.ForStart && pawnPos5.y1 < Constants.Size)
                    {
                        pawnMoves.Add(pawnPos5);
                    }
                    Position pawnPos6 = new Position();
                    pawnPos6.x1 = this.Position.PositionX + 1;
                    pawnPos6.y1 = this.Position.PositionY - 1;
                    if (pawnPos6.x1 >= Constants.ForStart && pawnPos6.x1 < Constants.Size && pawnPos6.y1 >= Constants.ForStart && pawnPos6.y1 < Constants.Size)
                    {
                        pawnMoves.Add(pawnPos6);
                    }
                    Position pawnPos7 = new Position();
                    pawnPos7.x1 = this.Position.PositionX;
                    pawnPos7.y1 = this.Position.PositionY - 1;
                    if (pawnPos7.x1 >= Constants.ForStart && pawnPos7.x1 < Constants.Size && pawnPos7.y1 >= Constants.ForStart && pawnPos7.y1 < Constants.Size)
                    {
                        pawnMoves.Add(pawnPos7);
                    }
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