using GameV1.Enums;

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
