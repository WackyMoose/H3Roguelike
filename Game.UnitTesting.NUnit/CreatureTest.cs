using GameV1;
using GameV1.Entities;
using GameV1.Interfaces;
using MooseEngine.Utilities;
using Raylib_cs;

namespace Game.UnitTesting.NUnit
{
    public class CreatureTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
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
            Assert.That(resultingHealth, Is.EqualTo(creature.Stats.Health));
        }
    }
}