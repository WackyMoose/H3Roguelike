using MooseEngine.Scenes;

namespace MooseEngine.Core;

public interface IGame
{
    Scene Scene { get; }

    public void Initialize();
    public void Uninitialize();
    public void Update(float deltaTime);
}
