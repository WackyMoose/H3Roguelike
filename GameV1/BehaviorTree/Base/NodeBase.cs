using GameV1.BehaviorTree.Interfaces;
using MooseEngine.Core;

namespace GameV1.BehaviorTree.Base
{

    public abstract class NodeBase : INode
    {
        public IBehaviorTree Root { get; set; }
        public INode? Parent { get; set; }
        public NodeStates State { get; set; }

        public NodeBase() { }

        public bool SaveData(string key, object value)
        {
            if (Root.Blackboard.ContainsKey(key) == false)
            {
                return Root.Blackboard.TryAdd(key, value);
            }     
            return false;
        }

        public object LoadData(string key)
        {
            if (Root.Blackboard.Count != 0 && 
                Root.Blackboard.ContainsKey(key) == true)
            {
                return Root.Blackboard[key];
            }
            return false;
        }

        public bool DeleteData(string key)
        {
            if (Root.Blackboard.Count != 0 && 
                Root.Blackboard.ContainsKey(key) == true)
            {
                return Root.Blackboard.Remove(key);
            }
            return false;
        }

        public abstract INode? Add(INode node);

        public INode? Up()
        {
            if (Parent != null) { return Parent; } else { return this; }
        }

        public abstract NodeStates Evaluate();

        public override string ToString()
        {
            return $"Node type: {GetType().Name}, Parent: {Parent?.GetType().Name}, State: {State}";
        }
    }
}
