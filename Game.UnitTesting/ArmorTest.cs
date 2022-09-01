using GameV1;
using GameV1.Entities;
using MooseEngine.Utilities;
using Raylib_cs;

namespace Game.UnitTesting
{
    [TestClass]
    public class ArmorTest
    {
        [TestMethod]
        public void DamageReductionTest()
        {
            // Arrange
            Armor armor = new Armor(100, 100, "", new Coords2D(0, 0), Color.WHITE);

            int minDamageReduction = 10;
            int maxDamageReduction = 100;


            // Act
            int damageReduction = armor.DamageReduction;

            // Assert
            Assert.IsTrue(armor.DamageReduction == damageReduction);
        }
    }
}
