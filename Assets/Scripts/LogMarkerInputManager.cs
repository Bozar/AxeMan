using UnityEngine;

namespace AxeMan.GameSystem.PlayerInput
{
    public class LogMarkerInputManager : MonoBehaviour, IInputManager
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

                // TODO: Remove these lines later.
                GetComponent<LogInput>(),
                GetComponent<SkillInput>(),
            };
        }
    }
}
