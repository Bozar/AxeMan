using AxeMan.DungeonObject;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.PlayerInput;
using System;
using UnityEngine;

namespace AxeMan.GameSystem.GameMode
{
    public class AimMode : MonoBehaviour
    {
        public event EventHandler<EventArgs> EnteringAimMode;

        public event EventHandler<EventArgs> LeavingAimMode;

        protected virtual void OnEnteringAimMode(EventArgs e)
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
                OnEnteringAimMode(EventArgs.Empty);
            }
            else if (LeaveMode(e))
            {
                OnLeavingAimMode(EventArgs.Empty);
            }
        }

        private bool EnterMode(PlayerCommandingEventArgs e)
        {
            SubTag sTag = e.Actor.GetComponent<MetaInfo>().STag;
            if ((sTag != SubTag.PC) && (sTag != SubTag.AimMarker))
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
            SubTag sTag = e.Actor.GetComponent<MetaInfo>().STag;
            if (sTag != SubTag.AimMarker)
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
}
