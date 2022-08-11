using MooseEngine.Core;
using MooseEngine.Extensions.Runtime;

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

        Renderer.Begin();
        for (int i = _entities.Count - 1; i >= 0; i--)
        {
            var entity = _entities[i];
            Renderer.RenderEntity(entity);
        }
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
