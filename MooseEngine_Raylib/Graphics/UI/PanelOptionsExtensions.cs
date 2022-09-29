using MooseEngine.Graphics.UI.Options;
using Raylib_cs;

namespace MooseEngine.Graphics.UI;

public static class PanelOptionsExtensions
{
    public static Rectangle GetBoundsWithoutStatusBar(this PanelOptions panelOptions)
    {
        var rect = panelOptions.GetBounds();

        // Text will be drawn as a header bar (if provided)
        if (!string.IsNullOrWhiteSpace(panelOptions.Text) && (rect.height < panelOptions.StatusBarHeight * 2.0f))
        {
            rect.height = panelOptions.StatusBarHeight * 2;
        }

        if (!string.IsNullOrWhiteSpace(panelOptions.Text))
        {
            // Move panel bounds after the header bar
            rect.y += panelOptions.StatusBarHeight - 1;
            rect.height -= panelOptions.StatusBarHeight + 1;
        }

        return rect;
    }

    public static Rectangle GetStatusBarRectangle(this PanelOptions panelOptions)
    {
        var bounds = panelOptions.GetBounds();

        var rect = new Rectangle
        {
            x = bounds.x,
            y = bounds.y,
            width = bounds.width,
            height = panelOptions.StatusBarHeight
        };

        return rect;
    }
}

