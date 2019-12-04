using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using UnityEngine;

namespace AxeMan.DungeonObject.PlayerInput
{
    public class ExamineMarkerInputSwitcher : MonoBehaviour
    {
        private void EnableInput(bool enable)
        {
            GetComponent<ExamineMarkerInputManager>().enabled = enable;
        }

        private void ExamineMarkerInputSwitcher_SwitchingGameMode(object sender,
            SwitchGameModeEventArgs e)
        {
            if (e.EnterMode == GameModeTag.ExamineMode)
            {
                EnableInput(true);
            }
            else if (e.LeaveMode == GameModeTag.ExamineMode)
            {
                EnableInput(false);
            }
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<GameModeManager>().SwitchingGameMode
                += ExamineMarkerInputSwitcher_SwitchingGameMode;
        }
    }
}
