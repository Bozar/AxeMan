using AxeMan.GameSystem.GameDataTag;
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

            Dictionary<SkillComponentTag, EffectData> compEffect;
            SkillComponentTag checkComp;

            if (skillType != SkillTypeTag.Buff)
            {
                range = baseRange;
                compEffect = GetComponent<PCSkillManager>()
                  .GetSkillEffect(skillNameTag);
                checkComp = SkillComponentTag.AirMerit;

                if (compEffect.TryGetValue(checkComp, out EffectData effectData))
                {
                    range += effectData.Power;
                }
            }

            nameRange[skillNameTag] = range;
            return range;
        }
    }
}
