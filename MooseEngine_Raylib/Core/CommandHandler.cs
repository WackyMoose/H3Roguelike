
namespace MooseEngine.Core
{
    public static class CommandHandler
    {
        private static Queue<Command> Commands = new Queue<Command>();

        public static void Add(Command? command)
        {
            if (command is not null)
            {
                Commands.Enqueue(command);
            }
        }

        public static void Execute()
        {
            if (Commands.Count > 0)
            {
                var command = Commands.Dequeue();
                command.Execute();
            }
        }
    }
}
