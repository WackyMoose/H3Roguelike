namespace MooseEngine.Graphics.UI.Options;

public class LabelOptions : TextOptions
{
    //public LabelOptions(UIScreenCoords position, string text)
    //    : this(position, 24, text)
    //{
    //}

    //public LabelOptions(UIScreenCoords position, int fontSize) 
    //    : this(position, fontSize, string.Empty)
    //{
    //}

    //public LabelOptions(UIScreenCoords position, int fontSize, string text)
    //: base(position, UIScreenCoords.Zero, text, fontSize, false)
    //{
    //}

    public LabelOptions(UIScreenCoords position, string text, int fontSize, Color textNormalColor)
        : this(position, UIScreenCoords.Zero, text, fontSize, DEFAULT_TEXT_SPACING, DEFAULT_BORDER_WIDTH, DEFAULT_PADDING, TextAlignment.Left, 
              DEFAULT_LINE_COLOR, DEFAULT_BACKGROUND_COLOR, 
              DEFAULT_NORMAL_COLOR, DEFAULT_FOCUSED_COLOR, DEFAULT_PRESSED_COLOR, DEFAULT_DISABLED_COLOR,
              textNormalColor, DEFAULT_TEXT_FOCUSED_COLOR, DEFAULT_TEXT_PRESSED_COLOR, DEFAULT_TEXT_DISABLED_COLOR,
              DEFAULT_BORDER_NORMAL_COLOR, DEFAULT_BORDER_FOCUSED_COLOR, DEFAULT_BORDER_PRESSED_COLOR, DEFAULT_BORDER_DISABLED_COLOR, false)
    {
    }

    public LabelOptions(UIScreenCoords position, UIScreenCoords size, string text, int fontSize, float textSpacing, int borderWidth, int padding, TextAlignment textAlignment, 
        Color lineColor, Color backgroundColor, 
        Color normalColor, Color focusedColor, Color pressedColor, Color disabledColor, 
        Color textNormalColor, Color textFocusedColor, Color textPressedColor, Color textDisabledColor, 
        Color borderNormalColor, Color borderFocusedColor, Color borderPressedColor, Color borderDisabledColor, 
        bool interactable = true) 
        : base(position, size, text, fontSize, textSpacing, borderWidth, padding, textAlignment, 
            lineColor, backgroundColor, 
            normalColor, focusedColor, pressedColor, disabledColor, 
            textNormalColor, textFocusedColor, textPressedColor, textDisabledColor, 
            borderNormalColor, borderFocusedColor, borderPressedColor, borderDisabledColor, 
            interactable)
    {
    }
}
