using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.PlayerInput;
using UnityEngine;

namespace AxeMan.GameSystem.GameMode
{
    public class StartScreen : MonoBehaviour
    {
        private bool LeaveStartScreen(PlayerInputEventArgs e)
        {
            if (e.GameMode != GameModeTag.StartMode)
            {
                return false;
            }
            return e.Command == CommandTag.Confirm;
        }

        private void Start()
        {
            GetComponent<InputManager>().PlayerInputting
                += StartScreen_PlayerInputting;
        }

        private void StartScreen_PlayerInputting(object sender,
            PlayerInputEventArgs e)
        {
            if (LeaveStartScreen(e))
            {
                GetComponent<GameModeManager>().SwitchGameMode(
                    new SwitchGameModeEventArgs(
                        GameModeTag.StartMode, GameModeTag.NormalMode));
            }
        }
    }
}
