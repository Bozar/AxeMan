using AxeMan.GameSystem.GameDataTag;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public interface IActorStatus
    {
        Dictionary<SkillComponentTag, int[]> CurrentStatus { get; }

        void AddStatus(SkillComponentTag skillComponentTag, int[] powerDuration);

        bool HasStatus(SkillComponentTag skillComponentTag,
            out int[] powerDuration);

        void RemoveStatus(SkillComponentTag skillComponentTag);
    }

    public class ActorStatus : MonoBehaviour, IActorStatus
    {
        private Dictionary<SkillComponentTag, int[]> compIntStatus;

        public Dictionary<SkillComponentTag, int[]> CurrentStatus
        {
            get
            {
                return new Dictionary<SkillComponentTag, int[]>(compIntStatus);
            }
        }

        public void AddStatus(SkillComponentTag skillComponentTag,
            int[] powerDuration)
        {
            throw new System.NotImplementedException();
        }

        public bool HasStatus(SkillComponentTag skillComponentTag,
            out int[] powerDuration)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveStatus(SkillComponentTag skillComponentTag)
        {
            throw new System.NotImplementedException();
        }

        private void Start()
        {
        }
    }
}
