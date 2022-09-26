using MooseEngine.Interfaces;
using System.Numerics;

namespace GameV1.Interfaces
{
    public interface ISelector<TEntity> : IEntity where TEntity : class, IEntity
    {
        //IEnumerable<TEntity> Entities { get; set; }
        IDictionary<Vector2, TEntity> Entities { get; set; }
        TEntity SelectedEntity { get; set; }
        int SelectedEntityIndex { get; set; }
    }
}
