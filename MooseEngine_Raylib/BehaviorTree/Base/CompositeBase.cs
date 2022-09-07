using MooseEngine.BehaviorTree.Interfaces;
using System.Xml.Linq;

namespace MooseEngine.BehaviorTree.Base
{
    public abstract class CompositeBase : NodeBase, IComposite
    {
        public IList<INode>? Children { get; set; }

        // TODO: Add ctors that take child node/nodes as argument?

        public CompositeBase() : base()
        {
            Children = new List<INode>();
        }

        public IComposite Add(params INode[] nodes)
        {
            foreach(INode node in nodes)
            {
                node.Root = Root;
                node.Parent = this;
                Children?.Add(node);
            }

            return this;
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
