using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using UnityEngine;

namespace AxeMan.GameSystem.UserInterface
{
    public class Canvas_Main : MonoBehaviour
    {
        private CanvasTag canvasTag;

        private void Awake()
        {
            canvasTag = CanvasTag.Canvas_Main;
        }

        private void Canvas_Main_SwitchingGameMode(object sender,
            SwitchGameModeEventArgs e)
        {
            // TODO: Maybe we will need to hide this canvas?
            if (e.EnterMode == GameModeTag.NormalMode)
            {
                SwitchVisibility(true);
            }
        }

        private void Start()
        {
            GetComponent<GameModeManager>().SwitchingGameMode
                += Canvas_Main_SwitchingGameMode;
        }

        private void SwitchVisibility(bool switchOn)
        {
            GetComponent<UIManager>().SwitchCanvasVisibility(canvasTag, switchOn);
        }
    }
}
