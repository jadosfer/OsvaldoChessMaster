using System;

    public class ___e : Piece
    {
        public ___e(bool color) : base(color) { }
        public override bool CanJump => false;
        public override bool IsValidMove(int x1, int y1, int x2, int y2)
        {
            return false;
        }        



    }



