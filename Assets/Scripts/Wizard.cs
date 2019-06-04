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
            float minX = -5.5f;
            float maxX = -1.5f;
            float minY = -3.5f;
            float maxY = 0.5f;
            for (float i = minX; i <= maxX; i += 0.5f)
            {
                for (float j = minY; j <= maxY; j += 0.5f)
                {
                    if ((j == maxY) || (i == maxX))
                    {
                        dummy = Instantiate(Resources.Load("Dummy") as GameObject);
                        dummy.transform.localPosition = new Vector3(i, j);
                    }
                }
            }
        }
    }
}
