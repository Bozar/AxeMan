using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public interface IConvertSkillMetaInfo
    {
        string GetBuildingEffectName(SkillComponentTag skillComponentTag,
            int powerDuration);

        string GetSkillEffectName(SkillComponentTag skillComponentTag,
            EffectData effectData);

        SkillNameTag GetSkillNameTag(UITag uiTag);

        SkillNameTag GetSkillNameTag(ActionTag actionTag);

        SkillNameTag GetSkillNameTag(CommandTag commandTag);
    }

    public class ConvertSkillMetaInfo : MonoBehaviour, IConvertSkillMetaInfo
    {
        public string GetBuildingEffectName(SkillComponentTag skillComponentTag,
            int powerDuration)
        {
            if (HideSkillPower(skillComponentTag))
            {
                return "T X " + powerDuration;
            }
            return powerDuration + " X " + powerDuration;
        }

        public string GetSkillEffectName(SkillComponentTag skillComponentTag,
           EffectData effectData)
        {
            int power = effectData.Power;
            int duration = effectData.Duration;

            if (HideSkillPower(skillComponentTag))
            {
                return "T X " + duration;
            }
            return power + " X " + duration;
        }

        public SkillNameTag GetSkillNameTag(UITag uiTag)
        {
            switch (uiTag)
            {
                case UITag.QText:
                case UITag.QData:
                case UITag.QType:
                    return SkillNameTag.SkillQ;

                case UITag.WText:
                case UITag.WData:
                case UITag.WType:
                    return SkillNameTag.SkillW;

                case UITag.EText:
                case UITag.EData:
                case UITag.EType:
                    return SkillNameTag.SkillE;

                case UITag.RText:
                case UITag.RData:
                case UITag.RType:
                    return SkillNameTag.SkillR;

                default:
                    return SkillNameTag.INVALID;
            }
        }

        public SkillNameTag GetSkillNameTag(CommandTag commandTag)
        {
            switch (commandTag)
            {
                case CommandTag.SkillQ:
                    return SkillNameTag.SkillQ;

                case CommandTag.SkillW:
                    return SkillNameTag.SkillW;

                case CommandTag.SkillE:
                    return SkillNameTag.SkillE;

                case CommandTag.SkillR:
                    return SkillNameTag.SkillR;

                default:
                    return SkillNameTag.INVALID;
            }
        }

        public SkillNameTag GetSkillNameTag(ActionTag actionTag)
        {
            switch (actionTag)
            {
                case ActionTag.UseSkillQ:
                    return SkillNameTag.SkillQ;

                case ActionTag.UseSkillW:
                    return SkillNameTag.SkillW;

                case ActionTag.UseSkillE:
                    return SkillNameTag.SkillE;

                case ActionTag.UseSkillR:
                    return SkillNameTag.SkillR;

                default:
                    return SkillNameTag.INVALID;
            }
        }

        private bool HideSkillPower(SkillComponentTag skillComponentTag)
        {
            switch (skillComponentTag)
            {
                case SkillComponentTag.FireMerit:
                case SkillComponentTag.FireFlaw:
                case SkillComponentTag.FireCurse:
                case SkillComponentTag.WaterMerit:
                case SkillComponentTag.WaterFlaw:
                case SkillComponentTag.WaterCurse:
                    return true;

                case SkillComponentTag.AirMerit:
                case SkillComponentTag.AirFlaw:
                case SkillComponentTag.AirCurse:
                case SkillComponentTag.EarthMerit:
                case SkillComponentTag.EarthFlaw:
                case SkillComponentTag.EarthCurse:
                    return false;

                default:
                    return false;
            }
        }
    }
}
