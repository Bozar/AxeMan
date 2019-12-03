using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using UnityEngine;

namespace AxeMan.GameSystem.UserInterface
{
    public class Canvas_PCStatus : MonoBehaviour
    {
        private CanvasTag canvasTag;

        private void Awake()
        {
            canvasTag = CanvasTag.Canvas_PCStatus;
        }

        private void Canvas_PCStatus_SwitchingGameMode(object sender,
            SwitchGameModeEventArgs e)
        {
            if (e.EnterMode == GameModeTag.LogMode)
            {
                SwitchVisibility(false);
            }
            else if (e.EnterMode == GameModeTag.NormalMode)
            {
                SwitchVisibility(true);
            }
        }

        private void Start()
        {
            GetComponent<GameModeManager>().SwitchingGameMode
                += Canvas_PCStatus_SwitchingGameMode;
        }

        private void SwitchVisibility(bool switchOn)
        {
            GetComponent<UIManager>().SwitchCanvasVisibility(canvasTag, switchOn);
        }
    }
}
