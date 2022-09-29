using GameV1.Interfaces.Items;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Utilities;
using System.Numerics;

namespace GameV1.Entities.Items
{
    public class LightSource : ItemBase, ILightSource
    {
        public int Range { get; set; }
        public Color TintModifier { get; set; }
        public IDictionary<Vector2, Color> Tints { get; set; }

        public LightSource(int range, Color tintModifier, int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint)
            : base(durability, maxValue, name, spriteCoords, colorTint)
        {
            Range = range;
            TintModifier = tintModifier;
            Tints = new Dictionary<Vector2, Color>();

            Tints = CreateTints(Range, TintModifier);
        }

        public IDictionary<Vector2, Color> CreateTints(int range, Color tint)
        {
            var center = Vector2.Zero;
            var topLft = new Vector2(-range, -range);
            var btmRgt = new Vector2(range, range);
            var maxDistanceSquared = range * range;
            var v = new Vector2();

            IDictionary<Vector2, Color> tints = new Dictionary<Vector2, Color>();

            for (v.Y = topLft.Y; v.Y <= btmRgt.Y; v.Y += Constants.DEFAULT_ENTITY_SIZE)
            {
                for (v.X = topLft.X; v.X <= btmRgt.X; v.X += Constants.DEFAULT_ENTITY_SIZE)
                {
                    var distanceSquared = MathFunctions.DistanceSquaredBetween(center, v);
                    //var distanceSquared = Vector2.DistanceSquared(this.Position, entity.Key);

                    var invLerp = MathFunctions.InverseLerp(maxDistanceSquared, 0, distanceSquared);
                    //var lerp = MathFunctions.Lerp(0, 1, inLerp);

                    var r = (int)(invLerp * tint.R);
                    var g = (int)(invLerp * tint.G);
                    var b = (int)(invLerp * tint.B);

                    var color = new Color(r, g, b, 255);

                    tints[v] = color;
                }
            }

            return tints;
        }

        public void Illuminate(IScene scene)
        {
            var layers = scene.EntityLayers.Keys;

            foreach (var layer in layers)
            {
                var entities = scene.EntityLayers[layer].ActiveEntities;

                //var entitiesWithinRange = scene.GetEntitiesWithinCircle(entities, this.Position, this.Range);
                var entitiesWithinRange = scene.GetEntitiesWithinRectangle(
                    entities,
                    Position - new Vector2(Range, Range),
                    Position + new Vector2(Range, Range));

                foreach (var entity in entitiesWithinRange)
                {
                    entity.Value.ColorTint += Tints[entity.Key - Position];
                }

                //foreach(var tint in Tints)
                //{
                //    if(entitiesWithinRange.ContainsKey(tint.Key + Position))
                //    {
                //        entitiesWithinRange[tint.Key + Position].ColorTint += tint.Value;
                //    }
                //}
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
