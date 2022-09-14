using GameV1.Entities.Items;
using GameV1.Interfaces.Weapons;
using MooseEngine.Graphics;
using MooseEngine.Utilities;

namespace GameV1.Entities.Weapons
{
    public abstract class WeaponBase : Item, IWeapon
    {
        #region Properties
        public int Range { get; set; }
        public int Damage => Randomizer.RandomInt(MinDamage, MaxDamage);
        public int CriticalChance { get; set; }
        public int CriticalDamage { get; set; }
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int ArmorPenetrationFlat { get; set; }
        public int ArmorPenetrationPercent { get; set; }
        #endregion

        #region Constructors
        public WeaponBase(int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint)
            : base(durability, maxValue, name, spriteCoords, colorTint)
        {
        }
        #endregion

        public int DoDamage()
        {
            return IsCritical() ? Damage * CriticalChance : Damage;
        }

        private bool IsCritical()
        {
            return Randomizer.RandomPercent() <= CriticalChance;
        }

        #region Methods
        public override void Initialize()
        {
        }

        public override void Update(float deltaTime)
        {
        }
        #endregion

    }
}
