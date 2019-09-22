using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.DungeonObject.ActorSkill
{
    public interface ISkillMetaInfo
    {
        SkillTypeTag GetSkillTypeTag(SkillNameTag skillNameTag);

        SkillTypeTag GetSkillTypeTag(CommandTag commandTag);

        SkillTypeTag GetSkillTypeTag(UITag uiTag);
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

        public SkillTypeTag GetSkillTypeTag(CommandTag commandTag)
        {
            return GetSkillTypeTag(
                GameCore.AxeManCore.GetComponent<ConvertSkillMetaInfo>()
                .GetSkillNameTag(commandTag));
        }

        public SkillTypeTag GetSkillTypeTag(UITag uiTag)
        {
            return GetSkillTypeTag(
                GameCore.AxeManCore.GetComponent<ConvertSkillMetaInfo>()
                .GetSkillNameTag(uiTag));
        }

        private void Awake()
        {
            skillTypeDict = new Dictionary<SkillNameTag, SkillTypeTag>();
        }

        private void Start()
        {
            // NOTE: Subscribe events to fill the dictionary.
            skillTypeDict[SkillNameTag.SkillQ] = SkillTypeTag.Attack;
            skillTypeDict[SkillNameTag.SkillW] = SkillTypeTag.Move;
            skillTypeDict[SkillNameTag.SkillE] = SkillTypeTag.Buff;
            skillTypeDict[SkillNameTag.SkillR] = SkillTypeTag.Curse;
        }
    }
}
