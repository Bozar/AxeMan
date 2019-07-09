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

        public CommandTag ConvertInput()
        {
            return GameCore.AxeManCore.GetComponent<InputManager>()
                .ConvertInput(InputComponent);
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
            return;
            //GameCore.AxeManCore.GetComponent<InputManager>().PublishCommand(
            //    gameObject, ConvertInput());
        }
    }
}
