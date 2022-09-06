using GameV1.Interfaces;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;

namespace GameV1.Entities
{
    internal class Selector : Entity, ISelector
    {

        public IEntity SelectedEntity { get; set; }

        //public IDictionary<Vector2, IEntity>? TilesWithinRange(IScene scene, IDictionary<Vector2, IEntity> Tiles, int range)
        //{
        //    return scene.GetEntitiesWithinRange(Tiles, SelectedEntity, range);
        //}

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
