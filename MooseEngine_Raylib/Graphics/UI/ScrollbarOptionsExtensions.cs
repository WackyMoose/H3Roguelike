using MooseEngine.Graphics.UI.Options;
using Raylib_cs;

namespace MooseEngine.Graphics.UI;

public static class ScrollbarOptionsExtensions
{
    public static Rectangle GetScrollbarBounds(this ScrollbarOptions scrollbarOptions, Rectangle bounds)
    {
        var rect = new Rectangle
        {
            x = bounds.x + bounds.width - scrollbarOptions.BorderWidth - scrollbarOptions.ScrollbarWidth,
            y = bounds.y + scrollbarOptions.BorderWidth,
            width = (float)scrollbarOptions.ScrollbarWidth,
            height = bounds.height - 2 * scrollbarOptions.BorderWidth
        };

        return rect;
    }
}
