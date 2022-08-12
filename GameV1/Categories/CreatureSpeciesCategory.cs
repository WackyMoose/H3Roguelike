using System.Collections.Generic;

namespace RPG_V3.Entities
{
    public struct CreatureSpeciesCategory
    {
        private CreatureSpeciesCategory(string speciesName, int maxHealthPoints, int maxDamagePoints)
        {
            Name = speciesName;
            MaxHealthPoints = maxHealthPoints;
            MaxDamage = maxDamagePoints;
        }

        public string Name { get; }
        public int MaxHealthPoints { get; }
        public int MaxDamage { get; }

        public static CreatureSpeciesCategory Human { get { return new CreatureSpeciesCategory("Human", 2000, 2000); } }
        public static CreatureSpeciesCategory Ork { get { return new CreatureSpeciesCategory("Ork", 700, 1000); } }
        public static CreatureSpeciesCategory Elf { get { return new CreatureSpeciesCategory("Elf", 400, 1500); } }
        public static CreatureSpeciesCategory Dwarf { get { return new CreatureSpeciesCategory("Dwarf", 800, 700); } }
        public static CreatureSpeciesCategory Giant { get { return new CreatureSpeciesCategory("Giant", 1800, 1700); } }
        public static CreatureSpeciesCategory Hobgoblin { get { return new CreatureSpeciesCategory("Hobgoblin", 200, 100); } }
        
        // NPC critters
        public static CreatureSpeciesCategory Griffin { get { return new CreatureSpeciesCategory("Griffin", 2000, 2000); } }
        public static CreatureSpeciesCategory Minotaur { get { return new CreatureSpeciesCategory("Minotaur", 1500, 2500); } }
        public static CreatureSpeciesCategory Centaur { get { return new CreatureSpeciesCategory("Centaur", 1500, 2500); } }
        public static CreatureSpeciesCategory Wolf { get { return new CreatureSpeciesCategory("Wolf", 1500, 2500); } }
        public static CreatureSpeciesCategory Goat { get { return new CreatureSpeciesCategory("Goat", 50, 25); } }
        public static CreatureSpeciesCategory Snake { get { return new CreatureSpeciesCategory("Snake", 150, 250); } }
        public static CreatureSpeciesCategory Bear { get { return new CreatureSpeciesCategory("Bear", 500, 500); } }
        public static CreatureSpeciesCategory Lion { get { return new CreatureSpeciesCategory("Lion", 500, 500); } }

        public static List<CreatureSpeciesCategory> List()
        {
            // Added extra Humans to balance out
            return new List<CreatureSpeciesCategory> 
            { 
                Ork, Elf, Dwarf, Giant, Hobgoblin, Griffin, Minotaur, Centaur, Wolf, 
                Goat, Snake, Bear, Lion, Human, Human, Human, Human 
            };
        }

        public static void PrintSpecies()
        {
            //List<EntitySpecies> list = new List<EntitySpecies>();

            //Type type = typeof(EntitySpecies);
            //PropertyInfo[] properties = type. .GetProperties();

            //foreach (PropertyInfo property in properties)
            //{
            //    Console.WriteLine(property.Name);
            //    list.Add(property);
            //}
        }

    }
}
