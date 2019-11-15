using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.GameSystem.PlayerInput
{
    public class LogInput : MonoBehaviour, IConvertInput
    {
        public CommandTag ConvertInput()
        {
            bool log = Input.GetKeyDown(KeyCode.M);

            if (log)
            {
                return CommandTag.LogMode;
            }
            return CommandTag.INVALID;
        }
    }
}
