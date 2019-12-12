using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.SchedulingSystem;
using UnityEngine;

namespace AxeMan.GameSystem.GameMode
{
    public class NormalMode : MonoBehaviour
    {
        private SwitchGameModeEventArgs endTurn;
        private SwitchGameModeEventArgs startTurn;

        private void Awake()
        {
            endTurn = new SwitchGameModeEventArgs(
                GameModeTag.NormalMode, GameModeTag.INVALID);
            startTurn = new SwitchGameModeEventArgs(
                GameModeTag.INVALID, GameModeTag.NormalMode);
        }

        private void NormalMode_EndingTurn(object sender,
            StartOrEndTurnEventArgs e)
        {
            GetComponent<GameModeManager>().SwitchGameMode(endTurn);
        }

        private void NormalMode_StartingTurn(object sender,
            StartOrEndTurnEventArgs e)
        {
            GetComponent<GameModeManager>().SwitchGameMode(startTurn);
        }

        private void Start()
        {
            //GetComponent<TurnManager>().StartingTurn += NormalMode_StartingTurn;
            //GetComponent<TurnManager>().EndingTurn += NormalMode_EndingTurn;
        }
    }
}
