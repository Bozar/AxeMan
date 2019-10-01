using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using System;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public interface INPCMove
    {
        int Distance { get; }

        void Approach();
    }

    public class NPCMove : MonoBehaviour, INPCMove
    {
        private int baseDistance;
        private int minDistance;

        public int Distance
        {
            get
            {
                if (GetComponent<ActorStatus>().HasStatus(
                    SkillComponentTag.EarthFlaw, out EffectData effect))
                {
                    return Math.Max(minDistance, baseDistance - effect.Power);
                }
                return baseDistance;
            }
        }

        public void Approach()
        {
            int[] source = GetComponent<MetaInfo>().Position;
            // TODO: Set maxStep
            int maxStep = 0;
            int[][] path = GetComponent<NPCFindPath>().GetNextStep(
                source, maxStep);
            // TODO: Follow the given path.
        }

        private void Awake()
        {
            MainTag mainTag = GetComponent<MetaInfo>().MainTag;
            SubTag subTag = GetComponent<MetaInfo>().SubTag;
            ActorDataTag dataTag = ActorDataTag.MoveDistance;

            baseDistance = GameCore.AxeManCore.GetComponent<ActorData>()
                .GetIntData(mainTag, subTag, dataTag);
            minDistance = 1;
        }
    }
}
