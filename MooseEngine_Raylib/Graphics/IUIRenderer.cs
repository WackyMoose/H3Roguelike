using MooseEngine.Core;
using Raylib_cs;
using System.Numerics;

namespace MooseEngine.Graphics;

public interface IUIRenderer
{
    IWindowData WindowData { get; }

    void DrawFPS(int x, int y);

    void DrawLabel(int x, int y, string text);
    bool DrawButton(int x, int y, string text);
    void DrawLine(int x, int y, int width, int height, string text);
    void DrawPanel(int x, int y, int width, int height, string title);
    int DrawListView(Rectangle bounds, string title, string text, ref int scrollIndex, int active);
    int DrawListViewEx(Rectangle bounds, string title, string[] text, int count, ref int focus, ref int scrollIndex, int active, bool stickToBottom = false);
    float DrawSlider(Rectangle bounds, string textLeft, string textRight, float value, float minValue, float maxValue, int sliderWidth);
    void DrawScrollPanel(Rectangle bounds, string text, Rectangle content, ref Vector2 scroll, bool enabled = true);
}
