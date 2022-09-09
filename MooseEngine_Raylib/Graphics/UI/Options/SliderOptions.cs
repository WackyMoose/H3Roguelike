namespace MooseEngine.Graphics.UI.Options;

public class SliderOptions : TextOptions
{
    public SliderOptions(bool interactable = true)
		: this(UIScreenCoords.Zero, UIScreenCoords.One, 24, string.Empty, TextAlignment.None, 0, 100, new Color(151, 232, 255, 255), interactable)
    {
	}

	public SliderOptions(UIScreenCoords position, UIScreenCoords size, int fontSize, string text, TextAlignment textAlignment, int minValue, int maxValue, bool interactable = true)
	: this(position, size, fontSize, text, textAlignment, minValue, maxValue, new Color(151, 232, 255, 255), interactable)
	{
	}

	public SliderOptions(UIScreenCoords position, UIScreenCoords size, int fontSize, string text, TextAlignment textAlignment, int minValue, int maxValue, Color color, bool interactable = true)
		: base(position, size, text, fontSize, interactable)
	{
		TextAlignment = textAlignment;
		BorderWidth = 0;
		Padding = 1;
		TextPadding = 2;

		MaxValue = maxValue;
		MinValue = minValue;
		Color = color;
	}

    public int TextPadding { get; set; }
    public int MaxValue { get; set; }
    public int MinValue { get; set; }

    public Color Color { get; set; }
}

[Obsolete]
public class SliderOptionsDep
{
	public int BorderWidth { get; set; }
	public int Padding { get; set; }
	public int Width { get; set; }
	public int TextPadding { get; set; }

	public SliderOptionsDep()
	{
		BorderWidth = 0;
		Padding = 1;
		Width = 1;
		TextPadding = 2;
	}
}
