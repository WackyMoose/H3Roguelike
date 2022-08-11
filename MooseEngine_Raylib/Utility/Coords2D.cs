using Raylib_cs;

namespace MooseEngine.Utility
{
    public struct Coords2D
    {
        public int X { get; init; }
        public int Y { get; init; }

        public Coords2D(int x, int y)
        {
            X = x; 
            Y = y; 
        }

        public Coords2D(Rectangle rectangle)
        {
            X = (int) rectangle.x;
            Y = (int) rectangle.y;
        }
    }
}