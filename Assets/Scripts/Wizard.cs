using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public class Dummy : IDungeonObject
    {
        public Dummy(GameObject actor)
        {
            Actor = actor;
        }

        public GameObject Actor { get; }

        public MainTag DataTag => MainTag.Actor;
    }

    public class UpdateUIEventArgs : EventArgs
    {
        public ReadOnlyDictionary<string, string> UIData;
        public string UITag;
    }

    public class Wizard : MonoBehaviour
    {
        private bool UIUpdated;

        public event EventHandler<UpdateUIEventArgs> UIText;

        protected virtual void OnUIText(UpdateUIEventArgs e)
        {
            UIText?.Invoke(this, e);
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
            UIUpdated = true;
        }

        private void Start()
        {
            Debug.Log(GameCore.AxeManCore.GetComponent<GameCore>().Hello);
            //Debug.Log(GetComponent<GameCore>().Hello);
            //Debug.Log(FindObjectOfType<GameCore>().Hello);

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
                        idoDummy = new Dummy(dummy);
                        GetComponent<DungeonBoard>().AddObject(i, j, idoDummy, false);
                    }
                }
            }
            dummy = Instantiate(Resources.Load("Dummy") as GameObject);
            dummy.transform.position = new Vector3(-3, -1);

            Debug.Log(GetComponent<DungeonBoard>().ExistObject(1, 1, MainTag.Actor));
            Debug.Log(GetComponent<DungeonBoard>().ExistObject(4, 0, MainTag.Actor));
            Dummy test = GetComponent<DungeonBoard>().RemoveObject(4, 0, MainTag.Actor) as Dummy;
            Debug.Log(test.DataTag);
            int[] testPos = GetComponent<ConvertCoordinate>().Convert(test.Actor.transform.position);
            Debug.Log(testPos[0]);
            Debug.Log(testPos[1]);
            Debug.Log(GetComponent<DungeonBoard>().ExistObject(4, 0, MainTag.Actor));
        }
    }
}
