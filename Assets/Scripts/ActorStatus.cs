using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.SchedulingSystem;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private int reduceDuration;

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

            PublishPCStatus();
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

            PublishPCStatus();
        }

        private void ActorStatus_EndingTurn(object sender, EndingTurnEventArgs e)
        {
            if (!GetComponent<LocalManager>().MatchID(e.ObjectID))
            {
                return;
            }

            if (HasStatus(SkillComponentTag.FireFlaw, out _))
            {
                TryCountdownDuration(positiveStatus);
                TryCountdownDuration(SkillComponentTag.FireFlaw);
            }
            else if (HasStatus(SkillComponentTag.WaterMerit, out _))
            {
                TryCountdownDuration(negativeStatus);
                TryCountdownDuration(SkillComponentTag.WaterMerit);
            }
            else
            {
                TryCountdownDuration(positiveStatus);
                TryCountdownDuration(negativeStatus);
            }

            TryRemoveStatus();
            PublishPCStatus();
        }

        private void Awake()
        {
            compIntStatus = new Dictionary<SkillComponentTag, EffectData>();
            reduceDuration = 1;

            positiveStatus = new SkillComponentTag[]
            {
                SkillComponentTag.FireMerit,
                SkillComponentTag.WaterMerit,
                SkillComponentTag.AirMerit,
                SkillComponentTag.EarthMerit,
            };

            // Fire and Water cancels each other. So does Air and Earth.
            negativeStatus = new SkillComponentTag[]
            {
                SkillComponentTag.WaterFlaw,
                SkillComponentTag.FireFlaw,
                SkillComponentTag.EarthFlaw,
                SkillComponentTag.AirFlaw,
            };
        }

        private void PublishPCStatus()
        {
            if (GetComponent<MetaInfo>().SubTag != SubTag.PC)
            {
                return;
            }
            GameCore.AxeManCore.GetComponent<PublishActorStatus>()
                .PublishChangedActorStatus(EventArgs.Empty);
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<TurnManager>().EndingTurn
                += ActorStatus_EndingTurn;
        }

        private void TryCountdownDuration(SkillComponentTag skillComponent)
        {
            if (compIntStatus.ContainsKey(skillComponent))
            {
                compIntStatus[skillComponent].Duration -= reduceDuration;
            }
        }

        private void TryCountdownDuration(SkillComponentTag[] skillComponents)
        {
            foreach (SkillComponentTag sct in skillComponents)
            {
                TryCountdownDuration(sct);
            }
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

        private void TryRemoveStatus()
        {
            foreach (SkillComponentTag sct in compIntStatus.Keys.ToArray())
            {
                if (compIntStatus[sct].Duration < 1)
                {
                    compIntStatus.Remove(sct);
                }
            }
        }
    }
}
