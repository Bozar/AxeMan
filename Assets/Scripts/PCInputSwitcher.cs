using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.SchedulingSystem;
using System;
using UnityEngine;

namespace AxeMan.DungeonObject.PlayerInput
{
    public class PCInputSwitcher : MonoBehaviour
    {
        private void EnableInput(bool enable)
        {
            GetComponent<PCInputManager>().enabled = enable;
        }

        private void PCInputSwitcher_BuryingPC(object sender, EventArgs e)
        {
            EnableInput(false);
        }

        private void PCInputSwitcher_EndingTurn(object sender,
            StartOrEndTurnEventArgs e)
        {
            if (!GetComponent<LocalManager>().MatchID(e.ObjectID))
            {
                return;
            }
            EnableInput(false);
        }

        private void PCInputSwitcher_EnteringAimMode(object sender, EventArgs e)
        {
            EnableInput(false);
        }

        private void PCInputSwitcher_LeavingAimMode(object sender, EventArgs e)
        {
            EnableInput(true);
        }

        private void PCInputSwitcher_StartingTurn(object sender,
            StartOrEndTurnEventArgs e)
        {
            if (!GetComponent<LocalManager>().MatchID(e.ObjectID))
            {
                return;
            }
            EnableInput(true);
        }

        private void PCInputSwitcher_SwitchingGameMode(object sender,
            SwitchGameModeEventArgs e)
        {
            if (e.LeaveMode == GameModeTag.NormalMode)
            {
                EnableInput(false);
            }
            else if (e.EnterMode == GameModeTag.NormalMode)
            {
                EnableInput(true);
            }
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<TurnManager>().StartingTurn
                += PCInputSwitcher_StartingTurn;
            GameCore.AxeManCore.GetComponent<TurnManager>().EndingTurn
                += PCInputSwitcher_EndingTurn;

            GameCore.AxeManCore.GetComponent<AimMode>().EnteringAimMode
                += PCInputSwitcher_EnteringAimMode;
            GameCore.AxeManCore.GetComponent<AimMode>().LeavingAimMode
                += PCInputSwitcher_LeavingAimMode;

            GameCore.AxeManCore.GetComponent<GameModeManager>().SwitchingGameMode
                += PCInputSwitcher_SwitchingGameMode;

            GameCore.AxeManCore.GetComponent<BuryPC>().BuryingPC
                += PCInputSwitcher_BuryingPC;
        }
    }
}
