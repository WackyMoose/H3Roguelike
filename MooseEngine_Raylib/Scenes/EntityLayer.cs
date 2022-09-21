using MooseEngine.Interfaces;
using System.Numerics;

namespace MooseEngine.Scenes;

public interface IEntityLayer
{
    IDictionary<Vector2, IEntity> ActiveEntities { get; }
    IList<IEntity> InactiveEntities { get; }

    IEnumerable<TEntityType> GetActiveEntitiesOfType<TEntityType>() where TEntityType : IEntity;

    bool DeactivateEntity(IEntity entity);
    bool ActivateEntity(IEntity entity);

    TEntityType GetFirstAvailableEntity<TEntityType>() where TEntityType : class, IEntity;

    //bool Add(IEntity entity);
    //IEntity Add(IEntity entity);
    void Remove(IEntity entity);
    void RemoveAll();
}

public interface IEntityLayer<TEntity> : IEntityLayer where TEntity : class, IEntity
{
    //bool InactivateEntity(TEntity entity);
    //bool ActivateEntity(TEntity entity);

    bool AddEntity(TEntity entity);
    void RemoveEntity(TEntity entity);
    void RemoveAll();
}

public class EntityLayer<TEntity> : IEntityLayer<TEntity> where TEntity : class, IEntity
{
    public IDictionary<Vector2, IEntity> ActiveEntities { get; } = new Dictionary<Vector2, IEntity>();
    public IList<IEntity> InactiveEntities { get; } = new List<IEntity>();

    public bool DeactivateEntity(IEntity entity)
    {
        if (ActiveEntities.ContainsKey(entity.Position))
        {
            InactiveEntities.Add(entity);
            ActiveEntities.Remove(entity.Position);

            return true;
        }
        return false;
    }
    public bool ActivateEntity(IEntity entity)
    {
        if (InactiveEntities.Contains(entity) && ActiveEntities.ContainsKey(entity.Position) == false)
        {
            ActiveEntities.Add(entity.Position, entity);
            InactiveEntities.Remove(entity);

            return true;
        }
        return false;
    }

    public TEntityType? GetFirstAvailableEntity<TEntityType>() where TEntityType : class, IEntity
    {
        foreach (var entity in InactiveEntities)
        {
            if (entity is TEntityType)
            {
                return entity as TEntityType;
            }
        }
        return null;
    }

    public bool AddEntity(TEntity entity)
    {
        ActiveEntities.Add(entity.Position, entity);

        return true;
    }

    public IEnumerable<TEntityType> GetActiveEntitiesOfType<TEntityType>() where TEntityType : IEntity
    {
        return ActiveEntities.Values.OfType<TEntityType>();
    }

    public void RemoveEntity(TEntity entity)
    {
        ActiveEntities.Remove(entity.Position);
    }

    public void Remove(IEntity entity)
    {
        ActiveEntities.Remove(entity.Position);
    }

    public void RemoveAll()
    {
        ActiveEntities.Clear();
    }
}