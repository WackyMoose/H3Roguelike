using MooseEngine.BehaviorTree.Interfaces;

namespace MooseEngine.BehaviorTree.Base
{
    public abstract class ActionBase : NodeBase, IAction
    {
        public ActionBase() : base() { }

        public override INode Add(INode node)
        {
            node.Root = Root;
            node.Parent = this;

            return default;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}