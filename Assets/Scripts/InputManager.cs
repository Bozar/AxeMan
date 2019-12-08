using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem.PlayerInput
{
    public interface IConvertInput
    {
        CommandTag ConvertInput();
    }

    public interface IInputManager
    {
        IConvertInput[] GetInputComponent();
    }

    public class InputManager : MonoBehaviour
    {
        private Dictionary<GameModeTag, IInputManager> modeInputDict;

        public event EventHandler<PlayerCommandingEventArgs> PlayerCommanding;

        public event EventHandler<PlayerInputEventArgs> PlayerInputting;

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

        protected virtual void OnPlayerInputting(PlayerInputEventArgs e)
        {
            PlayerInputting?.Invoke(this, e);
        }

        private void Start()
        {
            modeInputDict = new Dictionary<GameModeTag, IInputManager>()
            {
                // TODO: Change NormalMode value later.
                { GameModeTag.NormalMode, GetComponent<LogMarkerInputManager>() },

                { GameModeTag.LogMode, GetComponent<LogMarkerInputManager>() },
            };
        }

        private void Update()
        {
            if (!modeInputDict.TryGetValue(
                GetComponent<GameModeManager>().CurrentGameMode,
                out IInputManager manager))
            {
                return;
            }

            CommandTag command = ConvertInput(manager.GetInputComponent());
            if (command != CommandTag.INVALID)
            {
                OnPlayerInputting(new PlayerInputEventArgs(
                    GetComponent<GameModeManager>().CurrentGameMode, command));
            }
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

    public class PlayerInputEventArgs : EventArgs
    {
        public PlayerInputEventArgs(GameModeTag gameMode, CommandTag command)
        {
            GameMode = gameMode;
            Command = command;
        }

        public CommandTag Command { get; }

        public GameModeTag GameMode { get; }
    }
}
