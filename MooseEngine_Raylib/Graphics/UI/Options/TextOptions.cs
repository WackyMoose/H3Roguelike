namespace MooseEngine.Graphics.UI.Options;

public class TextOptions : UIOptionsBase
{
    public TextOptions(UIScreenCoords position, UIScreenCoords size, int fontSize, bool interactable = true)
		: base(position, size, interactable)
    {
        FontSize = fontSize;
		TextSpacing = 1.0f;
    }

    public int FontSize { get; set; }
	public float TextSpacing { get; set; }
}
