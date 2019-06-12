using AxeMan.GameSystem.Blueprint;
using AxeMan.GameSystem.ObjectPool;
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
            IPrototype[] proto = GetComponent<BpNewAltar>().GetBlueprint();
            foreach (IPrototype p in proto)
            {
                GetComponent<ObjectPoolCore>().CreateObject(p);
            }
        }

        private void CreateDummy()
        {
            GameObject dummy;
            IDungeonObject idoDummy;
            for (int i = 0; i < GetComponent<DungeonBoard>().DungeonWidth; i++)
            {
                for (int j = 0; j < GetComponent<DungeonBoard>().DungeonHeight; j++)
                {
                    if ((i == GetComponent<DungeonBoard>().DungeonWidth - 1) || (j == 0))
                    {
                        dummy = Instantiate(Resources.Load("Dummy") as GameObject);
                        dummy.transform.position
                            = GetComponent<ConvertCoordinate>().Convert(i, j);
                        idoDummy = new DungeonObject(MainTag.Actor, dummy);
                        GetComponent<DungeonBoard>().AddObject(i, j, idoDummy, false);
                    }
                }
            }
            dummy = Instantiate(Resources.Load("Dummy") as GameObject);
            dummy.transform.position = new Vector3(-3, -1);

            Debug.Log(GetComponent<DungeonBoard>().ExistObject(1, 1, MainTag.Actor));
            Debug.Log(GetComponent<DungeonBoard>().ExistObject(4, 0, MainTag.Actor));
            DungeonObject test = GetComponent<DungeonBoard>().RemoveObject(4, 0, MainTag.Actor) as DungeonObject;
            Debug.Log(test.DataTag);
            int[] testPos = GetComponent<ConvertCoordinate>().Convert(test.Data.transform.position);
            Debug.Log(testPos[0]);
            Debug.Log(testPos[1]);
            Debug.Log(GetComponent<DungeonBoard>().ExistObject(4, 0, MainTag.Actor));
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
            CreateDummy();
            CreateAltar();

            Debug.Log(GetComponent<DungeonBoard>().ExistObject(4, 4, MainTag.Building));

            UIUpdated = true;
        }

        private void Start()
        {
            Debug.Log(GameCore.AxeManCore.GetComponent<GameCore>().Hello);
            //Debug.Log(GetComponent<GameCore>().Hello);
            //Debug.Log(FindObjectOfType<GameCore>().Hello);
        }
    }
}
