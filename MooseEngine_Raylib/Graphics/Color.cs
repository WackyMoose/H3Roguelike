﻿namespace MooseEngine.Graphics;

public class Color
{
    public int R { get; set; }
    public int G { get; set; }
    public int B { get; set; }
    public int A { get; set; }

    #region Static Colors
    public static Color LightGray = new(200, 200, 200, 255);
    public static Color Gray = new(130, 130, 130, 255);
    public static Color DarkGray = new(80, 80, 80, 255);
    public static Color Yellow = new(253, 249, 0, 255);
    public static Color Gold = new(255, 203, 0, 255);
    public static Color Orange = new(255, 161, 0, 255);
    public static Color Pink = new(255, 109, 194, 255);
    public static Color Red = new(230, 41, 55, 255);
    public static Color Maroon = new(190, 33, 55, 255);
    public static Color Green = new(0, 228, 48, 255);
    public static Color Lime = new(0, 158, 47, 255);
    public static Color DarkGreen = new(0, 117, 44, 255);
    public static Color SkyBlue = new(102, 191, 255, 255);
    public static Color Blue = new(0, 121, 241, 255);
    public static Color DarkBlue = new(0, 82, 172, 255);
    public static Color Purple = new(200, 122, 255, 255);
    public static Color Violet = new(135, 60, 190, 255);
    public static Color DarkPurple = new(112, 31, 126, 255);
    public static Color Beige = new(211, 176, 131, 255);
    public static Color Brown = new(127, 106, 79, 255);
    public static Color DarkBrown = new(76, 63, 47, 255);
    public static Color White = new(255, 255, 255, 255);
    public static Color Black = new(0, 0, 0, 255);
    public static Color Blank = new(0, 0, 0, 0);
    public static Color Magenta = new(255, 0, 255, 255);
    public static Color RayWhite = new(245, 245, 245, 255);
    #endregion

    public Color(int r, int g, int b, int a)
    {
        R = Convert.ToByte(r);
        G = Convert.ToByte(g);
        B = Convert.ToByte(b);
        A = Convert.ToByte(a);
    }

    public static implicit operator Raylib_cs.Color(Color color)
    {
        return new Raylib_cs.Color(color.R, color.G, color.B, color.A);
    }
}