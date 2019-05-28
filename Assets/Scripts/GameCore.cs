using System;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public class GameCore : MonoBehaviour
    {
        public string Hello;

        public static GameObject AxeManCore { get; private set; }

        private void Awake()
        {
            Hello = "Hello world";
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
