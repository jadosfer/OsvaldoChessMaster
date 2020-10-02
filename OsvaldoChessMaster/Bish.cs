using System;

public class Bish : Piece
{
    public override bool CanJump => false;
    public Bish(bool color) : base(color) { }
    public override bool IsValidMove(int x1, int y1, int x2, int y2)
    {
        if (Math.Abs(y2 - y1) == (Math.Abs(x2 - x1)))
        {
            return true;
        }

        return false;
    }
}
