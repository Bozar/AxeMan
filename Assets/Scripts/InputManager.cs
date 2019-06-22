using System;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public interface IInputManager
    {
        CommandTag ConvertInput();
    }

    public class InputManager : MonoBehaviour
    {
        public event EventHandler<PlayerCommandingEventArgs> PlayerCommanding;

        public void PublishCommand(GameObject actor, CommandTag command)
        {
            if (command == CommandTag.INVALID)
            {
                return;
            }
            OnPlayerCommanding(new PlayerCommandingEventArgs(actor, command));
        }

        protected virtual void OnPlayerCommanding(PlayerCommandingEventArgs e)
        {
            PlayerCommanding?.Invoke(this, e);
        }
    }

    public class PlayerCommandingEventArgs : EventArgs
    {
        public PlayerCommandingEventArgs(GameObject actor, CommandTag command)
        {
            Actor = actor;
            Command = command;
        }

        public GameObject Actor { get; }

        public CommandTag Command { get; }
    }
}
