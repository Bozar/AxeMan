using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.PlayerInput;
using System;
using UnityEngine;

namespace AxeMan.GameSystem.GameMode
{
    public class LogMode : MonoBehaviour
    {
        public event EventHandler<EventArgs> EnteringLogMode;

        public event EventHandler<EventArgs> LeavingLogMode;

        protected virtual void OnEnteringLogMode(EventArgs e)
        {
            EnteringLogMode?.Invoke(this, e);
        }

        protected virtual void OnLeavingLogMode(EventArgs e)
        {
            LeavingLogMode?.Invoke(this, e);
        }

        private bool EnterLogMode(PlayerCommandingEventArgs e)
        {
            return (e.SubTag == SubTag.PC) && (e.Command == CommandTag.LogMode);
        }

        private bool LeaveLogMode(PlayerCommandingEventArgs e)
        {
            return (e.SubTag == SubTag.LogMarker)
                && (e.Command == CommandTag.Cancel);
        }

        private void LogMode_PlayerCommanding(object sender,
            PlayerCommandingEventArgs e)
        {
            if (EnterLogMode(e))
            {
                OnEnteringLogMode(EventArgs.Empty);
            }
            else if (LeaveLogMode(e))
            {
                OnLeavingLogMode(EventArgs.Empty);
            }
        }

        private void Start()
        {
            GetComponent<InputManager>().PlayerCommanding
                += LogMode_PlayerCommanding;
        }
    }
}
