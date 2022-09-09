using MooseEngine.Utilities;

namespace MooseEngine.Graphics.UI.Options;

public class PanelOptions : TextOptions
{
    public PanelOptions(UIScreenCoords position, UIScreenCoords size, string title)
        : this(position, size, title, 24, false)
    {
    }

    public PanelOptions(UIScreenCoords position, UIScreenCoords size, string title, bool interactable)
        : this(position, size, title, 24, interactable)
    {
    }

    public PanelOptions(UIScreenCoords position, UIScreenCoords size, string title, int statusBarHeight, bool interactable) 
        : base(position, size, title, Constants.DEFAULT_FONT_SIZE, 1, interactable)
    {
        StatusBarHeight = statusBarHeight;
    }

    public int StatusBarHeight { get; set; }
}
