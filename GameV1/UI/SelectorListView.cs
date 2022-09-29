using MooseEngine.Graphics;
using MooseEngine.Graphics.UI;
using MooseEngine.Graphics.UI.Options;

namespace GameV1.UI;

internal class SelectorListView : IUIElement
{
    private static SelectorListView? s_Instance;

    private ListViewOptions _listViewOptions;

    public IEnumerable<string>? _items;
    private int _active = -1;
    private int _focus = -1;
    private int _scrollIndex = -1;

    public SelectorListView(UIScreenCoords position, UIScreenCoords size)
    {
        s_Instance = this;

        _listViewOptions = new ListViewOptions(position, size, false);
        _listViewOptions.BorderWidth = 0;
        _listViewOptions.FontSize = 36;
        _listViewOptions.TextNormalColor = Color.White;
        _listViewOptions.BackgroundColor = Color.Blank;
    }

    public void OnGUI(IUIRenderer UIRenderer)
    {
        if(_items == default)
        {
            return;
        }

        UIRenderer.DrawListViewEx(_listViewOptions, _items, ref _focus, ref _scrollIndex, _active);
    }

    public static void SetData(IEnumerable<string> items)
    {
        if (s_Instance == null)
        {
            System.Diagnostics.Debugger.Break();
            return;
        }

        s_Instance._items = items;
    }
}
