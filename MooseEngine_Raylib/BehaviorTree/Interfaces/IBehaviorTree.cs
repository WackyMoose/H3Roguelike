using MooseEngine.Interfaces;

namespace MooseEngine.BehaviorTree.Interfaces
{
    public interface IBehaviorTree
    {
        IEntity Entity { get; set; }
        NodeStates State { get; set; }
        INode? RootNode { get; set; }
        IDictionary<string, object> Blackboard { get; set; }

        INode Add(INode node);
        NodeStates? Evaluate();
    }
}