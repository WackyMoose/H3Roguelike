using MooseEngine.Core;
using MooseEngine.Extensions.Runtime;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Utilities;
using System.Collections.Generic;
using System.Numerics;

namespace MooseEngine.Scenes;
public interface IEntityLayer
{
    IDictionary<Vector2, IEntity> Entities { get; }
}

public interface IEntityLayer<TEntity> : IEntityLayer
    where TEntity : class, IEntity
{
    void Add(TEntity entity);
    void Remove(TEntity entity);
}

public class EntityLayer<TEntity> : IEntityLayer<TEntity>
    where TEntity : class, IEntity
{
    public IDictionary<Vector2, IEntity> Entities { get; } = new Dictionary<Vector2, IEntity>();

    public void Add(TEntity entity)
    {
        Entities.Add(entity.Position, entity);
    }

    public void Remove(TEntity entity)
    {
        Entities.Remove(entity.Position);
    }
}

internal class Scene : Disposeable, IScene
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

    //private IDictionary<Vector2, IEntity>? _entities;

    private readonly float _defaultEntitySize;
    private ISceneCamera _cameraEntity;

    public Scene(IRenderer renderer, float defaultEntitySize = Constants.DEFAULT_ENTITY_SIZE)
    {
        //_entities = new Dictionary<Vector2, IEntity>();
        _entityLayers = new Dictionary<int, IEntityLayer>();

        Renderer = renderer;
        _defaultEntitySize = defaultEntitySize;
    }

    public IRenderer Renderer { get; }
    public ISceneCamera SceneCamera { get { return _cameraEntity; } set { _cameraEntity = value; } }
    public IDictionary<int, IEntityLayer> EntityLayers { get { return _entityLayers; } set { _entityLayers = value; } }
    //
    //public IDictionary<Vector2, IEntity>? Tiles { get { return _entities; } }

    public IEntity? EntityAtPosition(IDictionary<Vector2, IEntity> entities, Vector2 position)
    {
        // TODO: Performance check on lambda vs LINQ vs long-hand for loop.
        return entities.ContainsKey(position) ? entities[position] : default;
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

        var sceneCamera = GetSceneCamera();
        if (sceneCamera != default)
        {
            Renderer.BeginScene(sceneCamera);
            for (int i = _entities.Count - 1; i >= 0; i--)
            {
                var entity = _entities[i];
                Renderer.Render(entity, _defaultEntitySize);
            }
            Renderer.EndScene();
        }

        return TilesWithinDist;
    }


    protected override void DisposeManagedState()
    {
        _entityLayers.Clear();
        _entityLayers.GetEnumerator().Dispose();
    }

    public void UpdateRuntime(float deltaTime)
    {
        SceneCamera.Update(deltaTime);

        //if (SceneCamera != default)
        //{
        //    Renderer.Begin(SceneCamera);

        //    var defaultTint = new Color(128 - 64, 128, 128 + 64, 255);
        //    var windowSize = new Vector2((int)(Application.Instance.Window.Width * 0.5 - Application.Instance.Window.Width * 0.5 % Constants.DEFAULT_ENTITY_SIZE), (int)(Application.Instance.Window.Height * 0.5 - Application.Instance.Window.Height * 0.5 % Constants.DEFAULT_ENTITY_SIZE) );
        //    var topLft = new Vector2(SceneCamera.RaylibCamera.target.X - windowSize.X, SceneCamera.RaylibCamera.target.Y - windowSize.Y);
        //    var btmRgt = new Vector2(SceneCamera.RaylibCamera.target.X + windowSize.X, SceneCamera.RaylibCamera.target.Y + windowSize.Y);
        //    var v = new Vector2();

        //    for (v.Y = topLft.Y; v.Y <= btmRgt.Y; v.Y += Constants.DEFAULT_ENTITY_SIZE)
        //    {
        //        for (v.X = topLft.X; v.X <= btmRgt.X; v.X += Constants.DEFAULT_ENTITY_SIZE)
        //        {
        //            if (_entities.ContainsKey(v))
        //            {
        //                Renderer.Render(_entities[v], _defaultEntitySize);
        //                _entities[v].ColorTint = defaultTint;
        //            }
        //        }
        //    }

        //    Renderer.End();
        //}

        // TODO: Replace render with this
        var defaultTint = new Color(128 - 64, 128, 128 + 64, 255);

        var windowSize = new Vector2((int)(Application.Instance.Window.Width * 0.5 - (Application.Instance.Window.Width * 0.5 % Constants.DEFAULT_ENTITY_SIZE)), (int)(Application.Instance.Window.Height * 0.5 - (Application.Instance.Window.Height * 0.5 % Constants.DEFAULT_ENTITY_SIZE)));
        var topLft = new Vector2(SceneCamera.Position.X - windowSize.X, SceneCamera.Position.Y - windowSize.Y);
        var btmRgt = new Vector2(SceneCamera.Position.X + windowSize.X, SceneCamera.Position.Y + windowSize.Y);

        var layers = _entityLayers.Keys;
        Renderer.Begin(SceneCamera);
        foreach (var layer in layers)
        {
            var entities = _entityLayers[layer].Entities;

            if (SceneCamera == default)
            {
                continue;
            }

            var v = new Vector2();

            //foreach (IEntity entity in _entityLayers[layer].Entities.Values )
            //{
            //    Renderer.Render(entity, Constants.DEFAULT_ENTITY_SIZE);
            //    entity.ColorTint = defaultTint;
            //}

            for (v.Y = topLft.Y; v.Y <= btmRgt.Y; v.Y += Constants.DEFAULT_ENTITY_SIZE)
            {
                for (v.X = topLft.X; v.X <= btmRgt.X; v.X += Constants.DEFAULT_ENTITY_SIZE)
                {
                    if (entities.ContainsKey(v))
                    {
                        Renderer.Render((Entity)entities[v], Constants.DEFAULT_ENTITY_SIZE);
                        entities[v].ColorTint = defaultTint;
                    }

                }
            }
        }
        Renderer.End();
    }
}
