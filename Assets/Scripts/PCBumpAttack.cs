using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.SearchGameObject;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class PCBumpAttack : MonoBehaviour
    {
        private void PCBumpAttack_BlockingPCMovement(object sender,
            BlockPCMovementEventArgs e)
        {
            int x = e.TargetPosition[0];
            int y = e.TargetPosition[1];
            if (!GameCore.AxeManCore.GetComponent<SearchObject>().Search(
                x, y, MainTag.Actor, out GameObject[] actors))
            {
                return;
            }
            GameObject actor = actors[0];

            Debug.Log(actor.GetComponent<MetaInfo>().SubTag);
            Debug.Log(actor.GetComponent<MetaInfo>().Position[0]);
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<PublishPosition>()
                .BlockingPCMovement += PCBumpAttack_BlockingPCMovement;
        }
    }
}
