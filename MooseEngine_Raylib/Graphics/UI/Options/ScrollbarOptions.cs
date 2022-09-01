namespace MooseEngine.Graphics.UI.Options;

public interface IInt
{
}

public class ScrollbarOptions
{
    public int BorderWidth { get; set; }
    public bool ArrowsVisible { get; set; }
    public int ArrowsSize { get; set; }
    public int ScrollSliderPadding { get; set; }
    public int ScrollSliderSize { get; set; }
    public int ScrollPadding { get; set; }
    public int ScrollSpeed { get; set; }

    public ScrollbarOptions()
    {
        BorderWidth = 0;
        ArrowsVisible = false;
        ArrowsSize = 6;
        ScrollSliderPadding = 0;
        ScrollSliderSize = 16;
        ScrollPadding = 0;
        ScrollSpeed = 12;
    }
}
