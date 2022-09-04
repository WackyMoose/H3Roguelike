using MooseEngine.Core;
using MooseEngine.Interfaces;

namespace GameV1.BehaviorTree.Interfaces
{
    public interface IBehaviorTree
    {
        IEntity Entity { get; set; }
        NodeStates State { get; set; }
        INode? RootNode { get; set; }
        Dictionary<string, object> Blackboard { get; set; }
    }
}