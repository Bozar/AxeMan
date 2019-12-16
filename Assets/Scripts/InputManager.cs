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

        protected virtual void OnPlayerInputting(PlayerInputEventArgs e)
        {
            PlayerInputting?.Invoke(this, e);
        }

        private void Start()
        {
            modeInputDict = new Dictionary<GameModeTag, IInputManager>()
            {
                { GameModeTag.StartMode, GetComponent<StartScreenInputManager>() },
                { GameModeTag.NormalMode, GetComponent<PCInputManager>() },
                { GameModeTag.LogMode, GetComponent<LogMarkerInputManager>() },
                { GameModeTag.AimMode, GetComponent<AimMarkerInputManager>() },
                { GameModeTag.DeadMode, GetComponent<DeadPCInputManager>() },

                { GameModeTag.ExamineMode,
                    GetComponent<ExamineMarkerInputManager>() },
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
