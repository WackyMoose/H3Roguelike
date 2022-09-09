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

    public int X { get; set; }
    public int Y { get; set; }

    public static readonly UIScreenCoords Zero = new UIScreenCoords(0, 0);
    public static readonly UIScreenCoords One = new UIScreenCoords(1, 1);
}
