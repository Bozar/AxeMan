using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class SubscribePathFinding : MonoBehaviour
    {
        private void Start()
        {
            GameCore.AxeManCore.GetComponent<PathFinding>().SearchingObstacle
                += SubscribePathFinding_SearchingObstacle;
        }

        private void SubscribePathFinding_SearchingObstacle(object sender,
            SearchObstacleEventArgs e)
        {
            int[] position = GetComponent<MetaInfo>().Position;
            SubTag subTag = GetComponent<MetaInfo>().SubTag;

            switch (GetComponent<MetaInfo>().MainTag)
            {
                case MainTag.Altar:
                    e.Impassable.Push(position);
                    break;

                case MainTag.Trap:
                    e.Trap.Push(position);
                    break;

                case MainTag.Actor:
                    if (subTag == SubTag.PC)
                    {
                        e.PC = position;
                    }
                    else if (subTag != SubTag.INVALID)
                    {
                        e.Impassable.Push(position);
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
