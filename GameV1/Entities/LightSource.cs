using GameV1.Interfaces;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System.Collections.Generic;
using System.Numerics;

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

        public void Illuminate(IScene scene, IDictionary<Vector2, IEntity> Tiles)
        {
            var TilesWithinRange = scene.GetEntitiesWithinRange(Tiles, this.Position, this.Range);

            var maxDistanceSquared = this.Range * this.Range;

            foreach (var entity in TilesWithinRange)
            {
                // TODO: Implement true inverse distance squared light intensity
                var distanceSquared = MathFunctions.DistanceSquaredBetween(this.Position, entity.Key);

                var inLerp = MathFunctions.InverseLerp(maxDistanceSquared, 0, distanceSquared);
                //var lerp = MathFunctions.Lerp(0, 1, inLerp);

                var r = (int)(inLerp * TintModifier.R);
                var g = (int)(inLerp * TintModifier.G);
                var b = (int)(inLerp * TintModifier.B);

                var color = new Color(r, g, b, 255);

                entity.Value.ColorTint += color;
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
