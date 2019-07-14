using AxeMan.DungeonObject;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.ObjectFactory;
using AxeMan.GameSystem.PlayerInput;
using AxeMan.GameSystem.PrototypeFactory;
using AxeMan.GameSystem.SchedulingSystem;
using AxeMan.GameSystem.SearchGameObject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AxeMan.GameSystem
{
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

        public event EventHandler<UpdateUIEventArgs> UIText;

        public string NameTag { get { return "Wizard"; } }

        protected virtual void OnCreatedWorld()
        {
            CreatedWorld?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnUIText(UpdateUIEventArgs e)
        {
            UIText?.Invoke(this, e);
        }

        private void CreateAltar()
        {
            BlueprintTag[] tags = new BlueprintTag[]
            {
                BlueprintTag.Altar, BlueprintTag.Floor,
                BlueprintTag.Trap, BlueprintTag.Actor, BlueprintTag.AimMarker,
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
                    GetComponent<TurnManager>().StartTurn();
                    GetComponent<TileOverlay>().RefreshDungeonBoard();
                    Text ui = GetComponent<SearchUI>().SearchText(
                        CanvasTag.Canvas_World, UITag.Modeline);
                    ui.text = "[[ Examine ] g 4 [ 2, -3 ]]";

                    OnCreatedWorld();
                    turnStarted = true;
                }
                return;
            }
            Dictionary<string, string> data
                = new Dictionary<string, string>()
                {
                    {
                        "Modeline", "[[ Examine ] p 5 [ -1, -3 ]]"
                    },
                    {
                        "Text","Hello world"
                    }
                };
            OnUIText(new UpdateUIEventArgs
            {
                UITag = "CvsWorld",
                UIData = new ReadOnlyDictionary<string, string>(data)
            });
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

                default:
                    break;
            }
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
