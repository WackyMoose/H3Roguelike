using GameV1.BehaviorTree.Actions;
using GameV1.BehaviorTree.Composites;
using GameV1.BehaviorTree.Decorators;
using GameV1.BehaviorTree.Interfaces;

namespace GameV1.BehaviorTree
{
    public static class NodeFactory
    {
        public static INode Action(ActionDelegate @delegate)
        {
            return new ActionNode(@delegate);
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
    }
}
