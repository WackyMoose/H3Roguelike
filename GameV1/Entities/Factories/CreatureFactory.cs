using GameV1.Entities.Creatures;
using GameV1.Entities.Weapons;
using GameV1.Interfaces.Creatures;
using GameV1.Interfaces.Weapons;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Entities.Factories
{
    public static class CreatureFactory
    {
        public static ICreature CreateCreature<TCreature>(IScene scene, int entityLayerNum, CreatureSpecies species, string name, Coords2D spritePosition, Vector2 position, params int[] collisionLayers) where TCreature : class, ICreature, new()
        {
            IDictionary<CreatureSpecies, Coords2D> creatureSpriteCoords = new Dictionary<CreatureSpecies, Coords2D>()
            {
                { CreatureSpecies.Snake, new Coords2D(4, 1) },
                { CreatureSpecies.Wolf, new Coords2D(5, 1) },
                { CreatureSpecies.Rat, new Coords2D(6, 1) },
                { CreatureSpecies.Spider, new Coords2D(7, 1) },
                { CreatureSpecies.Turtle, new Coords2D(10, 1) },
                { CreatureSpecies.Octopus, new Coords2D(11, 1) },
                { CreatureSpecies.Crab, new Coords2D(12, 0) },
                { CreatureSpecies.Human, new Coords2D(8, 0) },
                { CreatureSpecies.Dwarf, new Coords2D(15, 0) },
                { CreatureSpecies.Orc, new Coords2D(18, 2) }
            };

            IDictionary<CreatureSpecies, MeleeWeapon> defaultWeapons = new Dictionary<CreatureSpecies, MeleeWeapon>()
            {
                { CreatureSpecies.Snake, new MeleeWeapon() { Name = "Poison fangs", Range = 1, MinDamage = 1, MaxDamage = 5 } },
                { CreatureSpecies.Wolf, new MeleeWeapon() { Name = "Fangs", Range = 1, MinDamage = 1, MaxDamage = 5 } },
                { CreatureSpecies.Rat, new MeleeWeapon() { Name = "Teeth", Range = 1, MinDamage = 1, MaxDamage = 5 } },
                { CreatureSpecies.Spider, new MeleeWeapon() { Name = "Poison", Range = 1, MinDamage = 1, MaxDamage = 5 } },
                { CreatureSpecies.Turtle, new MeleeWeapon() { Name = "Beak", Range = 1, MinDamage = 1, MaxDamage = 5 } },
                { CreatureSpecies.Octopus, new MeleeWeapon() { Name = "Tentacles", Range = 1, MinDamage = 1, MaxDamage = 5 } },
                { CreatureSpecies.Crab, new MeleeWeapon() { Name = "Claws", Range = 1, MinDamage = 1, MaxDamage = 5 } },
                { CreatureSpecies.Human, new MeleeWeapon() { Name = "Fist", Range = 1, MinDamage = 1, MaxDamage = 5 } },
                { CreatureSpecies.Dwarf, new MeleeWeapon() { Name = "Fist", Range = 1, MinDamage = 1, MaxDamage = 5 } },
                { CreatureSpecies.Orc, new MeleeWeapon() { Name = "Fist", Range = 1, MinDamage = 10, MaxDamage = 25 } }
            };

            var entityLayer = scene.GetLayer(entityLayerNum);

            // Is position available?
            var validPosition = scene.GetClosestValidPosition(entityLayerNum, position, collisionLayers);

            TCreature? newCreature = entityLayer.ActivateOrCreateEntity<TCreature>(validPosition);

            // Species
            //var speciesNum = Randomizer.RandomInt(0, creatureSpriteCoords.Count - 1);
            //newCreature.SpriteCoords = creatureSpriteCoords.ElementAt(speciesNum).Value;
            //newCreature.Species = creatureSpriteCoords.ElementAt(speciesNum).Key;
            newCreature.SpriteCoords = creatureSpriteCoords.ContainsKey(species) ? creatureSpriteCoords[species] : new Coords2D(4, 1);
            newCreature.Species = species;

            // Stats
            newCreature.Stats.Agility = Randomizer.RandomInt(0, 100);
            newCreature.Stats.Charisma = Randomizer.RandomInt(0, 100);
            newCreature.Stats.Fatigue = Randomizer.RandomInt(0, 100);
            newCreature.Stats.Health = Randomizer.RandomInt(0, 100);
            newCreature.Stats.Perception = Randomizer.RandomInt(0, 100);
            newCreature.Stats.Strength = Randomizer.RandomInt(0, 100);
            newCreature.Stats.Toughness = Randomizer.RandomInt(0, 100);
            
            // Inventory
            newCreature.Inventory.DefaultWeapon = defaultWeapons[species];

            // IEntity
            newCreature.Name = name;
            newCreature.Scale = Vector2.One;
            newCreature.ColorTint = Color.White;

            return newCreature;
        }
    }
}
