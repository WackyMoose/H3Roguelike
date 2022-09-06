using MooseEngine.Graphics.UI.Options;
using Raylib_cs;

namespace MooseEngine.Graphics.UI;

public static class ImageOptionsExtensions
{
    public static Rectangle GetImageDestination(this ImageOptions imageOptions)
    {
        var rect = new Rectangle
        {
            x = imageOptions.Position.X,
            y = imageOptions.Position.Y,
            width = imageOptions.Size.X,
            height = imageOptions.Size.Y
        };

        return rect;
    }

    public static Rectangle GetImageSource(this ImageOptions imageOptions)
    {
        var rect = new Rectangle
        {
            x = 0 + ((imageOptions.SpriteSize + 1) * imageOptions.Coords.X),
            y = 0 + ((imageOptions.SpriteSize + 1) * imageOptions.Coords.Y),
            width = imageOptions.SpriteSize,
            height = imageOptions.SpriteSize
        };

        return rect;
    }
}
