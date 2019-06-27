using AxeMan.GameSystem;
using AxeMan.GameSystem.SchedulingSystem;
using UnityEngine;

namespace AxeMan.DungeonObject.PlayerInput
{
    public class PCInputManager : MonoBehaviour, IInputManager
    {
        private IInputManager[] input;
        private bool listenInput;

        public CommandTag ConvertInput()
        {
            CommandTag command;

            foreach (IInputManager i in input)
            {
                command = i.ConvertInput();
                if (command != CommandTag.INVALID)
                {
                    return command;
                }
            }
            return CommandTag.INVALID;
        }

        private void PCInputManager_EndingTurn(object sender,
            EndingTurnEventArgs e)
        {
            listenInput = false;
        }

        private void PCInputManager_StartingTurn(object sender,
            StartingTurnEventArgs e)
        {
            listenInput = true;
        }

        private void Start()
        {
            input = new IInputManager[]
            {
                GetComponent<WizardInput>(),
                GetComponent<MovementInput>(),
            };

            GameCore.AxeManCore.GetComponent<TurnManager>().StartingTurn
                += PCInputManager_StartingTurn;
            GameCore.AxeManCore.GetComponent<TurnManager>().EndingTurn
                += PCInputManager_EndingTurn;
        }

        private void Update()
        {
            if (!listenInput)
            {
                return;
            }
            GameCore.AxeManCore.GetComponent<InputManager>().PublishCommand(
                gameObject, ConvertInput());
        }
    }
}
