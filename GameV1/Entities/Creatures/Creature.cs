using GameV1.Entities.Containers;
using GameV1.Entities.Weapons;
using GameV1.Interfaces.Creatures;
using GameV1.Interfaces.Items;
using MooseEngine.BehaviorTree.Interfaces;
using MooseEngine.Graphics;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System.Numerics;

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
        public IDictionary<Vector2, ICreature> CreaturesWithinPerceptionRange { get; set; }
        public ICreature? TargetCreature { get; set; }
        public IItem? TargetItem { get; set; }
        public IBehaviorTree? BehaviorTree { get; set; }
        public bool IsDead { get { return Stats.Health <= 0; } }

        public Creature(
            ICreatureSpecies species, 
            IEnumerable<ICreatureSkill> skills, 
            ICreatureStats stats, 
            ICreatureInventory inventory, 
            IEnumerable<ICreatureSpecies> enemySpecies, 
            IEnumerable<ICreature> enemyCreatures, 
            IDictionary<Vector2, ICreature> creaturesWithinPerceptionRange, 
            ICreature? targetCreature, 
            IItem? targetItem, 
            IBehaviorTree? behaviorTree)
        {
            Species = species;
            Skills = skills;
            Stats = stats;
            Inventory = inventory;
            EnemySpecies = enemySpecies;
            EnemyCreatures = enemyCreatures;
            CreaturesWithinPerceptionRange = creaturesWithinPerceptionRange;
            TargetCreature = targetCreature;
            TargetItem = targetItem;
            BehaviorTree = behaviorTree;
        }

        public Creature()
        {
            Species = new CreatureSpecies();
            Skills = new List<ICreatureSkill>();
            Stats = new CreatureStats();
            Inventory = new CreatureInventory(new MeleeWeapon(0, 0, "Fist", new Coords2D(), Color.White), new Container(ContainerType.Inventory, 10, "Inventory"));
            EnemySpecies = new List<ICreatureSpecies>();
            EnemyCreatures = new List<ICreature>();
            CreaturesWithinPerceptionRange = new Dictionary<Vector2, ICreature>();
        }

        public Creature(string name, int health, Coords2D spriteCoords, Color colorTint) : base(name, spriteCoords, colorTint)
        {
            Species = new CreatureSpecies();
            Skills = new List<ICreatureSkill>();
            Stats = new CreatureStats();
            Inventory = new CreatureInventory(new MeleeWeapon(0, 0, "Fist", new Coords2D(), Color.White), new Container(ContainerType.Inventory, 10, "Inventory"));
            EnemySpecies = new List<ICreatureSpecies>();
            EnemyCreatures = new List<ICreature>();
            CreaturesWithinPerceptionRange = new Dictionary<Vector2, ICreature>();

            Stats.Health = health;
        }

        public Creature(string name, int health, Coords2D spriteCoords) : base(name, spriteCoords)
        {
            Species = new CreatureSpecies();
            Skills = new List<ICreatureSkill>();
            Stats = new CreatureStats();
            Inventory = new CreatureInventory(new MeleeWeapon(0, 0, "Fist", new Coords2D(), Color.White), new Container(ContainerType.Inventory, 10, "Inventory"));
            EnemySpecies = new List<ICreatureSpecies>();
            EnemyCreatures = new List<ICreature>();
            CreaturesWithinPerceptionRange = new Dictionary<Vector2, ICreature>();

            Stats.Health = health;
        }

        public void TakeDamage(int damage)
        {
            Stats.Health -= damage;

            if (Stats.Health < 0)
            {
                Stats.Health = 0; 
                IsActive = false;
            }
            else if (Stats.Health > 0)
            {
                IsActive = true;
            }
        }

        public override void Initialize()
        {
        }

        public override void Update(float deltaTime)
        {
        }
    }
}
