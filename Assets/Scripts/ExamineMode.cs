using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.PlayerInput;
using UnityEngine;

namespace AxeMan.GameSystem.GameMode
{
    public class ExamineMode : MonoBehaviour
    {
        private bool EnterMode(PlayerCommandingEventArgs e)
        {
            if (e.SubTag != SubTag.PC)
            {
                return false;
            }
            return e.Command == CommandTag.ExamineMode;
        }

        private void ExamineMode_PlayerCommanding(object sender,
            PlayerCommandingEventArgs e)
        {
            if (EnterMode(e))
            {
                GetComponent<GameModeManager>().SwitchGameMode(
                    new SwitchGameModeEventArgs(
                        GameModeTag.NormalMode, GameModeTag.ExamineMode));
            }
            else if (LeaveMode(e))
            {
                GetComponent<GameModeManager>().SwitchGameMode(
                    new SwitchGameModeEventArgs(
                        GameModeTag.ExamineMode, GameModeTag.NormalMode));
            }
        }

        private bool LeaveMode(PlayerCommandingEventArgs e)
        {
            if (e.SubTag != SubTag.ExamineMarker)
            {
                return false;
            }
            return e.Command == CommandTag.Cancel;
        }

        private void Start()
        {
            GetComponent<InputManager>().PlayerCommanding
                += ExamineMode_PlayerCommanding;
        }
    }
}
