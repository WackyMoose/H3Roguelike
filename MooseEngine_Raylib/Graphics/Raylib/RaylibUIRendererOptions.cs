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

}

internal class RaylibUIRendererOptions : IRaylibUIRendererOptions
{
    public int FontSize { get; set; }
    public Font Font { get; set; }

    public RaylibUIRendererOptions(int fontSize)
    {
        FontSize = fontSize;

        var path = @"..\..\..\Resources\Fonts\Retro_Gaming.ttf";

        Font = File.Exists(path) ? Raylib.LoadFont(path) : Raylib.GetFontDefault();
    }
}

