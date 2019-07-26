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
        public Dictionary<SkillSlotTag, SkillComponentTag> GetSkillSlot(
            SkillNameTag skillNameTag)
        {
            throw new System.NotImplementedException();
        }

        public bool TrySetSkillSlot(SkillNameTag skillNameTag,
            SkillSlotTag skillSlotTag, SkillComponentTag skillComponentTag)
        {
            throw new System.NotImplementedException();
        }

        private void Start()
        {
        }
    }
}
