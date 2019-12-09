using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class AimMarkerPosition : MonoBehaviour
    {
        private void AimMarkerPosition_SwitchedGameMode(object sender,
            SwitchGameModeEventArgs e)
        {
            if (e.EnterMode == GameModeTag.NormalMode)
            {
                GameCore.AxeManCore.GetComponent<MarkerPosition>()
                    .ResetMarkerPosition(gameObject);
            }
        }

        private void AimMarkerPosition_SwitchingGameMode(object sender,
            SwitchGameModeEventArgs e)
        {
            // This event can be triggered by two objects: PC and AimMarker. In
            // the first case, player presses skill key (QWER) in Normal Mode to
            // enter Aim Mode. In the later case, player presses skill key
            // inside Aim Mode. We should only move the aim marker to PC's
            // position in the FIRST case.
            if (e.EnterMode == GameModeTag.AimMode)
            {
                if (e.LeaveMode == GameModeTag.AimMode)
                {
                    return;
                }
                GameCore.AxeManCore.GetComponent<MarkerPosition>()
                    .MoveMarkerToPC(gameObject);
            }
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<GameModeManager>().SwitchingGameMode
                += AimMarkerPosition_SwitchingGameMode;
            GameCore.AxeManCore.GetComponent<GameModeManager>().SwitchedGameMode
                += AimMarkerPosition_SwitchedGameMode;
        }
    }
}
