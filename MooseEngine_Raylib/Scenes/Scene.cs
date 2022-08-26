using MooseEngine.Extensions.Runtime;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Utilities;
using System.Collections.Generic;
using System.Numerics;

namespace MooseEngine.Scenes;

internal class Scene : Disposeable, IScene
{
    private IDictionary<Vector2, IEntity>? _entities;
    private readonly float _defaultEntitySize;
    private ISceneCamera _cameraEntity;

    public Scene(IRenderer renderer, float defaultEntitySize = Constants.DEFAULT_ENTITY_SIZE)
    {
        _entities = new Dictionary<Vector2, IEntity>();
        Renderer = renderer;
        _defaultEntitySize = defaultEntitySize;
    }

    public IRenderer Renderer { get; }
    public ISceneCamera SceneCamera { get { return _cameraEntity; } set { _cameraEntity = value; } }

    public IDictionary<Vector2, IEntity>? Entities { get { return _entities; } }
    
    public IEntity? EntityAtPosition(IDictionary<Vector2, IEntity> entities, Vector2 position)
    {
        // TODO: Performance check on lambda vs LINQ vs long-hand for loop.
        return (IEntity?)entities.FirstOrDefault(k => entities.ContainsKey(k.Value.Position)).Value;
    }

    public IEnumerable<IEntity>? EntitiesWithinDistanceOfEntity(IDictionary<Vector2, IEntity> entities, IEntity entity, int distance)
    {
        // TODO: Performance check on lambda vs LINQ vs long-hand for loop.
        int distanceSquared = distance * distance;
        return (IEnumerable<IEntity>?)entities.Where(x => MathFunctions.DistanceSquaredBetween(entity.Position, x.Key) <= distanceSquared ).ToList();
        //return entities.Where(x => MathFunctions.DistanceSquaredBetween(entity.Position, x.Position) <= distanceSquared && x != entity).ToList();
    }

    public IEnumerable<TEntity>? GetEntitiesOfType<TEntity>(IDictionary<Vector2, IEntity> entities )
    {
        return (IEnumerable<TEntity>?)entities.OfType<TEntity>().ToList();
    }

    public IEnumerable<TEntity>? GetEntitiesOfType<TEntity>()
    {
        return (IEnumerable<TEntity>?)_entities.OfType<TEntity>().ToList();
    }

    protected override void DisposeManagedState()
    {
        _entities.Clear();
        _entities.GetEnumerator().Dispose();
    }

    public void UpdateRuntime(float deltaTime)
    {
        //for (int i = _entities.Count - 1; i >= 0; i--)
        //{
        //    var entity = _entities.ElementAt(i);
        //    entity.Value.Update(deltaTime);
        //}

        //foreach (var entity in _entities)
        //{
        //    // do something with entry.Value or entry.Key
        //    entity.Value.Update(deltaTime);
        //}

        _entities.AsParallel().ForAll(e => e.Value.Update(deltaTime));

        SceneCamera.Update(deltaTime);

        if (SceneCamera != default)
        {
            Renderer.Begin(SceneCamera);
            //for (int i = _entities.Count - 1; i >= 0; i--)
            //{
            //    var entity = _entities.ElementAt(i);
            //    Renderer.Render(entity.Value, _defaultEntitySize);
            //}

            foreach (var entity in _entities)
            {
                Renderer.Render(entity.Value, _defaultEntitySize);
            }

            //_entities.AsParallel().ForAll(e => Renderer.Render(e.Value, _defaultEntitySize));

            Renderer.End();
        }
    }

    public void Add(IEntity entity)
    {
        _entities.TryAdd(entity.Position, entity);
    }

    public void Remove(IEntity entity)
    {
        _entities.Remove(entity.Position);
    }
}
