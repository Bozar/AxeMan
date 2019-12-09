using AxeMan.GameSystem.GameDataTag;
using System;
using UnityEngine;

namespace AxeMan.GameSystem.GameMode
{
    public interface IGameModeManager
    {
        GameModeTag CurrentGameMode { get; }

        void SwitchGameMode(SwitchGameModeEventArgs e);
    }

    public class GameModeManager : MonoBehaviour, IGameModeManager
    {
        public event EventHandler<SwitchGameModeEventArgs> SwitchedGameMode;

        public event EventHandler<SwitchGameModeEventArgs> SwitchingGameMode;

        public GameModeTag CurrentGameMode { get; private set; }

        public void SwitchGameMode(SwitchGameModeEventArgs e)
        {
            OnSwitchingGameMode(e);
            OnSwitchedGameMode(e);

            CurrentGameMode = e.EnterMode;
        }

        protected virtual void OnSwitchedGameMode(SwitchGameModeEventArgs e)
        {
            SwitchedGameMode?.Invoke(this, e);
        }

        protected virtual void OnSwitchingGameMode(SwitchGameModeEventArgs e)
        {
            SwitchingGameMode?.Invoke(this, e);
        }

        private void Awake()
        {
            CurrentGameMode = GameModeTag.NormalMode;
        }
    }

    public class SwitchGameModeEventArgs : EventArgs
    {
        public SwitchGameModeEventArgs(GameModeTag leaveMode,
            GameModeTag enterMode)
        {
            EnterMode = enterMode;
            LeaveMode = leaveMode;
        }

        public SwitchGameModeEventArgs(GameModeTag leaveMode,
            GameModeTag enterMode, CommandTag commandTag)
            : this(leaveMode, enterMode)
        {
            CommandTag = commandTag;
        }

        public CommandTag CommandTag { get; }

        public GameModeTag EnterMode { get; }

        public GameModeTag LeaveMode { get; }
    }
}
