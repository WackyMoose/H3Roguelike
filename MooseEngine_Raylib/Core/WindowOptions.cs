namespace MooseEngine.Core;

public struct WindowOptions
{
    public string Title { get; init; }
    public int Width { get; init; }
    public int Height { get; init; }

    public WindowOptions()
        : this("MooseEngine Raylib", 1600, 900)
    {
    }

    public WindowOptions(string title, int width, int height)
    {
        Title = title;
        Width = width;
        Height = height;
    }
}
