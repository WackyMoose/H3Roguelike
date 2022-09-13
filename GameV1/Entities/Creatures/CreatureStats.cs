using GameV1.Interfaces.Creatures;

namespace GameV1.Entities.Creatures
{
    public class CreatureStats : ICreatureStats
    {
        public CreatureStats()
        {
        }

        public CreatureStats(int fatigue, int fatigueDrecrease, int strength, int agility, int toughness, int perception, int charisma, int health)
        {
            Fatigue = fatigue;
            FatigueDrecrease = fatigueDrecrease;
            Strength = strength;
            Agility = agility;
            Toughness = toughness;
            Perception = perception;
            Charisma = charisma;
            Health = health;
        }

        public int Fatigue { get; set; }
        public int FatigueDrecrease { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Toughness { get; set; }
        public int Perception { get; set; }
        public int Charisma { get; set; }
        public int Health { get; set; }
    }
}
