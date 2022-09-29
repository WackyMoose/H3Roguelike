namespace MooseEngine.Graphics.UI.Options;

public class ScrollbarOptions : TextOptions
{
    protected readonly static bool DEFAULT_ARROWS_VISIBLE = false;
    protected readonly static int DEFAULT_ARROWS_SIZE = 6;
    protected readonly static int DEFAULT_SCROLL_SLIDER_PADDING = 0;
    protected readonly static int DEFAULT_SCROLL_SLIDER_SIZE = 16;
    protected readonly static int DEFAULT_SCROLL_PADDING = 0;
    protected readonly static int DEFAULT_SCROLL_SPEED = 12;
    protected readonly static int DEFAULT_SCROLL_BAR_WIDTH = 12;

    public ScrollbarOptions(UIScreenCoords position, UIScreenCoords size, string text, int fontSize, float textSpacing, int borderWidth, int padding, TextAlignment textAlignment, 
        Color lineColor, Color backgroundColor, 
        Color normalColor, Color focusedColor, Color pressedColor, Color disabledColor, 
        Color textNormalColor, Color textFocusedColor, Color textPressedColor, Color textDisabledColor, 
        Color borderNormalColor, Color borderFocusedColor, Color borderPressedColor, Color borderDisabledColor,
        bool arrowsVisible, int arrowsSize, int scrollSliderPadding, int scrollSliderSize, int scrollPadding, int scrollSpeed, bool interactable = true) 
        : base(position, size, text, fontSize, textSpacing, borderWidth, padding, textAlignment, 
            lineColor, backgroundColor, 
            normalColor, focusedColor, pressedColor, disabledColor, 
            textNormalColor, textFocusedColor, textPressedColor, textDisabledColor, 
            borderNormalColor, borderFocusedColor, borderPressedColor, borderDisabledColor,
            interactable)
    {
        ArrowsVisible = arrowsVisible;
        ArrowsSize = arrowsSize;
        ScrollSliderPadding = scrollSliderPadding;
        ScrollSliderSize = scrollSliderSize;
        ScrollPadding = scrollPadding;
        ScrollSpeed = scrollSpeed;
    }

    public bool ArrowsVisible { get; set; }
    public int ArrowsSize { get; set; }
    public int ScrollSliderPadding { get; set; }
    public int ScrollSliderSize { get; set; }
    public int ScrollPadding { get; set; }
    public int ScrollSpeed { get; set; }
    public int ScrollbarWidth { get; set; }
}
