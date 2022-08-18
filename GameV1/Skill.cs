using GameV1.Enums;
using GameV1.Interfaces;

namespace GameV1
{
    public abstract class Skill : ISkill
    {
        public Skill(CreatureSkillType type, int experience = 0)
        {
            Type = type;
            Experience = experience;
        }

        public CreatureSkillType Type { get; set; }
        public int Modifier { get; set; }
        public int Experience { get; set; }
        public int Level { get => (int)Math.Log10(Experience) + 1; }

    }
}
