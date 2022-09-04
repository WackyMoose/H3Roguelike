using GameV1.BehaviorTree.Interfaces;

namespace GameV1.BehaviorTree.Base
{
    public abstract class CompositeBase : NodeBase, IComposite
    {
        public IList<INode>? Children { get; set; }

        public CompositeBase()
        {
            Parent = null;
            Children = new List<INode>();
        }

        public override INode Add(INode node)
        {
            node.Root = Root;
            node.Parent = this;
            Children?.Add(node);

            return this;
        }

        public override string ToString()
        {
            return base.ToString() + $"";
        }
    }
}
