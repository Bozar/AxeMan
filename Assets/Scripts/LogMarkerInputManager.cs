using UnityEngine;

namespace AxeMan.GameSystem.PlayerInput
{
    public class LogMarkerInputManager : MonoBehaviour, IInputManager
    {
        public IConvertInput[] InputComponent { get; private set; }

        private void Start()
        {
            InputComponent = new IConvertInput[]
            {
                GetComponent<ConfirmCancelInput>(),

                // TODO: Remove LogInput later.
                GetComponent<LogInput>(),
            };
        }
    }
}
