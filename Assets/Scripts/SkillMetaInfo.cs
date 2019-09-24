using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.DungeonObject.ActorSkill
{
    public interface ISkillMetaInfo
    {
        SkillTypeTag GetSkillTypeTag(SkillNameTag skillNameTag);
    }

    public class SkillMetaInfo : MonoBehaviour, ISkillMetaInfo
    {
        public SkillTypeTag GetSkillTypeTag(SkillNameTag skillNameTag)
        {
            return GameCore.AxeManCore.GetComponent<SkillTemplateData>()
                 .GetSkillTypeTag(skillNameTag);
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
    }
}
