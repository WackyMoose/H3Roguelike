using MooseEngine.BehaviorTree.Interfaces;
using MooseEngine.Core;
using MooseEngine.Interfaces;

namespace MooseEngine.BehaviorTree
{
    public class BTree : IBehaviorTree
    {
        public IEntity Entity { get; set; }
        public NodeStates State { get; set; }
        public INode? RootNode { get; set; }
        public Dictionary<string, object> Blackboard { get; set; }

        public BTree(IEntity entity)
        {
            Entity = entity;
            State = NodeStates.Failure; // TODO: Define default state at construction
            Blackboard = new Dictionary<string, object>();
        }

        public INode Add(INode node)
        {
            if(RootNode == null)
            {
                RootNode = node;
                RootNode.Root = this;
            }
            return RootNode;
        }

        public NodeStates? Evaluate()
        {
            return RootNode?.Evaluate();
        }
    }
}