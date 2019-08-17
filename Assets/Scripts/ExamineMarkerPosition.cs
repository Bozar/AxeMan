using AxeMan.GameSystem;
using AxeMan.GameSystem.GameMode;
using System;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class ExamineMarkerPosition : MonoBehaviour
    {
        private void ExamineMarkerPosition_EnteringExamineMode(object sender,
            EventArgs e)
        {
            GameCore.AxeManCore.GetComponent<MarkerPosition>().MoveMarkerToPC(
                gameObject);
        }

        private void ExamineMarkerPosition_LeavingExamineMode(object sender,
            EventArgs e)
        {
            GameCore.AxeManCore.GetComponent<MarkerPosition>().
               ResetMarkerPosition(gameObject);
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<ExamineMode>().EnteringExamineMode
                += ExamineMarkerPosition_EnteringExamineMode;
            GameCore.AxeManCore.GetComponent<ExamineMode>().LeavingExamineMode
                += ExamineMarkerPosition_LeavingExamineMode;
        }
    }
}
