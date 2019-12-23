using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.PlayerInput;
using UnityEngine;

namespace AxeMan.GameSystem.GameMode
{
    public class BuildSkillMode : MonoBehaviour
    {
        private SwitchGameModeEventArgs leaveBuild;

        private void Awake()
        {
            leaveBuild = new SwitchGameModeEventArgs(
                GameModeTag.BuildSkillMode, GameModeTag.StartMode);
        }

        private void BuildSkillMode_PlayerInputting(object sender,
            PlayerInputEventArgs e)
        {
            if (e.GameMode != GameModeTag.BuildSkillMode)
            {
                return;
            }
            if (e.Command == CommandTag.Cancel)
            {
                GetComponent<GameModeManager>().SwitchGameMode(leaveBuild);
            }
        }

        private void Start()
        {
            GetComponent<InputManager>().PlayerInputting
                += BuildSkillMode_PlayerInputting;
        }
    }
}
