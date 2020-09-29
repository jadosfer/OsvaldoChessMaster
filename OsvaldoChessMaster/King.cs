using System;

public class King : Piece
{
    public override bool CanJump => false;
    public King(bool color) : base(color) { }
    public override bool IsValidMove(int x1, int y1, int x2, int y2)
    {
        if (Math.Sqrt((y2 - y1)* (y2 - y1) + (x2 - x1)* (x2 - x1)) < 2)
        {
            return true;
        }

        return false;
    }
}
