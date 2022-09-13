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
    protected readonly static Color DEFAULT_TEXT_NORMAL_COLOR = new Color(104, 104, 104, 255);
    protected readonly static Color DEFAULT_TEXT_FOCUSED_COLOR = new Color(108, 155, 188, 255);
    protected readonly static Color DEFAULT_TEXT_PRESSED_COLOR = new Color(54, 139, 175, 255);
    protected readonly static Color DEFAULT_TEXT_DISABLED_COLOR = new Color(174, 183, 184, 255);

    protected readonly static float DEFAULT_TEXT_SPACING = 1.0f;
    protected readonly static int DEFAULT_PADDING = 2;

    public TextOptions(UIScreenCoords position, UIScreenCoords size, string text, bool interactable = true)
        : this(position, size, text, Constants.DEFAULT_FONT_SIZE, interactable)
    {
    }

    public TextOptions(UIScreenCoords position, UIScreenCoords size, string text, int fontSize, bool interactable = true)
        : this(position, size, text, fontSize, DEFAULT_BORDER_WIDTH, interactable)
    {
    }

    public TextOptions(UIScreenCoords position, UIScreenCoords size, string text, int fontSize, int borderWidth, bool interactable = true)
        : this(position, size, text, fontSize, DEFAULT_TEXT_SPACING, borderWidth, DEFAULT_PADDING, TextAlignment.Left,
          DEFAULT_LINE_COLOR, DEFAULT_BACKGROUND_COLOR,
          DEFAULT_NORMAL_COLOR, DEFAULT_FOCUSED_COLOR, DEFAULT_PRESSED_COLOR, DEFAULT_DISABLED_COLOR,
          DEFAULT_TEXT_NORMAL_COLOR, DEFAULT_TEXT_FOCUSED_COLOR, DEFAULT_TEXT_PRESSED_COLOR, DEFAULT_TEXT_DISABLED_COLOR,
          DEFAULT_BORDER_NORMAL_COLOR, DEFAULT_BORDER_FOCUSED_COLOR, DEFAULT_BORDER_PRESSED_COLOR, DEFAULT_BORDER_DISABLED_COLOR, interactable)
    {
    }

    public TextOptions(UIScreenCoords position, UIScreenCoords size, string text, int fontSize, float textSpacing, int borderWidth, int padding, TextAlignment textAlignment, 
        Color textNormalColor, Color textFocusedColor, Color textPressedColor, Color textDisabledColor, bool interactable = true)
        : this(position, size, text, fontSize, textSpacing, borderWidth, padding, textAlignment,
          DEFAULT_LINE_COLOR, DEFAULT_BACKGROUND_COLOR,
          DEFAULT_NORMAL_COLOR, DEFAULT_FOCUSED_COLOR, DEFAULT_PRESSED_COLOR, DEFAULT_DISABLED_COLOR,
          textNormalColor, textFocusedColor, textPressedColor, textDisabledColor,
          DEFAULT_BORDER_NORMAL_COLOR, DEFAULT_BORDER_FOCUSED_COLOR, DEFAULT_BORDER_PRESSED_COLOR, DEFAULT_BORDER_DISABLED_COLOR, interactable)
    {
    }

    public TextOptions(UIScreenCoords position, UIScreenCoords size, string text, int fontSize, float textSpacing, int borderWidth, int padding, TextAlignment textAlignment, Color lineColor, Color backgroundColor,
        Color normalColor, Color focusedColor, Color pressedColor, Color disabledColor,
        Color textNormalColor, Color textFocusedColor, Color textPressedColor, Color textDisabledColor,
        Color borderNormalColor, Color borderFocusedColor, Color borderPressedColor, Color borderDisabledColor,
        bool interactable = true)
        : base(position, size, 
            lineColor, backgroundColor, 
            normalColor, focusedColor, pressedColor, disabledColor, 
            borderNormalColor, borderFocusedColor, borderPressedColor, borderDisabledColor, 
            borderWidth, interactable)
    {
        Text = text;
        FontSize = fontSize;
        TextSpacing = textSpacing;
        Padding = padding;
        TextAlignment = textAlignment;

        TextNormalColor = textNormalColor;
        TextFocusedColor = textFocusedColor;
        TextPressedColor = textPressedColor;
        TextDisabledColor = textDisabledColor;

        Font = Raylib_cs.Raylib.GetFontDefault();
    }

    public bool SetFontByPath(string path)
    {
        Raylib_cs.Font? newFont = Raylib_cs.Raylib.LoadFont(path);
        return newFont != null;
    }

    public string Text { get; set; }
    public int FontSize { get; set; }
    public float TextSpacing { get; set; }
    public int Padding { get; set; }
    public TextAlignment TextAlignment { get; set; }

    public Color TextNormalColor { get; set; }
    public Color TextFocusedColor { get; set; }
    public Color TextPressedColor { get; set; }
    public Color TextDisabledColor { get; set; }

    public Raylib_cs.Font Font { get; set; }
}
