using AxeMan.GameSystem.GameDataHub;
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
        private Text[] logUIs;
        private GameObject[] uiObjects;

        private void Awake()
        {
            canvasTag = CanvasTag.Canvas_Log;
        }

        private void Canvas_Log_CreatedWorld(object sender, EventArgs e)
        {
            uiObjects = GetComponent<SearchUI>().Search(canvasTag);
            SwitchVisibility(false);
            SetModelineText();

            logUIs = new Text[]
            {
                SearchText(UITag.Line1),
                SearchText(UITag.Line2),
                SearchText(UITag.Line3),
                SearchText(UITag.Line4),
                SearchText(UITag.Line5),
                SearchText(UITag.Line6),
                SearchText(UITag.Line7),
                SearchText(UITag.Line8),
                SearchText(UITag.Line9),
                SearchText(UITag.Line10),
                SearchText(UITag.Line11),
                SearchText(UITag.Line12),
                SearchText(UITag.Line13),
                SearchText(UITag.Line14),
                SearchText(UITag.Line15),
                SearchText(UITag.Line16),
                SearchText(UITag.Line17),
                SearchText(UITag.Line18),
                SearchText(UITag.Line19),
                SearchText(UITag.Line20),
            };
        }

        private void Canvas_Log_EnteringLogMode(object sender, EventArgs e)
        {
            PrintLog();
            SwitchVisibility(true);
        }

        private void Canvas_Log_LeavingLogMode(object sender, EventArgs e)
        {
            SwitchVisibility(false);
        }

        private void PrintLog()
        {
            for (int i = 0; i < logUIs.Length; i++)
            {
                logUIs[i].text = GetComponent<LogManager>().GetLog(i);
            }
        }

        private Text SearchText(UITag uiTag)
        {
            return GetComponent<SearchUI>().SearchText(uiObjects, uiTag);
        }

        private void SetModelineText()
        {
            string hint = GetComponent<UITextData>().
                GetStringData(UITextCategoryTag.Log, UITextDataTag.Hint);
            GetComponent<ColorManager>().SetColor(hint, ColorTag.Grey, out hint);
            SearchText(UITag.Modeline).text = hint;
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
