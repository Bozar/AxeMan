using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using UnityEngine;

namespace AxeMan.DungeonObject.PlayerInput
{
    public class AimMarkerInputSwitcher : MonoBehaviour
    {
        private void AimMarkerInputSwitcher_SwitchingGameMode(object sender,
            SwitchGameModeEventArgs e)
        {
            if (e.EnterMode == GameModeTag.AimMode)
            {
                EnableInput(true);
            }
            else if (e.LeaveMode == GameModeTag.AimMode)
            {
                EnableInput(false);
            }
        }

        private void EnableInput(bool enable)
        {
            GetComponent<AimMarkerInputManager>().enabled = enable;
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<GameModeManager>().SwitchingGameMode
                += AimMarkerInputSwitcher_SwitchingGameMode;
        }
    }
}
