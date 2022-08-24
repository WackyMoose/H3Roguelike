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
    void Add(IEntity entity);
    void Remove(IEntity entity);
    IEnumerable<IEntity>? Entities { get; }
    IEnumerable<IEntity>? EntitiesAtPosition(IEnumerable<IEntity> entities, Vector2 position);
    IEnumerable<IEntity>? EntitiesWithinDistanceOfPosition(IEnumerable<IEntity> entities, Vector2 position, int distance);
    IEnumerable<IEntity>? EntitiesWithType(IEnumerable<IEntity> entities, Type type);
    IEnumerable<TEntity>? GetEntitiesOfType<TEntity>();
    IEnumerable<TEntity>? GetEntitiesOfType<TEntity>(IEnumerable<IEntity> entities);
}

internal class Scene : Disposeable, IScene
{
    private readonly List<IEntity> _entities;
    private readonly float _defaultEntitySize;

    public Scene(IRenderer renderer, float defaultEntitySize = Constants.DEFAULT_ENTITY_SIZE)
    {
        _entities = new List<IEntity>();
        Renderer = renderer;
        _defaultEntitySize = defaultEntitySize;
    }

    public IRenderer Renderer { get; }

    public IEnumerable<IEntity>? Entities { get { return _entities; } }

    public IEnumerable<IEntity>? EntitiesAtPosition(IEnumerable<IEntity> entities, Vector2 position)
    {
        return entities.Where(x => x.Position == position).ToList();
    }

    public IEnumerable<IEntity>? EntitiesWithinDistanceOfPosition(IEnumerable<IEntity> entities, Vector2 position, int distance)
    {
        return entities.Where(x => MathFunctions.DistanceBetween(position, x.Position) <= distance).ToList();
    }

    public IEnumerable<IEntity>? EntitiesWithType(IEnumerable<IEntity> entities, Type type)
    {
        return entities.Where(x => x.GetType() == type).ToList();
    }

    public IEnumerable<TEntity>? GetEntitiesOfType<TEntity>()
    {
        return (IEnumerable<TEntity>?)_entities.Where(x => x.GetType() == typeof(TEntity));
    }

    public IEnumerable<TEntity>? GetEntitiesOfType<TEntity>(IEnumerable<IEntity> entities)
    {
        return (IEnumerable<TEntity>?)entities.Where(x => x.GetType() == typeof(TEntity));
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

    public void Add(IEntity entity)
    {
        _entities.Add(entity);
    }

    public void Remove(IEntity entity)
    {
        _entities.Remove(entity);
    }
}
