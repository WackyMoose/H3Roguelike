using MooseEngine.Interfaces;
using System.Numerics;

namespace MooseEngine.Scenes;

public interface IEntityLayer
{
    IDictionary<Vector2, IEntity> Entities { get; }

    IEnumerable<TEntityType> GetEntitiesOfType<TEntityType>() where TEntityType : IEntity;
    IEntity Add(IEntity entity);
    void Remove(IEntity entity);
    void RemoveAll();
}

public interface IEntityLayer<TEntity> : IEntityLayer
    where TEntity : class, IEntity
{
    IEntity Add(TEntity entity);
    void Remove(TEntity entity);
    void RemoveAll();
}

public class EntityLayer<TEntity> : IEntityLayer<TEntity>
    where TEntity : class, IEntity
{
    public IDictionary<Vector2, IEntity> Entities { get; } = new Dictionary<Vector2, IEntity>();

    public IEntity Add(TEntity entity)
    {
        Entities.Add(entity.Position, entity);
        
        return entity;
    }

    public IEntity Add(IEntity entity)
    {
        Entities.Add(entity.Position, entity);

        return entity;
    }

    public IEnumerable<TEntityType> GetEntitiesOfType<TEntityType>() where TEntityType : IEntity
    {
        return Entities.Values.OfType<TEntityType>();
    }

    public void Remove(TEntity entity)
    {
        Entities.Remove(entity.Position);
    }

    public void Remove(IEntity entity)
    {
        Entities.Remove(entity.Position);
    }

    public void RemoveAll()
    {
        Entities.Clear();
    }
}