using AxeMan.GameSystem.GameDataTag;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.DungeonObject.ActorSkill
{
    public interface ISkillMetaInfo
    {
        SkillNameTag Convert(CommandTag commandTag);

        SkillNameTag Convert(UITag uiTag);

        string GetSkillName(SkillNameTag skillName);

        SkillTypeTag GetSkillType(SkillNameTag skillName,
            out string shortTypeName, out string fullTypeName);
    }

    public class SkillMetaInfo : MonoBehaviour, ISkillMetaInfo
    {
        private Dictionary<SkillTypeTag, string> fullTypeNameDict;
        private Dictionary<SkillTypeTag, string> shortTypeNameDict;
        private Dictionary<SkillNameTag, string> skillNameDict;
        private Dictionary<SkillNameTag, SkillTypeTag> skillTypeDict;

        public SkillNameTag Convert(CommandTag commandTag)
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

        public SkillNameTag Convert(UITag uiTag)
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

        public string GetSkillName(SkillNameTag skillName)
        {
            return skillNameDict[skillName];
        }

        public SkillTypeTag GetSkillType(SkillNameTag skillName,
            out string shortTypeName, out string fullTypeName)
        {
            SkillTypeTag skillType = skillTypeDict[skillName];
            shortTypeName = shortTypeNameDict[skillType];
            fullTypeName = fullTypeNameDict[skillType];

            return skillType;
        }

        private void Awake()
        {
            skillNameDict = new Dictionary<SkillNameTag, string>();
            skillTypeDict = new Dictionary<SkillNameTag, SkillTypeTag>();
            shortTypeNameDict = new Dictionary<SkillTypeTag, string>();
            fullTypeNameDict = new Dictionary<SkillTypeTag, string>();
        }

        private void Start()
        {
            // NOTE: Subscribe events to fill the dictionary.
            skillNameDict[SkillNameTag.Q] = "Q";
            skillNameDict[SkillNameTag.W] = "W";
            skillNameDict[SkillNameTag.E] = "E";
            skillNameDict[SkillNameTag.R] = "R";

            skillTypeDict[SkillNameTag.Q] = SkillTypeTag.Attack;
            skillTypeDict[SkillNameTag.W] = SkillTypeTag.Attack;
            skillTypeDict[SkillNameTag.E] = SkillTypeTag.Move;
            skillTypeDict[SkillNameTag.R] = SkillTypeTag.Move;

            shortTypeNameDict[SkillTypeTag.Attack] = "Atk";
            shortTypeNameDict[SkillTypeTag.Move] = "Mov";

            fullTypeNameDict[SkillTypeTag.Attack] = "Attack";
            fullTypeNameDict[SkillTypeTag.Move] = "Move";
        }
    }
}
