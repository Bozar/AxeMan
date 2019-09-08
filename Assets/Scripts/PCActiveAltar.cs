using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.SearchGameObject;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class PCActiveAltar : MonoBehaviour
    {
        private void PCActiveAltar_BlockingPCMovement(object sender,
            BlockPCMovementEventArgs e)
        {
            int x = e.TargetPosition[0];
            int y = e.TargetPosition[1];
            if (!GameCore.AxeManCore.GetComponent<SearchObject>().Search(
                x, y, MainTag.Building, out GameObject[] altars))
            {
                return;
            }

            int current = GameCore.AxeManCore.GetComponent<AltarCooldown>()
                .CurrentCooldown;
            int min = GameCore.AxeManCore.GetComponent<AltarCooldown>()
                .MinCooldown;
            if (current > min)
            {
                return;
            }

            Debug.Log(altars[0].GetComponent<MetaInfo>().SubTag);

            ActionTag action = ActionTag.ActiveAltar;
            GetComponent<LocalManager>().TakenAction(action);
            GetComponent<LocalManager>().CheckingSchedule(action);
        }

        private void Start()
        {
            GetComponent<PCMove>().BlockingPCMovement
                += PCActiveAltar_BlockingPCMovement;
        }
    }
}
