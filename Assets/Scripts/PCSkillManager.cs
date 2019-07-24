﻿using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.DungeonObject.ActorSkill
{
    public class PCSkillManager : MonoBehaviour, ISkillMetaInfo, ISkillRange,
        ISkillCooldown, IConvertSkillMetaInfo
    {
        public int GetCurrentCooldown(SkillNameTag skillNameTag)
        {
            return GetComponent<SkillCooldown>().GetCurrentCooldown(skillNameTag);
        }

        public int GetCurrentCooldown(CommandTag commandTag)
        {
            return GetComponent<SkillCooldown>().GetCurrentCooldown(commandTag);
        }

        public int GetCurrentCooldown(UITag uiTag)
        {
            return GetComponent<SkillCooldown>().GetCurrentCooldown(uiTag);
        }

        public string GetLongSkillTypeName(SkillTypeTag skillTypeTag)
        {
            return GameCore.AxeManCore.GetComponent<ConvertSkillMetaInfo>()
                .GetLongSkillTypeName(skillTypeTag);
        }

        public int GetMaxCooldown(SkillNameTag skillNameTag)
        {
            return GetComponent<SkillCooldown>().GetMaxCooldown(skillNameTag);
        }

        public int GetMaxCooldown(CommandTag commandTag)
        {
            return GetComponent<SkillCooldown>().GetMaxCooldown(commandTag);
        }

        public int GetMaxCooldown(UITag uiTag)
        {
            return GetComponent<SkillCooldown>().GetMaxCooldown(uiTag);
        }

        public string GetShortSkillTypeName(SkillTypeTag skillTypeTag)
        {
            return GameCore.AxeManCore.GetComponent<ConvertSkillMetaInfo>()
                .GetShortSkillTypeName(skillTypeTag);
        }

        public string GetSkillName(SkillNameTag skillNameTag)
        {
            return GameCore.AxeManCore.GetComponent<ConvertSkillMetaInfo>()
                .GetSkillName(skillNameTag);
        }

        public string GetSkillName(CommandTag commandTag)
        {
            return GameCore.AxeManCore.GetComponent<ConvertSkillMetaInfo>()
               .GetSkillName(commandTag);
        }

        public string GetSkillName(UITag uiTag)
        {
            return GameCore.AxeManCore.GetComponent<ConvertSkillMetaInfo>()
               .GetSkillName(uiTag);
        }

        public SkillNameTag GetSkillNameTag(UITag uiTag)
        {
            return GameCore.AxeManCore.GetComponent<ConvertSkillMetaInfo>()
                .GetSkillNameTag(uiTag);
        }

        public SkillNameTag GetSkillNameTag(CommandTag commandTag)
        {
            return GameCore.AxeManCore.GetComponent<ConvertSkillMetaInfo>()
                .GetSkillNameTag(commandTag);
        }

        public SkillNameTag GetSkillNameTag(ActionTag actionTag)
        {
            return GameCore.AxeManCore.GetComponent<ConvertSkillMetaInfo>()
                .GetSkillNameTag(actionTag);
        }

        public int GetSkillRange(SkillNameTag skillName)
        {
            return GetComponent<SkillRange>().GetSkillRange(skillName);
        }

        public SkillTypeTag GetSkillTypeTag(SkillNameTag skillNameTag)
        {
            return GetComponent<SkillMetaInfo>().GetSkillTypeTag(skillNameTag);
        }

        public SkillTypeTag GetSkillTypeTag(CommandTag commandTag)
        {
            return GetComponent<SkillMetaInfo>().GetSkillTypeTag(commandTag);
        }

        public SkillTypeTag GetSkillTypeTag(UITag uiTag)
        {
            return GetComponent<SkillMetaInfo>().GetSkillTypeTag(uiTag);
        }
    }
}