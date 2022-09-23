using GameV1.Commands;
using GameV1.Entities.Weapons;
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
    public static class WeaponFactory
    {
        public static IMeleeWeapon CreateMeleeWeapon(IEntityLayer entityLayer, Vector2 position)
        {
            IDictionary<string, Coords2D> meleeWeaponSpriteCoords = new Dictionary<string, Coords2D>()
            {
                { "Club", new Coords2D(10, 3) },
                { "Sword", new Coords2D(6, 4) },
                { "Double Axe", new Coords2D(7, 4) },
                { "Trident", new Coords2D(10, 4) }
            };

            // check if deactivated weapon instance is available
            MeleeWeapon? newWeapon = entityLayer.GetFirstInactiveEntityOfType<MeleeWeapon>();

            if (newWeapon != null)
            {
                newWeapon.Position = position;

                entityLayer.ActivateEntity(newWeapon);
            }
            else
            {
                newWeapon = new MeleeWeapon();

                newWeapon.IsActive = true;
                newWeapon.Position = position;

                entityLayer.AddEntity(newWeapon);
            }

            // IWeaopn
            newWeapon.Range = 1;
            newWeapon.CriticalChance = Randomizer.RandomInt(0, 100);
            newWeapon.CriticalDamage = Randomizer.RandomInt(0, 100);
            newWeapon.MinDamage = Randomizer.RandomInt(0, 100);
            newWeapon.MaxDamage = newWeapon.MinDamage + Randomizer.RandomInt(0, 100 - newWeapon.MinDamage);
            newWeapon.ArmorPenetrationFlat = Randomizer.RandomInt(0, 100);
            newWeapon.ArmorPenetrationChance = Randomizer.RandomInt(0, 100);

            // IEntity
            newWeapon.Scale = Vector2.One;
            newWeapon.ColorTint = Color.White;
            var spriteNum = Randomizer.RandomInt(0, meleeWeaponSpriteCoords.Count - 1);

            newWeapon.Name = meleeWeaponSpriteCoords.ElementAt(spriteNum).Key;
            newWeapon.SpriteCoords = meleeWeaponSpriteCoords.ElementAt(spriteNum).Value;
            
            return newWeapon;
        }

        
        public static IWeapon? CreateWeapon<TWeapon>(IEntityLayer entityLayer, Vector2 position) where TWeapon : class, IWeapon, new()
        {
            IDictionary<string, Coords2D> meleeWeaponSpriteCoords = new Dictionary<string, Coords2D>()
            {
                { "Club", new Coords2D(10, 3) },
                { "Sword", new Coords2D(6, 4) },
                { "Double Axe", new Coords2D(7, 4) },
                { "Trident", new Coords2D(10, 4) }
            };

            // check if deactivated weapon instance is available
            TWeapon? newWeapon = entityLayer.GetFirstInactiveEntityOfType<TWeapon>();

            if (newWeapon != null)
            {
                newWeapon.Position = position;

                entityLayer.ActivateEntity(newWeapon);
            }
            else
            {
                newWeapon = new TWeapon();

                newWeapon.IsActive = true;
                newWeapon.Position = position;

                entityLayer.AddEntity(newWeapon);
            }


            // randomize weapon stats, depending on weapon type
            if (newWeapon is IMeleeWeapon)
            {
                // IWeaopn
                newWeapon.Range = 1;
                newWeapon.CriticalChance = Randomizer.RandomInt(0, 100);
                newWeapon.CriticalDamage = Randomizer.RandomInt(0, 100);
                newWeapon.MinDamage = Randomizer.RandomInt(0, 100);
                newWeapon.MaxDamage = newWeapon.MinDamage + Randomizer.RandomInt(0, 100 - newWeapon.MinDamage);
                newWeapon.ArmorPenetrationFlat = Randomizer.RandomInt(0, 100);
                newWeapon.ArmorPenetrationChance = Randomizer.RandomInt(0, 100);

                // IEntity
                newWeapon.Scale = Vector2.One;
                newWeapon.ColorTint = Color.White;
                
                var spriteNum = Randomizer.RandomInt(0, meleeWeaponSpriteCoords.Count - 1);

                newWeapon.Name = meleeWeaponSpriteCoords.ElementAt(spriteNum).Key;
                newWeapon.SpriteCoords = meleeWeaponSpriteCoords.ElementAt(spriteNum).Value;
                
                return newWeapon;
            }
            else if (newWeapon is IThrowingWeapon)
            {
                // IWeaopn
                newWeapon.Range = Randomizer.RandomInt(2, 4);
                newWeapon.CriticalChance = Randomizer.RandomInt(0, 100);
                newWeapon.CriticalDamage = Randomizer.RandomInt(0, 100);
                newWeapon.MinDamage = Randomizer.RandomInt(0, 100);
                newWeapon.MaxDamage = newWeapon.MinDamage + Randomizer.RandomInt(0, 100 - newWeapon.MinDamage);
                newWeapon.ArmorPenetrationFlat = Randomizer.RandomInt(0, 100);
                newWeapon.ArmorPenetrationChance = Randomizer.RandomInt(0, 100);

                // IEntity
                newWeapon.Scale = Vector2.One;
                newWeapon.ColorTint = Color.White;

                return newWeapon;
            }
            else if (newWeapon is IProjectileWeapon)
            {
                IProjectileWeapon? projectileWeapon = newWeapon as IProjectileWeapon;

                return projectileWeapon;
            }

            return null;
        }
    }
}
