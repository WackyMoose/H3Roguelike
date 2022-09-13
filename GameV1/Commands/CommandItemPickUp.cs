using GameV1.Interfaces;
using GameV1.Interfaces.Creatures;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.UI;

namespace GameV1.Commands
{
    internal class CommandItemPickUp : Command
    {
        public IScene Scene { get; set; }
        public ICreature Creature { get; set; }

        public CommandItemPickUp(IScene scene, ICreature creature)
        {
            Scene = scene;
            Creature = creature;
        }

        public override NodeStates Execute()
        {
            var itemLayer = Scene.GetLayer((int)EntityLayer.Items);

            IItem? item = (IItem?)Scene.GetEntityAtPosition(itemLayer.Entities, Creature.Position);

            // Does item exist?
            if (item == null) { return NodeStates.Failure; }

            // Attempt to add item to Creature inventory
            var result = Creature.Inventory.Inventory.AddItemToFirstEmptySlot(item);

            if (result == false) { return NodeStates.Failure; }

            // Remove item from ItemLayer
            itemLayer.Entities.Remove(item.Position);

            ConsolePanel.Add($"{Creature.Name} picked up {item.Name}");

            return NodeStates.Success;
        }
    }
}