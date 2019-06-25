using AxeMan.Actor;
using AxeMan.GameSystem.ObjectFactory;
using AxeMan.GameSystem.PrototypeFactory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AxeMan.GameSystem
{
    public class UpdateUIEventArgs : EventArgs
    {
        public ReadOnlyDictionary<string, string> UIData;
        public string UITag;
    }

    public class Wizard : MonoBehaviour
    {
        private bool UIUpdated;

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
            if (UIUpdated)
            {
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

            UIUpdated = true;
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

                default:
                    break;
            }
        }
    }
}
