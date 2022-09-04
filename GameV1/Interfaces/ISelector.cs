using MooseEngine.Interfaces;

namespace GameV1.Interfaces
{
    public interface ISelector
    {
        IEntity SelectedEntity { get; set; }
        //IDictionary<Vector2, IEntity>? TilesWithinRange(IScene scene, IDictionary<Vector2, IEntity> Tiles, int range);
    }
}
