using System;

public class Pawn : Piece
{
    public override bool CanJump => false;
    private bool CapturableByTheWay;
    private int turnNumberCapturableByTheWay;
    public Pawn(bool color) : base(color) 
    {
        CapturableByTheWay = false;
    }
        
    public override bool IsValidMove(int x1, int y1, int x2, int y2)
    {
        if (x1 == x2) //si quiere moverse en la misma columna
        {
            if ((Math.Abs(y2 - y1) == 1) || (Math.Abs(y2 - y1) == 2 && (y1 == 2 || y1 == 7)))
            {
                return true;
            }
        }

        //si quiere moverse a las columnas de al lado            
        if ((Math.Abs(x2 - x1) == 1) && Math.Abs(y2 - y1) == 1)
        {
            return true;
        }
        return false;
    }
    public override bool GetCapturableByTheWay()
    {
        return CapturableByTheWay;
    }
    public override int GetturnNumberCapturableByTheWay()
    {
        return turnNumberCapturableByTheWay;
    }
    public override void SetCapturableByTheWay(bool CapturableByTheWay, int turnNumber)
    {
        this.CapturableByTheWay = CapturableByTheWay;
        this.turnNumberCapturableByTheWay = turnNumber;
    }
}

