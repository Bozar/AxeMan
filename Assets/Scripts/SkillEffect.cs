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
        public Dictionary<SkillComponentTag, int[]> GetSkillEffect(
            SkillNameTag skillNameTag)
        {
            throw new System.NotImplementedException();
        }

        private void Start()
        {
        }
    }
}
