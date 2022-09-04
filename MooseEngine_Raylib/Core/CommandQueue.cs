
using MooseEngine.Interfaces;

namespace MooseEngine.Core
{
    public static class CommandQueue
    {
        private static Queue<ICommand> Commands = new Queue<ICommand>();

        public static bool IsEmpty { get { return Commands.Count() == 0; } }


        public static void Add(ICommand? command)
        {
            if (command is not null)
            {
                Commands.Enqueue(command);
            }
        }

        public static void Execute()
        {
            while (Commands.Count > 0)
            {
                Commands.Dequeue().Execute();
            }
        }
    }
}
