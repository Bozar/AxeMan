using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.PlayerInput;
using UnityEngine;

namespace AxeMan.DungeonObject.PlayerInput
{
    public class AimMarkerInputManager : MonoBehaviour, IConvertInput,
        IInputManager
    {
        private IConvertInput[] inputComponents;

        public CommandTag ConvertInput()
        {
            return GameCore.AxeManCore.GetComponent<InputManager>()
                .ConvertInput(GetInputComponent());
        }

        public IConvertInput[] GetInputComponent()
        {
            return inputComponents;
        }

        private void Start()
        {
            inputComponents = new IConvertInput[]
            {
                GameCore.AxeManCore.GetComponent<MovementInput>(),
                GameCore.AxeManCore.GetComponent<SkillInput>(),
                GameCore.AxeManCore.GetComponent<ConfirmCancelInput>(),
            };
        }

        private void Update()
        {
            int id = GetComponent<MetaInfo>().ObjectID;
            SubTag tag = GetComponent<MetaInfo>().SubTag;

            GameCore.AxeManCore.GetComponent<InputManager>().PublishCommand(
                ConvertInput(), id, tag);
        }
    }
}
