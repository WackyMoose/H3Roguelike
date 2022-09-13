using MooseEngine.Graphics;
using MooseEngine.Graphics.UI;
using MooseEngine.Graphics.UI.Options;
using MooseEngine.Utilities;

namespace MooseEngine.UI;

public static class IEnumerableExtensions
{
    public static IEnumerable<string> SplitString(this string str, int size)
    {
        return Enumerable.Range(0, str.Length / size).Select(i => str.Substring(i * size, size));
    }

    public static IEnumerable<string> Split(this string str, int length)
    {
        return str.Split(' ')
            .Aggregate(new [] { "" }.ToList(), (a, x) =>
            {
                var last = a[a.Count - 1];
                if ((last + " " + x).Length > length)
                {
                    a.Add(x);
                }
                else
                {
                    a[a.Count - 1] = (last + " " + x).Trim();
                }
                return a;
            });
    }

    public static IEnumerable<string> EnumByNearestSpace(this string value, int length)
    {
        if (String.IsNullOrEmpty(value))
            yield break;

        int bestDelta = int.MaxValue;
        int bestSplit = -1;

        int from = 0;

        for (int i = 0; i < value.Length; ++i)
        {
            var Ch = value[i];

            if (Ch != ' ')
                continue;

            int size = (i - from);
            int delta = (size - length > 0) ? size - length : length - size;

            if ((bestSplit < 0) || (delta < bestDelta))
            {
                bestSplit = i;
                bestDelta = delta;
            }
            else
            {
                yield return value.Substring(from, bestSplit - from);

                i = bestSplit;

                from = i + 1;
                bestSplit = -1;
                bestDelta = int.MaxValue;
            }
        }

        // String's tail
        if (from < value.Length)
        {
            if (bestSplit >= 0)
            {
                if (bestDelta < value.Length - from)
                    yield return value.Substring(from, bestSplit - from);

                from = bestSplit + 1;
            }

            if (from < value.Length)
                yield return value.Substring(from);
        }
    }
}

public class ConsolePanel
{
    private readonly static Color HEADER_COLOR = new Color(68, 48, 41);
    private readonly static Color HEADER_TEXT_COLOR = new Color(165, 98, 67);

    private readonly static Color BACKGROUND_COLOR = new Color(165, 98, 67);
    private readonly static Color TEXT_COLOR = new Color(43, 37, 36); // new Color(90, 44, 32); new Color(70, 26, 20); new Color(48, 15, 10);

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
        _listViewOptions = new ListViewOptions(listViewPosition, listViewSize, TITLE, HEADER_COLOR, HEADER_TEXT_COLOR, TEXT_COLOR, HEADER_COLOR, BACKGROUND_COLOR, false);
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
