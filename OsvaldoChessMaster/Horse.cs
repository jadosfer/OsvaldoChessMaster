using System;

public class Horse : Piece
{
    public Horse(bool color) : base(color) { }
    public override bool IsValidMove(int x1, int y1, int x2, int y2)
    {        
        if ((Math.Abs(y2 - y1) == 2 && (Math.Abs(x2 - x1) == 1)) || (Math.Abs(y2 - y1) == 1 && (Math.Abs(x2 - x1) == 2)))
        {            
            return true;
        }

        return false;
    }
}
