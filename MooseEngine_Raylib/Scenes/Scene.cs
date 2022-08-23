using MooseEngine.Extensions.Runtime;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Utilities;
using System.Collections.Generic;
using System.Numerics;

namespace MooseEngine.Scenes;

public interface IScene : IDisposable
{
    void UpdateRuntime(float deltaTime);
    void Add(Entity entity);
    void Remove(Entity entity);
    List<Entity>? Entities { get; }
    List<Entity>? EntitiesAtPosition(List<Entity> entities, Vector2 position);
    List<Entity>? EntitiesWithinDistanceOfPosition(List<Entity> entities, Vector2 position, int distance);
    List<Entity>? EntitiesWithType(List<Entity> entities, Type type);
}

internal class Scene : Disposeable, IScene
{
    private readonly List<Entity> _entities;
    private readonly float _defaultEntitySize;

    public Scene(IRenderer renderer, float defaultEntitySize = Constants.DEFAULT_ENTITY_SIZE)
    {
        _entities = new List<Entity>();
        Renderer = renderer;
        _defaultEntitySize = defaultEntitySize;
    }

    public IRenderer Renderer { get; }

    public List<Entity>? Entities { get { return _entities; } }

    public List<Entity>? EntitiesAtPosition(List<Entity> entities, Vector2 position)
    {
        return entities.Where(x => x.Position == position).ToList();
    }

    public List<Entity>? EntitiesWithinDistanceOfPosition(List<Entity> entities, Vector2 position, int distance)
    {
        return entities.Where(x => MathFunctions.DistanceBetween(position, x.Position) <= distance).ToList();
    }

    public List<Entity>? EntitiesWithType(List<Entity> entities, Type type)
    {
        return entities.Where(x => x.GetType() == type).ToList();
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

        var sceneCamera = GetSceneCamera();
        if (sceneCamera != default)
        {
            Renderer.Begin(sceneCamera);
            for (int i = _entities.Count - 1; i >= 0; i--)
            {
                var entity = _entities[i];
                Renderer.Render(entity, _defaultEntitySize);
            }
            Renderer.End();
        }
    }

    private ISceneCamera GetSceneCamera()
    {
        var sceneCamera = _entities.Where(x => x is ISceneCamera).SingleOrDefault();
        return (ISceneCamera)sceneCamera!;
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
