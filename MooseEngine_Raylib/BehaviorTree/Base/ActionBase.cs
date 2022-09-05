using MooseEngine.BehaviorTree.Interfaces;

namespace MooseEngine.BehaviorTree.Base
{
    public abstract class ActionBase : NodeBase, IAction
    {
        public override INode Add(INode node)
        {
            return default;
        }
    }
}