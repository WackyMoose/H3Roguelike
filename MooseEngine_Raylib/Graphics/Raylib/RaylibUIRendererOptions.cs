namespace MooseEngine.Graphics;

public enum TextAlignment : uint
{
    TEXT_ALIGN_LEFT = 0,
    TEXT_ALIGN_CENTER,
    TEXT_ALIGN_RIGHT,
}

public interface IRaylibUIRendererOptions
{
    int FontSize { get; set; }

    int BorderWidth { get; set; }
    int TextPadding { get; set; }
    float TextSpacing { get; set; }

    int ComboButtonWidth { get; set; }
    int ComboButtonSpacing { get; set; }

    TextAlignment LabelTextAlignment { get; set; }
    TextAlignment StatusBarTextAlignment { get; set; }

    Color? BaseColor { get; set; }
    Color? LineColor { get; set; }
    Color? TextColor { get; set; }
    Color? BorderColor { get; set; }
    Color? BackgroundColor { get; set; }
}

internal class RaylibUIRendererOptions : IRaylibUIRendererOptions
{
    public int FontSize { get; set; }

    public int BorderWidth { get; set; }
    public int TextPadding { get; set; }
    public float TextSpacing { get; set; }

    public int ComboButtonWidth { get; set; }
    public int ComboButtonSpacing { get; set; }

    public TextAlignment StatusBarTextAlignment { get; set; }
    public TextAlignment LabelTextAlignment { get; set; }

    public Color? BaseColor { get; set; }
    public Color? LineColor { get; set; }
    public Color? TextColor { get; set; }
    public Color? BorderColor { get; set; }
    public Color? BackgroundColor { get; set; }

    public RaylibUIRendererOptions(int fontSize)
    {
        FontSize = fontSize;
        BaseColor = new Color(201, 201, 201, 255);
        LineColor = new Color(0, 0, 0, 255);
        TextColor = new Color(104, 104, 104, 255);
        BorderColor = new Color(131, 131, 131, 255);
        BackgroundColor = new Color(245, 245, 245, 255);

        BorderWidth = 1;
        TextPadding = 4;
        TextSpacing = 1;

        ComboButtonWidth = 32;
        ComboButtonSpacing = 2;

        StatusBarTextAlignment = TextAlignment.TEXT_ALIGN_LEFT;
        LabelTextAlignment = TextAlignment.TEXT_ALIGN_LEFT;
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
