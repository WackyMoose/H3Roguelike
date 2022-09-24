using MooseEngine.Pathfinding;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System.Numerics;

namespace MooseEngine.Interfaces;

public interface IScene : IDisposable
{
    ISceneCamera SceneCamera { get; set; }
    IDictionary<int, IEntityLayer> EntityLayers { get; set; }
    Pathfinder Pathfinder { get; set; }
    PathMap PathMap { get; set; }

    bool TryMoveEntity(int entityLayer, IEntity entity, Vector2 targetPosition, params int[] collisionLayers);
    bool TryPlaceEntity(int entityLayer, IEntity entity, Vector2 targetPosition, params int[] collisionLayers);
    IDictionary<Vector2, TEntity>? GetEntitiesOfType<TEntity>(IEntityLayer entities) where TEntity : class, IEntity;
    IEntity? GetEntityAtPosition(IDictionary<Vector2, IEntity> Tiles, Vector2 position);
    IDictionary<Vector2, IEntity>? GetEntitiesWithinCircle(IDictionary<Vector2, IEntity> entities, Coords2D position, int distance);
    IDictionary<Vector2, IEntity>? GetEntitiesWithinRectangle(IDictionary<Vector2, IEntity> entities, Vector2 topLeft, Vector2 bottomRight);

    IEntityLayer<TEntity> AddLayer<TEntity>(int layer) where TEntity : class, IEntity;
    IEntityLayer GetLayer(int layer);

    void UpdateRuntime(float deltaTime);
}
