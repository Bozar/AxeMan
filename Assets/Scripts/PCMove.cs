using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class PCMove : MonoBehaviour
    {
        private ConvertCoordinate coord;
        private TileOverlay overlay;

        private int[] GetNewPosition(int[] source, CommandTag command)
        {
            int[] target;

            switch (command)
            {
                case CommandTag.Left:
                    target = new int[] { source[0] - 1, source[1] };
                    break;

                case CommandTag.Right:
                    target = new int[] { source[0] + 1, source[1] };
                    break;

                case CommandTag.Up:
                    target = new int[] { source[0], source[1] - 1 };
                    break;

                case CommandTag.Down:
                    target = new int[] { source[0], source[1] + 1 };
                    break;

                default:
                    target = source;
                    break;
            }
            return target;
        }

        private bool IsValidCommand(CommandTag command)
        {
            return (command == CommandTag.Left)
                || (command == CommandTag.Right)
                || (command == CommandTag.Up)
                || (command == CommandTag.Down);
        }

        private bool IsValidPosition(int[] position)
        {
            return GameCore.AxeManCore.GetComponent<SearchObject>().Search(
                position[0], position[1], SubTag.Floor, out _);
        }

        private void PCMove_PlayerCommanding(object sender,
                    PlayerCommandingEventArgs e)
        {
            if (!IsValidCommand(e.Command))
            {
                return;
            }

            int[] source = coord.Convert(transform.position);
            int[] target = GetNewPosition(source, e.Command);

            if (IsValidPosition(target))
            {
                transform.position = coord.Convert(target);
            }
            overlay.RefreshDungeonBoard();
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<InputManager>().PlayerCommanding
                += PCMove_PlayerCommanding;

            coord = GameCore.AxeManCore.GetComponent<ConvertCoordinate>();
            overlay = GameCore.AxeManCore.GetComponent<TileOverlay>();
        }
    }
}
