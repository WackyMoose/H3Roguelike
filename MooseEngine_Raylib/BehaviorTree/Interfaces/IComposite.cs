namespace MooseEngine.BehaviorTree.Interfaces
{
    public interface IComposite : INode
    {
        IList<INode>? Children { get; set; }
    }
}