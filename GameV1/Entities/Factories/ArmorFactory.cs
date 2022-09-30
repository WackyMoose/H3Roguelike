using GameV1.Interfaces.Armors;
using GameV1.Interfaces.Weapons;
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
using static System.Formats.Asn1.AsnWriter;

namespace GameV1.Entities.Factories
{
    public static class ArmorFactory
    {
        public static IArmor? CreateArmor<TArmor>(IScene scene, int entityLayerNum, Vector2 position, params int[] collisionLayers) where TArmor : class, IArmor, new()
        {
            IDictionary<string, Coords2D> headGearSpriteCoords = new Dictionary<string, Coords2D>()
            {
                { "Steel Helmet", new Coords2D(17, 7) },
                { "Iron Helmet", new Coords2D(17, 8) },
                { "Bronze Helmet", new Coords2D(17, 9) }
            };

            IDictionary<string, Coords2D> bodyArmorSpriteCoords = new Dictionary<string, Coords2D>()
            {
                { "Iron Armor", new Coords2D(16, 7) },
                { "Leather Armor", new Coords2D(16, 8) },
                { "Bronze Armor", new Coords2D(16, 9) },
                { "Steel Chainmail", new Coords2D(18, 7) },
                { "Iron Chainmail", new Coords2D(18, 8) },
                { "Bronze Chainmail", new Coords2D(18, 9) }
            };

            IDictionary<string, Coords2D> footWearSpriteCoords = new Dictionary<string, Coords2D>()
            {
                { "Steel Boots", new Coords2D(19, 7) },
                { "Leather Boots", new Coords2D(19, 8) },
                { "Bronze Boots", new Coords2D(19, 9) },
            };

            var entityLayer = scene.GetLayer(entityLayerNum);

            // Is position available?
            var validPosition = scene.GetClosestValidPosition(entityLayerNum, position, collisionLayers);

            // check if deactivated weapon instance is available
            TArmor? newArmor = entityLayer.ActivateOrCreateEntity<TArmor>(validPosition);

            // IArmor
            newArmor.DamageReduction = Randomizer.RandomInt(0, 100);

            // IEntity
            newArmor.Scale = Vector2.One;
            newArmor.ColorTint = Color.White;

            // randomize weapon stats, depending on weapon type
            if (newArmor is IHeadGear)
            {
                var spriteNum = Randomizer.RandomInt(0, headGearSpriteCoords.Count - 1);

                newArmor.Name = headGearSpriteCoords.ElementAt(spriteNum).Key;
                newArmor.SpriteCoords = headGearSpriteCoords.ElementAt(spriteNum).Value;

                return newArmor;
            }
            else if (newArmor is IBodyArmor)
            {
                var spriteNum = Randomizer.RandomInt(0, bodyArmorSpriteCoords.Count - 1);

                newArmor.Name = bodyArmorSpriteCoords.ElementAt(spriteNum).Key;
                newArmor.SpriteCoords = bodyArmorSpriteCoords.ElementAt(spriteNum).Value;

                return newArmor;
            }
            else if (newArmor is IFootWear)
            {
                var spriteNum = Randomizer.RandomInt(0, footWearSpriteCoords.Count - 1);

                newArmor.Name = footWearSpriteCoords.ElementAt(spriteNum).Key;
                newArmor.SpriteCoords = footWearSpriteCoords.ElementAt(spriteNum).Value;

                return newArmor;
            }

            return null;
        }
    }
}
