using MooseEngine.Extensions.Runtime;
using MooseEngine.Graphics;
using MooseEngine.Graphics.UI;
using MooseEngine.Graphics.UI.Options;
using MooseEngine.Utilities;

namespace MooseEngine.UI;

public class ConsolePanel
{
    private readonly static Color HEADER_COLOR = new Color(115, 67, 67);
    private readonly static Color BACKGROUND_COLOR = new Color(42, 40, 41);
    private readonly static Color TEXT_COLOR = Color.White; 

    private const string TITLE = "Log";
    public const int HEIGHT = 140;
    private const int MAX_TEXT_LENGTH = 100;

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
        _listViewOptions = new ListViewOptions(listViewPosition, listViewSize, TITLE, HEADER_COLOR, TEXT_COLOR, TEXT_COLOR, HEADER_COLOR, BACKGROUND_COLOR, false);
    }

    public void OnGUI(IUIRenderer UIRenderer)
    {
        _active = UIRenderer.DrawListViewEx(_listViewOptions, _history, ref _focus, ref _scrollIndex, _active);
    }

    private void AddToList(string msg)
    {
        if (_history.Count >= _history.Capacity)
        {
            _history.RemoveAt(0);
        }

        if (msg.Length <= MAX_TEXT_LENGTH)
        {
            _history.Add(msg);
            return;
        }

        var subStrings = msg.Split(MAX_TEXT_LENGTH);
        foreach (var str in subStrings)
        {
            _history.Add(str);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="msg"></param>
    public static void Add(string msg)
    {
        s_Instance.AddToList(msg);
    }
}
