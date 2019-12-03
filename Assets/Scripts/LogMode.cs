using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.PlayerInput;
using UnityEngine;

namespace AxeMan.GameSystem.GameMode
{
    public class LogMode : MonoBehaviour
    {
        private bool EnterLogMode(PlayerCommandingEventArgs e)
        {
            return (e.SubTag == SubTag.PC) && (e.Command == CommandTag.LogMode);
        }

        private bool LeaveLogMode(PlayerCommandingEventArgs e)
        {
            return (e.SubTag == SubTag.LogMarker)
                && (e.Command == CommandTag.Cancel);
        }

        private void LogMode_PlayerCommanding(object sender,
            PlayerCommandingEventArgs e)
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
            GetComponent<InputManager>().PlayerCommanding
                += LogMode_PlayerCommanding;
        }
    }
}
