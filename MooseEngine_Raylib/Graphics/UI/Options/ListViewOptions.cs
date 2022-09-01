namespace MooseEngine.Graphics.UI.Options;

public class ListViewOptions
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
