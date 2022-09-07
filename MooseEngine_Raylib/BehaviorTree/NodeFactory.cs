using MooseEngine.Core;
using MooseEngine.BehaviorTree.Actions;
using MooseEngine.BehaviorTree.Composites;
using MooseEngine.BehaviorTree.Decorators;
using MooseEngine.BehaviorTree.Interfaces;

namespace MooseEngine.BehaviorTree
{
    public static class NodeFactory
    {
        public static IAction Action(Actions.Delegate @delegate)
        {
            return new ActionDelegate(@delegate);
        }

        public static IAction Action(Command command)
        {
            return new ActionCommand(command);
        }

        public static IDecorator Breakpoint(string message)
        {
            return new Breakpoint(message);
        }

        public static IDecorator Delay(int numDelays)
        {
            return new Delay(numDelays);
        }
        public static IDecorator Inverter()
        {
            return new Inverter();
        }

        public static IDecorator Limiter(int numRepeats)
        {
            return new Limiter(numRepeats);
        }

        public static IDecorator Repeater(int numRepeats)
        {
            return new Repeater(numRepeats);
        }

        public static IComposite Selector()
        {
            return new Selector();
        }

        public static IComposite Sequence()
        {
            return new Sequence();
        }
        public static IComposite Serializer()
        {
            return new Serializer();
        }
    }
}