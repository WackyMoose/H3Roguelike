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
        public static IWeapon? CreateWeapon<TWeapon>(IEntityLayer entityLayer, Vector2 position) where TWeapon : class, IWeapon, new()
        {
            // check if deactivated weapon instance is available
            TWeapon? newWeapon = entityLayer.GetFirstInactiveEntityOfType<TWeapon>();

            // if not, create a new instance
            if (newWeapon == null)
            {
                newWeapon = new TWeapon();
                
                newWeapon.IsActive = true;
                newWeapon.Position = position;
                
                entityLayer.AddEntity(newWeapon);
            }
            else
            {
                newWeapon.Position = position;
                
                entityLayer.ActivateEntity(newWeapon);
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

                // IItem

                // IEntity
                newWeapon.Scale = Vector2.One;
                newWeapon.ColorTint = Color.White;
                    
                return newWeapon;
            }
            else if (newWeapon is IThrowingWeapon)
            {
                IThrowingWeapon? throwingWeapon = newWeapon as IThrowingWeapon;

                if(throwingWeapon is not null)
                {
                    throwingWeapon.Range = Randomizer.RandomInt(2, 4);
                    throwingWeapon.CriticalChance = Randomizer.RandomInt(0, 100);
                    throwingWeapon.CriticalDamage = Randomizer.RandomInt(0, 100);
                    throwingWeapon.MinDamage = Randomizer.RandomInt(0, 100);
                    throwingWeapon.MaxDamage = throwingWeapon.MinDamage + Randomizer.RandomInt(0, 100 - throwingWeapon.MinDamage);
                    throwingWeapon.ArmorPenetrationFlat = Randomizer.RandomInt(0, 100);
                    throwingWeapon.ArmorPenetrationChance = Randomizer.RandomInt(0, 100);

                    return throwingWeapon;
                }
                
                

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
