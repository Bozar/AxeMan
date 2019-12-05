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

        public event EventHandler<FailedVerifyingEventArgs> FailedVerifying;

        public event EventHandler<VerifiedSkillEventArgs> VerifiedSkill;

        public event EventHandler<VerifyingSkillEventArgs> VerifyingSkill;

        protected virtual void OnFailedVerifying(FailedVerifyingEventArgs e)
        {
            FailedVerifying?.Invoke(this, e);
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
                GetComponent<GameModeManager>().SwitchGameMode(
                    new SwitchGameModeEventArgs(
                        GameModeTag.NormalMode, GameModeTag.AimMode,
                        e.SubTag, e.Command));
            }
            else if (LeaveMode(e))
            {
                GetComponent<GameModeManager>().SwitchGameMode(
                   new SwitchGameModeEventArgs(
                       GameModeTag.AimMode, GameModeTag.NormalMode,
                       e.SubTag, e.Command));

                if (pcUseSkill != CommandTag.INVALID)
                {
                    OnVerifiedSkill(new VerifiedSkillEventArgs(pcUseSkill));
                }
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

        private bool IsValidSkill(CommandTag skill)
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
            return true;
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
                    return IsValidSkill(pcUseSkill);

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
