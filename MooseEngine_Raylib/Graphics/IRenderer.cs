using MooseEngine.Interfaces;
using MooseEngine.Scenes;

namespace MooseEngine.Graphics;

public interface IRenderer
{
    void Initialize();
    void Shutdown();

    void BeginFrame();
    void EndFrame();

    void BeginScene(ISceneCamera sceneCamera);
    void EndScene();

    void Render(IEntity entity, float scale);
}
