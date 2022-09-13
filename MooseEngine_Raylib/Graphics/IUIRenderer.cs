using MooseEngine.Core;
using MooseEngine.Graphics.UI.Options;

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
    void DrawImage(SubImageOptions imageOptions);
    bool DrawTextInputField(TextInputFieldOptions textInputFieldOptions, ref string text, int textSize, bool editMode);
    int DrawListViewEx(ListViewOptions listViewOptions, IEnumerable<string> items, ref int focus, ref int scrollIndex, int active);
    void DrawSeperator(SeperatorOptions seperatorOptions);
}
