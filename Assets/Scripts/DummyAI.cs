using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
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
        }

        private void DummyAI_StartingTurn(object sender,
            StartingTurnEventArgs e)
        {
            if (gameObject != e.Data)
            {
                return;
            }

            GetComponent<LocalManager>().TakingAction(
                new TakingActionEventArgs(gameObject, ActionTag.Skip));
            GetComponent<LocalManager>().TakenAction(
                new TakenActionEventArgs(gameObject, ActionTag.Skip));
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
