﻿using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.SchedulingSystem;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public interface INPCBonusAction
    {
        int CurrentCooldown { get; }

        bool HasBonusAction { get; }

        int MaxCooldown { get; }

        int MinCooldown { get; }

        void TakeBonusAction();
    }

    public class NPCBonusAction : MonoBehaviour, INPCBonusAction
    {
        private int decrease;
        private int restoreHP;

        public int CurrentCooldown { get; private set; }

        public bool HasBonusAction
        {
            get
            {
                return CurrentCooldown == MinCooldown;
            }
        }

        public int MaxCooldown { get; private set; }

        public int MinCooldown { get; private set; }

        public void TakeBonusAction()
        {
            GetComponent<HP>().Add(restoreHP);
            CurrentCooldown = MaxCooldown;
        }

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

        private void NPCBonusAction_StartedTurn(object sender,
            StartOrEndTurnEventArgs e)
        {
            SkillComponentTag flaw = SkillComponentTag.WaterFlaw;

            if (!GetComponent<LocalManager>().MatchID(e.ObjectID)
                || GetComponent<ActorStatus>().HasStatus(flaw, out _))
            {
                return;
            }

            if (CurrentCooldown > MinCooldown)
            {
                CurrentCooldown -= decrease;
            }
        }

        private void Start()
        {
            MainTag mainTag = GetComponent<MetaInfo>().MainTag;
            SubTag subTag = GetComponent<MetaInfo>().SubTag;
            restoreHP = GameCore.AxeManCore.GetComponent<ActorData>()
                .GetIntData(mainTag, subTag, ActorDataTag.RestoreHP);

            GameCore.AxeManCore.GetComponent<TurnManager>().StartedTurn
                += NPCBonusAction_StartedTurn;
        }
    }
}
