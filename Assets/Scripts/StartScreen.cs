using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.PlayerInput;
using UnityEngine;

namespace AxeMan.GameSystem.GameMode
{
    public class StartScreen : MonoBehaviour
    {
        private bool LeaveStartScreen(PlayerCommandingEventArgs e)
        {
            if (e.SubTag != SubTag.StartScreenCursor)
            {
                return false;
            }
            return e.Command == CommandTag.Confirm;
        }

        private void Start()
        {
            GetComponent<InputManager>().PlayerCommanding
                += StartScreen_PlayerCommanding;
        }

        private void StartScreen_PlayerCommanding(object sender,
            PlayerCommandingEventArgs e)
        {
            if (LeaveStartScreen(e))
            {
                Debug.Log("Leave");
            }
            else
            {
                Debug.Log("Unknown command");
            }
        }
    }
}
