using GameV1.Interfaces;
using MooseEngine.Utilities;
using Raylib_cs;

namespace GameV1.Entities
{
    public class Weapon : Item, IWeapon, IQuickSlottable
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
        public Weapon(int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint) : base(durability, maxValue, name, spriteCoords, colorTint)
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
            //throw new NotImplementedException();
        }

        public override void Update(float deltaTime)
        {
            //throw new NotImplementedException();
        }
        #endregion

    }
}
