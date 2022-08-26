using Raylib_cs;

namespace MooseEngine.Interfaces;

public interface ISceneCamera : IEntity
{
    Camera2D RaylibCamera { get; }
}
