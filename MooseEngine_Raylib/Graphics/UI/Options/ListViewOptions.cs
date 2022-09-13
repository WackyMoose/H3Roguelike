using MooseEngine.Utilities;

namespace MooseEngine.Graphics.UI.Options;

public class ListViewOptions : ScrollbarOptions
{
    protected readonly static int DEFAULT_ITEM_SPACING = 2;
    protected readonly static int DEFAULT_ITEM_HEIGHT = 24;
    protected readonly static ScrollbarSide DEFAULT_SCROLL_BAR_SIDE = ScrollbarSide.Right;

    public ListViewOptions(UIScreenCoords position, UIScreenCoords size, string text, bool interactable = true)
        : this(position, size, text, Constants.DEFAULT_FONT_SIZE, DEFAULT_TEXT_SPACING, DEFAULT_BORDER_WIDTH, DEFAULT_PADDING, TextAlignment.Left, 
              DEFAULT_LINE_COLOR, DEFAULT_BACKGROUND_COLOR,
              DEFAULT_NORMAL_COLOR, DEFAULT_FOCUSED_COLOR, DEFAULT_PRESSED_COLOR, DEFAULT_DISABLED_COLOR,
              DEFAULT_TEXT_NORMAL_COLOR, DEFAULT_TEXT_FOCUSED_COLOR, DEFAULT_TEXT_PRESSED_COLOR, DEFAULT_TEXT_DISABLED_COLOR,
              DEFAULT_BORDER_NORMAL_COLOR, DEFAULT_BORDER_FOCUSED_COLOR, DEFAULT_BORDER_PRESSED_COLOR, DEFAULT_BORDER_DISABLED_COLOR, 
              DEFAULT_STATUS_BAR_HEIGHT, HEADER_NORMAL_COLOR, HEADER_DISABLED_COLOR, DEFAULT_TEXT_NORMAL_COLOR, DEFAULT_TEXT_DISABLED_COLOR, 
              DEFAULT_ARROWS_VISIBLE, DEFAULT_ARROWS_SIZE, DEFAULT_SCROLL_SLIDER_PADDING, DEFAULT_SCROLL_SLIDER_SIZE, DEFAULT_SCROLL_PADDING, DEFAULT_SCROLL_SPEED, interactable)
    {
    }

    public ListViewOptions(UIScreenCoords position, UIScreenCoords size, string text, Color headerNormalColor, Color headerTextColor, Color textColor, Color borderColor, Color backgroundColor, bool interactable = true)
        : this(position, size, text, Constants.DEFAULT_FONT_SIZE, DEFAULT_TEXT_SPACING, DEFAULT_BORDER_WIDTH, DEFAULT_PADDING, TextAlignment.Left,
              DEFAULT_LINE_COLOR, backgroundColor,
              DEFAULT_NORMAL_COLOR, DEFAULT_FOCUSED_COLOR, DEFAULT_PRESSED_COLOR, DEFAULT_DISABLED_COLOR,
              textColor, DEFAULT_TEXT_FOCUSED_COLOR, DEFAULT_TEXT_PRESSED_COLOR, DEFAULT_TEXT_DISABLED_COLOR,
              borderColor, DEFAULT_BORDER_FOCUSED_COLOR, DEFAULT_BORDER_PRESSED_COLOR, DEFAULT_BORDER_DISABLED_COLOR,
              DEFAULT_STATUS_BAR_HEIGHT, headerNormalColor, HEADER_DISABLED_COLOR, headerTextColor, DEFAULT_TEXT_DISABLED_COLOR,
              DEFAULT_ARROWS_VISIBLE, DEFAULT_ARROWS_SIZE, DEFAULT_SCROLL_SLIDER_PADDING, DEFAULT_SCROLL_SLIDER_SIZE, DEFAULT_SCROLL_PADDING, DEFAULT_SCROLL_SPEED,
              DEFAULT_ITEM_SPACING, DEFAULT_ITEM_HEIGHT, DEFAULT_SCROLL_BAR_SIDE, interactable)
    {
    }

    public ListViewOptions(UIScreenCoords position, UIScreenCoords size, string text, int fontSize, float textSpacing, int borderWidth, int padding, TextAlignment textAlignment,
        Color lineColor, Color backgroundColor,
        Color normalColor, Color focusedColor, Color pressedColor, Color disabledColor,
        Color textNormalColor, Color textFocusedColor, Color textPressedColor, Color textDisabledColor,
        Color borderNormalColor, Color borderFocusedColor, Color borderPressedColor, Color borderDisabledColor,
        int statusBarHeight, Color headerNormalColor, Color headerDisabledColor, Color headerTextNormalColor, Color headerTextDisabledColor,
        bool arrowsVisible, int arrowsSize, int scrollSliderPadding, int scrollSliderSize, int scrollPadding, int scrollSpeed, bool interactable = true)
        : this(position, size, text, fontSize, textSpacing, borderWidth, padding, textAlignment,
            lineColor, backgroundColor,
            normalColor, focusedColor, pressedColor, disabledColor,
            textNormalColor, textFocusedColor, textPressedColor, textDisabledColor,
            borderNormalColor, borderFocusedColor, borderPressedColor, borderDisabledColor,
            statusBarHeight, headerNormalColor, headerDisabledColor, headerTextNormalColor, headerTextDisabledColor,
            arrowsVisible, arrowsSize, scrollSliderPadding, scrollSliderSize, scrollPadding, scrollSpeed, 
            DEFAULT_ITEM_SPACING, DEFAULT_ITEM_HEIGHT, DEFAULT_SCROLL_BAR_SIDE, interactable)
    {
    }

    public ListViewOptions(UIScreenCoords position, UIScreenCoords size, string text, int fontSize, float textSpacing, int borderWidth, int padding, TextAlignment textAlignment, 
        Color lineColor, Color backgroundColor, 
        Color normalColor, Color focusedColor, Color pressedColor, Color disabledColor, 
        Color textNormalColor, Color textFocusedColor, Color textPressedColor, Color textDisabledColor, 
        Color borderNormalColor, Color borderFocusedColor, Color borderPressedColor, Color borderDisabledColor, 
        int statusBarHeight, Color headerNormalColor, Color headerDisabledColor, Color headerTextNormalColor, Color headerTextDisabledColor,
        bool arrowsVisible, int arrowsSize, int scrollSliderPadding, int scrollSliderSize, int scrollPadding, int scrollSpeed,
        int itemSpacing, int itemHeight, ScrollbarSide scrollbarSide, bool interactable = true) 
        : base(position, size, text, fontSize, textSpacing, borderWidth, padding, textAlignment, 
            lineColor, backgroundColor, 
            normalColor, focusedColor, pressedColor, disabledColor, 
            textNormalColor, textFocusedColor, textPressedColor, textDisabledColor, 
            borderNormalColor, borderFocusedColor, borderPressedColor, borderDisabledColor, 
            statusBarHeight, headerNormalColor, headerDisabledColor, headerTextNormalColor, headerTextDisabledColor,
            arrowsVisible, arrowsSize, scrollSliderPadding, scrollSliderSize, scrollPadding, scrollSpeed, interactable)
    {
        ItemSpacing = itemSpacing;
        ItemHeight = itemHeight;
        ScrollbarSide = scrollbarSide;
    }

    public int ItemSpacing { get; set; }
    public int ItemHeight { get; set; }
    public ScrollbarSide ScrollbarSide { get; set; }
}

[Obsolete]
public class ListViewOptionsDep
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

    public ListViewOptionsDep()
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
