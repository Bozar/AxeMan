using UnityEngine;

namespace AxeMan.DungeonObject.PlayerInput
{
    public class LogMarkerInputSwitcher : MonoBehaviour
    {
        private void EnableInput(bool enable)
        {
            GetComponent<LogMarkerInputManager>().enabled = enable;
        }

        private void Start()
        {
        }
    }
}
