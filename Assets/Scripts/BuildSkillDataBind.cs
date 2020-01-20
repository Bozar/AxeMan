using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public class BuildSkillDataBind : MonoBehaviour
    {
        public void TryMoveUIFocus(CommandTag command)
        {
            Debug.Log(command);
        }

        private void Start()
        {
        }
    }
}
