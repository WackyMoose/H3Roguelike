using GameV1.Interfaces.Weapons;
using MooseEngine.Graphics;
using MooseEngine.Utilities;

namespace GameV1.Entities.Weapons
{
    // This weapon can only be used in close combat, but depending on range you can attack from several tiles away (spear, lance, longsword).
    public class MeleeWeapon : WeaponBase, IMeleeWeapon
    {
        public MeleeWeapon(int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint) : base(durability, maxValue, name, spriteCoords, colorTint)
        {
        }
    }
}
