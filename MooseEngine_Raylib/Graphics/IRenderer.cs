using MooseEngine.Interfaces;
using MooseEngine.Scenes;

namespace MooseEngine.Graphics;

public interface IRenderer
{
    void Initialize();
    void Shutdown();

    void Begin(ISceneCamera sceneCamera);
    void End();

    void Render(Entity entity);
}
