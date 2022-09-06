using MooseEngine.Core;
using MooseEngine.Graphics.UI.Options;
using Raylib_cs;
using System.Numerics;

namespace MooseEngine.Graphics;

public interface IUIRenderer
{
    IWindowData WindowData { get; }

    void Initialize();

    void DrawFPS(int x, int y);

    void DrawLabel(LabelOptions labelOptions);
    bool DrawButton(ButtonOptions buttonOptions);
    float DrawSliderBar(SliderOptions sliderOptions, float value);
    void DrawPanel(PanelOptions panelOptions);
    void DrawImage(ImageOptions imageOptions);
    //void DrawListViewEx(ListViewOptions listViewOptions);
    int DrawListViewEx(ListViewOptions listViewOptions, IEnumerable<string> items, ref int focus, ref int scrollIndex, int active);
    void DrawSeperator(SeperatorOptions seperatorOptions);

    [Obsolete]
    int DrawListView(Rectangle bounds, string title, string text, ref int scrollIndex, int active);
    [Obsolete]
    int DrawListViewEx(Rectangle bounds, string title, string[] text, int count, ref int focus, ref int scrollIndex, int active, bool stickToBottom = false);
    [Obsolete]
    void DrawScrollPanel(Rectangle bounds, string text, Rectangle content, ref Vector2 scroll, bool enabled = true);
}
