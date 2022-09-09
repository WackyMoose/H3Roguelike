using MooseEngine.Interfaces;
using System.Numerics;

namespace MooseEngine.Scenes;

public interface IEntityLayer
{
    IDictionary<Vector2, IEntity> Entities { get; }

    IEnumerable<TEntityType> GetEntitiesOfType<TEntityType>() where TEntityType : IEntity;
}

public interface IEntityLayer<TEntity> : IEntityLayer
    where TEntity : class, IEntity
{
    void Add(TEntity entity);
    void Remove(TEntity entity);
    void RemoveAll();
}

public class EntityLayer<TEntity> : IEntityLayer<TEntity>
    where TEntity : class, IEntity
{
    public IDictionary<Vector2, IEntity> Entities { get; } = new Dictionary<Vector2, IEntity>();

    public void Add(TEntity entity)
    {
        Entities.Add(entity.Position, entity);
    }

    public IEnumerable<TEntityType> GetEntitiesOfType<TEntityType>() where TEntityType : IEntity
    {
        return Entities.Values.OfType<TEntityType>();
    }

    public void Remove(TEntity entity)
    {
        Entities.Remove(entity.Position);
    }

    public void RemoveAll() 
    {
        Entities.Clear();
    }
}