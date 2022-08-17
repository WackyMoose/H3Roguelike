﻿using GameV1.Categories;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using Raylib_cs;

namespace GameV1.Entities
{
    public class Creature : Entity
    {
        public CreatureSpeciesCategory Species { get; set; }
        public List <Skill> Skills { get; set; }
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
        public Inventory Inventory { get; set; }
        public List<CreatureSpeciesCategory> HostileTowards { get; set; }
        public bool IsDead { get { return Health <= 0; } }
        public Slot<Weapon> MainHand { get; set; }
        public Slot<Weapon> OffHand { get; set; }
        public Slot<Armor> Chest { get; set; }

        public Creature(string name, int movementPoints, int health, Coords2D spriteCoords, Color colorTint) : base(name, spriteCoords, colorTint)
        {
            MovementPoints = movementPoints;
            Health = health;

            Species = new CreatureSpeciesCategory();
            Skills = new List<Skill>();
            Inventory = new Inventory(16, -1, 0);
            HostileTowards = new List<CreatureSpeciesCategory>();
            MainHand = new Slot<Weapon>();
            OffHand = new Slot<Weapon>();
            Chest = new Slot<Armor>();
        }

        public Creature(string name, int movementPoints, int health, Coords2D spriteCoords) : base(name, spriteCoords)
        {
            MovementPoints = movementPoints;
            Health = health;

            Species = new CreatureSpeciesCategory();
            Skills = new List<Skill>();
            Inventory = new Inventory(16, -1, 12);
            HostileTowards = new List<CreatureSpeciesCategory>();
            MainHand = new Slot<Weapon>();
            OffHand = new Slot<Weapon>();
            Chest = new Slot<Armor>();
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
