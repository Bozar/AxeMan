using AxeMan.GameSystem;
using AxeMan.GameSystem.GameMode;
using System;
using UnityEngine;

namespace AxeMan.DungeonObject.PlayerInput
{
    public class LogMarkerInputSwitcher : MonoBehaviour
    {
        private void EnableInput(bool enable)
        {
            GetComponent<LogMarkerInputManager>().enabled = enable;
        }

        private void LogMarkerInputSwitcher_EnteringLogMode(object sender,
            EventArgs e)
        {
            EnableInput(true);
        }

        private void LogMarkerInputSwitcher_LeavingLogMode(object sender,
            EventArgs e)
        {
            EnableInput(false);
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<LogMode>().EnteringLogMode
                += LogMarkerInputSwitcher_EnteringLogMode;
            GameCore.AxeManCore.GetComponent<LogMode>().LeavingLogMode
                += LogMarkerInputSwitcher_LeavingLogMode;
        }
    }
}
