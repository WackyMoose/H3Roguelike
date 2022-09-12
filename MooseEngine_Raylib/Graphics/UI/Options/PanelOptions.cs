using MooseEngine.Utilities;

namespace MooseEngine.Graphics.UI.Options;

public class PanelOptions : TextOptions
{
    public PanelOptions(UIScreenCoords position, UIScreenCoords size, string title)
        : this(position, size, title, 1, 24, false)
    {
    }

    public PanelOptions(UIScreenCoords position, UIScreenCoords size, string title, bool interactable)
        : this(position, size, title, 1, 24, interactable)
    {
    }

    public PanelOptions(UIScreenCoords position, UIScreenCoords size, string title, int borderWidth, bool interactable)
        : this(position, size, title, borderWidth, 24, interactable)
    {
    }

    public PanelOptions(UIScreenCoords position, UIScreenCoords size, string title, int borderWidth, int statusBarHeight, bool interactable)
        : base(position, size, title, Constants.DEFAULT_FONT_SIZE, borderWidth, interactable)
    {
        StatusBarHeight = statusBarHeight;
    }

    public int StatusBarHeight { get; set; }
}
