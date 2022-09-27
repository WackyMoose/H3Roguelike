using MooseEngine.Utilities;

namespace MooseEngine.Graphics.UI.Options;

public class ImageOptions : UIOptionsBase
{
    public ImageOptions(UIScreenCoords position, UIScreenCoords size, Raylib_cs.Texture2D image)
        : this(position, size, image, Color.White)
    {
        Image = image;
    }

    public ImageOptions(UIScreenCoords position, UIScreenCoords size, Raylib_cs.Texture2D image, Color tintColor)
        : base(position, size, false)
    {
        Position = position;
        Size = size;
        Image = image;
        TintColor = tintColor;
    }

    public Raylib_cs.Texture2D Image { get; set; }
    public Color TintColor { get; }
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
