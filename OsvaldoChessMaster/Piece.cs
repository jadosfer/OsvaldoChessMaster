﻿using System;

abstract public class Piece
{
    public abstract bool CanJump { get; }
    public bool Color;
    public Piece(bool color)
    {        
        this.Color = color;
    }

    abstract public bool IsValidMove(int x1, int y1, int x2, int y2);
}
