using MooseEngine.BehaviorTree.Interfaces;

namespace MooseEngine.BehaviorTree.Base
{
    public abstract class ActionBase : NodeBase, IAction
    {
        public ActionBase() : base() { }

        public override INode Add(INode node)
        {
            return default;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}