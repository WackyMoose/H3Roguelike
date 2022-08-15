using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Categories
{
    public struct CreatureSkillCategory
    {
        public CreatureSkillCategory(string name, int experience = 0, int level = 0)
        {
            Name = name;
            Experience = experience;
            Level = level;
        }

        public string Name { get; }
        public int Experience { get; set; }
        public int Level { get => (int)Math.Log10(Experience) + 1; set => Level = value; }

        public void LevelUp()
        {
            Level++;
        }

        public static CreatureSkillCategory Archery { get { return new CreatureSkillCategory("Archery"); } }
        public static CreatureSkillCategory Mining { get { return new CreatureSkillCategory("Mining"); } }
        public static CreatureSkillCategory SwordFighting { get { return new CreatureSkillCategory("SwordFighting"); } }
        public static CreatureSkillCategory Block { get { return new CreatureSkillCategory("Block"); } }
        public static CreatureSkillCategory Parry { get { return new CreatureSkillCategory("Parry"); } }

        public static List<CreatureSkillCategory> List()
        {
            return new List<CreatureSkillCategory>
            { 
                Archery, Mining, SwordFighting
            };
        }

    }
}
