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
        public override bool IsValidMove(int x1, int y1, int x2, int y2, bool turn, Board board)
        {
            //no es tu turno
            if (this.Color != turn)
                return false;

            // movimiento en el mismo lugar
            if (x1 == x2 && y1 == y2)
                return false;

            // 4 castling cases
            //blancas abajo
            if (this.Color == true && this.Color == board.player1 && CanCastling && (x1 == 4) && (y1 == 0) && (x2 == 6 || x2 == 2) && y2 == 0)
                return true;
            //blancas arriba
            if (this.Color == true && this.Color != board.player1 && CanCastling && (x1 == 3) && (y1 == 7) && (x2 == 5 || x2 == 1) && y2 == 7)
                return true;
            //negras abajo
            if (this.Color == false && this.Color == board.player1 && CanCastling && (x1 == 3) && (y1 == 0) && (x2 == 5 || x2 == 1) && y2 == 0)
                return true;
            //negras arriba
            if (this.Color == false && this.Color != board.player1 && CanCastling && (x1 == 4) && (y1 == 7) && (x2 == 6 || x2 == 2) && y2 == 7)
                return true;


            if (Math.Sqrt((y2 - y1) * (y2 - y1) +
                (x2 - x1) * (x2 - x1)) < 2)
            {
                return true;
            }

            return false;
        }

        public override bool LogicMove(int x1, int y1, int x2, int y2, Board board, BoardLogic boardLogic)
        {
            if (boardLogic.IsInRange(x1, y1, x2, y2))
            {
                if (!board.IsEmpty(x1, y1) && IsValidMove(x1, y1, x2, y2, board.Turn, board))
                {

                    if (board.IsEmpty(x2, y2) && !boardLogic.IsCastling(x1, y1, x2, board) && !boardLogic.CantMoveIsCheck(x1, y1, x2, y2, board))
                    {                        
                            board.Move(x1, y1, x2, y2);
                            boardLogic.CastlingChanges(x1, y1, x2, y2, this);
                            return true;
                        
                    }

                    if (!board.IsEmpty(x2, y2) && !boardLogic.IsAlly(x1, y1, x2, y2, board) && !boardLogic.IsCastling(x1, y1, x2, board) && !boardLogic.CantMoveIsCheck(x1, y1, x2, y2, board))
                    {                        
                            board.Remove(x2, y2);
                            board.Move(x1, y1, x2, y2);
                            boardLogic.CastlingChanges(x1, y1, x2, y2, this);
                            return true;                        
                    }

                    if (board.IsEmpty(x2, y2) && boardLogic.IsCastling(x1, y1, x2, board) && boardLogic.CanCastling(x1, y1, x2, board))
                    {
                        board.Move(x1, y1, x2, y2);
                        boardLogic.MoveRookCastling(x1, y1, x2, board);
                        CanCastling = false;
                        return true;
                    }
                }
            }

            return false;
        }

        public override HashSet<Position> ValidMoves(Board board)
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
                kingMoves.Add(kingPos2);
            }
            Position kingPos3 = new Position();
            kingPos3.x1 = this.Position.PositionX + 1;
            kingPos3.y1 = this.Position.PositionY + 1;
            if (kingPos3.x1 >= Constants.ForStart && kingPos3.x1 < Constants.Size && kingPos3.y1 >= Constants.ForStart && kingPos3.y1 < Constants.Size)
            {
                kingMoves.Add(kingPos3);
            }
            Position kingPos4 = new Position();
            kingPos4.x1 = this.Position.PositionX - 1;
            kingPos4.y1 = this.Position.PositionY + 1;
            if (kingPos4.x1 >= Constants.ForStart && kingPos4.x1 < Constants.Size && kingPos4.y1 >= Constants.ForStart && kingPos4.y1 < Constants.Size)
            {
                kingMoves.Add(kingPos4);
            }
            Position kingPos5 = new Position();
            kingPos5.x1 = this.Position.PositionX + 1;
            kingPos5.y1 = this.Position.PositionY - 1;
            if (kingPos5.x1 >= Constants.ForStart && kingPos5.x1 < Constants.Size && kingPos5.y1 >= Constants.ForStart && kingPos5.y1 < Constants.Size)
            {
                kingMoves.Add(kingPos5);
            }
            Position kingPos6 = new Position();
            kingPos6.x1 = this.Position.PositionX - 1;
            kingPos6.y1 = this.Position.PositionY - 1;
            if (kingPos6.x1 >= Constants.ForStart && kingPos6.x1 < Constants.Size && kingPos6.y1 >= Constants.ForStart && kingPos6.y1 < Constants.Size)
            {
                kingMoves.Add(kingPos6);
            }
            Position kingPos7 = new Position();
            kingPos7.x1 = this.Position.PositionX;
            kingPos7.y1 = this.Position.PositionY + 1;
            if (kingPos7.x1 >= Constants.ForStart && kingPos7.x1 < Constants.Size && kingPos7.y1 >= Constants.ForStart && kingPos7.y1 < Constants.Size)
            {
                kingMoves.Add(kingPos7);
            }
            Position kingPos8 = new Position();
            kingPos8.x1 = this.Position.PositionX;
            kingPos8.y1 = this.Position.PositionY - 1;
            if (kingPos8.x1 >= Constants.ForStart && kingPos8.x1 < Constants.Size && kingPos8.y1 >= Constants.ForStart && kingPos8.y1 < Constants.Size)
            {
                kingMoves.Add(kingPos8);
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