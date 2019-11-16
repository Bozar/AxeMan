using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SearchGameObject;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AxeMan.GameSystem.UserInterface
{
    public class Canvas_Log : MonoBehaviour
    {
        private CanvasTag canvasTag;
        private GameObject[] uiObjects;

        private void Awake()
        {
            canvasTag = CanvasTag.Canvas_Log;
        }

        private void Canvas_Log_CreatedWorld(object sender, EventArgs e)
        {
            uiObjects = GetComponent<SearchUI>().Search(canvasTag);
            SwitchVisibility(false);

            SearchText(UITag.UIText).text = "Log menu";
        }

        private void Canvas_Log_EnteringLogMode(object sender, EventArgs e)
        {
            SwitchVisibility(true);
        }

        private void Canvas_Log_LeavingLogMode(object sender, EventArgs e)
        {
            SwitchVisibility(false);
        }

        private Text SearchText(UITag uiTag)
        {
            return GetComponent<SearchUI>().SearchText(uiObjects, uiTag);
        }

        private void Start()
        {
            GetComponent<InitializeMainGame>().CreatedWorld
                += Canvas_Log_CreatedWorld;

            GetComponent<LogMode>().EnteringLogMode
                += Canvas_Log_EnteringLogMode;
            GetComponent<LogMode>().LeavingLogMode
                += Canvas_Log_LeavingLogMode;
        }

        private void SwitchVisibility(bool switchOn)
        {
            GetComponent<UIManager>().SwitchCanvasVisibility(canvasTag, switchOn);
        }
    }
}
