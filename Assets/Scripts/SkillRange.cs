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
        private int baseRange;
        private int invalidRange;
        private Dictionary<SkillNameTag, int> nameRange;
        private int zeroRange;

        public int GetSkillRange(SkillNameTag skillNameTag)
        {
            if (!nameRange.ContainsKey(skillNameTag))
            {
                return invalidRange;
            }

            int range = nameRange[skillNameTag];
            if (range == invalidRange)
            {
                range = SetBaseRange(skillNameTag);
            }

            // TODO: Change range based on PC status.

            return range;
        }

        private void Awake()
        {
            invalidRange = -1;
            zeroRange = 0;
            baseRange = 1;

            nameRange = new Dictionary<SkillNameTag, int>
            {
                [SkillNameTag.Q] = invalidRange,
                [SkillNameTag.W] = invalidRange,
                [SkillNameTag.E] = invalidRange,
                [SkillNameTag.R] = invalidRange,
            };
        }

        private int SetBaseRange(SkillNameTag skillNameTag)
        {
            SkillTypeTag skillType = GetComponent<PCSkillManager>()
               .GetSkillTypeTag(skillNameTag);
            int range = zeroRange;

            Dictionary<SkillComponentTag, int[]> compEffect;
            SkillComponentTag checkComp;

            if (skillType != SkillTypeTag.Buff)
            {
                range = baseRange;
                compEffect = GetComponent<PCSkillManager>()
                  .GetSkillEffect(skillNameTag);
                checkComp = SkillComponentTag.AirMerit;

                if (compEffect.TryGetValue(checkComp, out int[] powerDuration))
                {
                    range += powerDuration[0];
                }
            }

            nameRange[skillNameTag] = range;
            return range;
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
        }
    }
}
