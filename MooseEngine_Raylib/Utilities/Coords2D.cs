using Raylib_cs;

namespace MooseEngine.Utilities
{
    public struct Coords2D
    {
        public int X { get; init; }
        public int Y { get; init; }

        public Coords2D Up { get { return new Coords2D(0, -1); } }
        public Coords2D Down { get { return new Coords2D(0, 1); } }
        public Coords2D Left { get { return new Coords2D(-1, 0); } }
        public Coords2D Right { get { return new Coords2D(1, 0); } }


        public Coords2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Coords2D(Rectangle rectangle)
        {
            X = (int)rectangle.x;
            Y = (int)rectangle.y;
        }
    }
}