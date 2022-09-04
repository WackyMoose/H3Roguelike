using GameV1.BehaviorTree.Interfaces;

namespace GameV1.BehaviorTree.Base
{
    public abstract class DecoratorBase : NodeBase, IDecorator
    {
        public INode? Child { get; set; }

        public DecoratorBase()
        {
            Parent = null;
        }

        public override INode Add(INode node)
        {
            node.Root = Root;
            node.Parent = this;
            Child = node;

            return this;
        }

        public override string ToString()
        {
            return base.ToString() + $" Child: {Child}";
        }
    }
}