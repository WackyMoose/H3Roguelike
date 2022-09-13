using GameV1.Interfaces.Weapons;
using MooseEngine.Graphics;
using MooseEngine.Utilities;

namespace GameV1.Entities.Weapons
{
    public class MeleeWeapon : Weapon, IMeleeWeapon
    {
        public MeleeWeapon(int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint) : base(durability, maxValue, name, spriteCoords, colorTint)
        {
        }
    }
}
