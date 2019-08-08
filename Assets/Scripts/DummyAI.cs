using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.SchedulingSystem;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class DummyAI : MonoBehaviour
    {
        private void DummyAI_EndingTurn(object sender, EndingTurnEventArgs e)
        {
            if (!GetComponent<LocalManager>().MatchID(e.ObjectID))
            {
                return;
            }
        }

        private void DummyAI_StartingTurn(object sender, StartingTurnEventArgs e)
        {
            if (!GetComponent<LocalManager>().MatchID(e.ObjectID))
            {
                return;
            }

            ActionTag action = ActionTag.Skip;
            MainTag mainTag = GetComponent<MetaInfo>().MainTag;
            SubTag subTag = GetComponent<MetaInfo>().SubTag;
            int id = GetComponent<MetaInfo>().ObjectID;
            var ea = new PublishActionEventArgs(action, mainTag, subTag, id);

            GetComponent<LocalManager>().TakingAction(ea);
            GetComponent<LocalManager>().TakenAction(ea);
            GetComponent<LocalManager>().CheckingSchedule(ea);
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
