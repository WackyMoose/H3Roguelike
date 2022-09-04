using GameV1.Interfaces;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Utilities;

namespace GameV1.Entities
{
    public class LightSource : Item, ILightSource
    {
        public int Range { get; set; }
        public Color TintModifier { get; set; }

        public LightSource(int range, Color tintModifier, int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint)
            : base(durability, maxValue, name, spriteCoords, colorTint)
        {
            Range = range;
            TintModifier = tintModifier;
        }

        public void Illuminate(IScene scene)
        {

            var layers = scene.EntityLayers.Keys;

            foreach (var layer in layers)
            {
                var entities = scene.EntityLayers[layer].Entities;

                var entitiesWithinRange = scene.GetEntitiesWithinRange(entities, this.Position, this.Range);

                var maxDistanceSquared = this.Range * this.Range;

                foreach (var entity in entitiesWithinRange)
                {
                    // TODO: Implement true inverse distance squared light intensity
                    // TODO: Optimize algorithm, and cache result for reuse.
                    var distanceSquared = MathFunctions.DistanceSquaredBetween(this.Position, entity.Key);

                    var invLerp = MathFunctions.InverseLerp(maxDistanceSquared, 0, distanceSquared);
                    //var lerp = MathFunctions.Lerp(0, 1, inLerp);

                    var r = (int)(invLerp * TintModifier.R);
                    var g = (int)(invLerp * TintModifier.G);
                    var b = (int)(invLerp * TintModifier.B);

                    var color = new Color(r, g, b, 255);

                    entity.Value.ColorTint += color;
                }
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
