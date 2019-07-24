using AxeMan.GameSystem.GameDataTag;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public interface IConvertSkillMetaInfo
    {
        string GetLongSkillTypeName(SkillTypeTag skillTypeTag);

        string GetShortSkillTypeName(SkillTypeTag skillTypeTag);

        string GetSkillName(SkillNameTag skillNameTag);

        string GetSkillName(CommandTag commandTag);

        string GetSkillName(UITag uiTag);

        SkillNameTag GetSkillNameTag(UITag uiTag);

        SkillNameTag GetSkillNameTag(ActionTag actionTag);

        SkillNameTag GetSkillNameTag(CommandTag commandTag);
    }

    public class ConvertSkillMetaInfo : MonoBehaviour, IConvertSkillMetaInfo
    {
        private string invalidName;
        private Dictionary<SkillTypeTag, string> longTypeNameDict;
        private Dictionary<SkillTypeTag, string> shortTypeNameDict;
        private Dictionary<SkillNameTag, string> skillNameDict;

        public string GetLongSkillTypeName(SkillTypeTag skillTypeTag)
        {
            if (longTypeNameDict.TryGetValue(skillTypeTag, out string longType))
            {
                return longType;
            }
            return invalidName;
        }

        public string GetShortSkillTypeName(SkillTypeTag skillTypeTag)
        {
            if (shortTypeNameDict.TryGetValue(skillTypeTag, out string shortType))
            {
                return shortType;
            }
            return invalidName;
        }

        public string GetSkillName(SkillNameTag skillNameTag)
        {
            if (skillNameDict.TryGetValue(skillNameTag, out string skillName))
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
            skillNameDict = new Dictionary<SkillNameTag, string>();
            shortTypeNameDict = new Dictionary<SkillTypeTag, string>();
            longTypeNameDict = new Dictionary<SkillTypeTag, string>();

            invalidName = "INVALID_NAME";

            skillNameDict[SkillNameTag.Q] = "Q";
            skillNameDict[SkillNameTag.W] = "W";
            skillNameDict[SkillNameTag.E] = "E";
            skillNameDict[SkillNameTag.R] = "R";

            shortTypeNameDict[SkillTypeTag.Attack] = "Atk";
            shortTypeNameDict[SkillTypeTag.Move] = "Mov";

            longTypeNameDict[SkillTypeTag.Attack] = "Attack";
            longTypeNameDict[SkillTypeTag.Move] = "Move";
        }
    }
}
