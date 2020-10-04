using System;

abstract public class Piece
{
    public abstract bool CanJump { get; }
    public bool CanCastling;
    public bool LCastling;
    public bool SCastling;
    public bool Color;
    public Piece(bool color)
    {        
        this.Color = color;
        this.LCastling = false;
        this.SCastling = false;
        this.CanCastling = false;
    }

    abstract public bool IsValidMove(int x1, int y1, int x2, int y2); 

    virtual public bool GetCanCastling() { return true; } //Enroque del King

    virtual public void SetCanCastling(bool CanCastling) { } //Enroque del King

    virtual public bool GetCapturableByTheWay() { return true; }

    virtual public void SetCapturableByTheWay(bool CapturableByTheWay, int turnNumber) {}

    virtual public int GetturnNumberCapturableByTheWay() { return 0;}

}
