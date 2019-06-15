using System;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public class GameCore : MonoBehaviour
    {
        public static GameObject AxeManCore { get; private set; }

        private void Awake()
        {
            SetAxeManCore();
        }

        private void SetAxeManCore()
        {
            if (AxeManCore != null)
            {
                throw new Exception("Duplicated: AxeManCore.");
            }
            AxeManCore = gameObject;
        }
    }
}
