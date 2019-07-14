using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.PlayerInput;
using System;
using UnityEngine;

namespace AxeMan.GameSystem.GameMode
{
    public class AimMode : MonoBehaviour
    {
        public event EventHandler<EnteringAimModeEventArgs> EnteringAimMode;

        public event EventHandler<EventArgs> LeavingAimMode;

        protected virtual void OnEnteringAimMode(EnteringAimModeEventArgs e)
        {
            EnteringAimMode?.Invoke(this, e);
        }

        protected virtual void OnLeavingAimMode(EventArgs e)
        {
            LeavingAimMode?.Invoke(this, e);
        }

        private void AimMode_PlayerCommanding(object sender,
            PlayerCommandingEventArgs e)
        {
            if (EnterMode(e))
            {
                OnEnteringAimMode(new EnteringAimModeEventArgs(e.SubTag));
            }
            else if (LeaveMode(e))
            {
                OnLeavingAimMode(EventArgs.Empty);
            }
        }

        private bool EnterMode(PlayerCommandingEventArgs e)
        {
            if ((e.SubTag != SubTag.PC) && (e.SubTag != SubTag.AimMarker))
            {
                return false;
            }

            switch (e.Command)
            {
                case CommandTag.SkillQ:
                case CommandTag.SkillW:
                case CommandTag.SkillE:
                case CommandTag.SkillR:
                    return true;

                default:
                    return false;
            }
        }

        private bool LeaveMode(PlayerCommandingEventArgs e)
        {
            if (e.SubTag != SubTag.AimMarker)
            {
                return false;
            }
            return e.Command == CommandTag.Cancel;
        }

        private void Start()
        {
            GetComponent<InputManager>().PlayerCommanding
                += AimMode_PlayerCommanding;
        }
    }

    public class EnteringAimModeEventArgs : EventArgs
    {
        public EnteringAimModeEventArgs(SubTag subTag)
        {
            SubTag = subTag;
        }

        public SubTag SubTag { get; }
    }
}
