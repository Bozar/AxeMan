using AxeMan.GameSystem;
using AxeMan.GameSystem.GameMode;
using System;
using UnityEngine;

namespace AxeMan.DungeonObject.PlayerInput
{
    public class AimMarkerInputSwitcher : MonoBehaviour
    {
        private void AimMarkerInputSwitcher_EnteringAimMode(object sender,
            EventArgs e)
        {
            EnableInput(true);
        }

        private void AimMarkerInputSwitcher_LeavingAimMode(object sender,
            EventArgs e)
        {
            EnableInput(false);
        }

        private void EnableInput(bool enable)
        {
            GetComponent<AimMarkerInputManager>().enabled = enable;
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<AimMode>().EnteringAimMode
                += AimMarkerInputSwitcher_EnteringAimMode;
            GameCore.AxeManCore.GetComponent<AimMode>().LeavingAimMode
                += AimMarkerInputSwitcher_LeavingAimMode;
        }
    }
}
