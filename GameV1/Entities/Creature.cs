using GameV1.Categories;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using Raylib_cs;

namespace GameV1.Entities
{
    public class Creature : Entity
    {
        public CreatureSpeciesCategory Species { get; set; } = new CreatureSpeciesCategory();
        public CreatureOccupationCategory Occupation { get; set; } = new CreatureOccupationCategory();
        public List <CreatureSkillCategory> Skills { get; set; } = new List<CreatureSkillCategory> ();
        public int Fatigue { get; set; }
        public int FatigueDrecrease { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Toughness { get; set; }
        public int Perception { get; set; }
        public int Charisma { get; set; }
        public int MovementPoints { get; set; }
        public int Health { get; set; }
        public int Stamina { get; set; }
        public Inventory Inventory { get; set; } = new Inventory(-1, 12);
        public List<CreatureSpeciesCategory> HostileTowards { get; set; } = new List<CreatureSpeciesCategory> ();
        public bool IsDead { get { return Health <= 0; } }

        public Creature(string name, int movementPoints, int health, Coords2D spriteCoords, Color colorTint) : base(name, spriteCoords, colorTint)
        {
            MovementPoints = movementPoints;
            Health = health;
        }

        public Creature(string name, int movementPoints, int health, Coords2D spriteCoords) : base(name, spriteCoords)
        {
            MovementPoints = movementPoints;
            Health = health;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public override void Initialize()
        {
            //throw new NotImplementedException();
        }

        public override void Update(float deltaTime)
        {
            //throw new NotImplementedException();
        }
    }
}
