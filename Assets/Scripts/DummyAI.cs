using AxeMan.GameSystem;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.SchedulingSystem;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class DummyAI : MonoBehaviour
    {
        private void DummyAI_EndingTurn(object sender,
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

        private void DummyAI_StartingTurn(object sender,
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

            GetComponent<LocalManager>().TakingAction(
                new TakingActionEventArgs(gameObject, ActionTag.Skip));
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<TurnManager>().StartingTurn
                += DummyAI_StartingTurn;
            GameCore.AxeManCore.GetComponent<TurnManager>().EndingTurn
                += DummyAI_EndingTurn;
        }
    }
}
