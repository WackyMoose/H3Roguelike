using GameV1.Interfaces.Creatures;

namespace GameV1.Entities.Creatures
{
    public struct CreatureSpecies : ICreatureSpecies
    {
        private CreatureSpecies(string speciesName, int maxHealthPoints, int maxDamagePoints)
        {
            Name = speciesName;
            MaxHealthPoints = maxHealthPoints;
            MaxDamage = maxDamagePoints;
        }

        public string Name { get; }
        public int MaxHealthPoints { get; }
        public int MaxDamage { get; }

        // Playable species
        public static CreatureSpecies Human { get { return new CreatureSpecies("Human", 2000, 2000); } }
        public static CreatureSpecies Ork { get { return new CreatureSpecies("Ork", 700, 1000); } }
        
        // Non-playable Critter species
        public static CreatureSpecies Snake { get { return new CreatureSpecies("Snake", 1, 1); } }
        public static CreatureSpecies Wolf { get { return new CreatureSpecies("Wolf", 50, 25); } }
        public static CreatureSpecies Rat { get { return new CreatureSpecies("Rat", 2000, 2000); } }
        public static CreatureSpecies Spider { get { return new CreatureSpecies("Spider", 1500, 2500); } }
        public static CreatureSpecies Turtle { get { return new CreatureSpecies("Turtle", 1500, 2500); } }
        public static CreatureSpecies Crab { get { return new CreatureSpecies("Crab", 1500, 2500); } }
        public static CreatureSpecies Octopus { get { return new CreatureSpecies("Octopus", 1500, 2500); } }

        public static List<CreatureSpecies> List()
        {
            // Added extra Humans to balance out
            return new List<CreatureSpecies>
            {
                //
            };
        }
    }
}
