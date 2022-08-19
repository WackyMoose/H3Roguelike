using GameV1.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Interfaces
{
    public interface ISkill
    {
        CreatureSkillType Type { get; set; }
        int Modifier { get; set; }
        int Experience { get; set; }
        int Level { get; }
    }
}
