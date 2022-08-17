using GameV1.Enums;

namespace GameV1
{
    public abstract class Skill
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
