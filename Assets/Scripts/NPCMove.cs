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
        private int impassable;
        private int minDistance;
        private int move;
        private int passableInitial;
        private int[,] passableMap;
        private int trap;
        private int trapInitial;
        private int[,] trapMap;

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
        }

        private void Awake()
        {
            MainTag mainTag = GetComponent<MetaInfo>().MainTag;
            SubTag subTag = GetComponent<MetaInfo>().SubTag;
            ActorDataTag dataTag = ActorDataTag.MoveDistance;

            baseDistance = GameCore.AxeManCore.GetComponent<ActorData>()
                .GetIntData(mainTag, subTag, dataTag);
            minDistance = 1;

            int width = GameCore.AxeManCore.GetComponent<DungeonBoard>().Width;
            int height = GameCore.AxeManCore.GetComponent<DungeonBoard>().Height;

            passableMap = new int[width, height];
            trapMap = new int[width, height];

            passableInitial = -99999;
            trapInitial = 0;

            impassable = 99999;
            move = 10;
            trap = 5;
        }
    }
}
