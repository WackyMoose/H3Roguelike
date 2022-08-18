using GameV1;
using GameV1.Entities;
using MooseEngine.Utilities;
using Raylib_cs;

namespace Game.UnitTesting
{
    [TestClass]
    public class CreatureTest
    {
        [TestMethod]
        public void Creature_TakeDamage_Inflict_10_Damage()
        {
            // Arrange
            int damageInflicted = 10;
            int originalHealth = 1000;
            int resultingHealth = 990;
            Creature creature = new Creature("Test Creature", 100, originalHealth, new Coords2D(0, 0), Color.WHITE);

            // Act
            creature.TakeDamage(damageInflicted);

            // Assert
            Assert.AreEqual(resultingHealth, creature.Stats.Health);
        }
    }
}