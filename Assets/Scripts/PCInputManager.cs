using AxeMan.GameSystem;
using UnityEngine;

namespace AxeMan.DungeonObject.PlayerInput
{
    public class PCInputManager : MonoBehaviour, IInputManager
    {
        private IInputManager[] input;

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

        private void Start()
        {
            input = new IInputManager[]
            {
                GetComponent<WizardInput>(),
                GetComponent<MovementInput>(),
            };
        }

        private void Update()
        {
            GameCore.AxeManCore.GetComponent<InputManager>().PublishCommand(
                gameObject, ConvertInput());
        }
    }
}
