
using MooseEngine.Scenes;

namespace MooseEngine.Core
{
    public static class CommandHandler
    {
        private static Queue<Command> Commands = new Queue<Command>();

        public static bool IsEmpty { get { return Commands.Count() == 0; } }


        public static void Add(Command? command)
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
