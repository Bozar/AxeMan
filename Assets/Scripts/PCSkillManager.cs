﻿using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.DungeonObject.ActorSkill
{
    public class PCSkillManager : MonoBehaviour, ISkillMetaInfo
    {
        public string GetSkillName(SkillNameTag skillName)
        {
            return GetComponent<SkillMetaInfo>().GetSkillName(skillName);
        }

        public SkillNameTag GetSkillNameTag(UITag uiTag)
        {
            return GetComponent<SkillMetaInfo>().GetSkillNameTag(uiTag);
        }

        public SkillNameTag GetSkillNameTag(CommandTag commandTag)
        {
            return GetComponent<SkillMetaInfo>().GetSkillNameTag(commandTag);
        }

        public SkillTypeTag GetSkillTypeTag(SkillNameTag skillName,
            out string shortTypeName, out string longTypeName)
        {
            return GetComponent<SkillMetaInfo>().GetSkillTypeTag(skillName,
                out shortTypeName, out longTypeName);
        }
    }
}
