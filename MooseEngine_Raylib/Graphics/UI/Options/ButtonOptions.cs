using MooseEngine.Utilities;

namespace MooseEngine.Graphics.UI.Options;

public class ButtonOptions : TextOptions
{

    public ButtonOptions(UIScreenCoords position, UIScreenCoords size, string text, bool interactable = true)
        : this(position, size, Constants.DEFAULT_FONT_SIZE, text, 1.0f, 2, 2, TextAlignment.Center, interactable)
    {
    }

    public ButtonOptions(UIScreenCoords position, UIScreenCoords size, int fontSize, bool interactable = true)
        : this(position, size, fontSize, string.Empty, 1.0f, 2, 2, TextAlignment.Center, interactable)
    {
    }
    public ButtonOptions(UIScreenCoords position, UIScreenCoords size, int fontSize, string text, float textSpacing, int borderWidth, int padding, TextAlignment textAlignment, bool interactable = true) 
        : base(position, size, text, fontSize, textSpacing, borderWidth, padding, textAlignment, interactable)
    {
    }
}
