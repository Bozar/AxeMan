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
        private SkillComponentTag[] negativeStatus;
        private SkillComponentTag[] positiveStatus;

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
            TryMergeStatus(skillComponentTag, powerDuration);
            TryNegateStatus();
        }

        public bool HasStatus(SkillComponentTag skillComponentTag,
            out int[] powerDuration)
        {
            if (compIntStatus.TryGetValue(skillComponentTag, out powerDuration))
            {
                return true;
            }
            powerDuration = null;
            return false;
        }

        public void RemoveStatus(SkillComponentTag skillComponentTag)
        {
            compIntStatus.Remove(skillComponentTag);
        }

        private void Awake()
        {
            compIntStatus = new Dictionary<SkillComponentTag, int[]>();

            positiveStatus = new SkillComponentTag[]
            {
                SkillComponentTag.FireMerit,
                SkillComponentTag.WaterMerit,
                SkillComponentTag.AirMerit,
                SkillComponentTag.EarthMerit,
            };

            negativeStatus = new SkillComponentTag[]
            {
                SkillComponentTag.FireFlaw,
                SkillComponentTag.WaterFlaw,
                SkillComponentTag.AirFlaw,
                SkillComponentTag.EarthFlaw,
            };
        }

        private void Start()
        {
        }

        private void TryMergeStatus(SkillComponentTag skillComponentTag,
            int[] powerDuration)
        {
            if (compIntStatus.ContainsKey(skillComponentTag))
            {
                compIntStatus[skillComponentTag][0] += powerDuration[0];
                compIntStatus[skillComponentTag][1] += powerDuration[1];
            }
            else
            {
                compIntStatus[skillComponentTag] = powerDuration;
            }
        }

        private void TryNegateStatus()
        {
            SkillComponentTag removeComp;
            SkillComponentTag keepComp;
            int positiveMinusNegative;

            for (int i = 0; i < positiveStatus.Length; i++)
            {
                if (compIntStatus.ContainsKey(positiveStatus[i])
                    && compIntStatus.ContainsKey(negativeStatus[i]))
                {
                    positiveMinusNegative
                        = compIntStatus[positiveStatus[i]][1]
                        - compIntStatus[negativeStatus[i]][1];

                    if (positiveMinusNegative > 0)
                    {
                        keepComp = positiveStatus[i];
                        removeComp = negativeStatus[i];
                    }
                    else if (positiveMinusNegative < 0)
                    {
                        keepComp = negativeStatus[i];
                        removeComp = positiveStatus[i];
                    }
                    else
                    {
                        RemoveStatus(positiveStatus[i]);
                        RemoveStatus(negativeStatus[i]);
                        continue;
                    }

                    compIntStatus[keepComp][1] -= compIntStatus[removeComp][1];
                    RemoveStatus(removeComp);
                }
            }
        }
    }
}
