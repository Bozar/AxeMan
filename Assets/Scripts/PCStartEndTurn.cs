using AxeMan.GameSystem;
using AxeMan.GameSystem.SchedulingSystem;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class PCStartEndTurn : MonoBehaviour
    {
        private void PCStartEndTurn_EndingTurn(object sender,
            EndingTurnEventArgs e)
        {
            if (gameObject != e.Data)
            {
                return;
            }

            int[] pos = GameCore.AxeManCore.GetComponent<ConvertCoordinate>()
                .Convert(transform.position);
            Debug.Log("End: " + GetComponent<MetaInfo>().STag + ", "
                + pos[0] + ", " + pos[1]);
        }

        private void PCStartEndTurn_StartingTurn(object sender,
            StartingTurnEventArgs e)
        {
            if (gameObject != e.Data)
            {
                return;
            }

            int[] pos = GameCore.AxeManCore.GetComponent<ConvertCoordinate>()
                .Convert(transform.position);
            Debug.Log("Start: " + GetComponent<MetaInfo>().STag + ", "
                + pos[0] + ", " + pos[1]);
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<TurnManager>().StartingTurn
                += PCStartEndTurn_StartingTurn;
            GameCore.AxeManCore.GetComponent<TurnManager>().EndingTurn
                += PCStartEndTurn_EndingTurn;
        }
    }
}
