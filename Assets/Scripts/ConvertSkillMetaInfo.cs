using AxeMan.GameSystem.GameDataTag;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public interface IConvertSkillMetaInfo
    {
        string GetLongSkillTypeName(SkillTypeTag skillTypeTag);

        string GetShortSkillTypeName(SkillTypeTag skillTypeTag);

        string GetSkillComponentName(SkillComponentTag skillComponentTag);

        string GetSkillEffectName(SkillComponentTag skillComponentTag,
            int[] powerDuration);

        string GetSkillName(SkillNameTag skillNameTag);

        string GetSkillName(CommandTag commandTag);

        string GetSkillName(UITag uiTag);

        SkillNameTag GetSkillNameTag(UITag uiTag);

        SkillNameTag GetSkillNameTag(ActionTag actionTag);

        SkillNameTag GetSkillNameTag(CommandTag commandTag);
    }

    public class ConvertSkillMetaInfo : MonoBehaviour, IConvertSkillMetaInfo
    {
        private Dictionary<SkillComponentTag, string> compString;
        private string invalidName;
        private Dictionary<SkillNameTag, string> nameString;
        private Dictionary<SkillTypeTag, string> typeLongName;
        private Dictionary<SkillTypeTag, string> typeShortName;

        public string GetLongSkillTypeName(SkillTypeTag skillTypeTag)
        {
            if (typeLongName.TryGetValue(skillTypeTag, out string longType))
            {
                return longType;
            }
            return invalidName;
        }

        public string GetShortSkillTypeName(SkillTypeTag skillTypeTag)
        {
            if (typeShortName.TryGetValue(skillTypeTag, out string shortType))
            {
                return shortType;
            }
            return invalidName;
        }

        public string GetSkillComponentName(SkillComponentTag skillComponentTag)
        {
            if (compString.TryGetValue(skillComponentTag, out string compName))
            {
                return compName;
            }
            return invalidName;
        }

        public string GetSkillEffectName(SkillComponentTag skillComponentTag,
            int[] powerDuration)
        {
            int power = powerDuration[0];
            int duration = powerDuration[1];

            if (HideSkillPower(skillComponentTag))
            {
                return "T X " + duration;
            }
            return power + " X " + duration;
        }

        public string GetSkillName(SkillNameTag skillNameTag)
        {
            if (nameString.TryGetValue(skillNameTag, out string skillName))
            {
                return skillName;
            }
            return invalidName;
        }

        public string GetSkillName(CommandTag commandTag)
        {
            return GetSkillName(GetSkillNameTag(commandTag));
        }

        public string GetSkillName(UITag uiTag)
        {
            return GetSkillName(GetSkillNameTag(uiTag));
        }

        public SkillNameTag GetSkillNameTag(UITag uiTag)
        {
            switch (uiTag)
            {
                case UITag.QText:
                case UITag.QData:
                case UITag.QType:
                    return SkillNameTag.Q;

                case UITag.WText:
                case UITag.WData:
                case UITag.WType:
                    return SkillNameTag.W;

                case UITag.EText:
                case UITag.EData:
                case UITag.EType:
                    return SkillNameTag.E;

                case UITag.RText:
                case UITag.RData:
                case UITag.RType:
                    return SkillNameTag.R;

                default:
                    return SkillNameTag.INVALID;
            }
        }

        public SkillNameTag GetSkillNameTag(CommandTag commandTag)
        {
            switch (commandTag)
            {
                case CommandTag.SkillQ:
                    return SkillNameTag.Q;

                case CommandTag.SkillW:
                    return SkillNameTag.W;

                case CommandTag.SkillE:
                    return SkillNameTag.E;

                case CommandTag.SkillR:
                    return SkillNameTag.R;

                default:
                    return SkillNameTag.INVALID;
            }
        }

        public SkillNameTag GetSkillNameTag(ActionTag actionTag)
        {
            switch (actionTag)
            {
                case ActionTag.UseSkillQ:
                    return SkillNameTag.Q;

                case ActionTag.UseSkillW:
                    return SkillNameTag.W;

                case ActionTag.UseSkillE:
                    return SkillNameTag.E;

                case ActionTag.UseSkillR:
                    return SkillNameTag.R;

                default:
                    return SkillNameTag.INVALID;
            }
        }

        private void Awake()
        {
            invalidName = "INVALID_NAME";

            nameString = new Dictionary<SkillNameTag, string>
            {
                [SkillNameTag.Q] = "Q",
                [SkillNameTag.W] = "W",
                [SkillNameTag.E] = "E",
                [SkillNameTag.R] = "R"
            };

            typeShortName = new Dictionary<SkillTypeTag, string>
            {
                [SkillTypeTag.Attack] = "Atk",
                [SkillTypeTag.Move] = "Mov",
                [SkillTypeTag.Enhance] = "Enh",
                [SkillTypeTag.Curse] = "Cur"
            };

            typeLongName = new Dictionary<SkillTypeTag, string>
            {
                [SkillTypeTag.Attack] = "Attack",
                [SkillTypeTag.Move] = "Move",
                [SkillTypeTag.Enhance] = "Enhance",
                [SkillTypeTag.Curse] = "Curse"
            };

            compString = new Dictionary<SkillComponentTag, string>
            {
                [SkillComponentTag.FireMerit] = "Fire+",
                [SkillComponentTag.FireFlaw] = "Fire-",
                [SkillComponentTag.FireCurse] = "Fire?",

                [SkillComponentTag.WaterMerit] = "Water+",
                [SkillComponentTag.WaterFlaw] = "Water-",
                [SkillComponentTag.WaterCurse] = "Water?",

                [SkillComponentTag.AirMerit] = "Air+",
                [SkillComponentTag.AirFlaw] = "Air-",
                [SkillComponentTag.AirCurse] = "Air?",

                [SkillComponentTag.EarthMerit] = "Earth+",
                [SkillComponentTag.EarthFlaw] = "Earth-",
                [SkillComponentTag.EarthCurse] = "Earth?",
            };
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
