using MooseEngine.Graphics.UI.Options;
using Raylib_cs;

namespace MooseEngine.Graphics.UI;

public static class ListViewOptionsExtensions
{
    public static Rectangle GetItemBounds(this ListViewOptions listViewOptions)
    {
        var bounds = listViewOptions.GetBounds();

        var rect = new Rectangle
        {
            x = bounds.x + listViewOptions.ItemSpacing,
            y = bounds.y + listViewOptions.ItemSpacing + listViewOptions.BorderWidth,
            width = bounds.width - 2 * listViewOptions.ItemSpacing - listViewOptions.BorderWidth,
            height = listViewOptions.ItemHeight
        };

        return rect;
    }
}
