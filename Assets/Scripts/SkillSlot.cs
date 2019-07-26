using AxeMan.GameSystem.GameDataTag;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.DungeonObject.ActorSkill
{
    public interface ISkillSlot
    {
        Dictionary<SkillSlotTag, SkillComponentTag> GetSkillSlot(
            SkillNameTag skillNameTag);

        bool TrySetSkillSlot(SkillNameTag skillNameTag, SkillSlotTag skillSlotTag,
            SkillComponentTag skillComponentTag);
    }

    public class SkillSlot : MonoBehaviour, ISkillSlot
    {
        private Dictionary<SkillNameTag,
            Dictionary<SkillSlotTag, SkillComponentTag>> componentDict;

        public Dictionary<SkillSlotTag, SkillComponentTag> GetSkillSlot(
            SkillNameTag skillNameTag)
        {
            if (componentDict.TryGetValue(skillNameTag, out var slotComp))
            {
                return new Dictionary<SkillSlotTag, SkillComponentTag>(slotComp);
            }
            return null;
        }

        public bool TrySetSkillSlot(SkillNameTag skillNameTag,
            SkillSlotTag skillSlotTag, SkillComponentTag skillComponentTag)
        {
            throw new System.NotImplementedException();
        }

        private void Awake()
        {
            componentDict = new Dictionary<SkillNameTag,
                Dictionary<SkillSlotTag, SkillComponentTag>>();
            SkillNameTag[] skillNameTags = new SkillNameTag[]
            {
                SkillNameTag.Q, SkillNameTag.W, SkillNameTag.E, SkillNameTag.R
            };

            foreach (SkillNameTag snt in skillNameTags)
            {
                componentDict[snt]
                    = new Dictionary<SkillSlotTag, SkillComponentTag>();
            }
        }

        private void Start()
        {
        }
    }
}
