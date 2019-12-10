using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.PlayerInput;
using UnityEngine;

namespace AxeMan.GameSystem.GameMode
{
    public class ExamineMode : MonoBehaviour
    {
        private bool EnterMode(PlayerInputEventArgs e)
        {
            return (e.GameMode == GameModeTag.NormalMode)
                && (e.Command == CommandTag.ExamineMode);
        }

        private void ExamineMode_PlayerInputting(object sender,
            PlayerInputEventArgs e)
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

        private bool LeaveMode(PlayerInputEventArgs e)
        {
            return (e.GameMode == GameModeTag.ExamineMode)
                && (e.Command == CommandTag.Cancel);
        }

        private void Start()
        {
            GetComponent<InputManager>().PlayerInputting
                += ExamineMode_PlayerInputting;
        }
    }
}
