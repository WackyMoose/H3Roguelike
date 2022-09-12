using MooseEngine.Graphics.UI.Options;
using Raylib_cs;

namespace MooseEngine.Graphics;

public enum ScrollbarSide
{
    Left,
    Right
}

public interface IRaylibUIRendererOptions
{
    int FontSize { get; set; }
    Font Font { get; set; }

    int BorderWidth { get; set; }
    int TextPadding { get; set; }
    float TextSpacing { get; set; }

    ColorOptions Colors { get; set; }
    ScrollbarOptions ScrollbarOptions { get; set; }
    TextAlignment StatusBarTextAlignment { get; set; }
}

internal class RaylibUIRendererOptions : IRaylibUIRendererOptions
{
    public int FontSize { get; set; }
    public Font Font { get; set; }

    public int BorderWidth { get; set; }
    public int TextPadding { get; set; }
    public float TextSpacing { get; set; }

    public ColorOptions Colors { get; set; }
    public ScrollbarOptions ScrollbarOptions { get; set; }

    public TextAlignment StatusBarTextAlignment { get; set; }

    public RaylibUIRendererOptions(int fontSize)
    {
        FontSize = fontSize;

        var path = @"..\..\..\Resources\Fonts\Retro_Gaming.ttf";

        Font = File.Exists(path) ? Raylib.LoadFont(path) : Raylib.GetFontDefault();

        BorderWidth = 1;
        TextPadding = 4;
        TextSpacing = 1;

        Colors = new ColorOptions();
        ScrollbarOptions = new ScrollbarOptions();

        StatusBarTextAlignment = TextAlignment.Left;
    }
}

