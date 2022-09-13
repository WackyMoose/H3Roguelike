using GameV1.Interfaces.Weapons;
using MooseEngine.Graphics;
using MooseEngine.Utilities;

namespace GameV1.Entities.Weapons
{
    internal class Projectile : Weapon, IProjectile
    {
        public Projectile(int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint) : base(durability, maxValue, name, spriteCoords, colorTint)
        {
        }
    }
}
