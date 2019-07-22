using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.DungeonObject.ActorSkill
{
    public class PCSkillManager : MonoBehaviour, ISkillMetaInfo
    {
        public SkillNameTag Convert(CommandTag commandTag)
        {
            return GetComponent<SkillMetaInfo>().Convert(commandTag);
        }

        public SkillNameTag Convert(UITag uiTag)
        {
            return GetComponent<SkillMetaInfo>().Convert(uiTag);
        }

        public string GetSkillName(SkillNameTag skillName)
        {
            return GetComponent<SkillMetaInfo>().GetSkillName(skillName);
        }

        public SkillTypeTag GetSkillType(SkillNameTag skillName,
            out string shortTypeName, out string longTypeName)
        {
            return GetComponent<SkillMetaInfo>().GetSkillType(skillName,
                out shortTypeName, out longTypeName);
        }
    }
}
