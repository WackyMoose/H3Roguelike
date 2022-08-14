namespace GameV1.Categories
{
    public struct CreatureOccupationCategory
    {
        private CreatureOccupationCategory(string name) 
        { 
            Name = name; 
        }

        public string Name { get; }
        

        // NPC game-critical occupations
        public static CreatureOccupationCategory Blacksmith { get { return new CreatureOccupationCategory("Blacksmith"); } }
        public static CreatureOccupationCategory Merchant { get { return new CreatureOccupationCategory("Merchant"); } }
        public static CreatureOccupationCategory Healer { get { return new CreatureOccupationCategory("Healer"); } }

        //
        public static CreatureOccupationCategory InnKeeper { get { return new CreatureOccupationCategory("InnKeeper"); } }
        public static CreatureOccupationCategory Peasant { get { return new CreatureOccupationCategory("Peasant"); } }
        public static CreatureOccupationCategory Miller { get { return new CreatureOccupationCategory("Miller"); } }
        public static CreatureOccupationCategory Thief { get { return new CreatureOccupationCategory("Thief"); } }
        public static CreatureOccupationCategory Robber { get { return new CreatureOccupationCategory("Robber"); } }
        public static CreatureOccupationCategory Rogue { get { return new CreatureOccupationCategory("Rogue"); } }
        public static CreatureOccupationCategory Marauder { get { return new CreatureOccupationCategory("Marauder"); } }
        public static CreatureOccupationCategory Alchemist { get { return new CreatureOccupationCategory("Alchemist"); } }
        public static CreatureOccupationCategory Torturer { get { return new CreatureOccupationCategory("Torturer"); } }
        public static CreatureOccupationCategory Warlock { get { return new CreatureOccupationCategory("Warlock"); } }
        public static CreatureOccupationCategory Warrior { get { return new CreatureOccupationCategory("Warrior"); } }
        public static CreatureOccupationCategory Knight { get { return new CreatureOccupationCategory("Knight"); } }
        public static CreatureOccupationCategory Mercenary { get { return new CreatureOccupationCategory("Mercenary"); } }
        public static CreatureOccupationCategory Assasin { get { return new CreatureOccupationCategory("Assasin"); } }
        public static CreatureOccupationCategory Archer { get { return new CreatureOccupationCategory("Archer"); } }
        public static CreatureOccupationCategory Priest { get { return new CreatureOccupationCategory("Priest"); } }
        public static CreatureOccupationCategory Druid { get { return new CreatureOccupationCategory("Druid"); } }

        public static List<CreatureOccupationCategory> List()
        {
            return new List<CreatureOccupationCategory> 
            { 
                Peasant, Miller, Blacksmith, Merchant, InnKeeper, Thief, Robber, Rogue, Marauder, Alchemist,
                Torturer, Warlock, Warrior, Knight, Mercenary, Assasin, Archer, Priest, Druid
            };
        }
    }
}
