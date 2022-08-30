namespace MooseEngine.Core;

public struct ApplicationOptions
{
    public string Name { get; init; }
    public int WindowWidth { get; init; }
    public int WindowHeight { get; init; }

    public ApplicationOptions()
        : this("MooseEngine Raylib", 1600, 900)
    {
    }

    public ApplicationOptions(string name, int width, int height)
    {
        Name = name;
        WindowWidth = width;
        WindowHeight = height;
    }

    public static implicit operator WindowOptions(ApplicationOptions applicationSpecification)
    {
        return new WindowOptions
        {
            Title = applicationSpecification.Name,
            Width = applicationSpecification.WindowWidth,
            Height = applicationSpecification.WindowHeight
        };
    }
}
