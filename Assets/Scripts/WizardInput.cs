using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.GameSystem.PlayerInput
{
    public class WizardInput : MonoBehaviour, IConvertInput
    {
        public CommandTag ConvertInput()
        {
            bool reload = Input.GetKeyDown(KeyCode.Space);
            bool schedule = Input.GetKeyDown(KeyCode.P);
            bool changeHP = Input.GetKeyDown(KeyCode.Minus);

            if (reload)
            {
                return CommandTag.Reload;
            }
            else if (schedule)
            {
                return CommandTag.PrintSchedule;
            }
            else if (changeHP)
            {
                return CommandTag.ChangeHP;
            }
            return CommandTag.INVALID;
        }
    }
}
