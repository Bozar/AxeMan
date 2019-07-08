using AxeMan.GameSystem.GameDataTag;
using System;
using UnityEngine;

namespace AxeMan.GameSystem.PlayerInput
{
    public interface IConvertInput
    {
        CommandTag ConvertInput();
    }

    public interface IInputManager
    {
        IConvertInput[] InputComponent { get; }

        bool ListenInput { get; }
    }

    public class InputManager : MonoBehaviour
    {
        public event EventHandler<PlayerCommandingEventArgs> PlayerCommanding;

        public CommandTag ConvertInput(IConvertInput[] input)
        {
            CommandTag command;

            foreach (IConvertInput i in input)
            {
                command = i.ConvertInput();
                if (command != CommandTag.INVALID)
                {
                    return command;
                }
            }
            return CommandTag.INVALID;
        }

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
