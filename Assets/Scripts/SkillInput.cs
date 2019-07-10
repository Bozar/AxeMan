using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.GameSystem.PlayerInput
{
    public class SkillInput : MonoBehaviour, IConvertInput
    {
        public CommandTag ConvertInput()
        {
            bool q = Input.GetKeyDown(KeyCode.Q);
            bool w = Input.GetKeyDown(KeyCode.W);
            bool e = Input.GetKeyDown(KeyCode.E);
            bool r = Input.GetKeyDown(KeyCode.R);

            if (q)
            {
                return CommandTag.SkillQ;
            }
            else if (w)
            {
                return CommandTag.SkillW;
            }
            else if (e)
            {
                return CommandTag.SkillE;
            }
            else if (r)
            {
                return CommandTag.SkillR;
            }
            return CommandTag.INVALID;
        }
    }
}
