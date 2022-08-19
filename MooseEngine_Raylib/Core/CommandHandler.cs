
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

        private static void Dequeue()
        {
            while (Commands.Count > 0)
            {
                Commands.Dequeue();
            }
        }

        public static void Execute()
        {
            if (Commands.Count > 0)
            {
                foreach (Command command in Commands)
                {
                    command.Execute();
                }

                Dequeue();
            }
        }
    }
}
