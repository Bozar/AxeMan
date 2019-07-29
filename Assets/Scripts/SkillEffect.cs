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
            skillEffectDict;

        public Dictionary<SkillComponentTag, int[]> GetSkillEffect(
            SkillNameTag skillNameTag)
        {
            // NOTE: Set skill effect by subscribing an event. The event is
            // published when player finished creating the character.

            //SetSkillEffect(SkillNameTag.Q);

            if (skillEffectDict.TryGetValue(skillNameTag, out var comp))
            {
                return comp;
            }
            return null;
        }

        private void Awake()
        {
            basePower = 2;
            baseDuration = 2;

            SkillNameTag[] skillNames = new SkillNameTag[]
            {
                SkillNameTag.Q, SkillNameTag.W, SkillNameTag.E, SkillNameTag.R,
            };
            skillEffectDict = new Dictionary<SkillNameTag,
                Dictionary<SkillComponentTag, int[]>>();
            foreach (SkillNameTag snt in skillNames)
            {
                skillEffectDict[snt] = new Dictionary<SkillComponentTag, int[]>();
            }
        }

        private void SetSkillEffect(SkillNameTag skillNameTag)
        {
            if (!skillEffectDict.TryGetValue(skillNameTag, out var compDict))
            {
                return;
            }

            Dictionary<SkillSlotTag, SkillComponentTag> slotComp
                = GetComponent<PCSkillManager>().GetSkillSlot(skillNameTag);

            foreach (SkillComponentTag comp in slotComp.Values)
            {
                if (compDict.TryGetValue(comp, out int[] powerDuration))
                {
                    powerDuration[0] += basePower;
                    powerDuration[1] += baseDuration;
                }
                else
                {
                    compDict[comp] = new int[] { basePower, baseDuration };
                }
            }
        }

        private void Start()
        {
        }
    }
}
