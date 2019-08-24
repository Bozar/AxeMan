using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.ObjectFactory;
using AxeMan.GameSystem.PrototypeFactory;
using AxeMan.GameSystem.SchedulingSystem;
using AxeMan.GameSystem.SearchGameObject;
using System;
using UnityEngine;

namespace AxeMan.GameSystem.InitializeGameWorld
{
    public class InitializeMainGame : MonoBehaviour
    {
        private bool endedLoop1;
        private bool endedLoop2;

        public event EventHandler CreatedWorld;

        public event EventHandler<SettingReferenceEventArgs> SettingReference;

        public event EventHandler<EventArgs> SwitchOffInitMainGame;

        protected virtual void OnCreatedWorld()
        {
            CreatedWorld?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnSettingReference(SettingReferenceEventArgs e)
        {
            SettingReference?.Invoke(this, e);
        }

        protected virtual void OnSwitchOffInitMainGame(EventArgs e)
        {
            SwitchOffInitMainGame?.Invoke(this, e);
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

        private void SetReference()
        {
            GameObject aimMarker = GetComponent<SearchObject>().Search(
                SubTag.AimMarker)[0];
            GameObject examineMarker = GetComponent<SearchObject>().Search(
                SubTag.ExamineMarker)[0];
            GameObject pc = GetComponent<SearchObject>().Search(SubTag.PC)[0];

            OnSettingReference(new SettingReferenceEventArgs(aimMarker,
                examineMarker, pc));
        }

        private void Update()
        {
            if (!endedLoop1)
            {
                CreateAltar();
                endedLoop1 = true;

                return;
            }
            else
            {
                if (endedLoop2)
                {
                    return;
                }
                SetReference();
                OnCreatedWorld();

                GetComponent<TurnManager>().StartTurn();
                GetComponent<TileOverlay>().RefreshDungeonBoard();
                endedLoop2 = true;

                OnSwitchOffInitMainGame(EventArgs.Empty);
                return;
            }
        }
    }

    public class SettingReferenceEventArgs : EventArgs
    {
        public SettingReferenceEventArgs(GameObject aimMarker,
            GameObject examineMarker, GameObject pc)
        {
            AimMarker = aimMarker;
            ExamineMarker = examineMarker;
            PC = pc;
        }

        public GameObject AimMarker { get; }

        public GameObject ExamineMarker { get; }

        public GameObject PC { get; }
    }
}
