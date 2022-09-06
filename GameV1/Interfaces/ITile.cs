namespace GameV1.Interfaces
{
    public interface ITile
    {
        bool IsWalkable { get; set; }
        float PathWeight { get; set; }
    }
}
