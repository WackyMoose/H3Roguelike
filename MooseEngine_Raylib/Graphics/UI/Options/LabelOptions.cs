namespace MooseEngine.Graphics.UI.Options;

public class LabelOptions : TextOptions
{
    public LabelOptions(UIScreenCoords position, string text)
        : this(position, 24, text)
    {
    }

    public LabelOptions(UIScreenCoords position, int fontSize) 
        : this(position, fontSize, string.Empty)
    {
    }

    public LabelOptions(UIScreenCoords position, int fontSize, string text)
    : base(position, UIScreenCoords.Zero, text, fontSize, false)
    {
    }
}
