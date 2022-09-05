using MooseEngine.BehaviorTree.Interfaces;

namespace MooseEngine.BehaviorTree.Base
{
    public abstract class CompositeBase : NodeBase, IComposite
    {
        public IList<INode>? Children { get; set; }

        public CompositeBase()
        {
            Children = new List<INode>();
        }

        public override INode Add(INode node)
        {
            Children?.Add(node);
            node.Root = Root;
            node.Parent = this;


            return this;
        }

        public override string ToString()
        {
            return base.ToString() + $"";
        }
    }
}
