namespace MooseEngine.Graphics.UI.Options;

public struct UIScreenCoords
{
    public UIScreenCoords()
    {
		X = 0;
		Y = 0;
    }

	public UIScreenCoords(int x, int y)
	{
		X = x;
		Y = y;
	}

	public int X { get; set; }
    public int Y { get; set; }

	public static readonly UIScreenCoords Zero = new UIScreenCoords(0, 0);
	public static readonly UIScreenCoords One = new UIScreenCoords(1, 1);
}

public enum TextAlignmentSlider
{
	None = 0,
	Left = 1,
	Right = 2
}

public class SliderOptions : TextOptions
{
    public string Text { get; set; }
    public TextAlignmentSlider TextAlignment { get; set; }
    public int BorderWidth { get; set; }
    public int Padding { get; set; }
	public int TextPadding { get; set; }

	public int MaxValue { get; set; }
	public int MinValue { get; set; }
	public int Value { get; set; }

    public SliderOptions(bool interactable = true)
		: this(UIScreenCoords.Zero, UIScreenCoords.One, 24, string.Empty, TextAlignmentSlider.None, 0, 100, interactable)
    {
	}

    public SliderOptions(UIScreenCoords position, UIScreenCoords size, int fontSize, string text, TextAlignmentSlider textAlignment, int minValue, int maxValue, bool interactable = true)
		: base(position, size, fontSize, interactable)
	{
		Text = text;
		TextAlignment = textAlignment;
		BorderWidth = 0;
		Padding = 1;
		TextPadding = 2;

		MaxValue = maxValue;
		MinValue = minValue;
		Value = 0;
	}
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
