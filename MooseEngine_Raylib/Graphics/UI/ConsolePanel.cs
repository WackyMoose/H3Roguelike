using MooseEngine.Utilities;
using System;

namespace MooseEngine.Graphics.UI;

public class ConsolePanel
{
    private const string TITLE = "Console";

    private static ConsolePanel s_Instance;

    private Coords2D _position;
    private Coords2D _size;

    private int _focus = -1;
    private int _scrollIndex = 3;
    private int _active = -1;
    private bool _stickToBottom = true;
    private int _historyCapacity;

    private List<string> _history;

    public ConsolePanel(Coords2D position, Coords2D size, int capacity = 10)
    {
        s_Instance = this;

        _position = position;
        _size = size;
        _historyCapacity = capacity;

        _history = new List<string>(_historyCapacity);
    }

    public void OnGUI(IUIRenderer UIRenderer)
    {
        var rect = new Raylib_cs.Rectangle(_position.X, _position.Y, _size.X, _size.Y);
        _active = UIRenderer.DrawListViewEx(rect, _history.ToArray(), _history.Count, ref _focus, ref _scrollIndex, _active, _stickToBottom);
    }

    public static void Add(string msg)
    {
        if(s_Instance._history.Count >= s_Instance._history.Capacity)
        {
            s_Instance._history.RemoveAt(0);
        }
        s_Instance._history.Add(msg);
    }
}
