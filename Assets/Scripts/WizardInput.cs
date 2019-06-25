using AxeMan.GameSystem;
using UnityEngine;

namespace AxeMan.Actor.PlayerInput
{
    public class WizardInput : MonoBehaviour, IInputManager
    {
        public CommandTag ConvertInput()
        {
            bool reload = Input.GetKeyDown(KeyCode.Space);

            if (reload)
            {
                return CommandTag.Reload;
            }
            return CommandTag.INVALID;
        }
    }
}
