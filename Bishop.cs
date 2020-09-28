using System;

public class Bishop : Piece
{
    public bool isValidMove(int x1, int x2, int y1, int y2)
    {
        if (Math.Abs(y2 - y1) == (Math.Abs(x2 - x1)))
        {
            return true;
        }

        return false;
    }
}
