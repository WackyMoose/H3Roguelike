using MooseEngine.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooseEngine.Interfaces
{
    public interface ICommand
    {
        Entity Entity { get; set; }
        Scene Scene { get; set; }

        void Execute();
    }
}
