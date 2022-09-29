namespace GameV1.Interfaces.Creatures
{
    public interface ICreatureSpecies
    {
        string Name { get; }
        int MaxHealthPoints { get; }
        int MaxDamage { get; }
    }
}
