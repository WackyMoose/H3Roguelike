using MooseEngine.Graphics.UI.Options;
using Raylib_cs;

namespace MooseEngine.Graphics.UI;

public static class UIOptionsBaseExtentions
{
    public static Rectangle GetBounds(this UIOptionsBase options)
    {
        var rect = new Rectangle
        {
            x = options.Position.X,
            y = options.Position.Y,
            width = options.Size.X,
            height = options.Size.Y
        };

        return rect;
    }
}
