using GameV1.Entities.Containers;
using GameV1.Entities.Weapons;
using GameV1.Interfaces.Creatures;
using MooseEngine.Graphics;
using MooseEngine.Scenes;
using MooseEngine.Utilities;

namespace GameV1.Entities.Creatures
{
    public class Creature : Entity, ICreature
    {
        public ICreatureSpecies Species { get; set; }
        public IEnumerable<ICreatureSkill> Skills { get; set; }
        public ICreatureStats Stats { get; set; }
        public ICreatureInventory Inventory { get; set; }
        public IEnumerable<ICreatureSpecies> EnemySpecies { get; set; }
        public IEnumerable<ICreature> EnemyCreatures { get; set; }
        public bool IsDead { get { return Stats.Health <= 0; } }
        public ICreature? TargetCreature { get; set; }

        public Creature(string name, int health, Coords2D spriteCoords, Color colorTint) : base(name, spriteCoords, colorTint)
        {
            Species = new CreatureSpecies();
            Skills = new List<ICreatureSkill>();
            Stats = new CreatureStats();
            Inventory = new CreatureInventory(new MeleeWeapon(0, 0, "Fist", new Coords2D(), Color.White), new Container(8));
            EnemySpecies = new List<ICreatureSpecies>();
            EnemyCreatures = new List<ICreature>();

            Stats.Health = health;
        }

        public Creature(string name, int health, Coords2D spriteCoords) : base(name, spriteCoords)
        {
            Species = new CreatureSpecies();
            Skills = new List<ICreatureSkill>();
            Stats = new CreatureStats();
            Inventory = new CreatureInventory(new MeleeWeapon(0, 0, "Fist", new Coords2D(), Color.White), new Container(8));
            EnemySpecies = new List<ICreatureSpecies>();
            EnemyCreatures = new List<ICreature>();

            Stats.Health = health;
        }

        public void TakeDamage(int damage)
        {
            Stats.Health -= damage;

            if (Stats.Health < 0) { Stats.Health = 0; IsActive = false; }
        }

        public override void Initialize()
        {
        }

        public override void Update(float deltaTime)
        {
        }
    }
}
