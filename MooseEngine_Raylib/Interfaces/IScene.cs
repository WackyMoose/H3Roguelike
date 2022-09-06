using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System.Numerics;

namespace MooseEngine.Interfaces;

public interface IScene : IDisposable
{
    ISceneCamera SceneCamera { get; set; }
    IDictionary<int, IEntityLayer> EntityLayers { get; set; }

    IDictionary<Vector2, IEntity>? GetEntitiesOfType<TType>(IEntityLayer entities);
    IEntity? GetEntityAtPosition(IDictionary<Vector2, IEntity> Tiles, Vector2 position);
    IDictionary<Vector2, IEntity>? GetEntitiesWithinCircle(IDictionary<Vector2, IEntity> Tiles, Coords2D position, int distance);
    IDictionary<Vector2, IEntity>? GetEntitiesWithinRectangle(IDictionary<Vector2, IEntity> entities, Vector2 topLeft, Vector2 bottomRight);

    IEntityLayer<TEntity> AddLayer<TEntity>(int layer) where TEntity : class, IEntity;
    IEntityLayer GetLayer(int layer);

    void UpdateRuntime(float deltaTime);
}
