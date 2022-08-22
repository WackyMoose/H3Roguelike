using Raylib_cs;

namespace MooseEngine.Graphics;

public interface IRaylibRendererOptions : IRendererOptions
{
    int TargetFPS { get; set; }
}

public class RaylibRendererOptions : IRaylibRendererOptions, IRendererSpritesheetOptions
{
    public Color ClearColor { get; set; }
    public int TargetFPS { get; set; }

    public string? SpritesheetPath { get; set; }
    public int SpriteSize { get; set; }
    public int Offset { get; set; }
    public int Padding { get; set; }
}
