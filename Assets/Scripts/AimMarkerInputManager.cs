using UnityEngine;

namespace AxeMan.GameSystem.PlayerInput
{
    public class AimMarkerInputManager : MonoBehaviour, IInputManager
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
                GetComponent<MovementInput>(),
                GetComponent<SkillInput>(),
                GetComponent<ConfirmCancelInput>(),
            };
        }
    }
}
