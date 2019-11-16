using AxeMan.GameSystem;
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

        private void PCInputSwitcher_EnteringExamineMode(object sender,
            EventArgs e)
        {
            EnableInput(false);
        }

        private void PCInputSwitcher_EnteringLogMode(object sender, EventArgs e)
        {
            EnableInput(false);
        }

        private void PCInputSwitcher_LeavingAimMode(object sender, EventArgs e)
        {
            EnableInput(true);
        }

        private void PCInputSwitcher_LeavingExamineMode(object sender,
            EventArgs e)
        {
            EnableInput(true);
        }

        private void PCInputSwitcher_LeavingLogMode(object sender, EventArgs e)
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

            GameCore.AxeManCore.GetComponent<ExamineMode>().EnteringExamineMode
                += PCInputSwitcher_EnteringExamineMode;
            GameCore.AxeManCore.GetComponent<ExamineMode>().LeavingExamineMode
                += PCInputSwitcher_LeavingExamineMode;

            GameCore.AxeManCore.GetComponent<LogMode>().EnteringLogMode
                += PCInputSwitcher_EnteringLogMode;
            GameCore.AxeManCore.GetComponent<LogMode>().LeavingLogMode
                += PCInputSwitcher_LeavingLogMode;

            GameCore.AxeManCore.GetComponent<BuryPC>().BuryingPC
                += PCInputSwitcher_BuryingPC;
        }
    }
}
