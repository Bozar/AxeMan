using AxeMan.GameSystem.GameDataTag;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.DungeonObject.ActorSkill
{
    public interface ISkillRange
    {
        int GetSkillRange(SkillNameTag skillName);
    }

    public class SkillRange : MonoBehaviour, ISkillRange
    {
        private Dictionary<SkillNameTag, int> skillRangeDict;

        public int GetSkillRange(SkillNameTag skillName)
        {
            int invalid = -1;

            if (skillRangeDict.TryGetValue(skillName, out int range))
            {
                return range;
            }
            return invalid;
        }

        private void Awake()
        {
            skillRangeDict = new Dictionary<SkillNameTag, int>();
        }

        private void Start()
        {
            // TODO: Fill the dictionary by events.
            skillRangeDict[SkillNameTag.Q] = 3;
            skillRangeDict[SkillNameTag.W] = 4;
            skillRangeDict[SkillNameTag.E] = 6;
            skillRangeDict[SkillNameTag.R] = 1;
        }
    }
}
