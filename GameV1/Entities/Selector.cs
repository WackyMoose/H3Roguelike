using GameV1.Interfaces;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;

namespace GameV1.Entities
{
    internal class Selector<TEntity> : Entity, ISelector<TEntity> where TEntity : class, IEntity
    {
        public IEnumerable<TEntity> Entities { get; set; }
        public TEntity SelectedEntity { get; set; }
        public int SelectedEntityIndex { get; set; }

        public Selector(string name, Coords2D spriteCoords) : base(name, spriteCoords)
        {
        }

        public Selector(string name, Coords2D spriteCoords, Color colorTint) : base(name, spriteCoords, colorTint)
        {
        }

        public override void Initialize()
        {

        }

        public override void Update(float deltaTime)
        {

        }
    }
}
