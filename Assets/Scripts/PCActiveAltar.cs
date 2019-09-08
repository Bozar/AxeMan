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

            if (GameCore.AxeManCore.GetComponent<SearchObject>().Search(
                x, y, MainTag.Building, out GameObject[] altars))
            {
                Debug.Log(altars[0].GetComponent<MetaInfo>().SubTag);
            }
            else
            {
                Debug.Log("Blocked");
            }
        }

        private void Start()
        {
            GetComponent<PCMove>().BlockingPCMovement
                += PCActiveAltar_BlockingPCMovement;
        }
    }
}
