using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.PlayerInput;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AxeMan.GameSystem.GameMode
{
    public class DeadMode : MonoBehaviour
    {
        private void DeadMode_BuryingPC(object sender, EventArgs e)
        {
            GameModeTag leave = GetComponent<GameModeManager>().CurrentGameMode;
            GetComponent<GameModeManager>().SwitchGameMode(
                new SwitchGameModeEventArgs(leave, GameModeTag.DeadMode));
        }

        private void DeadMode_PlayerInputting(object sender,
            PlayerInputEventArgs e)
        {
            if ((e.GameMode != GameModeTag.DeadMode)
                || (e.Command != CommandTag.Reload))
            {
                return;
            }
            SceneManager.LoadSceneAsync(0);
        }

        private void Start()
        {
            GetComponent<BuryPC>().BuryingPC += DeadMode_BuryingPC;
            GetComponent<InputManager>().PlayerInputting
                += DeadMode_PlayerInputting;
        }
    }
}
