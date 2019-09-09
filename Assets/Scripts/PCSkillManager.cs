using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.DungeonObject.ActorSkill
{
    public class PCSkillManager : MonoBehaviour, ISkillMetaInfo, ISkillRange,
        ISkillCooldown, ISkillDamage, ISkillSlot, ISkillEffect,
        IConvertSkillMetaInfo
    {
        public int MinCooldown
        {
            get { return GetComponent<SkillCooldown>().MinCooldown; }
        }

        public string GetBuildingEffectName(SkillComponentTag skillComponentTag,
            int powerDuration)
        {
            return GameCore.AxeManCore.GetComponent<ConvertSkillMetaInfo>()
                .GetBuildingEffectName(skillComponentTag, powerDuration);
        }

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
            return GameCore.AxeManCore.GetComponent<SkillData>()
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
            return GameCore.AxeManCore.GetComponent<SkillData>()
                .GetShortSkillTypeName(skillTypeTag);
        }

        public string GetSkillComponentName(SkillComponentTag skillComponentTag)
        {
            return GameCore.AxeManCore.GetComponent<SkillData>()
                .GetSkillComponentName(skillComponentTag);
        }

        public int GetSkillDamage(SkillNameTag skillNameTag)
        {
            return GetComponent<SkillDamage>().GetSkillDamage(skillNameTag);
        }

        public int GetSkillDamage(CommandTag commandTag)
        {
            return GetComponent<SkillDamage>().GetSkillDamage(commandTag);
        }

        public Dictionary<SkillComponentTag, EffectData> GetSkillEffect(
            SkillNameTag skillNameTag)
        {
            return GetComponent<SkillEffect>().GetSkillEffect(skillNameTag);
        }

        public string GetSkillEffectName(SkillComponentTag skillComponentTag,
            EffectData effectData)
        {
            return GameCore.AxeManCore.GetComponent<ConvertSkillMetaInfo>()
                .GetSkillEffectName(skillComponentTag, effectData);
        }

        public string GetSkillName(SkillNameTag skillNameTag)
        {
            return GameCore.AxeManCore.GetComponent<SkillData>()
                .GetSkillName(skillNameTag);
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

        public Dictionary<SkillSlotTag, SkillComponentTag> GetSkillSlot(
            SkillNameTag skillNameTag)
        {
            return GetComponent<SkillSlot>().GetSkillSlot(skillNameTag);
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

        public void RemoveSkillComponent(SkillNameTag skillNameTag,
            SkillSlotTag skillSlotTag)
        {
            GetComponent<SkillSlot>().RemoveSkillComponent(skillNameTag,
                skillSlotTag);
        }

        public bool TrySetSkillSlot(SkillNameTag skillNameTag,
            SkillSlotTag skillSlotTag, SkillComponentTag skillComponentTag)
        {
            return GetComponent<SkillSlot>().TrySetSkillSlot(skillNameTag,
                skillSlotTag, skillComponentTag);
        }
    }
}
