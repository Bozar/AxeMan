﻿using AxeMan.DungeonObject;
using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.ObjectFactory;
using AxeMan.GameSystem.PlayerInput;
using AxeMan.GameSystem.PrototypeFactory;
using AxeMan.GameSystem.SchedulingSystem;
using AxeMan.GameSystem.SearchGameObject;
using System;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AxeMan.GameSystem
{
    public class SettingReferenceEventArgs : EventArgs
    {
        public SettingReferenceEventArgs(GameObject aimMarker, GameObject pc)
        {
            AimMarker = aimMarker;
            PC = pc;
        }

        public GameObject AimMarker { get; }

        public GameObject PC { get; }
    }

    public class UpdateUIEventArgs : EventArgs
    {
        public ReadOnlyDictionary<string, string> UIData;
        public string UITag;
    }

    public class Wizard : MonoBehaviour
    {
        private bool turnStarted;

        private bool uiUpdated;

        public event EventHandler CreatedWorld;

        public event EventHandler<SettingReferenceEventArgs> SettingReference;

        protected virtual void OnCreatedWorld()
        {
            CreatedWorld?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnSettingReference(SettingReferenceEventArgs e)
        {
            SettingReference?.Invoke(this, e);
        }

        private void AddStatus()
        {
            GameObject pc = GetComponent<SearchObject>().Search(SubTag.PC)[0];
            ActorStatus actorStatus = pc.GetComponent<ActorStatus>();

            actorStatus.AddStatus(SkillComponentTag.AirFlaw, new EffectData(2, 5));
        }

        private void CreateAltar()
        {
            BlueprintTag[] tags = new BlueprintTag[]
            {
                BlueprintTag.Altar, BlueprintTag.Floor,
                BlueprintTag.Trap, BlueprintTag.Actor,
                BlueprintTag.AimMarker, BlueprintTag.ExamineMarker,
            };
            IPrototype[] proto;

            foreach (BlueprintTag t in tags)
            {
                proto = GetComponent<Blueprint>().GetBlueprint(t);
                GetComponent<CreateObject>().Create(proto);
            }
        }

        private void LateUpdate()
        {
            if (uiUpdated)
            {
                if (!turnStarted)
                {
                    SetReference();
                    OnCreatedWorld();

                    GetComponent<TurnManager>().StartTurn();
                    GetComponent<TileOverlay>().RefreshDungeonBoard();

                    turnStarted = true;
                }
                return;
            }
            CreateAltar();

            uiUpdated = true;
        }

        private void ListenPCInput(PlayerCommandingEventArgs e)
        {
            if (e.SubTag != SubTag.PC)
            {
                return;
            }

            switch (e.Command)
            {
                case CommandTag.Reload:
                    SceneManager.LoadSceneAsync(0);
                    break;

                case CommandTag.PrintSchedule:
                    GetComponent<Schedule>().Print();
                    break;

                case CommandTag.ChangeHP:
                    TestHP();
                    break;

                case CommandTag.PrintSkill:
                    //PrintSkill();
                    AddStatus();
                    break;

                default:
                    break;
            }
        }

        private void PrintSkill()
        {
            GameObject pc = GetComponent<SearchObject>().Search(SubTag.PC)[0];
            PCSkillManager skillManager = pc.GetComponent<PCSkillManager>();
            var effectDict = skillManager.GetSkillEffect(SkillNameTag.E);
            string compName;
            string effect;

            foreach (var comp in effectDict.Keys)
            {
                compName = GetComponent<ConvertSkillMetaInfo>()
                    .GetSkillComponentName(comp);
                effect = GetComponent<ConvertSkillMetaInfo>()
                    .GetSkillEffectName(comp, effectDict[comp]);
                Debug.Log(compName + ": " + effect);
            }
        }

        private void SetReference()
        {
            GameObject aimMarker = GetComponent<SearchObject>().Search(SubTag.AimMarker)[0];
            GameObject pc = GetComponent<SearchObject>().Search(SubTag.PC)[0];
            OnSettingReference(new SettingReferenceEventArgs(aimMarker, pc));
        }

        private void Start()
        {
            GetComponent<InputManager>().PlayerCommanding
                += Wizard_PlayerCommanding;
        }

        private void TestHP()
        {
            GameObject pc = GetComponent<SearchObject>().Search(SubTag.PC)[0];

            pc.GetComponent<HP>().Subtract(5);
            pc.GetComponent<HP>().Add(2);
        }

        private void Wizard_PlayerCommanding(object sender,
            PlayerCommandingEventArgs e)
        {
            ListenPCInput(e);
        }
    }
}
