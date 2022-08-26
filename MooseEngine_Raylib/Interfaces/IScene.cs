using MooseEngine.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MooseEngine.Interfaces
{
    public interface IScene : IDisposable
    {
        IDictionary<Vector2, IEntity>? Entities { get; }
        ISceneCamera SceneCamera { get; set; }
        IEntity? EntityAtPosition(IDictionary<Vector2, IEntity> entities, Vector2 position);
        IDictionary<Vector2, IEntity>? EntitiesWithinDistanceOfEntity(IDictionary<Vector2, IEntity> entities, IEntity entity, int distance);
        IEnumerable<TEntity>? GetEntitiesOfType<TEntity>();
        IEnumerable<TEntity>? GetEntitiesOfType<TEntity>(IDictionary<Vector2, IEntity> entities);

        void UpdateRuntime(float deltaTime);
        void Add(IEntity entity);
        void Remove(IEntity entity);
    }
}
