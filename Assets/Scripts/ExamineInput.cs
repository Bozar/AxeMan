using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.GameSystem.PlayerInput
{
    public class ExamineInput : MonoBehaviour, IConvertInput
    {
        public CommandTag ConvertInput()
        {
            bool examine = Input.GetKeyDown(KeyCode.X);

            if (examine)
            {
                return CommandTag.ExamineMode;
            }
            return CommandTag.INVALID;
        }
    }
}
