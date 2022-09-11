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
        public CreatureSpeciesCategory Species { get; set; }
        public IEnumerable<ISkill> Skills { get; set; }
        public CreatureStats Stats { get; set; }
        public IInventory Inventory { get; set; }
        public IEnumerable<CreatureSpeciesCategory> EnemySpecies { get; set; }
        public IEnumerable<ICreature> EnemyCreatures { get; set; }
        public override bool IsDead { get { return Stats.Health <= 0; } }
        public ISlot<IWeapon> PrimaryWeapon { get; set; }
        public ISlot<IWeapon> SecondaryWeapon { get; set; }
        public ISlot<IArmor> BodyArmor { get; set; }
        public IWeapon? StrongestWeapon => PrimaryWeapon.Item; // MainHand.Item?.Damage > OffHand.Item?.Damage ? MainHand.Item : OffHand.Item;
        public IWeapon DefaultWeapon { get; set; }
        public ICreature? TargetCreature { get; set; }

        public Creature(string name, int health, Coords2D spriteCoords, Color colorTint) : base(name, spriteCoords, colorTint)
        {
            Species = new CreatureSpeciesCategory();
            Skills = new List<Skill>();
            Stats = new CreatureStats();
            Inventory = new Inventory(8, 0, 0);
            EnemySpecies = new List<CreatureSpeciesCategory>();
            EnemyCreatures = new List<Creature>();
            PrimaryWeapon = new Slot<IWeapon>();
            SecondaryWeapon = new Slot<IWeapon>();
            BodyArmor = new Slot<IArmor>();

            Stats.Health = health;
        }

        public Creature(string name, int health, Coords2D spriteCoords) : base(name, spriteCoords)
        {
            Species = new CreatureSpeciesCategory();
            Skills = new List<Skill>();
            Stats = new CreatureStats();
            Inventory = new Inventory(16, 0, 0);
            EnemySpecies = new List<CreatureSpeciesCategory>();
            EnemyCreatures = new List<Creature>();
            PrimaryWeapon = new Slot<IWeapon>();
            SecondaryWeapon = new Slot<IWeapon>();
            BodyArmor = new Slot<IArmor>();

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
