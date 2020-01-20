using UnityEngine;

namespace AxeMan.GameSystem.PlayerInput
{
    public class BuildSkillInputManager : MonoBehaviour, IInputManager
    {
        private IConvertInput[] inputComponents;

        public IConvertInput[] GetInputComponent()
        {
            return inputComponents;
        }

        private void Start()
        {
            inputComponents = new IConvertInput[]
            {
                GetComponent<ConfirmCancelInput>(),
                GetComponent<MovementInput>(),
                GetComponent<SkillInput>(),
            };
        }
    }
}
