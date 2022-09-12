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
        public bool IsDead { get { return Stats.Health <= 0; } }
        public ISlot<IHeadGear> HeadGear { get; set; }
        public ISlot<IWeapon> PrimaryWeapon { get; set; }
        public ISlot<IWeapon> SecondaryWeapon { get; set; }
        public ISlot<IBodyArmor> BodyArmor { get; set; }
        public ISlot<IFootWear> FootWear { get; set; }
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
            
            // Body slots
            HeadGear = new Slot<IHeadGear>();
            PrimaryWeapon = new Slot<IWeapon>();
            SecondaryWeapon = new Slot<IWeapon>();
            BodyArmor = new Slot<IBodyArmor>();
            FootWear = new Slot<IFootWear>();

            Stats.Health = health;
        }

        public Creature(string name, int health, Coords2D spriteCoords) : base(name, spriteCoords)
        {
            Species = new CreatureSpeciesCategory();
            Skills = new List<Skill>();
            Stats = new CreatureStats();
            Inventory = new Inventory(8, 0, 0);
            EnemySpecies = new List<CreatureSpeciesCategory>();
            EnemyCreatures = new List<Creature>();

            // Body slots
            HeadGear = new Slot<IHeadGear>();
            PrimaryWeapon = new Slot<IWeapon>();
            SecondaryWeapon = new Slot<IWeapon>();
            BodyArmor = new Slot<IBodyArmor>();
            FootWear = new Slot<IFootWear>();

            Stats.Health = health;
        }

        public Creature(
            CreatureSpeciesCategory species, 
            IEnumerable<ISkill> skills, 
            CreatureStats stats, 
            IInventory inventory, 
            IEnumerable<CreatureSpeciesCategory> enemySpecies, 
            IEnumerable<ICreature> enemyCreatures, 
            ISlot<IHeadGear> headGear, 
            ISlot<IWeapon> primaryWeapon, 
            ISlot<IWeapon> secondaryWeapon, 
            ISlot<IBodyArmor> bodyArmor, 
            ISlot<IFootWear> footWear, 
            IWeapon defaultWeapon, 
            ICreature? targetCreature)
        {
            Species = species;
            Skills = skills ?? throw new ArgumentNullException(nameof(skills));
            Stats = stats ?? throw new ArgumentNullException(nameof(stats));
            Inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            EnemySpecies = enemySpecies ?? throw new ArgumentNullException(nameof(enemySpecies));
            EnemyCreatures = enemyCreatures ?? throw new ArgumentNullException(nameof(enemyCreatures));
            HeadGear = headGear ?? throw new ArgumentNullException(nameof(headGear));
            PrimaryWeapon = primaryWeapon ?? throw new ArgumentNullException(nameof(primaryWeapon));
            SecondaryWeapon = secondaryWeapon ?? throw new ArgumentNullException(nameof(secondaryWeapon));
            BodyArmor = bodyArmor ?? throw new ArgumentNullException(nameof(bodyArmor));
            FootWear = footWear ?? throw new ArgumentNullException(nameof(footWear));
            DefaultWeapon = defaultWeapon ?? throw new ArgumentNullException(nameof(defaultWeapon));
            TargetCreature = targetCreature;
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
