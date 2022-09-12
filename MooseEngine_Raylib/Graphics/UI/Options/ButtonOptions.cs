using MooseEngine.Utilities;

namespace MooseEngine.Graphics.UI.Options;

public class ButtonOptions : TextOptions
{
    //public ButtonOptions(UIScreenCoords position, UIScreenCoords size, string text, bool interactable = true)
    //    : this(position, size, Constants.DEFAULT_FONT_SIZE, text, 1.0f, 2, 2, TextAlignment.Center, interactable)
    //{
    //}

    //public ButtonOptions(UIScreenCoords position, UIScreenCoords size, int fontSize, bool interactable = true)
    //    : this(position, size, fontSize, string.Empty, 1.0f, 2, 2, TextAlignment.Center, interactable)
    //{
    //}
    //public ButtonOptions(UIScreenCoords position, UIScreenCoords size, int fontSize, string text, float textSpacing, int borderWidth, int padding, TextAlignment textAlignment, bool interactable = true) 
    //    : base(position, size, text, fontSize, textSpacing, borderWidth, padding, textAlignment, interactable)
    //{
    //}
    public ButtonOptions(UIScreenCoords position, UIScreenCoords size, string text)
        : this(position, size, text, Constants.DEFAULT_FONT_SIZE, DEFAULT_TEXT_SPACING, DEFAULT_BORDER_WIDTH, DEFAULT_PADDING, TextAlignment.Center,
              DEFAULT_LINE_COLOR, DEFAULT_BACKGROUND_COLOR,
              DEFAULT_NORMAL_COLOR, DEFAULT_FOCUSED_COLOR, DEFAULT_PRESSED_COLOR, DEFAULT_DISABLED_COLOR,
              DEFAULT_TEXT_NORMAL_COLOR, DEFAULT_TEXT_FOCUSED_COLOR, DEFAULT_TEXT_PRESSED_COLOR, DEFAULT_TEXT_DISABLED_COLOR,
              DEFAULT_BORDER_NORMAL_COLOR, DEFAULT_BORDER_FOCUSED_COLOR, DEFAULT_BORDER_PRESSED_COLOR, DEFAULT_BORDER_DISABLED_COLOR, true)
    {
    }

    public ButtonOptions(UIScreenCoords position, UIScreenCoords size, string text, int fontSize, float textSpacing, int borderWidth, int padding, TextAlignment textAlignment, 
        Color lineColor, Color backgroundColor, 
        Color normalColor, Color focusedColor, Color pressedColor, Color disabledColor, 
        Color textNormalColor, Color textFocusedColor, Color textPressedColor, Color textDisabledColor, 
        Color borderNormalColor, Color borderFocusedColor, Color borderPressedColor, Color borderDisabledColor, 
        bool interactable) 
        : base(position, size, text, fontSize, textSpacing, borderWidth, padding, textAlignment, 
            lineColor, backgroundColor, 
            normalColor, focusedColor, pressedColor, disabledColor, 
            textNormalColor, textFocusedColor, textPressedColor, textDisabledColor, 
            borderNormalColor, borderFocusedColor, borderPressedColor, borderDisabledColor, 
            interactable)
    {
    }
}
