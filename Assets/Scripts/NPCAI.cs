using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.SchedulingSystem;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class NPCAI : MonoBehaviour
    {
        private void NPCAI_EndingTurn(object sender,
            StartOrEndTurnEventArgs e)
        {
            if (!GetComponent<LocalManager>().MatchID(e.ObjectID))
            {
                return;
            }
        }

        private void NPCAI_StartingTurn(object sender,
            StartOrEndTurnEventArgs e)
        {
            if (!GetComponent<LocalManager>().MatchID(e.ObjectID))
            {
                return;
            }

            if (GetComponent<NPCBonusAction>().HasBonusAction)
            {
                GetComponent<NPCBonusAction>().TakeBonusAction();
            }

            ActionTag action;
            if (GetComponent<NPCAttack>().IsInsideRage(out int outOfRange))
            {
                GetComponent<NPCAttack>().DealDamage();
                GetComponent<NPCAttack>().Curse();

                action = ActionTag.NPCAttack;
            }
            else
            {
                GetComponent<NPCMove>().Approach(outOfRange);

                action = ActionTag.Move;
            }

            GetComponent<LocalManager>().TakingAction(action);
            GetComponent<LocalManager>().TakenAction(action);
            GetComponent<LocalManager>().CheckingSchedule(action);
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<TurnManager>().StartingTurn
                += NPCAI_StartingTurn;
            GameCore.AxeManCore.GetComponent<TurnManager>().EndingTurn
                += NPCAI_EndingTurn;
        }
    }
}
