using Raylib_cs;

namespace MooseEngine.Core;

public interface IWindow
{
    int Width { get; }
    int Height { get; }
    bool IsRunning { get; }

    void Initialize();
    void Shutdown();
}

internal class Window : IWindow
{
    private WindowSpecification _specification;


    public Window(WindowSpecification specification)
    {
        _specification = specification;
    }

    public int Width => _specification.Width;
    public int Height => _specification.Height; 
    public bool IsRunning => !Raylib.WindowShouldClose();

    public void Initialize()
    {
        Raylib.InitWindow(_specification.Width, _specification.Height, _specification.Title);
    }

    public void Shutdown()
    {
        Raylib.CloseWindow();
    }
}
