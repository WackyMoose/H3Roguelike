using MooseEngine.Scenes;
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
        ISceneCamera SceneCamera { get; set; }
        IDictionary<int, IEntityLayer> EntityLayers { get; set; }
        IEntity? EntityAtPosition(IDictionary<Vector2, IEntity> Tiles, Vector2 position);
        IDictionary<Vector2, IEntity>? GetEntitiesWithinRange(IDictionary<Vector2, IEntity> Tiles, Coords2D position, int distance);

        IEntityLayer<TEntity> AddLayer<TEntity>(int layer) where TEntity : class, IEntity;
        IEntityLayer GetLayer(int layer);

        void UpdateRuntime(float deltaTime);
    }
}
