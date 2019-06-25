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
            bool remove = Input.GetKeyDown(KeyCode.R);
            bool next = Input.GetKeyDown(KeyCode.N);

            if (reload)
            {
                return CommandTag.Reload;
            }
            else if (schedule)
            {
                return CommandTag.PrintSchedule;
            }
            else if (remove)
            {
                return CommandTag.RemoveFromSchedule;
            }
            else if (next)
            {
                return CommandTag.NextInSchedule;
            }
            return CommandTag.INVALID;
        }
    }
}
