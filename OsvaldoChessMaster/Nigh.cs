using System;

public class Nigh : Piece
{
    public override bool CanJump => true;
    public Nigh(bool color) : base(color) { }
    public override bool IsValidMove(int x1, int y1, int x2, int y2)
    {        
        if ((Math.Abs(y2 - y1) == 2 && (Math.Abs(x2 - x1) == 1)) || (Math.Abs(y2 - y1) == 1 && (Math.Abs(x2 - x1) == 2)))
        {            
            return true;
        }

        return false;
    }
}
