﻿using MooseEngine.Scenes;
using MooseEngine.Utilities;
using Raylib_cs;
using RPG_V3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Entities
{
    public class Creature : Entity
    {
        public CreatureSpeciesCategory Species { get; set; }
        public CreatureOccupationCategory Occupation { get; set; }
        public int Fatigue { get; set; }
        public int FatigueDrecrease { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Experience { get; set; }
        public int Level { get; set; }
        public int Toughness { get; set; }
        public int Perception { get; set; }
        public int Charisma { get; set; }
        public int MovementPoints { get; set; }
        public int Health { get; set; }
        public int Stamina { get; set; }
        public Container Inventory { get; set; }
        public List<CreatureSpeciesCategory> HostileTowards { get; set; }
        public List<CreatureSpeciesCategory> FriendlyTowards { get; set; }

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
