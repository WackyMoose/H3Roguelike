using MooseEngine.Core;
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
    public IDictionary<Vector2, IEntity>? Tiles { get { return _entities; } }

    public IEntity? EntityAtPosition(IDictionary<Vector2, IEntity> Tiles, Vector2 position)
    {
        // TODO: Performance check on lambda vs LINQ vs long-hand for loop.
        return (IEntity?)Tiles.FirstOrDefault(k => Tiles.ContainsKey(k.Value.Position)).Value;
    }

    public IDictionary<Vector2, IEntity>? GetEntitiesWithinRange(IDictionary<Vector2, IEntity> Tiles, Coords2D position, int distance)
    {
        int distanceSquared = distance * distance;

        Dictionary<Vector2, IEntity> TilesWithinDist = new Dictionary<Vector2, IEntity>();

        var topLft = new Vector2(position.X - distance, position.Y - distance);
        var btmRgt = new Vector2(position.X + distance, position.Y + distance);
        var v = new Vector2();

        for (v.Y = topLft.Y; v.Y <= btmRgt.Y; v.Y += Constants.DEFAULT_ENTITY_SIZE)
        {
            for (v.X = topLft.X; v.X <= btmRgt.X; v.X += Constants.DEFAULT_ENTITY_SIZE)
            {
                if (Tiles.ContainsKey(v))
                {
                    if (MathFunctions.DistanceSquaredBetween(position, v) <= distanceSquared)
                    {
                        TilesWithinDist.Add(v, Tiles[v]);
                    }
                }

            }
        }

        return TilesWithinDist;
    }

    public IEnumerable<TEntity>? GetEntitiesOfType<TEntity>(IDictionary<Vector2, IEntity> Tiles )
    {
        // TODO
        return (IEnumerable<TEntity>?)Tiles.OfType<TEntity>().ToList();
    }

    public IEnumerable<TEntity>? GetEntitiesOfType<TEntity>()
    {
        // TODO
        return (IEnumerable<TEntity>?)_entities.OfType<TEntity>().ToList();
    }

    protected override void DisposeManagedState()
    {
        _entities.Clear();
        _entities.GetEnumerator().Dispose();
    }

    public void UpdateRuntime(float deltaTime)
    {
        SceneCamera.Update(deltaTime);

        if (SceneCamera != default)
        {
            Renderer.Begin(SceneCamera);

            var defaultTint = new Color(128 - 64, 128, 128 + 64, 255);
            var windowSize = new Vector2((int)(Application.Instance.Window.Width * 0.5 - Application.Instance.Window.Width * 0.5 % Constants.DEFAULT_ENTITY_SIZE), (int)(Application.Instance.Window.Height * 0.5 - Application.Instance.Window.Height * 0.5 % Constants.DEFAULT_ENTITY_SIZE) );
            var topLft = new Vector2(SceneCamera.RaylibCamera.target.X - windowSize.X, SceneCamera.RaylibCamera.target.Y - windowSize.Y);
            var btmRgt = new Vector2(SceneCamera.RaylibCamera.target.X + windowSize.X, SceneCamera.RaylibCamera.target.Y + windowSize.Y);
            var v = new Vector2();

            for (v.Y = topLft.Y; v.Y <= btmRgt.Y; v.Y += Constants.DEFAULT_ENTITY_SIZE)
            {
                for (v.X = topLft.X; v.X <= btmRgt.X; v.X += Constants.DEFAULT_ENTITY_SIZE)
                {
                    if (_entities.ContainsKey(v))
                    {
                        Renderer.Render(_entities[v], _defaultEntitySize);
                        _entities[v].ColorTint = defaultTint;
                    }
                }
            }

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
