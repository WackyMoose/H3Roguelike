using MooseEngine.Core;
using MooseEngine.Interfaces;

namespace GameV1.Commands
{
    public class CommandCheckForCreaturesWithinRange : Command
    {
        public CommandCheckForCreaturesWithinRange(IScene scene, IEntity entity) 
            : base(scene, entity)
        {
        }

        public override NodeStates Execute()
        {
            throw new NotImplementedException();
        }
    }
}
