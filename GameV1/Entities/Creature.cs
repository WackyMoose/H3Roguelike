using GameV1.Categories;
using GameV1.Interfaces;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;

namespace GameV1.Entities
{
    public class Creature : Entity, ICreature
    {
        public ICreatureSpeciesCategory Species { get; set; }
        public IEnumerable<ISkill?> Skills { get; set; }
        public CreatureStats Stats { get; set; }
        public CreatureInventory Inventory { get; set; }
        public IEnumerable<ICreatureSpeciesCategory?> EnemySpecies { get; set; }
        public IEnumerable<ICreature?> EnemyCreatures { get; set; }
        public bool IsDead { get { return Stats.Health <= 0; } }
        public ICreature? TargetCreature { get; set; }

        public Creature(string name, int health, Coords2D spriteCoords, Color colorTint) : base(name, spriteCoords, colorTint)
        {
            Species = new CreatureSpeciesCategory();
            Skills = new List<ISkill>();
            Stats = new CreatureStats();
            Inventory = new CreatureInventory(new Weapon(0, 0, "Fist", new Coords2D(), Color.White), new Inventory(8, 0, 0));
            EnemySpecies = new List<ICreatureSpeciesCategory>();
            EnemyCreatures = new List<ICreature>();
            
            Stats.Health = health;
        }

        public Creature(string name, int health, Coords2D spriteCoords) : base(name, spriteCoords)
        {
            Species = new CreatureSpeciesCategory();
            Skills = new List<ISkill>();
            Stats = new CreatureStats();
            Inventory = new CreatureInventory(new Weapon(0, 0, "Fist", new Coords2D(), Color.White), new Inventory(8, 0, 0));
            EnemySpecies = new List<ICreatureSpeciesCategory>();
            EnemyCreatures = new List<ICreature>();

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
