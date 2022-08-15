using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooseEngine.Core
{
    public static class CommandHandler
    {
        private static Queue<Command> Commands = new Queue<Command>();

        public static void Add(Command command)
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
                foreach (Command command in Commands)
                {
                    command.Execute();
                }

                Commands.Dequeue();
            }
        }
    }
}
