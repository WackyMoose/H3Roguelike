using GameV1.BehaviorTree.Interfaces;
using MooseEngine.Core;
using MooseEngine.Interfaces;

namespace GameV1.BehaviorTree
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
            node.Root = this;
            RootNode = node;
            return RootNode;
        }

        public NodeStates? Evaluate()
        {
            return RootNode?.Evaluate();
        }
    }
}
