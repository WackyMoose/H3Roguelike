using MooseEngine.Interfaces;
using System.Numerics;

namespace MooseEngine.Scenes;


public class EntityLayer<TEntity> : IEntityLayer<TEntity> where TEntity : class, IEntity
{
    public IDictionary<Vector2, IEntity> ActiveEntities { get; } = new Dictionary<Vector2, IEntity>();
    public IList<IEntity> InactiveEntities { get; } = new List<IEntity>();

    public TEntityType ActivateOrCreateEntity<TEntityType>(Vector2 position) where TEntityType : class, IEntity, new()
    {
        TEntityType? entity = GetFirstInactiveEntityOfType<TEntityType>();
        
        if (entity != null)
        {
            entity.Position = position;

            ActivateEntity(entity);
        }
        else
        {
            entity = new TEntityType();

            entity.IsActive = true;
            entity.Position = position;

            AddEntity(entity);
        }

        return entity;
    }

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
        if (ActiveEntities.ContainsKey(entity.Position) == false)
        {
            ActiveEntities.Add(entity.Position, entity);
            return true;
        }
        return false;
    }

    public bool AddEntity(IEntity entity)
    {
        if (ActiveEntities.ContainsKey(entity.Position) == false)
        {
            ActiveEntities.Add(entity.Position, entity);
            return true;
        }
        return false;
    }

    public bool RemoveEntity(IEntity entity)
    {
        return ActiveEntities.Remove(entity.Position);
    }

    public bool RemoveEntity(TEntity entity)
    {
        return ActiveEntities.Remove(entity.Position);
    }

    public void RemoveAll()
    {
        ActiveEntities?.Clear();
    }
}