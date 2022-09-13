using GameV1.Interfaces.Creatures;

namespace GameV1.Entities.Creatures
{
    public abstract class CreatureSkill : ICreatureSkill
    {
        public CreatureSkill(CreatureSkillType type, int experience = 0)
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
