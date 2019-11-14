using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.SchedulingSystem;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class PCStartEndTurn : MonoBehaviour
    {
        private string newTurn;

        private void Awake()
        {
            newTurn = GameCore.AxeManCore.GetComponent<LogData>()
                .GetStringData(new LogMessage(
                LogCategoryTag.GameProgress, LogMessageTag.NewTurn));
        }

        private void PCStartEndTurn_EndingTurn(object sender,
            StartOrEndTurnEventArgs e)
        {
            if (!GetComponent<LocalManager>().MatchID(e.ObjectID))
            {
                return;
            }
        }

        private void PCStartEndTurn_StartingTurn(object sender,
            StartOrEndTurnEventArgs e)
        {
            if (!GetComponent<LocalManager>().MatchID(e.ObjectID))
            {
                return;
            }

            if (GameCore.AxeManCore.GetComponent<LogManager>().GetLog(0)
                != newTurn)
            {
                GameCore.AxeManCore.GetComponent<LogManager>().Add(newTurn);
            }
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
