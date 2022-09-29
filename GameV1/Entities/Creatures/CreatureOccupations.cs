namespace GameV1.Entities.Creatures
{
    public struct CreatureOccupations
    {
        private CreatureOccupations(string name)
        {
            Name = name;
        }

        public string Name { get; }


        // NPC game-critical occupations
        public static CreatureOccupations Blacksmith { get { return new CreatureOccupations("Blacksmith"); } } // Weapons and Armor
        public static CreatureOccupations Merchant { get { return new CreatureOccupations("Merchant"); } } // Consumables, small arms
        public static CreatureOccupations Healer { get { return new CreatureOccupations("Healer"); } } // Healing potions
        public static CreatureOccupations InnKeeper { get { return new CreatureOccupations("InnKeeper"); } } // Consumables

        //
        public static CreatureOccupations Guard { get { return new CreatureOccupations("Guard"); } } // Protects given area against enemies and critters
        public static CreatureOccupations Knight { get { return new CreatureOccupations("Knight"); } } // Stronger version og Guard
        
        
        //
        public static CreatureOccupations Peasant { get { return new CreatureOccupations("Peasant"); } }
        public static CreatureOccupations Miller { get { return new CreatureOccupations("Miller"); } }
        public static CreatureOccupations Thief { get { return new CreatureOccupations("Thief"); } }
        public static CreatureOccupations Robber { get { return new CreatureOccupations("Robber"); } }
        public static CreatureOccupations Rogue { get { return new CreatureOccupations("Rogue"); } }
        public static CreatureOccupations Marauder { get { return new CreatureOccupations("Marauder"); } }
        public static CreatureOccupations Alchemist { get { return new CreatureOccupations("Alchemist"); } }
        public static CreatureOccupations Torturer { get { return new CreatureOccupations("Torturer"); } }
        public static CreatureOccupations Warlock { get { return new CreatureOccupations("Warlock"); } }
        public static CreatureOccupations Warrior { get { return new CreatureOccupations("Warrior"); } }
        
        public static CreatureOccupations Mercenary { get { return new CreatureOccupations("Mercenary"); } }
        public static CreatureOccupations Assasin { get { return new CreatureOccupations("Assasin"); } }
        public static CreatureOccupations Archer { get { return new CreatureOccupations("Archer"); } }
        public static CreatureOccupations Priest { get { return new CreatureOccupations("Priest"); } }
        public static CreatureOccupations Druid { get { return new CreatureOccupations("Druid"); } }

        public static List<CreatureOccupations> List()
        {
            return new List<CreatureOccupations>
            {
                Peasant, Miller, Blacksmith, Merchant, InnKeeper, Thief, Robber, Rogue, Marauder, Alchemist,
                Torturer, Warlock, Warrior, Knight, Mercenary, Assasin, Archer, Priest, Druid
            };
        }
    }
}
