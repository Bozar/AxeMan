using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using System;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class AimMarkerPosition : MonoBehaviour
    {
        private void AimMarkerPosition_EnteringAimMode(object sender,
            EnterAimModeEventArgs e)
        {
            // This event can be triggered by two objects: PC and AimMarker. In
            // the first case, player presses skill key (QWER) in Normal Mode to
            // enter Aim Mode. In the later case, player presses skill key inside
            // Aim Mode. We should only move the aim marker to PC's position in
            // the FIRST case.
            if (e.SubTag == SubTag.AimMarker)
            {
                return;
            }

            GameCore.AxeManCore.GetComponent<MarkerPosition>().MoveMarkerToPC(
                gameObject);
        }

        private void AimMarkerPosition_LeavingAimMode(object sender, EventArgs e)
        {
            GameCore.AxeManCore.GetComponent<MarkerPosition>().
                ResetMarkerPosition(gameObject);
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<AimMode>().EnteringAimMode
                += AimMarkerPosition_EnteringAimMode;
            GameCore.AxeManCore.GetComponent<AimMode>().LeavingAimMode
                += AimMarkerPosition_LeavingAimMode;
        }
    }
}
