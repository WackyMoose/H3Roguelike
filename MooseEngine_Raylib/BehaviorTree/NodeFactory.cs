using MooseEngine.BehaviorTree.Actions;
using MooseEngine.BehaviorTree.Composites;
using MooseEngine.BehaviorTree.Decorators;
using MooseEngine.BehaviorTree.Interfaces;
using MooseEngine.Core;

namespace MooseEngine.BehaviorTree
{
    public static class NodeFactory
    {
        //public static INode Action(ActionDelegate @delegate)
        //{
        //    return new ActionNode(@delegate);
        //}

        public static INode Action(Command command)
        {
            return new ActionNode(command);
        }

        public static INode Breakpoint(string message)
        {
            return new Breakpoint(message);
        }

        public static INode Delay(int numDelays)
        {
            return new Delay(numDelays);
        }
        public static INode Inverter()
        {
            return new Inverter();
        }

        public static INode Limiter(int numRepeats)
        {
            return new Limiter(numRepeats);
        }

        public static INode Repeater(int numRepeats)
        {
            return new Repeater(numRepeats);
        }

        public static INode Selector()
        {
            return new Selector();
        }

        public static INode Sequence()
        {
            return new Sequence();
        }
        public static INode Serializer()
        {
            return new Serializer();
        }
    }
}