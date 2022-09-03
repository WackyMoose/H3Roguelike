using GameV1.BehaviorTree.Interfaces;

namespace GameV1.BehaviorTree
{
    public class BTree : IBehaviorTree
    {
        public NodeStates State { get; set; }
        public INode? RootNode { get; set; }
        public Dictionary<string, object> Blackboard { get; set; }

        public BTree() 
        {
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
