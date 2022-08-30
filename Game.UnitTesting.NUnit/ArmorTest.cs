using GameV1.Entities;
using MooseEngine.Utilities;
using Raylib_cs;

namespace Game.UnitTesting.NUnit
{
    public class ArmorTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void DamageReductionTest()
        {
            // Arrange
            Armor armor = new Armor(100, 100, "", new Coords2D(0, 0), Color.WHITE);

            int minDamageReduction = 10;
            int maxDamageReduction = 100;

            armor.MinDamageReduction = minDamageReduction;
            armor.MaxDamageReduction = maxDamageReduction;

            // Act
            int damageReduction = armor.DamageReduction;

            // Assert
            Assert.That(damageReduction, Is.InRange(minDamageReduction, maxDamageReduction));
        }
    }
}
