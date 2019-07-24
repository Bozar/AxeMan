using AxeMan.GameSystem.GameDataTag;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.DungeonObject.ActorSkill
{
    public interface ISkillMetaInfo
    {
        SkillTypeTag GetSkillTypeTag(SkillNameTag skillNameTag);
    }

    public class SkillMetaInfo : MonoBehaviour, ISkillMetaInfo
    {
        private Dictionary<SkillNameTag, SkillTypeTag> skillTypeDict;

        public SkillTypeTag GetSkillTypeTag(SkillNameTag skillNameTag)
        {
            if (skillTypeDict.TryGetValue(skillNameTag,
                out SkillTypeTag skillTypeTag))
            {
                return skillTypeTag;
            }
            return SkillTypeTag.INVALID;
        }

        private void Awake()
        {
            skillTypeDict = new Dictionary<SkillNameTag, SkillTypeTag>();
        }

        private void Start()
        {
            // NOTE: Subscribe events to fill the dictionary.
            skillTypeDict[SkillNameTag.Q] = SkillTypeTag.Attack;
            skillTypeDict[SkillNameTag.W] = SkillTypeTag.Attack;
            skillTypeDict[SkillNameTag.E] = SkillTypeTag.Move;
            skillTypeDict[SkillNameTag.R] = SkillTypeTag.Move;
        }
    }
}
