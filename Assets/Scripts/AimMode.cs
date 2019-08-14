using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.PlayerInput;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem.GameMode
{
    public class AimMode : MonoBehaviour
    {
        private CommandTag pcUseSkill;

        public event EventHandler<EnterAimModeEventArgs> EnteredAimMode;

        public event EventHandler<EnterAimModeEventArgs> EnteringAimMode;

        public event EventHandler<FailedVerifyingEventArgs> FailedVerifying;

        public event EventHandler<EventArgs> LeavingAimMode;

        public event EventHandler<VerifiedSkillEventArgs> VerifiedSkill;

        public event EventHandler<VerifyingSkillEventArgs> VerifyingSkill;

        protected virtual void OnEnteredAimMode(EnterAimModeEventArgs e)
        {
            EnteredAimMode?.Invoke(this, e);
        }

        protected virtual void OnEnteringAimMode(EnterAimModeEventArgs e)
        {
            EnteringAimMode?.Invoke(this, e);
        }

        protected virtual void OnFailedVerifying(FailedVerifyingEventArgs e)
        {
            FailedVerifying?.Invoke(this, e);
        }

        protected virtual void OnLeavingAimMode(EventArgs e)
        {
            LeavingAimMode?.Invoke(this, e);
        }

        protected virtual void OnVerifiedSkill(VerifiedSkillEventArgs e)
        {
            VerifiedSkill?.Invoke(this, e);
        }

        protected virtual void OnVerifyingSkill(VerifyingSkillEventArgs e)
        {
            VerifyingSkill?.Invoke(this, e);
        }

        private void AimMode_PlayerCommanding(object sender,
            PlayerCommandingEventArgs e)
        {
            if (EnterMode(e))
            {
                OnEnteringAimMode(new EnterAimModeEventArgs(e.SubTag, e.Command));
                OnEnteredAimMode(new EnterAimModeEventArgs(e.SubTag, e.Command));
            }
            else if (LeaveMode(e))
            {
                OnLeavingAimMode(EventArgs.Empty);
            }
        }

        private void Awake()
        {
            pcUseSkill = CommandTag.INVALID;
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
                    pcUseSkill = e.Command;
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
            switch (e.Command)
            {
                case CommandTag.Confirm:
                    return VerifySkill(pcUseSkill);

                case CommandTag.Cancel:
                    pcUseSkill = CommandTag.INVALID;
                    return true;

                default:
                    return false;
            }
        }

        private void Start()
        {
            GetComponent<InputManager>().PlayerCommanding
                += AimMode_PlayerCommanding;
        }

        private bool VerifySkill(CommandTag skill)
        {
            Stack<bool> result = new Stack<bool>();
            OnVerifyingSkill(new VerifyingSkillEventArgs(skill, result));

            while (result.Count > 0)
            {
                if (!result.Pop())
                {
                    OnFailedVerifying(new FailedVerifyingEventArgs(skill));
                    return false;
                }
            }
            OnVerifiedSkill(new VerifiedSkillEventArgs(skill));
            return true;
        }
    }

    public class EnterAimModeEventArgs : EventArgs
    {
        public EnterAimModeEventArgs(SubTag subTag, CommandTag commandTag)
        {
            SubTag = subTag;
            CommandTag = commandTag;
        }

        public CommandTag CommandTag { get; }

        public SubTag SubTag { get; }
    }

    public class FailedVerifyingEventArgs : EventArgs
    {
        public FailedVerifyingEventArgs(CommandTag useSkill)
        {
            UseSkill = useSkill;
        }

        public CommandTag UseSkill { get; }
    }

    public class VerifiedSkillEventArgs : EventArgs
    {
        public VerifiedSkillEventArgs(CommandTag useSkill)
        {
            UseSkill = useSkill;
        }

        public CommandTag UseSkill { get; }
    }

    public class VerifyingSkillEventArgs : EventArgs
    {
        public VerifyingSkillEventArgs(CommandTag useSkill,
            Stack<bool> canUseSkill)
        {
            UseSkill = useSkill;
            CanUseSkill = canUseSkill;
        }

        public Stack<bool> CanUseSkill { get; }

        public CommandTag UseSkill { get; }
    }
}
