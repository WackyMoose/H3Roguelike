using MooseEngine.Interfaces;
using MooseEngine.Scenes;

namespace GameV1.Interfaces
{
    public interface ISelector
    {
        IEntity SelectedEntity { get; set; }
        IEnumerable<IEntity>? EntitiesWithinRange(IScene scene, IEnumerable<IEntity> entities, int range);
    }
}
