using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.SchedulingSystem;
using System;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public interface INPCBonusAction
    {
        int CurrentCooldown { get; }

        int MaxCooldown { get; }

        int MinCooldown { get; }
    }

    public class NPCBonusAction : MonoBehaviour, INPCBonusAction
    {
        private int decrease;

        // TODO: Reset to max cooldown.
        public int CurrentCooldown { get; private set; }

        public int MaxCooldown { get; private set; }

        public int MinCooldown { get; private set; }

        private void Awake()
        {
            MainTag mainTag = GetComponent<MetaInfo>().MainTag;
            SubTag subTag = GetComponent<MetaInfo>().SubTag;
            ActorDataTag dataTag = ActorDataTag.Cooldown;

            MaxCooldown = GameCore.AxeManCore.GetComponent<ActorData>()
                .GetIntData(mainTag, subTag, dataTag);
            CurrentCooldown = MaxCooldown;
            MinCooldown = 0;

            decrease = 1;
        }

        private void NPCBonusAction_StartingTurn(object sender,
            StartOrEndTurnEventArgs e)
        {
            if (!GetComponent<LocalManager>().MatchID(e.ObjectID))
            {
                return;
            }
            if (GetComponent<ActorStatus>().HasStatus(
                SkillComponentTag.WaterFlaw, out _))
            {
                return;
            }

            if (CurrentCooldown > MinCooldown)
            {
                CurrentCooldown -= decrease;
            }
            CurrentCooldown = Math.Max(MinCooldown, CurrentCooldown);
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<TurnManager>().StartingTurn
                += NPCBonusAction_StartingTurn;
        }
    }
}
