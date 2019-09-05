using AxeMan.GameSystem.GameDataTag;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.DungeonObject.ActorSkill
{
    public interface ISkillEffect
    {
        Dictionary<SkillComponentTag, EffectData> GetSkillEffect(
            SkillNameTag skillNameTag);
    }

    public class EffectData
    {
        public EffectData(int power, int duration)
        {
            Power = power;
            Duration = duration;
        }

        public int Duration { get; set; }

        public int Power { get; set; }
    }

    public class SkillEffect : MonoBehaviour, ISkillEffect
    {
        private int baseDuration;
        private int basePower;

        private Dictionary<SkillNameTag,
            Dictionary<SkillComponentTag, EffectData>> nameComp;

        public Dictionary<SkillComponentTag, EffectData> GetSkillEffect(
            SkillNameTag skillNameTag)
        {
            if (!nameComp.ContainsKey(skillNameTag))
            {
                return null;
            }

            Dictionary<SkillComponentTag, EffectData> compInt
                = nameComp[skillNameTag];
            if (compInt == null)
            {
                compInt = SetSkillEffect(skillNameTag);
            }
            return new Dictionary<SkillComponentTag, EffectData>(compInt);
        }

        private void Awake()
        {
            basePower = 2;
            baseDuration = 2;

            nameComp = new Dictionary<SkillNameTag,
                Dictionary<SkillComponentTag, EffectData>>()
            {
                { SkillNameTag.Q, null },
                { SkillNameTag.W, null },
                { SkillNameTag.E, null },
                { SkillNameTag.R, null },
            };
        }

        private Dictionary<SkillComponentTag, EffectData> SetSkillEffect(
            SkillNameTag skillNameTag)
        {
            Dictionary<SkillSlotTag, SkillComponentTag> slotComp
                = GetComponent<PCSkillManager>().GetSkillSlot(skillNameTag);
            Dictionary<SkillComponentTag, EffectData> compInt
                = new Dictionary<SkillComponentTag, EffectData>();

            foreach (SkillComponentTag sct in slotComp.Values)
            {
                if (compInt.TryGetValue(sct, out EffectData effectData))
                {
                    effectData.Power += basePower;
                    effectData.Duration += baseDuration;
                }
                else
                {
                    compInt[sct] = new EffectData(basePower, baseDuration);
                }
            }
            return compInt;
        }
    }
}
