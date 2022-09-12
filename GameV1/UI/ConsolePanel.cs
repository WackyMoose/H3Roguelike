using MooseEngine.Graphics;
using MooseEngine.Graphics.UI;
using MooseEngine.Graphics.UI.Options;
using MooseEngine.Utilities;

namespace MooseEngine.UI;

public class ConsolePanel
{
    private const string TITLE = "Console";
    public const int HEIGHT = 140;

    private static ConsolePanel s_Instance;

    private Coords2D _position;
    private Coords2D _size;

    private int _focus = -1;
    private int _scrollIndex = 3;
    private int _active = -1;
    private int _historyCapacity;

    private List<string> _history;

    private ListViewOptions _listViewOptions;

    public ConsolePanel(Coords2D position, Coords2D size, int capacity = 10)
    {
        s_Instance = this;

        _position = position;
        _size = size;
        _historyCapacity = capacity;

        _history = new List<string>(_historyCapacity);

        var listViewPosition = new UIScreenCoords(_position.X, _position.Y);
        var listViewSize = new UIScreenCoords(_size.X, _size.Y);
        _listViewOptions = new ListViewOptions(listViewPosition, listViewSize, TITLE, false);
    }

    public void OnGUI(IUIRenderer UIRenderer)
    {
        _active = UIRenderer.DrawListViewEx(_listViewOptions, _history, ref _focus, ref _scrollIndex, _active);

        //var rect = new Raylib_cs.Rectangle(_position.X, _position.Y, _size.X, _size.Y);
        //_active = UIRenderer.DrawListViewEx(rect, TITLE, _history.ToArray(), _history.Count, ref _focus, ref _scrollIndex, _active, _stickToBottom);
    }

    private void AddToList(string msg)
    {
        if (_history.Count >= _history.Capacity)
        {
            _history.RemoveAt(0);
        }
        _history.Add(msg);
    }

    public static void Add(string msg)
    {
        s_Instance.AddToList(msg);
    }
}
