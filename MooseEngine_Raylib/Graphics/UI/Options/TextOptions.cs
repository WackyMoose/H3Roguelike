using MooseEngine.Utilities;

namespace MooseEngine.Graphics.UI.Options;

public enum TextAlignment
{
    None = 0,
    Center,
    Left,
    Right
}

public class TextOptions : UIOptionsBase
{
    public TextOptions(UIScreenCoords position, UIScreenCoords size, string text, bool interactable = true)
        : this(position, size, text, Constants.DEFAULT_FONT_SIZE, 1.0f, 2, 2, TextAlignment.Left, interactable)
    {
    }

    public TextOptions(UIScreenCoords position, UIScreenCoords size, string text, int fontSize, bool interactable = true)
        : this(position, size, text, fontSize, 1.0f, 2, 2, TextAlignment.Left, interactable)
    {
    }

    public TextOptions(UIScreenCoords position, UIScreenCoords size, string text, int fontSize, int borderWidth, bool interactable = true)
    : this(position, size, text, fontSize, 1.0f, borderWidth, 2, TextAlignment.Left, interactable)
    {
    }

    public TextOptions(UIScreenCoords position, UIScreenCoords size, string text, int fontSize, float textSpacing, int borderWidth, int padding, TextAlignment textAlignment, bool interactable = true)
        : base(position, size, interactable)
    {
        Text = text;
        FontSize = fontSize;
        TextSpacing = textSpacing;
        BorderWidth = borderWidth;
        Padding = padding;
        TextAlignment = textAlignment;

        TextNormalColor = new Color(165, 98, 67, 255);
        //TextNormalColor = new Color(104, 104, 104, 255);
        TextFocusedColor = new Color(108, 155, 188, 255);
        TextPressedColor = new Color(54, 139, 175, 255);
        TextDisabledColor = new Color(174, 183, 184, 255);
    }

    public string Text { get; set; }
    public int FontSize { get; set; }
    public float TextSpacing { get; set; }
    public int BorderWidth { get; set; }
    public int Padding { get; set; }
    public TextAlignment TextAlignment { get; set; }

    public Color TextNormalColor { get; set; }
    public Color TextFocusedColor { get; set; }
    public Color TextPressedColor { get; set; }
    public Color TextDisabledColor { get; set; }
}
