﻿using GameV1.Commands.Factory;
using GameV1.Entities.Creatures;
using GameV1.Interfaces.Creatures;
using MooseEngine.Core;
using MooseEngine.Interfaces;

namespace GameV1
{
    public static class AI
    {
        public static void Execute(IScene scene, ICreature player)
        {
            // TODO: Add Initializer that does player look-up once.
            //var player = scene.GetEntitiesOfType<Player>(scene.Tiles).FirstOrDefault();
            //var npcs = scene.GetEntitiesOfType<Npc>(scene.Tiles).ToList();

            var creatureLayer = scene.GetLayer((int)EntityLayer.Creatures);

            var npcs = creatureLayer.GetEntitiesOfType<Creature>();

            foreach (var npc in npcs)
            {
                // Check if loot or lootables are in sight
                // If yes: Walk towards, Pick up loot, or loot lootable.

                // Check if enemies are in sight
                // If yes: Walk towards or attack enemy


                // TODO: Refactor. This doesn't belong here and can't be implemented with single layer
                // Check for dead creatures
                //ICommand maintenanceCommand = new CommandSwapDeadCreaturesWithCorpse(creatureLayer, npc);
                //CommandQueue.Add(maintenanceCommand);

                // Generate random walk command
                var input = GenerateRandomInput();

                ICommand command = CommandFactory.Create(input, scene, npc);
                CommandQueue.Add(command);
            }
        }

        public static InputOptions GenerateRandomInput()
        {
            Array values = Enum.GetValues(typeof(InputOptions));
            var random = new Random();
            int index = random.Next(0, values.Length);
            InputOptions randomInput = (InputOptions)values.GetValue(index);
            return randomInput;
        }
    }
}
