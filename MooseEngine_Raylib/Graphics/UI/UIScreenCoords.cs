namespace MooseEngine.Graphics.UI;

public struct UIScreenCoords
{
    public UIScreenCoords()
    {
        X = 0;
        Y = 0;
    }

    public UIScreenCoords(int x, int y)
    {
        X = x;
        Y = y;
    }

    public UIScreenCoords(UIScreenCoords other)
    {
        X = other.X;
        Y = other.Y;
    }

    public int X { get; set; }
    public int Y { get; set; }

    public static readonly UIScreenCoords Zero = new UIScreenCoords(0, 0);
    public static readonly UIScreenCoords One = new UIScreenCoords(1, 1);

    public static UIScreenCoords operator -(UIScreenCoords a, int value)
    {
        return new UIScreenCoords(a.X - value, a.Y - value);
    }
}
