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

        public void PublishCommand(CommandTag command, int id, SubTag tag)
        {
            if (command == CommandTag.INVALID)
            {
                return;
            }
            OnPlayerCommanding(new PlayerCommandingEventArgs(command, id, tag));
        }

        protected virtual void OnPlayerCommanding(PlayerCommandingEventArgs e)
        {
            PlayerCommanding?.Invoke(this, e);
        }
    }

    public class PlayerCommandingEventArgs : EventArgs
    {
        public PlayerCommandingEventArgs(CommandTag command,
            int objectID, SubTag subTag)
        {
            Command = command;
            ObjectID = objectID;
            SubTag = subTag;
        }

        public CommandTag Command { get; }

        public int ObjectID { get; }

        public SubTag SubTag { get; }
    }
}
