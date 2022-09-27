using MooseEngine.Graphics.UI.Options;
using Raylib_cs;

namespace MooseEngine.Graphics.UI;

public static class TextOptionsExtentions
{
    public static Rectangle GetTextBounds(this TextOptions textOptions, Rectangle bounds)
    {
        Rectangle textBounds = bounds;

        textBounds.x = bounds.x + textOptions.BorderWidth;
        textBounds.y = bounds.y + textOptions.BorderWidth;
        textBounds.width = bounds.width - 2 * textOptions.BorderWidth;
        textBounds.height = bounds.height - 2 * textOptions.BorderWidth;

        if (textOptions.TextAlignment == TextAlignment.Right)
        {
            textBounds.x -= textOptions.Padding;
        }
        else
        {
            textBounds.x += textOptions.Padding;
        }
        textBounds.width -= 2 * textOptions.Padding;

        return textBounds;
    }

    public static Rectangle GetTextBounds(this TextOptions textOptions)
    {
        var bounds = textOptions.GetBounds();

        Rectangle textBounds = bounds;

        textBounds.x = bounds.x + textOptions.BorderWidth;
        textBounds.y = bounds.y + textOptions.BorderWidth;
        textBounds.width = bounds.width - 2 * textOptions.BorderWidth;
        textBounds.height = bounds.height - 2 * textOptions.BorderWidth;

        if (textOptions.TextAlignment == TextAlignment.Right)
        {
            textBounds.x -= textOptions.Padding;
        }
        else
        {
            textBounds.x += textOptions.Padding;
        }
        textBounds.width -= 2 * textOptions.Padding;

        return textBounds;
    }
}
