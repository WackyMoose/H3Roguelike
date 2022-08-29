using MooseEngine.Utilities;

namespace MooseEngine.Graphics.UI;

public class ConsolePanel
{
    private static ConsolePanel s_Instance;

    private Coords2D _position;
    private Coords2D _size;

    private const string TITLE = "Console";

    private List<string> _history;

    public ConsolePanel(Coords2D position, Coords2D size)
    {
        s_Instance = this;

        _position = position;
        _size = size;

        _history = new List<string>();
    }

    public void OnGUI(IUIRenderer UIRenderer)
    {
        UIRenderer.DrawPanel(_position.X, _position.Y, _size.X, _size.Y, TITLE);

        for (int i = 0; i < 5; i++)
        {
            if(i >= _history.Count)
            {
                continue;
            }

            var message = _history[i];
            if(string.IsNullOrWhiteSpace(message))
            {
                continue;
            }

            var x = 10;
            var y = _position.Y + (i * 30) + 30;

            UIRenderer.DrawLabel(x, y, message);
            UIRenderer.DrawLine(x, y + 24, 1000, 1, string.Empty);
        }
    }

    public static void Add(string msg)
    {
        s_Instance._history.Add(msg);
    }
}
