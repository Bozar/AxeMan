using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.PlayerInput;
using AxeMan.GameSystem.SchedulingSystem;
using UnityEngine;

namespace AxeMan.DungeonObject.PlayerInput
{
    public class PCInputManager : MonoBehaviour, IInputManager
    {
        private IInputManager[] input;

        public bool ListenInput { get; private set; }

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
            ListenInput = false;
        }

        private void PCInputManager_StartingTurn(object sender,
            StartingTurnEventArgs e)
        {
            ListenInput = true;
        }

        private void Start()
        {
            input = new IInputManager[]
            {
                GetComponent<MovementInput>(),
                GetComponent<SkillInput>(),
                GetComponent<WizardInput>(),
            };

            GameCore.AxeManCore.GetComponent<TurnManager>().StartingTurn
                += PCInputManager_StartingTurn;
            GameCore.AxeManCore.GetComponent<TurnManager>().EndingTurn
                += PCInputManager_EndingTurn;
        }

        private void Update()
        {
            if (!ListenInput)
            {
                return;
            }
            GameCore.AxeManCore.GetComponent<InputManager>().PublishCommand(
                gameObject, ConvertInput());
        }
    }
}
