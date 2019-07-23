using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.SearchGameObject;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.DungeonObject.ActorSkill
{
    public interface ISkillRange
    {
        int GetSkillRange(SkillNameTag skillName);
    }

    public class SkillRange : MonoBehaviour, ISkillRange
    {
        private Dictionary<SkillNameTag, int> skillRangeDict;

        public int GetSkillRange(SkillNameTag skillName)
        {
            int invalid = -1;

            if (skillRangeDict.TryGetValue(skillName, out int range))
            {
                return range;
            }
            return invalid;
        }

        private void Awake()
        {
            skillRangeDict = new Dictionary<SkillNameTag, int>();
        }

        private void SkillRange_VerifyingSkill(object sender,
            VerifyingSkillEventArgs e)
        {
            int[] aimMarker
                = GameCore.AxeManCore.GetComponent<SearchObject>()
                .Search(SubTag.AimMarker)[0]
                .GetComponent<MetaInfo>().Position;
            int distance = GetComponent<LocalManager>().GetDistance(aimMarker);
            int skillRange = GetSkillRange(
                GetComponent<PCSkillManager>().GetSkillNameTag(e.UseSkill));

            e.CanUseSkill.Push(distance <= skillRange);
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<AimMode>().VerifyingSkill
                += SkillRange_VerifyingSkill;

            // TODO: Fill the dictionary by events.
            skillRangeDict[SkillNameTag.Q] = 3;
            skillRangeDict[SkillNameTag.W] = 4;
            skillRangeDict[SkillNameTag.E] = 6;
            skillRangeDict[SkillNameTag.R] = 1;
        }
    }
}
