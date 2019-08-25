using AxeMan.GameSystem;
using AxeMan.GameSystem.GameMode;
using System;
using UnityEngine;

namespace AxeMan.DungeonObject.PlayerInput
{
    public class StartScreenCursorInputSwitcher : MonoBehaviour
    {
        private void Start()
        {
            GameCore.AxeManCore.GetComponent<StartScreen>().LeavingStartScreen
                += StartScreenCursorInputSwitcher_LeavingStartScreen;
        }

        private void StartScreenCursorInputSwitcher_LeavingStartScreen(
            object sender, EventArgs e)
        {
            GetComponent<StartScreenCursorInputManager>().enabled = false;
        }
    }
}
