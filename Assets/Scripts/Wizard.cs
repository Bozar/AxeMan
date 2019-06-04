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
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if ((i == 8) || (j == 0))
                    {
                        dummy = Instantiate(Resources.Load("Dummy") as GameObject);
                        dummy.transform.localPosition
                            = GetComponent<ConvertCoordinate>().Convert(i, j);
                    }
                }
            }
            int[] pos = GetComponent<ConvertCoordinate>().Convert(new Vector3(-3, -1));
            Debug.Log(pos[0]);
            Debug.Log(pos[1]);
            dummy = Instantiate(Resources.Load("Dummy") as GameObject);
            dummy.transform.localPosition
                = new Vector3(-3, -1);
        }
    }
}
