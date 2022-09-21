using MooseEngine.Interfaces;

namespace GameV1.Interfaces
{
    public interface ISelector<TEntity> : IEntity where TEntity : class, IEntity
    {
        IEnumerable<TEntity> Entities { get; set; }
        TEntity SelectedEntity { get; set; }
        int SelectedEntityIndex { get; set; }
    }
}
