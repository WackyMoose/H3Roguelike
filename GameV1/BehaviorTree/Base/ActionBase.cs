using GameV1.BehaviorTree.Interfaces;

namespace GameV1.BehaviorTree.Base
{
    public abstract class ActionBase : NodeBase, IAction
    {
        public override INode Add(INode node)
        {
            return default;
        }
    }
}
