using MooseEngine.Core;
using MooseEngine.Extensions.Runtime;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Pathfinding;
using MooseEngine.Utilities;
using System.Numerics;

namespace MooseEngine.Scenes;

public class Scene : Disposeable, IScene
{
    private IDictionary<int, IEntityLayer> _entityLayers;

    public IEntityLayer<TEntity> AddLayer<TEntity>(int layer)
        where TEntity : class, IEntity
    {
        var entityLayer = new EntityLayer<TEntity>();
        _entityLayers.Add(layer, entityLayer);
        return entityLayer;
    }

    public IEntityLayer GetLayer(int layer)
    {
        return _entityLayers[layer];
    }

    // Pathfinding
    private Pathfinder _pathfinder = new Pathfinder();
    private PathMap _pathMap;

    public Pathfinder Pathfinder { get { return _pathfinder; } set { _pathfinder = value; } }
    public PathMap PathMap { get { return _pathMap; } set { _pathMap = value; } }

    private readonly float _defaultEntitySize;
    private ISceneCamera _cameraEntity;

    public Scene(IRenderer renderer, float defaultEntitySize = Constants.DEFAULT_ENTITY_SIZE)
    {
        _entityLayers = new Dictionary<int, IEntityLayer>();

        Renderer = renderer;
        _defaultEntitySize = defaultEntitySize;
    }

    public IRenderer Renderer { get; }
    public ISceneCamera SceneCamera { get { return _cameraEntity; } set { _cameraEntity = value; } }
    public IDictionary<int, IEntityLayer> EntityLayers { get { return _entityLayers; } set { _entityLayers = value; } }

    public bool TryMoveEntity(int entityLayer, IEntity entity, Vector2 targetPosition, params int[] collisionLayers)
    {
        //if (collisionLayers.Contains(entityLayer) == false) { collisionLayers.Append(entityLayer); }

        foreach (var layer in collisionLayers)
        {
            bool isKeyAvailable = !GetLayer(layer).ActiveEntities.ContainsKey(targetPosition);

            if (isKeyAvailable == true) { continue; } else { return false; }
        }

        if (GetLayer(entityLayer).ActiveEntities.TryAdd(targetPosition, entity) == true)
        {
            GetLayer(entityLayer).ActiveEntities.Remove(entity.Position);
            entity.Position = targetPosition;

            return true;
        }

        return false;
    }

    public bool TryPlaceEntity(int entityLayer, IEntity entity, Vector2 targetPosition, params int[] collisionLayers)
    {
        //if (collisionLayers.Contains(entityLayer) == false) { collisionLayers.Append(entityLayer); }

        foreach (var layer in collisionLayers)
        {
            bool isKeyAvailable = !GetLayer(layer).ActiveEntities.ContainsKey(targetPosition);

            if (isKeyAvailable == true) { continue; } else { return false; }
        }

        if (GetLayer(entityLayer).ActiveEntities.TryAdd(targetPosition, entity) == true)
        {
            entity.Position = targetPosition;

            return true;
        }

        return false;
    }

    public IDictionary<Vector2, TEntity>? GetEntitiesOfType<TEntity>(IEntityLayer entities) where TEntity : class, IEntity
    {
        Dictionary<Vector2, TEntity>? entitiesOfType = new Dictionary<Vector2, TEntity>();

        foreach (var entity in entities.ActiveEntities)
        {
            if (entity.Value.GetType() == typeof(TEntity))
            {
                entitiesOfType.Add(entity.Key, (TEntity)entity.Value);
            }
        }

        return entitiesOfType;
    }

    public IEntity? GetEntityAtPosition(IDictionary<Vector2, IEntity> entities, Vector2 position)
    {
        // TODO: Performance check on lambda vs LINQ vs long-hand for loop.
        return entities.ContainsKey(position) ? entities[position] : default;
    }
    
    public Vector2 GetRandomValidPosition(IDictionary<Vector2, IEntity> entities)
    {
        return entities.ElementAt(Randomizer.RandomInt(0, entities.Count - 1)).Key;
    }

    public Vector2 GetClosestValidPosition(int entityLayerNum, Vector2 targetPosition, params int[] collisionLayerNums)
    {
        bool searching = true;
        int searchDist = 0;

        while (true)
        {
            var topLft = new Vector2(-searchDist, -searchDist);
            var btmRgt = new Vector2(searchDist, searchDist);

            for (int x = (int)topLft.X; x <= btmRgt.X; x++)
            {
                for (int y = (int)topLft.Y; y <= btmRgt.Y; y++)
                {
                    var deltaPosition = new Vector2(x, y) * Constants.DEFAULT_ENTITY_SIZE;

                    var testPosition = targetPosition + deltaPosition;

                    searching = false;

                    foreach (var layer in collisionLayerNums)
                    {
                        bool isPositionTaken = GetLayer(layer).ActiveEntities.ContainsKey(testPosition);

                        if (isPositionTaken) { searching = true; break; }
                    }

                    if (searching == false) { return testPosition; }
                }
            }

            searchDist++;
        }
    }

    public IDictionary<Vector2, IEntity>? GetEntitiesWithinCircle(IDictionary<Vector2, IEntity> entities, Coords2D position, int distance)
    {
        int distanceSquared = distance * distance;

        Dictionary<Vector2, IEntity> tilesWithinDist = new Dictionary<Vector2, IEntity>();

        var topLft = new Vector2(position.X - distance, position.Y - distance);
        var btmRgt = new Vector2(position.X + distance, position.Y + distance);
        var v = new Vector2();

        for (v.Y = topLft.Y; v.Y <= btmRgt.Y; v.Y += Constants.DEFAULT_ENTITY_SIZE)
        {
            for (v.X = topLft.X; v.X <= btmRgt.X; v.X += Constants.DEFAULT_ENTITY_SIZE)
            {
                if (entities.ContainsKey(v))
                {
                    if (MathFunctions.DistanceSquaredBetween(position, v) <= distanceSquared)
                    //if (Vector2.DistanceSquared(position, v) <= distanceSquared)
                    {
                        tilesWithinDist.Add(v, entities[v]);
                    }
                }
            }
        }

        return tilesWithinDist;
    }

    public IDictionary<Vector2, TEntity>? GetEntitiesWithinCircle<TEntity>(IDictionary<Vector2, IEntity> entities, Coords2D position, int distance) where TEntity : class, IEntity
    {
        int distanceSquared = distance * distance;

        var tilesWithinDist = new Dictionary<Vector2, TEntity>();

        var topLft = new Vector2(position.X - distance, position.Y - distance);
        var btmRgt = new Vector2(position.X + distance, position.Y + distance);
        var v = new Vector2();

        for (v.Y = topLft.Y; v.Y <= btmRgt.Y; v.Y += Constants.DEFAULT_ENTITY_SIZE)
        {
            for (v.X = topLft.X; v.X <= btmRgt.X; v.X += Constants.DEFAULT_ENTITY_SIZE)
            {
                if (entities.ContainsKey(v) && entities[v] is TEntity)
                {
                    if (MathFunctions.DistanceSquaredBetween(position, v) <= distanceSquared)
                    //if (Vector2.DistanceSquared(position, v) <= distanceSquared)
                    {
                        tilesWithinDist.Add(v, (TEntity)entities[v]);
                    }
                }
            }
        }

        return tilesWithinDist;
    }

    //public IList<IEntity>? GetEntitiesWithinCircle(IDictionary<Vector2, IEntity> entities, Coords2D position, int distance)
    //{
    //    int distanceSquared = distance * distance;

    //    var tilesWithinDist = new List<IEntity>();

    //    var topLft = new Vector2(position.X - distance, position.Y - distance);
    //    var btmRgt = new Vector2(position.X + distance, position.Y + distance);
    //    var v = new Vector2();

    //    for (v.Y = topLft.Y; v.Y <= btmRgt.Y; v.Y += Constants.DEFAULT_ENTITY_SIZE)
    //    {
    //        for (v.X = topLft.X; v.X <= btmRgt.X; v.X += Constants.DEFAULT_ENTITY_SIZE)
    //        {
    //            if (entities.ContainsKey(v))
    //            {
    //                if (MathFunctions.DistanceSquaredBetween(position, v) <= distanceSquared)
    //                //if (Vector2.DistanceSquared(position, v) <= distanceSquared)
    //                {
    //                    tilesWithinDist.Add(entities[v]);
    //                }
    //            }
    //        }
    //    }

    //    return tilesWithinDist;
    //}

    public IDictionary<Vector2, IEntity>? GetEntitiesWithinRectangle(IDictionary<Vector2, IEntity> entities, Vector2 topLeft, Vector2 bottomRight)
    {
        Dictionary<Vector2, IEntity> tilesWithinDist = new Dictionary<Vector2, IEntity>();

        //var v = new Vector2();

        //for (v.Y = topLeft.Y; v.Y <= bottomRight.Y; v.Y += Constants.DEFAULT_ENTITY_SIZE)
        //{
        //    for (v.X = topLeft.X; v.X <= bottomRight.X; v.X += Constants.DEFAULT_ENTITY_SIZE)
        //    {
        //        if (entities.ContainsKey(v))
        //        {
        //            tilesWithinDist.Add(v, entities[v]);
        //        }
        //    }
        //}

        for (int y = (int)topLeft.Y; y < bottomRight.Y; y += Constants.DEFAULT_ENTITY_SIZE)
        {
            for (int x = (int)topLeft.X; x < bottomRight.X; x += Constants.DEFAULT_ENTITY_SIZE)
            {
                var v = new Vector2(x, y);

                if (entities.ContainsKey(v))
                {
                    tilesWithinDist.Add(v, entities[v]);
                }
            }
        }

        return tilesWithinDist;
    }

    protected override void DisposeManagedState()
    {
        for(int i = 0; i < _entityLayers.Count; i++)
        {
            _entityLayers[i].ActiveEntities.Clear();
            _entityLayers[i].InactiveEntities.Clear();
            _entityLayers.Remove(i);
        }

        _entityLayers.Clear();
        _entityLayers.GetEnumerator().Dispose();
    }

    public void UpdateRuntime(float deltaTime)
    {

        SceneCamera.Update(deltaTime);

        var defaultTint = new Color(128 - 64, 128, 128 + 64, 255);

        var windowSize = new Vector2((int)(Application.Instance.Window.Width * 0.5 - (Application.Instance.Window.Width * 0.5 % Constants.DEFAULT_ENTITY_SIZE)), (int)(Application.Instance.Window.Height * 0.5 - (Application.Instance.Window.Height * 0.5 % Constants.DEFAULT_ENTITY_SIZE)));
        var topLft = new Vector2(SceneCamera.Position.X - windowSize.X, SceneCamera.Position.Y - windowSize.Y);
        var btmRgt = new Vector2(SceneCamera.Position.X + windowSize.X, SceneCamera.Position.Y + windowSize.Y);


        var layers = _entityLayers.Keys;

        Renderer.BeginScene(SceneCamera);

        foreach (var layer in layers)
        {
            var entities = _entityLayers[layer].ActiveEntities;

            var v = new Vector2();

            for (v.Y = topLft.Y; v.Y <= btmRgt.Y; v.Y += Constants.DEFAULT_ENTITY_SIZE)
            {
                for (v.X = topLft.X; v.X <= btmRgt.X; v.X += Constants.DEFAULT_ENTITY_SIZE)
                {
                    if (entities.ContainsKey(v))
                    {
                        Renderer.Render(entities[v], Constants.DEFAULT_ENTITY_SIZE);
                        entities[v].ColorTint = defaultTint;
                    }
                }
            }
        }

        Renderer.EndScene();
    }
}