using GameV1.Interfaces;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Entities
{
    public class LightSource : Entity, ILightSource
    {
        public int Range { get; set; }
        public Color TintModifier { get; set; }

        public int Durability { get; set; }
        public int MaxValue { get; set; }
        public List<Material> Materials { get; set; }
        public bool IsBroken => throw new NotImplementedException();

        public LightSource(int range, Color tintModifier, string name, Coords2D spriteCoords) : base(name, spriteCoords)
        {
            Range = range;
            TintModifier = tintModifier;
        }

        public LightSource(int range, Color tintModifier, string name, Coords2D spriteCoords, Color colorTint) : base(name, spriteCoords, colorTint)
        {
            Range = range;
            TintModifier = tintModifier;
        }

        public void Illuminate(IScene scene, IEnumerable<IEntity> entities)
        {
            //var entitiesWithinRange = scene.EntitiesWithinDistanceOfPosition()


            foreach (IEntity entity in entities)
            {

            }
        }

        public override void Initialize()
        {
            
        }

        public override void Update(float deltaTime)
        {
            
        }
    }
}
