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
        void AddStatus(SkillComponentTag skillComponentTag,
            EffectData effectData);

        bool HasStatus(SkillComponentTag skillComponentTag,
            out EffectData effectData);

        void RemoveStatus(SkillComponentTag skillComponentTag);
    }

    public class ActorStatus : MonoBehaviour, IActorStatus
    {
        private Dictionary<SkillComponentTag, EffectData> compIntStatus;
        private int maxPowerDuration;
        private SkillComponentTag[] negativeStatus;
        private SkillComponentTag[] positiveStatus;
        private int reduceDuration;

        public void AddStatus(SkillComponentTag skillComponentTag,
            EffectData effectData)
        {
            TryMergeStatus(skillComponentTag, effectData);
            TryNegateStatus();
            SetMaxPowerDuration();

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

        private void ActorStatus_EndingTurn(object sender,
            StartOrEndTurnEventArgs e)
        {
            SkillComponentTag merit = SkillComponentTag.WaterMerit;
            SkillComponentTag flaw = SkillComponentTag.FireFlaw;

            if (!GetComponent<LocalManager>().MatchID(e.ObjectID))
            {
                return;
            }

            if (HasStatus(flaw, out _))
            {
                TryCountdownDuration(flaw);
                TryCountdownDuration(positiveStatus);
            }
            else if (HasStatus(merit, out _))
            {
                TryCountdownDuration(merit);
                TryCountdownDuration(negativeStatus);
            }
            else
            {
                TryCountdownDuration(positiveStatus);
                TryCountdownDuration(negativeStatus);
            }
            PublishPCStatus();
        }

        private void ActorStatus_StartingTurn(object sender,
            StartOrEndTurnEventArgs e)
        {
            if (!GetComponent<LocalManager>().MatchID(e.ObjectID))
            {
                return;
            }
            TryRemoveStatus();
            PublishPCStatus();
        }

        private void Awake()
        {
            compIntStatus = new Dictionary<SkillComponentTag, EffectData>();
            reduceDuration = 1;
            maxPowerDuration = 9;

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
                .PublishChangedActorStatus();
        }

        private void SetMaxPowerDuration()
        {
            foreach (SkillComponentTag sct in compIntStatus.Keys)
            {
                compIntStatus[sct].Power
                    = Math.Min(maxPowerDuration, compIntStatus[sct].Power);
                compIntStatus[sct].Duration
                    = Math.Min(maxPowerDuration, compIntStatus[sct].Duration);
            }
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<TurnManager>().StartingTurn
                += ActorStatus_StartingTurn;
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
