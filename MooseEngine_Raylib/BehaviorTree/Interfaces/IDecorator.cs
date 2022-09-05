namespace MooseEngine.BehaviorTree.Interfaces
{
    public interface IDecorator : INode
    {
        public INode? Child { get; set; }
    }
}