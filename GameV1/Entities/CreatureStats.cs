namespace GameV1.Entities
{
    public class CreatureStats
    {
        public CreatureStats()
        {
        }

        public CreatureStats(int fatigue, int fatigueDrecrease, int strength, int agility, int toughness, int perception, int charisma, int movementPoints, int health, int stamina)
        {
            Fatigue = fatigue;
            FatigueDrecrease = fatigueDrecrease;
            Strength = strength;
            Agility = agility;
            Toughness = toughness;
            Perception = perception;
            Charisma = charisma;
            MovementPoints = movementPoints;
            Health = health;
            Stamina = stamina;
        }

        public int Fatigue { get; set; }
        public int FatigueDrecrease { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Toughness { get; set; }
        public int Perception { get; set; }
        public int Charisma { get; set; }
        public int MovementPoints { get; set; }
        public int Health { get; set; }
        public int Stamina { get; set; }
    }
}
