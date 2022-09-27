using GameV1.Interfaces.Creatures;
using MooseEngine.Interfaces;
using System.Numerics;

namespace GameV1.Interfaces
{
    public interface ISelector
    {
        IDictionary<Vector2, ICreature> Entities { get; set; }
        IEntity SelectedEntity { get; set; }
        int SelectedEntityIndex { get; set; }

        void AddEntities(IDictionary<Vector2, ICreature> entities);
        
        void SelectNextEntity();
    }
}
