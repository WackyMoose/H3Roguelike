using GameV1.Interfaces;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System.Collections.Generic;

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
            var entitiesWithinRange = scene.EntitiesWithinDistanceOfEntity(entities, this, this.Range);

            var maxDistanceSquared = this.Range * this.Range;

            foreach (IEntity entity in entitiesWithinRange)
            {
                // TODO: Implement true inverse distance squared light intensity
                var distanceSquared = MathFunctions.DistanceSquaredBetween(this.Position, entity.Position);

                var inLerp = MathFunctions.InverseLerp(maxDistanceSquared, 0, distanceSquared);
                //var lerp = MathFunctions.Lerp(0, 1, inLerp);

                var r = (int)(inLerp * (128 + 32));
                var g = (int)(inLerp * (128 + 16));
                var b = (int)(inLerp * (128 +  0));

                var color = new Color(r, g, b, 255);

                entity.ColorTint += color;
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
