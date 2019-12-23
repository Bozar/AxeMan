using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.PlayerInput;
using UnityEngine;

namespace AxeMan.GameSystem.GameMode
{
    public class StartScreen : MonoBehaviour
    {
        private SwitchGameModeEventArgs enterBuildSkill;
        private SwitchGameModeEventArgs enterNormal;

        private void Awake()
        {
            enterNormal = new SwitchGameModeEventArgs(
                GameModeTag.StartMode, GameModeTag.NormalMode);
            enterBuildSkill = new SwitchGameModeEventArgs(
                GameModeTag.StartMode, GameModeTag.BuildSkillMode);
        }

        private bool EnterBuildSkill(PlayerInputEventArgs e)
        {
            return e.Command == CommandTag.PrintSkill;
        }

        private bool LeaveStartScreen(PlayerInputEventArgs e)
        {
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
            if (e.GameMode != GameModeTag.StartMode)
            {
                return;
            }
            if (LeaveStartScreen(e))
            {
                GetComponent<GameModeManager>().SwitchGameMode(enterNormal);
            }
            else if (EnterBuildSkill(e))
            {
                GetComponent<GameModeManager>().SwitchGameMode(enterBuildSkill);
            }
        }
    }
}
