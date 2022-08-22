using Raylib_cs;

namespace MooseEngine.Graphics;

public interface IRendererSpritesheetOptions
{
    string? SpritesheetPath { get; set; }
    int SpriteSize { get; set; }
    int Offset { get; set; }
    int Padding { get; set; }
}
