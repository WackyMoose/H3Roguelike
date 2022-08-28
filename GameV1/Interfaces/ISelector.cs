using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using System.Numerics;

namespace GameV1.Interfaces
{
    public interface ISelector
    {
        IEntity SelectedEntity { get; set; }
        //IDictionary<Vector2, IEntity>? EntitiesWithinRange(IScene scene, IDictionary<Vector2, IEntity> entities, int range);
    }
}
