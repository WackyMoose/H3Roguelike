using GameV1.Interfaces;
using GameV1.Interfaces.Creatures;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System.Numerics;

namespace GameV1.Entities
{
    internal class Selector : ISelector
    {
        public IDictionary<Vector2, ICreature> Entities { get; set; }
        public IEntity SelectedEntity { get; set; }
        public int SelectedEntityIndex { get; set; }

        public Selector()
        {
        }

        public void AddEntities(IDictionary<Vector2, ICreature> entities)
        {
            Entities = entities;
        }

        public void SelectNextEntity()
        {
            if (SelectedEntityIndex < Entities.Count - 1)
            {
                SelectedEntityIndex++;
            }
            else
            {
                SelectedEntityIndex = 0;
            }
            
            SelectedEntity = Entities.ElementAt(SelectedEntityIndex).Value;
        }
    }
}
