using AxeMan.GameSystem.GameDataTag;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem.GameDataHub
{
    public interface ISkillData
    {
        string GetLongSkillTypeName(SkillTypeTag skillType);

        string GetShortSkillTypeName(SkillTypeTag skillType);

        string GetSkillComponentName(SkillComponentTag skillComponent);

        string GetSkillName(SkillNameTag skillName);
    }

    public class SkillData : MonoBehaviour, ISkillData
    {
        private Dictionary<SkillComponentTag, string> compString;

        private string invalidName;

        private Dictionary<SkillNameTag, string> nameString;

        private Dictionary<SkillTypeTag, string> typeLongName;

        private Dictionary<SkillTypeTag, string> typeShortName;

        public string GetLongSkillTypeName(SkillTypeTag skillType)
        {
            if (typeLongName.TryGetValue(skillType, out string longType))
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

        public string GetSkillName(SkillNameTag skillNameTag)
        {
            if (nameString.TryGetValue(skillNameTag, out string skillName))
            {
                return skillName;
            }
            return invalidName;
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
                [SkillTypeTag.Buff] = "Buf",
                [SkillTypeTag.Curse] = "Cur"
            };

            typeLongName = new Dictionary<SkillTypeTag, string>
            {
                [SkillTypeTag.Attack] = "Attack",
                [SkillTypeTag.Move] = "Move",
                [SkillTypeTag.Buff] = "Buff",
                [SkillTypeTag.Curse] = "Curse"
            };

            compString = new Dictionary<SkillComponentTag, string>
            {
                [SkillComponentTag.Life] = "Life",

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
    }
}
