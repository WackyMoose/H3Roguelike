using MooseEngine.Core;
using MooseEngine.Extensions.Runtime;
using Raylib_cs;

namespace MooseEngine.Scenes;

public class Scene : Disposeable
{
    private readonly List<Entity> _entities;

    public Scene()
    {
        _entities = new List<Entity>();
    }

    protected override void DisposeManagedState()
    {
        _entities.Clear();
        _entities.GetEnumerator().Dispose();
    }

    public void UpdateRuntime(float deltaTime)
    {
        for (int i = _entities.Count - 1; i >= 0; i--)
        {
            var entity = _entities[i];
            entity.Update(deltaTime);
        }

        var camera = _entities.SingleOrDefault(x => x.GetType() == typeof(Camera));
        Throw.IfNull(camera, "Scene does not contain a Camera entity!");

        Renderer.Begin((Camera)camera!);

        for (int i = _entities.Count - 1; i >= 0; i--)
        {
            var entity = _entities[i];
            Renderer.RenderEntity(entity);
        }

        Raylib.EndMode2D();

        // Draw crosshair
        Raylib.DrawLine(0, Application.Instance.Window.Height / 2, Application.Instance.Window.Width, Application.Instance.Window.Height / 2, Color.GREEN);
        Raylib.DrawLine(Application.Instance.Window.Width / 2, 0, Application.Instance.Window.Width / 2, Application.Instance.Window.Height, Color.GREEN);

        Renderer.End();
    }

    public void Add(Entity entity)
    {
        _entities.Add(entity);
    }

    public void Remove(Entity entity)
    {
        _entities.Remove(entity);
    }
}
