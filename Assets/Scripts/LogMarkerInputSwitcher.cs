using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using UnityEngine;

namespace AxeMan.DungeonObject.PlayerInput
{
    public class LogMarkerInputSwitcher : MonoBehaviour
    {
        private void EnableInput(bool enable)
        {
            GetComponent<LogMarkerInputManager>().enabled = enable;
        }

        private void LogMarkerInputSwitcher_SwitchingGameMode(object sender,
            SwitchGameModeEventArgs e)
        {
            if (e.EnterMode == GameModeTag.LogMode)
            {
                EnableInput(true);
            }
            else if (e.LeaveMode == GameModeTag.LogMode)
            {
                EnableInput(false);
            }
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<GameModeManager>().SwitchingGameMode
                += LogMarkerInputSwitcher_SwitchingGameMode;
        }
    }
}
