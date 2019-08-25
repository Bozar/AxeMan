using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.SearchGameObject;
using AxeMan.GameSystem.UserInterface;
using UnityEngine;
using UnityEngine.UI;

namespace AxeMan.GameSystem.InitializeGameWorld
{
    public class InitializeStartScreen : MonoBehaviour
    {
        private bool hideCanvas;
        private bool skipStart;

        private void Awake()
        {
            //skipStart = true;
            skipStart = false;
        }

        private void Update()
        {
            if (skipStart)
            {
                GetComponent<UIManager>().SwitchCanvasVisibility(
                   CanvasTag.Canvas_Main, true);
                GetComponent<UIManager>().SwitchCanvasVisibility(
                   CanvasTag.Canvas_Start, false);

                GetComponent<InitializeMainGame>().enabled = true;
                enabled = false;

                return;
            }

            if (!hideCanvas)
            {
                GetComponent<UIManager>().SwitchCanvasVisibility(
                    CanvasTag.Canvas_Main, false);

                GameObject ui = GetComponent<SearchUI>().Search(
                    CanvasTag.Canvas_Start, UITag.UIText);
                ui.GetComponent<Text>().text = "Start screen";

                hideCanvas = true;
            }
        }
    }
}
