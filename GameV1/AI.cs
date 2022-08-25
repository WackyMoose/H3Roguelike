using GameV1.Commands;
using GameV1.Commands.Factory;
using GameV1.Entities;
using GameV1.Interfaces;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;

namespace GameV1
{
    public static class AI
    {
        public static void Execute(IScene scene)
        {
            // TODO: Add Initializer that does player look-up once.
            var player = scene.GetEntitiesOfType<Player>(scene.Entities).FirstOrDefault();
            //var npcs = scene.GetEntitiesOfType<Npc>(scene.Entities).ToList();

            foreach (var npc in scene.Entities.OfType<Npc>())
            {
                // Check if loot or lootables are in sight
                // If yes: Walk towards, Pick up loot, or loot lootable.


                // Check if enemies are in sight
                // If yes: Walk towards or attack enemy

                // Bring out yer' dead!
                ICommand maintenanceCommand = new CommandReplaceDeadCreaturesWithCorpseItem(scene, npc);
                CommandQueue.Add(maintenanceCommand);

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
