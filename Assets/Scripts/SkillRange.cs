using AxeMan.GameSystem.GameDataTag;
using System;
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
        private int minRange;
        private Dictionary<SkillNameTag, int> nameRange;

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

            range += StatusMod();
            range = Math.Max(minRange, range);

            return range;
        }

        private void Awake()
        {
            invalidRange = -1;
            minRange = 1;
            baseRange = 1;

            nameRange = new Dictionary<SkillNameTag, int>
            {
                [SkillNameTag.SkillQ] = invalidRange,
                [SkillNameTag.SkillW] = invalidRange,
                [SkillNameTag.SkillE] = invalidRange,
                [SkillNameTag.SkillR] = invalidRange,
            };
        }

        private int SetBaseRange(SkillNameTag skillNameTag)
        {
            SkillTypeTag skillType = GetComponent<PCSkillManager>()
               .GetSkillTypeTag(skillNameTag);
            int range = minRange;

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

        private int StatusMod()
        {
            int increase = 0;
            int decrease = 0;
            ActorStatus actorStatus = GetComponent<ActorStatus>();
            EffectData effectData;

            if (actorStatus.HasStatus(SkillComponentTag.AirMerit,
                out effectData))
            {
                increase = effectData.Power;
            }
            if (actorStatus.HasStatus(SkillComponentTag.EarthFlaw,
                out effectData))
            {
                decrease = effectData.Power;
            }
            return increase - decrease;
        }
    }
}
