using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.GameSystem.PlayerInput
{
    public class DeadPCInput : MonoBehaviour, IConvertInput
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
