using MooseEngine.Core;
using MooseEngine.Extensions.Runtime;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;

namespace MooseEngine.Scenes;

public interface IScene : IDisposable
{
    void UpdateRuntime(float deltaTime);
    void Add(Entity entity);
    void Remove(Entity entity);
}

internal class Scene : Disposeable, IScene
{
    private readonly List<Entity> _entities;

    public Scene(IRenderer renderer)
    {
        _entities = new List<Entity>();
        Renderer = renderer;
    }

    public IRenderer Renderer { get; }

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
        if (camera != null)
        {
            Renderer.Begin((ISceneCamera)camera!);
            for (int i = _entities.Count - 1; i >= 0; i--)
            {
                var entity = _entities[i];
                Renderer.Render(entity);
            }
            Renderer.End();
        }
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
