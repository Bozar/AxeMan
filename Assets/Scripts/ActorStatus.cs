using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using System;
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

            GameCore.AxeManCore.GetComponent<PublishActorStatus>()
                .PublishChangedActorStatus(EventArgs.Empty);
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

            GameCore.AxeManCore.GetComponent<PublishActorStatus>()
               .PublishChangedActorStatus(EventArgs.Empty);
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
                        // Avoid using RemoveStatus() so as not to trigger
                        // ChangedActorStatus event.
                        compIntStatus.Remove(positiveStatus[i]);
                        compIntStatus.Remove(negativeStatus[i]);
                        continue;
                    }

                    compIntStatus[keepComp].Duration
                        -= compIntStatus[removeComp].Duration;
                    compIntStatus.Remove(removeComp);
                }
            }
        }
    }
}
