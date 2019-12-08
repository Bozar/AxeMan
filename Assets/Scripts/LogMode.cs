using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.PlayerInput;
using UnityEngine;

namespace AxeMan.GameSystem.GameMode
{
    public class LogMode : MonoBehaviour
    {
        private bool EnterLogMode(PlayerInputEventArgs e)
        {
            return (e.GameMode == GameModeTag.NormalMode)
                && (e.Command == CommandTag.LogMode);
        }

        private bool LeaveLogMode(PlayerInputEventArgs e)
        {
            return (e.GameMode == GameModeTag.LogMode)
                && (e.Command == CommandTag.Cancel);
        }

        private void LogMode_PlayerInputting(object sender,
            PlayerInputEventArgs e)
        {
            SwitchGameModeEventArgs sgme;

            if (EnterLogMode(e))
            {
                sgme = new SwitchGameModeEventArgs(
                    GameModeTag.NormalMode, GameModeTag.LogMode);
                GetComponent<GameModeManager>().SwitchGameMode(sgme);
            }
            else if (LeaveLogMode(e))
            {
                sgme = new SwitchGameModeEventArgs(
                    GameModeTag.LogMode, GameModeTag.NormalMode);
                GetComponent<GameModeManager>().SwitchGameMode(sgme);
            }
        }

        private void Start()
        {
            GetComponent<InputManager>().PlayerInputting
                += LogMode_PlayerInputting;
        }
    }
}
