using Raylib_cs;

namespace MooseEngine.Core;

public interface IWindowData
{
    string Title { get; }
    int Width { get; }
    int Height { get; }
}

public interface IWindow : IWindowData
{
    bool IsRunning { get; }

    void Initialize();
    void Shutdown();
}

internal class Window : IWindow
{
    private WindowOptions _specification;


    public Window(WindowOptions specification)
    {
        _specification = specification;
    }

    public string Title => _specification.Title;
    public int Width => _specification.Width;
    public int Height => _specification.Height;
    public bool IsRunning => !Raylib.WindowShouldClose();

    public void Initialize()
    {
        //Raylib.SetConfigFlags(ConfigFlags.FLAG_WINDOW_RESIZABLE);
        Raylib.InitWindow(_specification.Width, _specification.Height, _specification.Title);
    }

    public void Shutdown()
    {
        Raylib.CloseWindow();
    }
}
