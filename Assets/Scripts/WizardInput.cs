using AxeMan.GameSystem;
using UnityEngine;

namespace AxeMan.Actor.PlayerInput
{
    public class WizardInput : MonoBehaviour, IInputManager
    {
        public CommandTag ConvertInput()
        {
            bool reload = Input.GetKeyDown(KeyCode.Space);
            bool schedule = Input.GetKeyDown(KeyCode.P);

            if (reload)
            {
                return CommandTag.Reload;
            }
            else if (schedule)
            {
                return CommandTag.PrintSchedule;
            }
            return CommandTag.INVALID;
        }
    }
}
