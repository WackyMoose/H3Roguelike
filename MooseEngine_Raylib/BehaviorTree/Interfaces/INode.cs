using MooseEngine.Core;

namespace MooseEngine.BehaviorTree.Interfaces
{
    public interface INode
    {
        IBehaviorTree Root { get; set; }
        INode? Parent { get; set; }
        NodeStates State { get; set; }
        bool SaveData(string key, object value);
        object LoadData(string key);
        bool DeleteData(string key);
        INode? Add(INode node);
        abstract INode? Up();
        abstract NodeStates Evaluate();
    }
}