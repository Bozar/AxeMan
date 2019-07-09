using AxeMan.GameSystem;
using AxeMan.GameSystem.SchedulingSystem;
using UnityEngine;

namespace AxeMan.DungeonObject.PlayerInput
{
    public class PCInputSwitcher : MonoBehaviour
    {
        private void EnableInput(bool enable)
        {
            GetComponent<PCInputManager>().enabled = enable;
        }

        private void PCInputSwitcher_EndingTurn(object sender,
                    EndingTurnEventArgs e)
        {
            if (e.Data != gameObject)
            {
                return;
            }
            EnableInput(false);
        }

        private void PCInputSwitcher_StartingTurn(object sender,
            StartingTurnEventArgs e)
        {
            if (e.Data != gameObject)
            {
                return;
            }
            EnableInput(true);
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<TurnManager>().StartingTurn
                += PCInputSwitcher_StartingTurn;
            GameCore.AxeManCore.GetComponent<TurnManager>().EndingTurn
                += PCInputSwitcher_EndingTurn;
        }
    }
}
