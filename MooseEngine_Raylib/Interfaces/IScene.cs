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
        IDictionary<Vector2, IEntity>? Tiles { get; }
        ISceneCamera SceneCamera { get; set; }
        IEntity? EntityAtPosition(IDictionary<Vector2, IEntity> Tiles, Vector2 position);
        IDictionary<Vector2, IEntity>? GetEntitiesWithinRange(IDictionary<Vector2, IEntity> Tiles, Coords2D position, int distance);
        IEnumerable<TEntity>? GetEntitiesOfType<TEntity>();
        IEnumerable<TEntity>? GetEntitiesOfType<TEntity>(IDictionary<Vector2, IEntity> Tiles);

        void UpdateRuntime(float deltaTime);
        void Add(IEntity entity);
        void Remove(IEntity entity);
    }
}
