using MooseEngine.Graphics.UI.Options;
using Raylib_cs;
using System.Numerics;

namespace MooseEngine.Graphics.UI;

public static class SliderOptionsExtensions
{
    public static Rectangle GetSliderRectangle(this SliderOptions sliderOptions, float value)
    {
        var rect = new Rectangle
        {
            x = sliderOptions.Position.X + sliderOptions.BorderWidth,
            y = sliderOptions.Position.Y + sliderOptions.BorderWidth + sliderOptions.Padding,
            width = value,
            height = sliderOptions.Size.Y - 2.0f * sliderOptions.BorderWidth - 2.0f * sliderOptions.Padding
        };

        return rect;
    }

    public static Rectangle GetTextBounds(this SliderOptions sliderOptions)
    {
        var bounds = sliderOptions.GetBounds();

        Rectangle rect = new(0, 0, 0, 0)
        {
            width = GetTextWidth(sliderOptions.Text, sliderOptions.FontSize, sliderOptions.TextSpacing),
            height = sliderOptions.FontSize
        };
        rect.x = sliderOptions.TextAlignment == TextAlignment.Left ? bounds.x - rect.width - sliderOptions.TextPadding : bounds.x + bounds.width + sliderOptions.TextPadding;
        rect.y = bounds.y + bounds.height / 2 - sliderOptions.FontSize / 2;

        return rect;
    }

    public static float ClampValue(this SliderOptions sliderOptions, float value)
    {
        var val = value;
        if (val > sliderOptions.MaxValue)
        {
            val = sliderOptions.MaxValue;
        }

        if (val < sliderOptions.MinValue)
        {
            val = sliderOptions.MinValue;
        }

        return val;
    }

    const int ICON_TEXT_PADDING = 4;
    const int RAYGUI_ICON_SIZE = 16;
    private static int GetTextWidth(string text, float fontSize, float textSpacing)
    {
        Vector2 size = Vector2.Zero;
        int textIconOffset = 0;

        if (!string.IsNullOrWhiteSpace(text) && text[0] != '\0')
        {
            if (text[0] == '#')
            {
                for (int i = 1; text[i] != '\0' && i < 5; i++)
                {
                    if (text[i] == '#')
                    {
                        textIconOffset = i;
                        break;
                    }
                }
            }

            size = Raylib.MeasureTextEx(Raylib.GetFontDefault(), text + textIconOffset, fontSize, textSpacing);
            if (textIconOffset > 0) size.X += RAYGUI_ICON_SIZE - ICON_TEXT_PADDING;
        }

        return (int)size.X;
    }
}
