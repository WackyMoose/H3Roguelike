using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MooseEngine.Interfaces
{
    public interface IEntityLayer
    {
        IDictionary<Vector2, IEntity> ActiveEntities { get; }
        IList<IEntity> InactiveEntities { get; }

        TEntityType ActivateOrCreateEntity<TEntityType>(Vector2 position) where TEntityType : class, IEntity, new();

        bool DeactivateEntity(IEntity entity);
        bool ActivateEntity(IEntity entity);

        IEnumerable<TEntityType> GetActiveEntitiesOfType<TEntityType>() where TEntityType : IEntity;

        TEntityType? GetFirstInactiveEntityOfType<TEntityType>() where TEntityType : class, IEntity;

        bool AddEntity(IEntity entity);
        bool RemoveEntity(IEntity entity);
        void RemoveAll();
    }

    public interface IEntityLayer<TEntity> : IEntityLayer where TEntity : class, IEntity
    {

        bool AddEntity(TEntity entity);
        bool RemoveEntity(TEntity entity);
    }
}
