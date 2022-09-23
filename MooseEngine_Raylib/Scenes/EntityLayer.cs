using MooseEngine.Interfaces;
using System.Numerics;

namespace MooseEngine.Scenes;

public interface IEntityLayer
{
    IDictionary<Vector2, IEntity> ActiveEntities { get; }
    IList<IEntity> InactiveEntities { get; }

    bool DeactivateEntity(IEntity entity);
    bool ActivateEntity(IEntity entity);

    IEnumerable<TEntityType> GetActiveEntitiesOfType<TEntityType>() where TEntityType : IEntity;

    TEntityType? GetFirstInactiveEntityOfType<TEntityType>() where TEntityType : class, IEntity;

    bool AddEntity(IEntity entity);
    void RemoveEntity(IEntity entity);
    void RemoveAll();
}

public interface IEntityLayer<TEntity> : IEntityLayer where TEntity : class, IEntity
{

    bool AddEntity(TEntity entity);
    void RemoveEntity(TEntity entity);
}

public class EntityLayer<TEntity> : IEntityLayer<TEntity> where TEntity : class, IEntity
{
    public IDictionary<Vector2, IEntity> ActiveEntities { get; } = new Dictionary<Vector2, IEntity>();
    public IList<IEntity> InactiveEntities { get; } = new List<IEntity>();

    public bool DeactivateEntity(IEntity entity)
    {
        if (InactiveEntities.Contains(entity) == false && ActiveEntities.ContainsKey(entity.Position) == true)
        {
            entity.IsActive = false;
            InactiveEntities.Add(entity);
            ActiveEntities.Remove(entity.Position);

            return true;
        }
        return false;
    }
    public bool ActivateEntity(IEntity entity)
    {
        if (InactiveEntities.Contains(entity) == true && ActiveEntities.ContainsKey(entity.Position) == false)
        {
            entity.IsActive = true;
            ActiveEntities.Add(entity.Position, entity);
            InactiveEntities.Remove(entity);

            return true;
        }
        return false;
    }

    public TEntityType? GetFirstInactiveEntityOfType<TEntityType>() where TEntityType : class, IEntity
    {
        if (InactiveEntities == null || InactiveEntities.Count == 0) { return null; }

        foreach (var entity in InactiveEntities)
        {
            if (entity is TEntityType)
            {
                return entity as TEntityType;
            }
        }
        return null;
    }

    public IEnumerable<TEntityType> GetActiveEntitiesOfType<TEntityType>() where TEntityType : IEntity
    {
        return ActiveEntities.Values.OfType<TEntityType>();
    }

    public bool AddEntity(TEntity entity)
    {
        ActiveEntities.Add(entity.Position, entity);

        return true;
    }

    public bool AddEntity(IEntity entity)
    {
        ActiveEntities.Add(entity.Position, entity);

        return true;
    }

    public void RemoveEntity(IEntity entity)
    {
        ActiveEntities.Remove(entity.Position);
    }

    public void RemoveEntity(TEntity entity)
    {
        ActiveEntities.Remove(entity.Position);
    }

    public void RemoveAll()
    {
        ActiveEntities?.Clear();
    }
}