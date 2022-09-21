using MooseEngine.BehaviorTree.Interfaces;
using MooseEngine.Interfaces;

namespace MooseEngine.BehaviorTree
{
    public class BTree : IBehaviorTree
    {
        public IEntity Entity { get; set; }
        public NodeStates State { get; set; }
        public INode? RootNode { get; set; }
        public IDictionary<string, object> Blackboard { get; set; }

        public BTree(IEntity entity)
        {
            Entity = entity;
            State = NodeStates.Failure; // TODO: Define default state at construction
            Blackboard = new Dictionary<string, object>();
        }

        public BTree(IEntity entity, INode rootNode) : this(entity)
        {
            RootNode = rootNode;

            Compile();
        }

        public INode Add(INode node)
        {
            if (RootNode == null)
            {
                RootNode = node;
                RootNode.Root = this;
            }
            return RootNode;
        }

        private IBehaviorTree Compile()
        {
            // Set Root and Parent for all nodes.

            return this;
        }

        public NodeStates? Evaluate()
        {
            return RootNode?.Evaluate();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}