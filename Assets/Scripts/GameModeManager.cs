using AxeMan.GameSystem.GameDataTag;
using System;
using UnityEngine;

namespace AxeMan.GameSystem.GameMode
{
    public interface IGameModeManager
    {
        void SwitchGameMode(SwitchGameModeEventArgs e);
    }

    public class GameModeManager : MonoBehaviour, IGameModeManager
    {
        public event EventHandler<SwitchGameModeEventArgs> SwitchedGameMode;

        public event EventHandler<SwitchGameModeEventArgs> SwitchingGameMode;

        public void SwitchGameMode(SwitchGameModeEventArgs e)
        {
            OnSwitchingGameMode(e);
            OnSwitchedGameMode(e);
        }

        protected virtual void OnSwitchedGameMode(SwitchGameModeEventArgs e)
        {
            SwitchedGameMode?.Invoke(this, e);
        }

        protected virtual void OnSwitchingGameMode(SwitchGameModeEventArgs e)
        {
            SwitchingGameMode?.Invoke(this, e);
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

        public GameModeTag EnterMode { get; }

        public GameModeTag LeaveMode { get; }
    }
}
