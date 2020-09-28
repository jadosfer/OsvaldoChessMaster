using System;

public class Rook : Piece
{
    public Rook(bool color) : base(color) { }
    public override bool IsValidMove(int x1, int y1, int x2, int y2)
    {        
        if (x1 == x2 || y1 == y2) 
        {
            return true;
        }   

        return false;        
    }
}
