using GameV1.Interfaces;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System.Numerics;

namespace GameV1.Entities
{
    internal class Selector<TEntity> : Entity, ISelector<TEntity> where TEntity : class, IEntity
    {
        // 

        //public IEnumerable<TEntity> Entities { get; set; }
        public IDictionary<Vector2, TEntity> Entities { get; set; }
        public TEntity SelectedEntity { get; set; }
        public int SelectedEntityIndex { get; set; }

        public Selector(IDictionary<Vector2, TEntity> entities)
        {
            Entities = entities;
        }

        public Selector(string name, Coords2D spriteCoords) : base(name, spriteCoords)
        {
        }

        public Selector(string name, Coords2D spriteCoords, Color colorTint) : base(name, spriteCoords, colorTint)
        {
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

        public override void Initialize()
        {

        }

        public override void Update(float deltaTime)
        {

        }
    }
}
