using MooseEngine.Graphics.UI.Options;
using Raylib_cs;
using System.Numerics;

namespace MooseEngine.Graphics;

public static class SliderOptionsExtensions
{
    public static Rectangle GetSliderRectangle(this SliderOptions sliderOptions)
    {
        var rect = new Rectangle
        {
            x = sliderOptions.Position.X + sliderOptions.BorderWidth,
            y = sliderOptions.Position.Y + sliderOptions.BorderWidth + sliderOptions.Padding,
            width = (float)sliderOptions.Value,
            height = sliderOptions.Size.Y - 2.0f * sliderOptions.BorderWidth - 2.0f * sliderOptions.Padding
        };

        return rect;
    }

    public static Rectangle GetTextBounds(this SliderOptions sliderOptions)
    {
        var bounds = sliderOptions.GetBounds();

        Rectangle rect = new(0, 0, 0, 0)
        {
            width = (float)GetTextWidth(sliderOptions.Text, sliderOptions.FontSize, sliderOptions.TextSpacing),
            height = sliderOptions.FontSize
        };
        rect.x = sliderOptions.TextAlignment == TextAlignmentSlider.Left ? bounds.x - rect.width - sliderOptions.TextPadding : bounds.x + bounds.width + sliderOptions.TextPadding;
        rect.y = bounds.y + bounds.height / 2 - sliderOptions.FontSize / 2;

        return rect;
    }

    public static Rectangle GetBounds(this SliderOptions sliderOptions)
    {
        var rect = new Rectangle
        {
            x = sliderOptions.Position.X,
            y = sliderOptions.Position.Y,
            width = sliderOptions.Size.X,
            height = sliderOptions.Size.Y
        };

        return rect;
    }

    public static void ClampValue(this SliderOptions sliderOptions)
    {
        if (sliderOptions.Value > sliderOptions.MaxValue)
        {
            sliderOptions.Value = sliderOptions.MaxValue;
        }

        if (sliderOptions.Value < sliderOptions.MinValue)
        {
            sliderOptions.Value = sliderOptions.MinValue;
        }
    }

    const int ICON_TEXT_PADDING = 4;
    const int RAYGUI_ICON_SIZE = 16;
    private static int GetTextWidth(string text, float fontSize, float textSpacing)
    {
        Vector2 size = Vector2.Zero;
        int textIconOffset = 0;

        if ((!string.IsNullOrWhiteSpace(text)) && (text[0] != '\0'))
        {
            if (text[0] == '#')
            {
                for (int i = 1; (text[i] != '\0') && (i < 5); i++)
                {
                    if (text[i] == '#')
                    {
                        textIconOffset = i;
                        break;
                    }
                }
            }

            size = Raylib.MeasureTextEx(Raylib.GetFontDefault(), text + textIconOffset, fontSize, textSpacing);
            if (textIconOffset > 0) size.X += (RAYGUI_ICON_SIZE - ICON_TEXT_PADDING);
        }

        return (int)size.X;
    }
}
