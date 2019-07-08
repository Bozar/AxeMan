using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.PlayerInput;
using AxeMan.GameSystem.SchedulingSystem;
using UnityEngine;

namespace AxeMan.DungeonObject.PlayerInput
{
    public class PCInputManager : MonoBehaviour, IConvertInput, IInputManager
    {
        public IConvertInput[] InputComponent { get; private set; }

        public bool ListenInput { get; private set; }

        public CommandTag ConvertInput()
        {
            return GameCore.AxeManCore.GetComponent<InputManager>()
                .ConvertInput(InputComponent);
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
            InputComponent = new IConvertInput[]
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
