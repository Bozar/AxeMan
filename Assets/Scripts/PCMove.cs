using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.PlayerInput;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class PCMove : MonoBehaviour
    {
        private Dictionary<GameModeTag, SubTag> modeSubDict;

        private void Awake()
        {
            modeSubDict = new Dictionary<GameModeTag, SubTag>()
            {
                { GameModeTag.NormalMode, SubTag.PC },
                { GameModeTag.ExamineMode, SubTag.ExamineMarker },
                { GameModeTag.AimMode, SubTag.AimMarker },
            };
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

        private bool IsValidGameMode(GameModeTag gameMode)
        {
            if (modeSubDict.TryGetValue(gameMode, out SubTag subTag))
            {
                return subTag == GetComponent<MetaInfo>().SubTag;
            }
            return false;
        }

        private void PCMove_PlayerInputting(object sender,
            PlayerInputEventArgs e)
        {
            if (!IsValidGameMode(e.GameMode)
                || !IsValidCommand(e.Command))
            {
                return;
            }

            int[] source = GetComponent<MetaInfo>().Position;
            int[] target = GetNewPosition(source, e.Command);

            if (!GetComponent<LocalManager>().IsPassable(target))
            {
                GameCore.AxeManCore.GetComponent<PublishPosition>()
                    .BlockPCMovement(new BlockPCMovementEventArgs(target));
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
            GameCore.AxeManCore.GetComponent<InputManager>().PlayerInputting
                += PCMove_PlayerInputting;
        }
    }
}
