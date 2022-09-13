namespace MooseEngine.Graphics.UI.Options;

public abstract class UIOptionsBase
{
	protected readonly static Color DEFAULT_LINE_COLOR = new Color(144, 171, 181, 255);
	protected readonly static Color DEFAULT_BACKGROUND_COLOR = new Color(245, 245, 245, 255);

	protected readonly static Color DEFAULT_NORMAL_COLOR = new Color(201, 201, 201, 255);
	protected readonly static Color DEFAULT_FOCUSED_COLOR = new Color(201, 239, 254, 255);
	protected readonly static Color DEFAULT_PRESSED_COLOR = new Color(151, 232, 255, 255);
	protected readonly static Color DEFAULT_DISABLED_COLOR = new Color(230, 233, 233, 255);

	protected readonly static Color DEFAULT_BORDER_NORMAL_COLOR = new Color(131, 131, 131, 255);
	protected readonly static Color DEFAULT_BORDER_FOCUSED_COLOR = new Color(91, 178, 217, 255);
	protected readonly static Color DEFAULT_BORDER_PRESSED_COLOR = new Color(4, 146, 199, 255);
	protected readonly static Color DEFAULT_BORDER_DISABLED_COLOR = new Color(181, 193, 194, 255);

	protected readonly static int DEFAULT_BORDER_WIDTH = 2;

    public UIOptionsBase(bool interactable)
        : this(UIScreenCoords.Zero, UIScreenCoords.One, DEFAULT_BORDER_WIDTH, interactable)
    {
    }

	public UIOptionsBase(UIScreenCoords position, UIScreenCoords size, bool interactable)
	: this(position, size,
		  DEFAULT_LINE_COLOR, DEFAULT_BACKGROUND_COLOR,
		  DEFAULT_NORMAL_COLOR, DEFAULT_FOCUSED_COLOR, DEFAULT_PRESSED_COLOR, DEFAULT_DISABLED_COLOR,
		  DEFAULT_BORDER_NORMAL_COLOR, DEFAULT_BORDER_FOCUSED_COLOR, DEFAULT_BORDER_PRESSED_COLOR, DEFAULT_BORDER_DISABLED_COLOR,
		  DEFAULT_BORDER_WIDTH, interactable)
	{
	}

	public UIOptionsBase(UIScreenCoords position, UIScreenCoords size, int borderWidth, bool interactable)
		: this(position, size, 
			  DEFAULT_LINE_COLOR, DEFAULT_BACKGROUND_COLOR, 
			  DEFAULT_NORMAL_COLOR, DEFAULT_FOCUSED_COLOR, DEFAULT_PRESSED_COLOR, DEFAULT_DISABLED_COLOR,
			  DEFAULT_BORDER_NORMAL_COLOR, DEFAULT_BORDER_FOCUSED_COLOR, DEFAULT_BORDER_PRESSED_COLOR, DEFAULT_BORDER_DISABLED_COLOR, 
			  borderWidth, interactable)
    {
    }

    public UIOptionsBase(UIScreenCoords position, UIScreenCoords size, 
		Color lineColor, Color backgroundColor, 
		Color normalColor, Color focusedColor, Color pressedColor, Color disabledColor,
		Color borderNormalColor, Color borderFocusedColor, Color borderPressedColor, Color borderDisabledColor,
		int borderWidth, bool interactable)
    {
		Position = position;
		Size = size;
		LineColor = lineColor;
		BackgroundColor = backgroundColor;

		NormalColor = normalColor;
		FocusedColor = focusedColor;
		PressedColor = pressedColor;
		DisabledColor = disabledColor;

		BorderNormalColor = borderNormalColor;
		BorderFocusedColor = borderFocusedColor;
		BorderPressedColor = borderPressedColor;
		BorderDisabledColor = borderDisabledColor;

		BorderWidth = borderWidth;
		Interactable = interactable;
	}

	public UIScreenCoords Position { get; set; }
    public UIScreenCoords Size { get; set; }
	public bool Interactable { get; set; }
	public int BorderWidth { get; set; }

	public Color LineColor { get; set; }
	public Color BackgroundColor { get; set; }

	public Color NormalColor { get; set; }
	public Color FocusedColor { get; set; }
	public Color PressedColor { get; set; }
	public Color DisabledColor { get; set; }

	public Color BorderNormalColor { get; set; }
	public Color BorderFocusedColor { get; set; }
	public Color BorderPressedColor { get; set; }
	public Color BorderDisabledColor { get; set; }
}
