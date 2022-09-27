using MooseEngine.Utilities;

namespace MooseEngine.Graphics.UI.Options;

public class TextInputFieldOptions : TextOptions
{
    protected readonly static int DEFAULT_INNER_PADDING = 2;
    private readonly static int DEFAULT_TEXT_PADDING = 2;

    public TextInputFieldOptions(UIScreenCoords position, UIScreenCoords size, bool interactable = true)
        : this(position, size, string.Empty, Constants.DEFAULT_FONT_SIZE, DEFAULT_TEXT_SPACING, DEFAULT_BORDER_WIDTH, DEFAULT_PADDING, TextAlignment.Left,
              DEFAULT_LINE_COLOR, DEFAULT_BACKGROUND_COLOR,
              DEFAULT_NORMAL_COLOR, DEFAULT_FOCUSED_COLOR, DEFAULT_PRESSED_COLOR, DEFAULT_DISABLED_COLOR,
              DEFAULT_TEXT_NORMAL_COLOR, DEFAULT_TEXT_FOCUSED_COLOR, DEFAULT_TEXT_PRESSED_COLOR, DEFAULT_TEXT_DISABLED_COLOR,
              DEFAULT_BORDER_NORMAL_COLOR, DEFAULT_BORDER_FOCUSED_COLOR, DEFAULT_BORDER_PRESSED_COLOR, DEFAULT_BORDER_DISABLED_COLOR, DEFAULT_INNER_PADDING, DEFAULT_TEXT_PADDING, 
              interactable)
    {
    }

    public TextInputFieldOptions(UIScreenCoords position, UIScreenCoords size, string text, int fontSize, float textSpacing, int borderWidth, int padding, TextAlignment textAlignment, 
        Color lineColor, Color backgroundColor, 
        Color normalColor, Color focusedColor, Color pressedColor, Color disabledColor, 
        Color textNormalColor, Color textFocusedColor, Color textPressedColor, Color textDisabledColor, 
        Color borderNormalColor, Color borderFocusedColor, Color borderPressedColor, Color borderDisabledColor, 
        int innerPadding, int textPadding, bool interactable = true) 
        : base(position, size, text, fontSize, textSpacing, borderWidth, padding, textAlignment, 
            lineColor, backgroundColor, 
            normalColor, focusedColor, pressedColor, disabledColor, 
            textNormalColor, textFocusedColor, textPressedColor, textDisabledColor, 
            borderNormalColor, borderFocusedColor, borderPressedColor, borderDisabledColor, 
            interactable)
    {
        TextPadding = textPadding;
        InnerPadding = innerPadding;
    }

    public int TextPadding { get; set; }
    public int InnerPadding { get; set; }
}
