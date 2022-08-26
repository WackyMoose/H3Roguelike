using GameV1.Interfaces;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Entities
{
    internal class Selector : Entity, ISelector
    {

        public IEntity SelectedEntity { get; set; }

        public IEnumerable<IEntity>? EntitiesWithinRange(IScene scene, IDictionary<Vector2, IEntity> entities, int range)
        {
            return scene.EntitiesWithinDistanceOfEntity(entities, SelectedEntity, range);
        }

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
