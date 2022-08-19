namespace MooseEngine.Core;

public struct WindowSpecification
{
    public string Title { get; init; }
    public int Width { get; init; }
    public int Height { get; init; }

    public WindowSpecification()
        : this("MooseEngine Raylib", 1600, 900)
    {
    }

    public WindowSpecification(string title, int width, int height)
    {
        Title = title;
        Width = width;
        Height = height;
    }
}
