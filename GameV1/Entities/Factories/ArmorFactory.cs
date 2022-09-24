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

namespace GameV1.Entities.Factories
{
    public static class ArmorFactory
    {
        public static IArmor? CreateArmor<TArmor>(IEntityLayer entityLayer, Vector2 position) where TArmor : class, IArmor, new()
        {
            IDictionary<string, Coords2D> headGearSpriteCoords = new Dictionary<string, Coords2D>()
            {
                { "Helmet", new Coords2D(6, 4) },
                { "Hat", new Coords2D(6, 4) }
            };

            IDictionary<string, Coords2D> bodyArmorSpriteCoords = new Dictionary<string, Coords2D>()
            {
                { "Armor", new Coords2D(10, 3) },
                { "Chainmail", new Coords2D(6, 4) }
            };

            IDictionary<string, Coords2D> footWearSpriteCoords = new Dictionary<string, Coords2D>()
            {
                { "Boots", new Coords2D(6, 4) },
                { "Shoes", new Coords2D(6, 4) }
            };

            // check if deactivated weapon instance is available
            TArmor? newArmor = entityLayer.ActivateOrCreateEntity<TArmor>(position);

            // randomize weapon stats, depending on weapon type
            if (newArmor is IHeadGear)
            {
                // IArmor
                newArmor.DamageReduction = Randomizer.RandomInt(0, 100);

                // IEntity
                newArmor.Scale = Vector2.One;
                newArmor.ColorTint = Color.White;

                var spriteNum = Randomizer.RandomInt(0, headGearSpriteCoords.Count - 1);

                newArmor.Name = headGearSpriteCoords.ElementAt(spriteNum).Key;
                newArmor.SpriteCoords = headGearSpriteCoords.ElementAt(spriteNum).Value;

                return newArmor;
            }
            else if (newArmor is IBodyArmor)
            {
                // IArmor
                newArmor.DamageReduction = Randomizer.RandomInt(0, 100);

                // IEntity
                newArmor.Scale = Vector2.One;
                newArmor.ColorTint = Color.White;

                var spriteNum = Randomizer.RandomInt(0, bodyArmorSpriteCoords.Count - 1);

                newArmor.Name = bodyArmorSpriteCoords.ElementAt(spriteNum).Key;
                newArmor.SpriteCoords = bodyArmorSpriteCoords.ElementAt(spriteNum).Value;

                return newArmor;
            }
            else if (newArmor is IFootWear)
            {
                // IArmor
                newArmor.DamageReduction = Randomizer.RandomInt(0, 100);

                // IEntity
                newArmor.Scale = Vector2.One;
                newArmor.ColorTint = Color.White;

                var spriteNum = Randomizer.RandomInt(0, footWearSpriteCoords.Count - 1);

                newArmor.Name = footWearSpriteCoords.ElementAt(spriteNum).Key;
                newArmor.SpriteCoords = footWearSpriteCoords.ElementAt(spriteNum).Value;

                return newArmor;
            }

            return null;
        }
    }
}
