namespace GameV1.Interfaces.Creatures
{
    public interface ICreatureStats
    {
        int Fatigue { get; set; }
        int FatigueDrecrease { get; set; }
        int Strength { get; set; }
        int Agility { get; set; }
        int Toughness { get; set; }
        int Perception { get; set; }
        int Charisma { get; set; }
        int Health { get; set; }
    }
}