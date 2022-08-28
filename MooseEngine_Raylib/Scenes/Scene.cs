using MooseEngine.Extensions.Runtime;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Utilities;
using System.Collections.Generic;
using System.Numerics;

namespace MooseEngine.Scenes;

internal class Scene : Disposeable, IScene
{
    private IDictionary<Vector2, IEntity>? _tiles;
    private IDictionary<Vector2, IEntity>? _creatures;
    private IDictionary<Vector2, IEntity>? _items;
    
    private readonly float _defaultEntitySize;
    private ISceneCamera _cameraEntity;

    public Scene(IRenderer renderer, float defaultEntitySize = Constants.DEFAULT_ENTITY_SIZE)
    {
        _tiles = new Dictionary<Vector2, IEntity>();
        _creatures = new Dictionary<Vector2, IEntity>();
        _items = new Dictionary<Vector2, IEntity>();
        
        Renderer = renderer;
        _defaultEntitySize = defaultEntitySize;
    }

    public IRenderer Renderer { get; }
    public ISceneCamera SceneCamera { get { return _cameraEntity; } set { _cameraEntity = value; } }
    public IDictionary<Vector2, IEntity>? Tiles { get { return _tiles; } }
    public IDictionary<Vector2, IEntity>? Creatures { get { return _creatures; } }
    public IDictionary<Vector2, IEntity>? Items { get { return _items; } }

    public IEntity? EntityAtPosition(IDictionary<Vector2, IEntity> Tiles, Vector2 position)
    {
        // TODO: Performance check on lambda vs LINQ vs long-hand for loop.
        return (IEntity?)Tiles.FirstOrDefault(k => Tiles.ContainsKey(k.Value.Position)).Value;
    }

    public IDictionary<Vector2, IEntity>? GetTilesWithinRange(IDictionary<Vector2, IEntity> Tiles, Coords2D position, int distance)
    {
        // TODO: Performance check on lambda vs LINQ vs long-hand for loop.
        int distanceSquared = distance * distance;

        //Dictionary<Vector2, IEntity> TilesWithinDist = Tiles.Where(x => MathFunctions.DistanceSquaredBetween(entity.Position, x.Key) <= distanceSquared).ToDictionary(entity => entity.Value.Position, entity => entity.Value);
        //return TilesWithinDist;
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

        //return (IEnumerable<IEntity>?)Tiles.Where(x => MathFunctions.DistanceSquaredBetween(entity.Position, x.Value.Position) <= distanceSquared ).ToList();
        //return Tiles.Where(x => MathFunctions.DistanceSquaredBetween(entity.Position, x.Position) <= distanceSquared && x != entity).ToList();
    }

    public IEnumerable<TEntity>? GetTilesOfType<TEntity>(IDictionary<Vector2, IEntity> Tiles )
    {
        return (IEnumerable<TEntity>?)Tiles.OfType<TEntity>().ToList();
    }

    public IEnumerable<TEntity>? GetTilesOfType<TEntity>()
    {
        return (IEnumerable<TEntity>?)_tiles.OfType<TEntity>().ToList();
    }

    protected override void DisposeManagedState()
    {
        _tiles.Clear();
        _tiles.GetEnumerator().Dispose();
    }

    public void UpdateRuntime(float deltaTime)
    {
        //for (int i = _tiles.Count - 1; i >= 0; i--)
        //{
        //    var entity = _tiles.ElementAt(i);
        //    entity.Value.Update(deltaTime);
        //}

        //foreach (var entity in _tiles)
        //{
        //    // do something with entry.Value or entry.Key
        //    entity.Value.Update(deltaTime);
        //}

        //_tiles.AsParallel().ForAll(e => e.Value.Update(deltaTime));

        SceneCamera.Update(deltaTime);

        if (SceneCamera != default)
        {
            Renderer.Begin(SceneCamera);
            //for (int i = _tiles.Count - 1; i >= 0; i--)
            //{
            //    var entity = _tiles.ElementAt(i);
            //    Renderer.Render(entity.Value, _defaultEntitySize);
            //}

            //foreach (var entity in _tiles)
            //{
            //    Renderer.Render(entity.Value, _defaultEntitySize);
            //}
            var defaultTint = new Color(128 - 64, 128, 128 + 64, 255);
            var topLft = new Vector2(SceneCamera.RaylibCamera.target.X - 16 * Constants.DEFAULT_ENTITY_SIZE, SceneCamera.RaylibCamera.target.Y - 16 * Constants.DEFAULT_ENTITY_SIZE);
            var btmRgt = new Vector2(SceneCamera.RaylibCamera.target.X + 16 * Constants.DEFAULT_ENTITY_SIZE, SceneCamera.RaylibCamera.target.Y + 16 * Constants.DEFAULT_ENTITY_SIZE);
            var v = new Vector2();

            for (v.Y = topLft.Y; v.Y <= btmRgt.Y; v.Y += Constants.DEFAULT_ENTITY_SIZE)
            {
                for (v.X = topLft.X; v.X <= btmRgt.X; v.X += Constants.DEFAULT_ENTITY_SIZE)
                {
                    //v.X = x;
                    //v.Y = y;

                    if (_tiles.ContainsKey(v))
                    {
                        Renderer.Render(_tiles[v], _defaultEntitySize);
                        _tiles[v].ColorTint = defaultTint;
                    }

                    //_tiles.TryGetValue(v, out e);
                    //Renderer.Render(e, _defaultEntitySize);
                }
            }

            //_tiles.AsParallel().ForAll(e => Renderer.Render(e.Value, _defaultEntitySize));

            Renderer.End();
        }
    }

    public void Add(IEntity entity)
    {
        _tiles.TryAdd(entity.Position, entity);
    }

    public void Remove(IEntity entity)
    {
        _tiles.Remove(entity.Position);
    }
}
