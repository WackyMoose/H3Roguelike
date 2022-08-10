namespace MooseEngine.Core;

public struct ApplicationSpecification
{
    public string Name { get; init; }
    public int WindowWidth { get; init; }
    public int WindowHeight { get; init; }

    public ApplicationSpecification()
        : this("MooseEngine Raylib", 1600, 900)
    {
    }

    public ApplicationSpecification(string name, int width, int height)
    {
        Name = name;
        WindowWidth = width;
        WindowHeight = height;
    }
}
