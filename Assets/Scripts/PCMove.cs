using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.PlayerInput;
using System;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class BlockPCMovementEventArgs : EventArgs
    {
        public BlockPCMovementEventArgs(int[] targetPosition)
        {
            TargetPosition = targetPosition;
        }

        public int[] TargetPosition { get; }
    }

    public class PCMove : MonoBehaviour
    {
        public event EventHandler<BlockPCMovementEventArgs> BlockingPCMovement;

        protected virtual void OnBlockingPCMovement(BlockPCMovementEventArgs e)
        {
            BlockingPCMovement?.Invoke(this, e);
        }

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

        private void PCMove_PlayerCommanding(object sender,
            PlayerCommandingEventArgs e)
        {
            if (!GetComponent<LocalManager>().MatchID(e.ObjectID))
            {
                return;
            }
            if (!IsValidCommand(e.Command))
            {
                return;
            }

            int[] source = GetComponent<MetaInfo>().Position;
            int[] target = GetNewPosition(source, e.Command);

            if (!GetComponent<LocalManager>().IsPassable(target))
            {
                OnBlockingPCMovement(new BlockPCMovementEventArgs(target));
                return;
            }

            ActionTag action = ActionTag.Move;

            GetComponent<LocalManager>().SetPosition(target);
            GetComponent<LocalManager>().TakenAction(action);

            GameCore.AxeManCore.GetComponent<TileOverlay>().TryHideTile(source);
            GameCore.AxeManCore.GetComponent<TileOverlay>().TryHideTile(target);

            GetComponent<LocalManager>().CheckingSchedule(action);
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<InputManager>().PlayerCommanding
                += PCMove_PlayerCommanding;
        }
    }
}
