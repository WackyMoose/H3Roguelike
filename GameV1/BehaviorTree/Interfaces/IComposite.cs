namespace GameV1.BehaviorTree.Interfaces
{
    public interface IComposite : INode
    {
        IList<INode>? Children { get; set; }
    }
}
