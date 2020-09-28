﻿using System;

public class Queen : Piece
{
    public Queen(bool color) : base(color) { }
    public override bool IsValidMove(int x1, int y1, int x2, int y2)
    {
        if (x1 == x2 || y1 == y2)
        {
            return true;
        }

        if (Math.Abs(y2 - y1) == (Math.Abs(x2 - x1)))
        {
            return true;
        }

        return false;
    }
}
