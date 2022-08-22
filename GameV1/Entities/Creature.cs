using GameV1.Categories;
using GameV1.Interfaces;
using MooseEngine.Graphics;
using MooseEngine.Scenes;
using MooseEngine.Utilities;

namespace GameV1.Entities
{
    public class Creature : Entity, ICreature
    {
        public CreatureSpeciesCategory Species { get; set; }
        public List<Skill> Skills { get; set; }
        public CreatureStats Stats { get; set; }
        public Inventory Inventory { get; set; }
        public List<CreatureSpeciesCategory> HostileTowards { get; set; }
        public bool IsDead { get { return Stats.Health <= 0; } }
        public Slot<Weapon> MainHand { get; set; }
        public Slot<Weapon> OffHand { get; set; }
        public Slot<Armor> Chest { get; set; }

        public Creature(string name, int movementPoints, int health, Coords2D spriteCoords, Color colorTint) : base(name, spriteCoords, colorTint)
        {
            Species = new CreatureSpeciesCategory();
            Skills = new List<Skill>();
            Stats = new CreatureStats();
            Inventory = new Inventory(16, 0, 0);
            HostileTowards = new List<CreatureSpeciesCategory>();
            MainHand = new Slot<Weapon>();
            OffHand = new Slot<Weapon>();
            Chest = new Slot<Armor>();

            Stats.MovementPoints = movementPoints;
            Stats.Health = health;
        }

        public Creature(string name, int movementPoints, int health, Coords2D spriteCoords) : base(name, spriteCoords)
        {
            Species = new CreatureSpeciesCategory();
            Skills = new List<Skill>();
            Stats = new CreatureStats();
            Inventory = new Inventory(16, 0, 0);
            HostileTowards = new List<CreatureSpeciesCategory>();
            MainHand = new Slot<Weapon>();
            OffHand = new Slot<Weapon>();
            Chest = new Slot<Armor>();

            Stats.MovementPoints = movementPoints;
            Stats.Health = health;
        }

        public void TakeDamage(int damage)
        {
            Stats.Health -= damage;
        }

        public override void Initialize()
        {
        }

        public override void Update(float deltaTime)
        {
        }
    }
}
