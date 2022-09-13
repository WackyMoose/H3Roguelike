using MooseEngine.Utilities;

namespace MooseEngine.Graphics.UI.Options;

public class PanelOptions : TextOptions
{
    protected readonly static Color HEADER_NORMAL_COLOR = new Color(201, 201, 201, 255);
    protected readonly static Color HEADER_DISABLED_COLOR = new Color(230, 233, 233, 255);
    protected readonly static int DEFAULT_STATUS_BAR_HEIGHT = 24;

    public PanelOptions(UIScreenCoords position, UIScreenCoords size, string text, bool interactable = true)
        : this(position, size, text, DEFAULT_BORDER_NORMAL_COLOR, DEFAULT_TEXT_NORMAL_COLOR, DEFAULT_BORDER_NORMAL_COLOR, DEFAULT_BACKGROUND_COLOR, interactable)
    {
    }

    public PanelOptions(UIScreenCoords position, UIScreenCoords size, string text, Color headerNormalColor, Color headerTextColor, Color borderColor, Color backgroundColor, bool interactable = true)
        : this(position, size, text, Constants.DEFAULT_FONT_SIZE, DEFAULT_TEXT_SPACING, DEFAULT_BORDER_WIDTH, DEFAULT_PADDING, TextAlignment.Left,
              DEFAULT_LINE_COLOR, backgroundColor,
              DEFAULT_NORMAL_COLOR, DEFAULT_FOCUSED_COLOR, DEFAULT_PRESSED_COLOR, DEFAULT_DISABLED_COLOR,
              headerTextColor, DEFAULT_TEXT_FOCUSED_COLOR, DEFAULT_TEXT_PRESSED_COLOR, DEFAULT_TEXT_DISABLED_COLOR,
              borderColor, DEFAULT_BORDER_FOCUSED_COLOR, DEFAULT_BORDER_PRESSED_COLOR, DEFAULT_BORDER_DISABLED_COLOR,
              DEFAULT_STATUS_BAR_HEIGHT, headerNormalColor, Color.Blank, DEFAULT_TEXT_NORMAL_COLOR, DEFAULT_TEXT_DISABLED_COLOR, interactable)
    {
    }

    public PanelOptions(UIScreenCoords position, UIScreenCoords size, string text, int fontSize, float textSpacing, int borderWidth, int padding, TextAlignment textAlignment,
        Color lineColor, Color backgroundColor,
        Color normalColor, Color focusedColor, Color pressedColor, Color disabledColor, 
        Color textNormalColor, Color textFocusedColor, Color textPressedColor, Color textDisabledColor, 
        Color borderNormalColor, Color borderFocusedColor, Color borderPressedColor, Color borderDisabledColor, 
        int statusBarHeight, Color headerNormalColor, Color headerDisabledColor, Color headerTextNormalColor, Color headerTextDisabledColor, bool interactable = true) 
        : base(position, size, text, fontSize, textSpacing, borderWidth, padding, textAlignment, 
            lineColor, backgroundColor, 
            normalColor, focusedColor, pressedColor, disabledColor, 
            textNormalColor, textFocusedColor, textPressedColor, textDisabledColor, 
            borderNormalColor, borderFocusedColor, borderPressedColor, borderDisabledColor, 
            interactable)
    {
        StatusBarHeight = statusBarHeight;
        HeaderNormalColor = headerNormalColor;
        HeaderDisabledColor = headerDisabledColor;
        HeaderTextNormalColor = headerTextNormalColor;
        HeaderTextDisabledColor = headerTextDisabledColor;
    }

    public int StatusBarHeight { get; set; }
    public Color HeaderNormalColor { get; set; }
    public Color HeaderDisabledColor { get; set; }
    public Color HeaderTextNormalColor { get; set; }
    public Color HeaderTextDisabledColor { get; set; }
}
