using MooseEngine.Core;

namespace MooseEngine.Graphics;

public interface IUIRenderer
{
    IWindowData WindowData { get; }

    void DrawFPS(int x, int y);
    void DrawText(string text, int x, int y, int fontScale, Color background, Color fontColor);
}
