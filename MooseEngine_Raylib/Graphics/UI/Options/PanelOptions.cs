namespace MooseEngine.Graphics.UI.Options;

public class PanelOptions : TextOptions
{
    public PanelOptions(UIScreenCoords position, UIScreenCoords size, string title) 
        : base(position, size, false)
    {
        StatusBarHeight = 24;
        BorderWidth = 1;
        Title = title;
    }

    public string Title { get; set; }
    public int StatusBarHeight { get; set; }
}
