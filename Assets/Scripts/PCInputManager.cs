using UnityEngine;

namespace AxeMan.GameSystem.PlayerInput
{
    public class PCInputManager : MonoBehaviour, IInputManager
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
                GetComponent<LogInput>(),
                GetComponent<SkillInput>(),
                GetComponent<ExamineInput>(),
                GetComponent<WizardInput>(),
            };
        }
    }
}
