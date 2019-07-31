using AxeMan.GameSystem.GameDataTag;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.DungeonObject.ActorSkill
{
    public interface ISkillEffect
    {
        // int[] { power, duration }
        Dictionary<SkillComponentTag, int[]> GetSkillEffect(
            SkillNameTag skillNameTag);
    }

    public class SkillEffect : MonoBehaviour, ISkillEffect
    {
        private int baseDuration;
        private int basePower;

        private Dictionary<SkillNameTag, Dictionary<SkillComponentTag, int[]>>
            nameComp;

        public Dictionary<SkillComponentTag, int[]> GetSkillEffect(
            SkillNameTag skillNameTag)
        {
            if (!nameComp.ContainsKey(skillNameTag))
            {
                return null;
            }

            Dictionary<SkillComponentTag, int[]> compInt = nameComp[skillNameTag];
            if (compInt == null)
            {
                compInt = SetSkillEffect(skillNameTag);
            }
            return new Dictionary<SkillComponentTag, int[]>(compInt);
        }

        private void Awake()
        {
            basePower = 2;
            baseDuration = 2;

            nameComp = new Dictionary<SkillNameTag,
                Dictionary<SkillComponentTag, int[]>>()
            {
                { SkillNameTag.Q, null },
                { SkillNameTag.W, null },
                { SkillNameTag.E, null },
                { SkillNameTag.R, null },
            };
        }

        private Dictionary<SkillComponentTag, int[]> SetSkillEffect(
            SkillNameTag skillNameTag)
        {
            Dictionary<SkillSlotTag, SkillComponentTag> slotComp
                = GetComponent<PCSkillManager>().GetSkillSlot(skillNameTag);
            Dictionary<SkillComponentTag, int[]> compInt
                = new Dictionary<SkillComponentTag, int[]>();

            foreach (SkillComponentTag sct in slotComp.Values)
            {
                if (compInt.TryGetValue(sct, out int[] powerDuration))
                {
                    powerDuration[0] += basePower;
                    powerDuration[1] += baseDuration;
                }
                else
                {
                    compInt[sct] = new int[] { basePower, baseDuration };
                }
            }
            return compInt;
        }

        private void Start()
        {
        }
    }
}
