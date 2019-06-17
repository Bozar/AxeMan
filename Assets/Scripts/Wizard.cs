using AxeMan.GameSystem.ObjectFactory;
using AxeMan.GameSystem.PrototypeFactory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

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
            IPrototype[] proto = GetComponent<Blueprint>().GetBlueprint(
                BlueprintTag.Altar);
            GetComponent<CreateObject>().Create(proto);

            proto = GetComponent<Blueprint>().GetBlueprint(BlueprintTag.Floor);
            GetComponent<CreateObject>().Create(proto);
        }

        private void CreateDummy()
        {
            GameObject dummy;
            for (int i = 0; i < GetComponent<DungeonBoard>().DungeonWidth; i++)
            {
                for (int j = 0; j < GetComponent<DungeonBoard>().DungeonHeight; j++)
                {
                    if ((i == GetComponent<DungeonBoard>().DungeonWidth - 1) || (j == 0))
                    {
                        dummy = Instantiate(Resources.Load("Dummy") as GameObject);
                        dummy.transform.position
                            = GetComponent<ConvertCoordinate>().Convert(i, j);
                    }
                }
            }
            dummy = Instantiate(Resources.Load("Dummy") as GameObject);
            dummy.transform.position = new Vector3(-3, -1);
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
            //CreateDummy();
            CreateAltar();

            UIUpdated = true;
        }
    }
}
