namespace MooseEngine.Graphics.UI.Options;

public class SeperatorOptions : UIOptionsBase
{
    public SeperatorOptions(UIScreenCoords position, int width, int height = 5) 
        : base(position, new UIScreenCoords(width, height), false)
    {
        
    }
}
