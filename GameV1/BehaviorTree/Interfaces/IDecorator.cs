namespace GameV1.BehaviorTree.Interfaces
{
    public interface IDecorator : INode
    {
        public INode? Child { get; set; }
    }
}
