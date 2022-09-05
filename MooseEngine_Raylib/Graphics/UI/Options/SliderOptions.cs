namespace MooseEngine.Graphics.UI.Options;

public class SliderOptions
{
	public int BorderWidth { get; set; }
	public int Padding { get; set; }
	public int Width { get; set; }
	public int TextPadding { get; set; }

	public SliderOptions()
	{
		BorderWidth = 0;
		Padding = 1;
		Width = 1;
		TextPadding = 2;
	}
}
