using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem.GameDataTag;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public interface IActorStatus
    {
        Dictionary<SkillComponentTag, EffectData> CurrentStatus { get; }

        void AddStatus(SkillComponentTag skillComponentTag,
            EffectData effectData);

        bool HasStatus(SkillComponentTag skillComponentTag,
            out EffectData effectData);

        void RemoveStatus(SkillComponentTag skillComponentTag);
    }

    public class ActorStatus : MonoBehaviour, IActorStatus
    {
        private Dictionary<SkillComponentTag, EffectData> compIntStatus;
        private SkillComponentTag[] negativeStatus;
        private SkillComponentTag[] positiveStatus;

        public Dictionary<SkillComponentTag, EffectData> CurrentStatus
        {
            get
            {
                return new Dictionary<SkillComponentTag, EffectData>(
                    compIntStatus);
            }
        }

        public void AddStatus(SkillComponentTag skillComponentTag,
            EffectData effectData)
        {
            TryMergeStatus(skillComponentTag, effectData);
            TryNegateStatus();
        }

        public bool HasStatus(SkillComponentTag skillComponentTag,
            out EffectData effectData)
        {
            if (compIntStatus.TryGetValue(skillComponentTag, out effectData))
            {
                return true;
            }
            effectData = null;
            return false;
        }

        public void RemoveStatus(SkillComponentTag skillComponentTag)
        {
            compIntStatus.Remove(skillComponentTag);
        }

        private void Awake()
        {
            compIntStatus = new Dictionary<SkillComponentTag, EffectData>();

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
            Test();
        }

        private void Test()
        {
            if (GetComponent<MetaInfo>().SubTag != SubTag.PC)
            {
                return;
            }

            AddStatus(SkillComponentTag.AirFlaw, new EffectData(2, 3));
            AddStatus(SkillComponentTag.AirMerit, new EffectData(2, 3));

            AddStatus(SkillComponentTag.EarthMerit, new EffectData(2, 3));

            AddStatus(SkillComponentTag.FireMerit, new EffectData(4, 1));
            AddStatus(SkillComponentTag.FireMerit, new EffectData(4, 2));
            AddStatus(SkillComponentTag.FireFlaw, new EffectData(4, 2));
        }

        private void TryMergeStatus(SkillComponentTag skillComponentTag,
            EffectData effectData)
        {
            if (compIntStatus.ContainsKey(skillComponentTag))
            {
                compIntStatus[skillComponentTag].Power += effectData.Power;
                compIntStatus[skillComponentTag].Duration += effectData.Duration;
            }
            else
            {
                compIntStatus[skillComponentTag] = effectData;
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
                        = compIntStatus[positiveStatus[i]].Duration
                        - compIntStatus[negativeStatus[i]].Duration;

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

                    compIntStatus[keepComp].Duration
                        -= compIntStatus[removeComp].Duration;
                    RemoveStatus(removeComp);
                }
            }
        }
    }
}
