using MooseEngine.Core;

namespace MooseEngine.Graphics;

public interface IUIRenderer
{
    IWindowData WindowData { get; }

    void DrawFPS(int x, int y);
    void DrawText(string text, int x, int y, int fontScale, Color background, Color fontColor);

    bool DrawButton(int x, int y, string text, bool enabled = true);
    void DrawLabel(int x, int y, string text);
    void DrawLine(int x, int y, int width, int height, string text);
    void DrawPanel(int x, int y, int width, int height, string title, bool enabled = true);
}
