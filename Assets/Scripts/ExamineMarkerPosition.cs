using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class ExamineMarkerPosition : MonoBehaviour
    {
        private void ExamineMarkerPosition_SwitchingGameMode(object sender,
            SwitchGameModeEventArgs e)
        {
            if (e.EnterMode == GameModeTag.ExamineMode)
            {
                GameCore.AxeManCore.GetComponent<MarkerPosition>().
                    MoveMarkerToPC(gameObject);
            }
            else if (e.LeaveMode == GameModeTag.ExamineMode)
            {
                GameCore.AxeManCore.GetComponent<MarkerPosition>().
                    ResetMarkerPosition(gameObject);
            }
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<GameModeManager>().SwitchingGameMode
                += ExamineMarkerPosition_SwitchingGameMode;
        }
    }
}
