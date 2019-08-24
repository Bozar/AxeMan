using System;
using UnityEngine;

namespace AxeMan.GameSystem.InitializeGameWorld
{
    public class InitializerSwitcher : MonoBehaviour
    {
        private void InitializerSwitcher_SwitchOffInitMainGame(object sender,
            EventArgs e)
        {
            GetComponent<InitializeMainGame>().enabled = false;
        }

        private void Start()
        {
            GetComponent<InitializeMainGame>().SwitchOffInitMainGame
                += InitializerSwitcher_SwitchOffInitMainGame;
        }
    }
}
