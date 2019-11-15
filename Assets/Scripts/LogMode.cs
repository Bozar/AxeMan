using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.PlayerInput;
using UnityEngine;

namespace AxeMan.GameSystem.GameMode
{
    public class LogMode : MonoBehaviour
    {
        private void LogMode_PlayerCommanding(object sender,
            PlayerCommandingEventArgs e)
        {
            if ((e.SubTag == SubTag.PC)
                && (e.Command == CommandTag.LogMode))
            {
                Debug.Log("Log mode");
            }
        }

        private void Start()
        {
            GetComponent<InputManager>().PlayerCommanding
                += LogMode_PlayerCommanding;
        }
    }
}
