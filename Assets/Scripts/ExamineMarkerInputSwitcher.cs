using AxeMan.GameSystem;
using AxeMan.GameSystem.GameMode;
using System;
using UnityEngine;

namespace AxeMan.DungeonObject.PlayerInput
{
    public class ExamineMarkerInputSwitcher : MonoBehaviour
    {
        private void EnableInput(bool enable)
        {
            GetComponent<ExamineMarkerInputManager>().enabled = enable;
        }

        private void ExamineMarkerInputSwitcher_EnteringExamineMode(
            object sender, EventArgs e)
        {
            EnableInput(true);
        }

        private void ExamineMarkerInputSwitcher_LeavingExamineMode(
            object sender, EventArgs e)
        {
            EnableInput(false);
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<ExamineMode>().EnteringExamineMode
                += ExamineMarkerInputSwitcher_EnteringExamineMode;
            GameCore.AxeManCore.GetComponent<ExamineMode>().LeavingExamineMode
                += ExamineMarkerInputSwitcher_LeavingExamineMode;
        }
    }
}
