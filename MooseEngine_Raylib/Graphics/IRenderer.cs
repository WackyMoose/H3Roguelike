namespace MooseEngine.Graphics;

public interface IRenderer
{
    void Initialize();
    void Shutdown();

    void Begin(Interfaces.ISceneCamera sceneCamera);
    void End();

    void Render(Scenes.Entity entity);
}
