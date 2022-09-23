using GameV1.Entities.Creatures;
using GameV1.Interfaces.Creatures;
using GameV1.Interfaces.Weapons;
using MooseEngine.Graphics;
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
        public static ICreature CreateCreature<TCreature>(IEntityLayer entityLayer, CreatureSpecies species, Vector2 position) where TCreature : class, ICreature, new()
        {
            IDictionary<CreatureSpecies, Coords2D> creatureSpriteCoords = new Dictionary<CreatureSpecies, Coords2D>()
            {
                { CreatureSpecies.Snake, new Coords2D(4, 1) },
                { CreatureSpecies.Wolf, new Coords2D(5, 1) },
                { CreatureSpecies.Rat, new Coords2D(6, 1) },
                { CreatureSpecies.Spider, new Coords2D(7, 1) },
                { CreatureSpecies.Turtle, new Coords2D(10, 1) },
                { CreatureSpecies.Octopus, new Coords2D(11, 1) },
                { CreatureSpecies.Crab, new Coords2D(12, 0) }
            };

            // check if deactivated weapon instance is available
            TCreature? newCreature = entityLayer.GetFirstInactiveEntityOfType<TCreature>();
            if (newCreature != null)
            {
                newCreature.Position = position;

                entityLayer.ActivateEntity(newCreature);
            }
            else
            {
                newCreature = new TCreature();

                newCreature.IsActive = true;
                newCreature.Position = position;

                entityLayer.AddEntity(newCreature);
            }

            // Species
            //var speciesNum = Randomizer.RandomInt(0, creatureSpriteCoords.Count - 1);
            //newCreature.SpriteCoords = creatureSpriteCoords.ElementAt(speciesNum).Value;
            //newCreature.Species = creatureSpriteCoords.ElementAt(speciesNum).Key;
            newCreature.SpriteCoords = creatureSpriteCoords.ContainsKey(species) ? creatureSpriteCoords[species] : new Coords2D(4, 1);
            newCreature.Species = species;

            newCreature.Name = $"{newCreature.Species.Name}";

            // Stats
            newCreature.Stats.Agility = Randomizer.RandomInt(0, 100);
            newCreature.Stats.Charisma = Randomizer.RandomInt(0, 100);
            newCreature.Stats.Fatigue = Randomizer.RandomInt(0, 100);
            newCreature.Stats.Health = Randomizer.RandomInt(0, 100);
            newCreature.Stats.Perception = Randomizer.RandomInt(0, 100);
            newCreature.Stats.Strength = Randomizer.RandomInt(0, 100);
            newCreature.Stats.Toughness = Randomizer.RandomInt(0, 100);

            // IEntity
            newCreature.Scale = Vector2.One;
            newCreature.ColorTint = Color.White;

            return newCreature;

        }
    }
}
