using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.PlayerInput;
using UnityEngine;

namespace AxeMan.DungeonObject.PlayerInput
{
    public class AimMarkerInputManager : MonoBehaviour, IConvertInput,
        IInputManager
    {
        public IConvertInput[] InputComponent { get; private set; }

        public bool ListenInput { get; private set; }

        public CommandTag ConvertInput()
        {
            CommandTag command;

            foreach (IConvertInput i in InputComponent)
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
            InputComponent = new IConvertInput[]
            {
                GetComponent<MovementInput>(),
                GetComponent<SkillInput>(),
            };
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
