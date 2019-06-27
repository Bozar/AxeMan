using AxeMan.GameSystem;
using UnityEngine;

namespace AxeMan.DungeonObject.PlayerInput
{
    public class MovementInput : MonoBehaviour, IInputManager
    {
        public CommandTag ConvertInput()
        {
            bool left = Input.GetKeyDown(KeyCode.H)
                || Input.GetKeyDown(KeyCode.LeftArrow);
            bool right = Input.GetKeyDown(KeyCode.L)
               || Input.GetKeyDown(KeyCode.RightArrow);
            bool up = Input.GetKeyDown(KeyCode.K)
               || Input.GetKeyDown(KeyCode.UpArrow);
            bool down = Input.GetKeyDown(KeyCode.J)
               || Input.GetKeyDown(KeyCode.DownArrow);

            if (left)
            {
                return CommandTag.Left;
            }
            else if (right)
            {
                return CommandTag.Right;
            }
            else if (up)
            {
                return CommandTag.Up;
            }
            else if (down)
            {
                return CommandTag.Down;
            }
            return CommandTag.INVALID;
        }
    }
}
