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

        public event EventHandler<UpdateUIEventArgs> UIText;

        public string NameTag { get { return "Wizard"; } }

        protected virtual void OnUIText(UpdateUIEventArgs e)
        {
            UIText?.Invoke(this, e);
        }

        private void CreateAltar()
        {
            BlueprintTag[] tags = new BlueprintTag[]
            {
                BlueprintTag.Altar, BlueprintTag.Floor,
                BlueprintTag.Trap, BlueprintTag.Actor
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
                    Text[] ui = GetComponent<SearchUI>().SearchText(
                        CanvasTag.Canvas_World, UITag.Modeline);
                    ui[0].text = "[[ Examine ] g 4 [ 2, -3 ]]";

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

        private void Start()
        {
            GetComponent<InputManager>().PlayerCommanding
                += Wizard_PlayerCommanding;
        }

        private void Wizard_PlayerCommanding(object sender,
            PlayerCommandingEventArgs e)
        {
            if (e.Actor.GetComponent<MetaInfo>().STag != SubTag.PC)
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

                case CommandTag.RemoveFromSchedule:
                    if (GetComponent<SearchObject>().Search(1, 1, SubTag.Dummy,
                        out GameObject[] search))
                    {
                        search[0].GetComponent<LocalManager>().Remove();
                    }
                    break;

                case CommandTag.NextInSchedule:
                    GetComponent<TurnManager>().NextActor();
                    break;

                default:
                    break;
            }
        }
    }
}
