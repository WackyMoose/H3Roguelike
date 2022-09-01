namespace MooseEngine.Graphics;

public enum TextAlignment : uint
{
    Left = 0,
    Center,
    Right,
}

public enum ScrollbarSide
{
    Left,
    Right
}

public struct ListViewOptions
{
    public int ScrollbarWidth { get; set; }
    public int ItemSpacing { get; set; }
    public int ItemHeight { get; set; }
    public ScrollbarSide ScrollbarSide { get; set; }
    public TextAlignment TextAlignment { get; set; }

    public Color NormalColor { get; set; }
    public Color FocusedColor { get; set; }
    public Color PressedColor { get; set; }
    public Color DisabledColor { get; set; }

    public Color BorderNormalColor { get; set; }
    public Color BorderFocusedColor { get; set; }
    public Color BorderPressedColor { get; set; }
    public Color BorderDisabledColor { get; set; }

    public Color TextNormalColor { get; set; }
    public Color TextFocusedColor { get; set; }
    public Color TextPressedColor { get; set; }
    public Color TextDisabledColor { get; set; }

    public ListViewOptions()
    {
        ItemHeight = 24;
        ItemSpacing = 2;
        ScrollbarWidth = 12;
        ScrollbarSide = ScrollbarSide.Right;
        TextAlignment = TextAlignment.Left;

        NormalColor = new Color(201, 201, 201, 255);
        FocusedColor = new Color(201, 239, 254, 255);
        PressedColor = new Color(151, 232, 255, 255);
        DisabledColor = new Color(230, 233, 233, 255);

        BorderNormalColor = new Color(131, 131, 131, 255);
        BorderFocusedColor = new Color(91, 178, 217, 255);
        BorderPressedColor = new Color(4, 146, 199, 255);
        BorderDisabledColor = new Color(181, 193, 194, 255);

        TextNormalColor = new Color(104, 104, 104, 255);
        TextFocusedColor = new Color(108, 155, 188, 255);
        TextPressedColor = new Color(54, 139, 175, 255);
        TextDisabledColor = new Color(174, 183, 184, 255);
    }
}

public interface IRaylibUIRendererOptions
{
    int FontSize { get; set; }

    int BorderWidth { get; set; }
    int TextPadding { get; set; }
    float TextSpacing { get; set; }

    int ComboButtonWidth { get; set; }
    int ComboButtonSpacing { get; set; }

    int ScrollbarScrollSliderSize { get; set; }
    bool ScrollbarArrowsVisible { get; set; }
    int ScrollbarBorderWidth { get; set; }
    int ScrollbarScrollSpeed { get; set; }
    int ScrollbarScrollPadding { get; set; }

    ListViewOptions ListViewOptions { get; set; }

    TextAlignment LabelTextAlignment { get; set; }
    TextAlignment StatusBarTextAlignment { get; set; }

    Color BaseColor { get; set; }
    Color LineColor { get; set; }
    Color TextColor { get; set; }
    Color BorderColor { get; set; }
    Color BorderDisabledColor { get; set; }
    Color DropdownBorderColor { get; set; }
    Color BackgroundColor { get; set; }
}

internal class RaylibUIRendererOptions : IRaylibUIRendererOptions
{
    public int FontSize { get; set; }

    public int BorderWidth { get; set; }
    public int TextPadding { get; set; }
    public float TextSpacing { get; set; }

    public int ComboButtonWidth { get; set; }
    public int ComboButtonSpacing { get; set; }

    public int ScrollbarScrollSliderSize { get; set; }
    public bool ScrollbarArrowsVisible { get; set; }
    public int ScrollbarBorderWidth { get; set; }
    public int ScrollbarScrollSpeed { get; set; }
    public int ScrollbarScrollPadding { get; set; }

    public ListViewOptions ListViewOptions { get; set; }

    public TextAlignment StatusBarTextAlignment { get; set; }
    public TextAlignment LabelTextAlignment { get; set; }

    public Color BaseColor { get; set; }
    public Color LineColor { get; set; }
    public Color TextColor { get; set; }
    public Color BorderColor { get; set; }
    public Color BorderDisabledColor { get; set; }
    public Color DropdownBorderColor { get; set; }
    public Color BackgroundColor { get; set; }

    public RaylibUIRendererOptions(int fontSize)
    {
        FontSize = fontSize;
        BaseColor = new Color(201, 201, 201, 255);
        LineColor = new Color(0, 0, 0, 255);
        TextColor = new Color(104, 104, 104, 255);
        BorderDisabledColor = new Color(181, 193, 194, 255);
        BorderColor = new Color(131, 131, 131, 255);
        DropdownBorderColor = new Color(131, 131, 131, 255);
        BackgroundColor = new Color(245, 245, 245, 255);

        BorderWidth = 1;
        TextPadding = 4;
        TextSpacing = 1;

        ComboButtonWidth = 32;
        ComboButtonSpacing = 2;

        ScrollbarScrollSliderSize = 16;
        ScrollbarArrowsVisible = false;
        ScrollbarScrollSpeed = 12;

        ListViewOptions = new ListViewOptions();

        StatusBarTextAlignment = TextAlignment.Left;
        LabelTextAlignment = TextAlignment.Left;
    }
}

/*
GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiControlProperty.BORDER_COLOR_NORMAL, 0x838383ff);
GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiControlProperty.BASE_COLOR_NORMAL, 0xc9c9c9ff);
GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiControlProperty.TEXT_COLOR_NORMAL, 0x686868ff);
GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiControlProperty.BORDER_COLOR_FOCUSED, 0x5bb2d9ff);
GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiControlProperty.BASE_COLOR_FOCUSED, 0xc9effeff);
GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiControlProperty.TEXT_COLOR_FOCUSED, 0x6c9bbcff);
GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiControlProperty.BORDER_COLOR_PRESSED, 0x0492c7ff);
GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiControlProperty.BASE_COLOR_PRESSED, 0x97e8ffff);
GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiControlProperty.TEXT_COLOR_PRESSED, 0x368bafff);
GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiControlProperty.BORDER_COLOR_DISABLED, 0xb5c1c2ff);
GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiControlProperty.BASE_COLOR_DISABLED, 0xe6e9e9ff);
GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiControlProperty.TEXT_COLOR_DISABLED, 0xaeb7b8ff);
*/
