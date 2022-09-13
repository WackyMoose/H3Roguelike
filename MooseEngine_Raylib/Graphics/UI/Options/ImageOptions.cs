using MooseEngine.Utilities;

namespace MooseEngine.Graphics.UI.Options;

public class ImageOptions : UIOptionsBase
{
    public ImageOptions(UIScreenCoords position, UIScreenCoords size, Coords2D coords, int spriteSize, Raylib_cs.Texture2D image)
        : base(position, size, false)
    {
        Coords = coords;
        SpriteSize = spriteSize;
        Image = image;
    }

    public int SpriteSize { get; set; }
    public Coords2D Coords { get; set; }
    public Raylib_cs.Texture2D Image { get; set; }
}
