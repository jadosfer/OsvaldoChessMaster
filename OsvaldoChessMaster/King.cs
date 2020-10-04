using System;

public class King : Piece
{
    //private bool CanCastling;
    public override bool CanJump => false;
    public King(bool color) : base(color) 
    {
        CanCastling = true;
    }

    public override bool IsValidMove(int x1, int y1, int x2, int y2)
    {
        if (Math.Sqrt((y2 - y1)* (y2 - y1) + (x2 - x1)* (x2 - x1)) < 2)
        {
            return true;
        }

        return false;
    }

    public override bool GetCanCastling() 
    { 
        return CanCastling; 
    }
    public override void SetCanCastling(bool CanCastling)
    {
        this.CanCastling = CanCastling;
    }
}
