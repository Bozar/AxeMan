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
        private int height;
        private int impassable;
        private int minDistance;
        private int move;
        private int passableInitial;
        private int[,] passableMap;
        private int trap;
        private int trapInitial;
        private int[,] trapMap;
        private int width;

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
            ResetMap(passableMap, passableInitial);
            ResetMap(trapMap, trapInitial);
        }

        private void Awake()
        {
            MainTag mainTag = GetComponent<MetaInfo>().MainTag;
            SubTag subTag = GetComponent<MetaInfo>().SubTag;
            ActorDataTag dataTag = ActorDataTag.MoveDistance;

            baseDistance = GameCore.AxeManCore.GetComponent<ActorData>()
                .GetIntData(mainTag, subTag, dataTag);
            minDistance = 1;

            width = GameCore.AxeManCore.GetComponent<DungeonBoard>().Width;
            height = GameCore.AxeManCore.GetComponent<DungeonBoard>().Height;

            passableMap = new int[width, height];
            trapMap = new int[width, height];

            passableInitial = -99999;
            trapInitial = 0;

            impassable = 99999;
            move = 10;
            trap = 5;
        }

        private void ResetMap(int[,] map, int initial)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    map[i, j] = initial;
                }
            }
        }
    }
}
