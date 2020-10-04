using System;

public class Rook : Piece
{
    public override bool CanJump => false;

    public Rook(bool color) : base(color) 
    {
        CanCastling = true;
    }
    public override bool IsValidMove(int x1, int y1, int x2, int y2)
    {        
        if (x1 == x2 || y1 == y2) 
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
