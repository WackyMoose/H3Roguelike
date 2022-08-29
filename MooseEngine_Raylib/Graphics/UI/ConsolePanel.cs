using MooseEngine.Utilities;

namespace MooseEngine.Graphics.UI;

public class ConsolePanel
{
    private Coords2D _position;
    private Coords2D _size;

    private const string TITLE = "Console";

    public ConsolePanel(Coords2D position, Coords2D size)
    {
        _position = position;
        _size = size;
    }

    public void OnGUI(IUIRenderer UIRenderer)
    {
        UIRenderer.DrawPanel(_position.X, _position.Y, _size.X, _size.Y, TITLE);
        for (int i = 0; i < 5; i++)
        {
            var x = 10;
            var y = _position.Y + (i * 30) + 30;

            UIRenderer.DrawLabel(x, y, $"Text {i}");
            UIRenderer.DrawLine(x, y + 24, 1000, 1, string.Empty);
        }
    }
}
