using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SearchGameObject;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AxeMan.GameSystem.UserInterface
{
    public class Canvas_Help : MonoBehaviour
    {
        private CanvasTag canvasTag;
        private GameObject[] uiObjects;

        private void Awake()
        {
            canvasTag = CanvasTag.Canvas_Help;
        }

        private void Canvas_Help_CreatedWorld(object sender, EventArgs e)
        {
            uiObjects = GetComponent<SearchUI>().Search(canvasTag);

            SearchText(UITag.UIText).text = "Help menu";
        }

        private void Canvas_Help_EnteringLogMode(object sender, EventArgs e)
        {
            SwitchVisibility(false);
        }

        private void Canvas_Help_LeavingLogMode(object sender, EventArgs e)
        {
            SwitchVisibility(true);
        }

        private Text SearchText(UITag uiTag)
        {
            return GetComponent<SearchUI>().SearchText(uiObjects, uiTag);
        }

        private void Start()
        {
            GetComponent<InitializeMainGame>().CreatedWorld
                += Canvas_Help_CreatedWorld;

            GetComponent<LogMode>().EnteringLogMode
                += Canvas_Help_EnteringLogMode;
            GetComponent<LogMode>().LeavingLogMode
                += Canvas_Help_LeavingLogMode;
        }

        private void SwitchVisibility(bool switchOn)
        {
            GetComponent<UIManager>().SwitchCanvasVisibility(canvasTag, switchOn);
        }
    }
}
