using GameV1.Interfaces.Weapons;
using MooseEngine.Graphics;
using MooseEngine.Utilities;

namespace GameV1.Entities.Weapons
{
    // This weapon (bow, crossbow, slingshot, blowpipe, etc.) can only be used in conjunction with the right type of projectile (arrow, bolt, rock, poison dart,etc.).
    // After having fired the weapon at an enemy, the projectile can be picked up again on the ground or found in the dead enemy's inventory.
    public class ProjectileWeapon : WeaponBase, IProjectileWeapon
    {
        public ProjectileWeapon(int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint) : base(durability, maxValue, name, spriteCoords, colorTint)
        {
        }
    }
}