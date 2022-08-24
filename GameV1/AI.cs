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
            var player = scene.EntitiesWithType(scene.Entities, typeof(Player));

            var creatures = scene.EntitiesWithType(scene.Entities, typeof(Creature));

            foreach (ICreature creature in creatures)
            {
                // Check if loot or lootables are in sight
                // If yes: Walk towards, Pick up loot, or loot lootable.


                // Check if enemies are in sight
                // If yes: Walk towards or attack enemy

                var input = GenerateRandomInput();

                ICommand command = CommandFactory.Create(input, scene, (Entity)creature);

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
