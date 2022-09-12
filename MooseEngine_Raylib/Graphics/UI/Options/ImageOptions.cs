using MooseEngine.Utilities;

namespace MooseEngine.Graphics.UI.Options;

public class ImageOptions : UIOptionsBase
{
    public ImageOptions(UIScreenCoords position, UIScreenCoords size, Raylib_cs.Texture2D image)
        : base(position, size, false)
    {
        Image = image;
    }

    public Raylib_cs.Texture2D Image { get; set; }
}

public class SubImageOptions : ImageOptions
{
    public SubImageOptions(UIScreenCoords position, UIScreenCoords size, Coords2D coords, int spriteSize, Raylib_cs.Texture2D image)
        : base(position, size, image)
    {
        Coords = coords;
        SpriteSize = spriteSize;
    }

    public int SpriteSize { get; set; }
    public Coords2D Coords { get; set; }
}
