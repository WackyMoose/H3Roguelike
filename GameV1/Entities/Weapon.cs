using MooseEngine.Scenes;
using MooseEngine.Utilities;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Entities
{
    internal class Weapon : Item
    {
        #region Properties
        public int Range { get; set; }
        public int Damage { get; set; }
        public int CriticalChance { get; set; }
        public int CriticalDamage { get; set; }
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        #endregion

        #region Constructors
        public Weapon(int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint) : base(durability, maxValue, name, spriteCoords, colorTint)
        {
        }
        #endregion

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
