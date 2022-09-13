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
        public static CreatureSpecies Elf { get { return new CreatureSpecies("Elf", 400, 1500); } }
        public static CreatureSpecies Hobgoblin { get { return new CreatureSpecies("Hobgoblin", 200, 100); } }

        // Non-playable Critter species
        public static CreatureSpecies Rabbit { get { return new CreatureSpecies("Rabbit", 1, 1); } }
        public static CreatureSpecies Goat { get { return new CreatureSpecies("Goat", 50, 25); } }
        public static CreatureSpecies Griffin { get { return new CreatureSpecies("Griffin", 2000, 2000); } }
        public static CreatureSpecies Wolf { get { return new CreatureSpecies("Wolf", 1500, 2500); } }
        public static CreatureSpecies Snake { get { return new CreatureSpecies("Snake", 150, 250); } }
        public static CreatureSpecies Bear { get { return new CreatureSpecies("Bear", 500, 500); } }
        public static CreatureSpecies Lion { get { return new CreatureSpecies("Lion", 500, 500); } }

        public static List<CreatureSpecies> List()
        {
            // Added extra Humans to balance out
            return new List<CreatureSpecies>
            {
                Ork, Elf, Human
            };
        }
    }
}
