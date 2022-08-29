using MooseEngine.Interfaces;
using System.Numerics;

namespace MooseEngine.Scenes;

public interface EntityLayer
{
    IDictionary<Vector2, IEntity> Entities { get; }
}

public interface IEntityLayer<TEntity> : EntityLayer
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