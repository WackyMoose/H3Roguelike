using MooseEngine.Graphics.UI.Options;
using Raylib_cs;

namespace MooseEngine.Graphics.UI;

public static class PanelOptionsExtensions
{
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
