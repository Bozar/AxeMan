using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.PlayerInput;
using System;
using UnityEngine;

namespace AxeMan.GameSystem.GameMode
{
    public class ExamineMode : MonoBehaviour
    {
        public event EventHandler<EventArgs> EnteredExamineMode;

        public event EventHandler<EventArgs> EnteringExamineMode;

        public event EventHandler<EventArgs> LeavingExamineMode;

        protected virtual void OnEnteredExamineMode(EventArgs e)
        {
            EnteredExamineMode?.Invoke(this, e);
        }

        protected virtual void OnEnteringExamineMode(EventArgs e)
        {
            EnteringExamineMode?.Invoke(this, e);
        }

        protected virtual void OnLeavingExamineMode(EventArgs e)
        {
            LeavingExamineMode?.Invoke(this, e);
        }

        private bool EnterMode(PlayerCommandingEventArgs e)
        {
            if (e.SubTag != SubTag.PC)
            {
                return false;
            }
            return e.Command == CommandTag.ExamineMode;
        }

        private void ExamineMode_PlayerCommanding(object sender,
            PlayerCommandingEventArgs e)
        {
            if (EnterMode(e))
            {
                OnEnteringExamineMode(EventArgs.Empty);
            }
            else if (LeaveMode(e))
            {
                OnLeavingExamineMode(EventArgs.Empty);
            }
        }

        private bool LeaveMode(PlayerCommandingEventArgs e)
        {
            if (e.SubTag != SubTag.ExamineMarker)
            {
                return false;
            }
            return e.Command == CommandTag.Cancel;
        }

        private void Start()
        {
            GetComponent<InputManager>().PlayerCommanding
                += ExamineMode_PlayerCommanding;
        }
    }
}
