using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System.Numerics;

namespace MooseEngine.Interfaces;

public interface IScene : IDisposable
{
    ISceneCamera SceneCamera { get; set; }
    IDictionary<int, EntityLayer> EntityLayers { get; set; }

    IEntity? EntityAtPosition(IDictionary<Vector2, IEntity> Tiles, Vector2 position);
    IDictionary<Vector2, IEntity>? GetEntitiesWithinRange(IDictionary<Vector2, IEntity> Tiles, Coords2D position, int distance);

    IEntityLayer<TEntity> AddLayer<TEntity>(int layer) where TEntity : class, IEntity;
    EntityLayer GetLayer(int layer);

    void UpdateRuntime(float deltaTime);
}
