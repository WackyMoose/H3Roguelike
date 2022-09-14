using GameV1.Interfaces.Weapons;
using MooseEngine.Graphics;
using MooseEngine.Utilities;

namespace GameV1.Entities.Weapons
{
    // This weapon can be thrown at enemy and be picked up again from the ground, or collected from dead enemy's inventory
    public class ThrowingWeapon : WeaponBase, IThrowingWeapon
    {
        public ThrowingWeapon(int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint) : base(durability, maxValue, name, spriteCoords, colorTint)
        {
        }
    }
}
