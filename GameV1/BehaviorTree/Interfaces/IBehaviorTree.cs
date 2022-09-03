using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.BehaviorTree.Interfaces
{
    public interface IBehaviorTree
    {
        NodeStates State { get; set; }
        INode? RootNode { get; set; }
        Dictionary<string, object> Blackboard { get; set; }
    }
}
