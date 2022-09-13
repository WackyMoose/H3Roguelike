using GameV1.Entities.Creatures;

namespace GameV1.Interfaces.Creatures
{
    public interface ICreatureSkill
    {
        CreatureSkillType Type { get; set; }
        int Modifier { get; set; }
        int Experience { get; set; }
        int Level { get; }
    }
}
