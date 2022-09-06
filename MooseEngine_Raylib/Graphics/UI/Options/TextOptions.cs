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
    public TextOptions(UIScreenCoords position, UIScreenCoords size, bool interactable = true)
        : this(position, size, Constants.DEFAULT_FONT_SIZE, 1.0f, 2, 2, TextAlignment.Left, interactable)
    {
    }

    public TextOptions(UIScreenCoords position, UIScreenCoords size, int fontSize, bool interactable = true)
        : this(position, size, fontSize, 1.0f, 2, 2, TextAlignment.Left, interactable)
    {
    }

    public TextOptions(UIScreenCoords position, UIScreenCoords size, int fontSize, float textSpacing, int borderWidth, int padding, TextAlignment textAlignment, bool interactable = true)
		: base(position, size, interactable)
    {
        FontSize = fontSize;
		TextSpacing = textSpacing;
        BorderWidth = borderWidth;
        Padding = padding;
        TextAlignment = textAlignment;
    }

    public int FontSize { get; set; }
	public float TextSpacing { get; set; }
    public int BorderWidth { get; set; }
    public int Padding { get; set; }
    public TextAlignment TextAlignment { get; set; }
}
