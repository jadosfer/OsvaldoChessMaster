﻿namespace OsvaldoChessMaster.Piece
{
    public class EmptyPiece : PieceBase
    {
        public override bool CanJump => false;

        public EmptyPiece(bool color, int PositionX, int PositionY)
            : base(color, PositionX, PositionY) { }

        public override bool IsValidMove(int x2, int y2)
        {
            return false;
        }

        public override string ToString()
        {
            return "____";
        }
    }
}