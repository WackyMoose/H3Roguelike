using MooseEngine.Core;
using Raylib_cs;

namespace MooseEngine.Graphics;

internal class UIRaylibRenderer : IUIRenderer
{
    public UIRaylibRenderer(IWindow windowData)
    {
        WindowData = windowData;
    }

    public IWindowData WindowData { get; }

    public void DrawFPS(int x, int y)
    {
        Raylib.DrawFPS(x, y);
    }

    public void DrawText(string text, int x, int y, int fontScale, Color background, Color fontColor)
    {
        var textWidth = Raylib.MeasureText(text, fontScale);

        Raylib.DrawRectangle(x - 4, y, textWidth + 8, fontScale + 4, background);
        Raylib.DrawText(text, x, y, fontScale, fontColor);
    }
}
