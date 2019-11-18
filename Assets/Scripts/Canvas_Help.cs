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
            PrintNormalModeText();
        }

        private void Canvas_Help_EnteringAimMode(object sender,
            EnterAimModeEventArgs e)
        {
            ClearText();
            PrintAimModeText();
        }

        private void Canvas_Help_EnteringExamineMode(object sender, EventArgs e)
        {
            ClearText();
            PrintExamineModeText();
        }

        private void Canvas_Help_EnteringLogMode(object sender, EventArgs e)
        {
            SwitchVisibility(false);
        }

        private void Canvas_Help_LeavingAimMode(object sender, EventArgs e)
        {
            ClearText();
            PrintNormalModeText();
        }

        private void Canvas_Help_LeavingExamineMode(object sender, EventArgs e)
        {
            ClearText();
            PrintNormalModeText();
        }

        private void Canvas_Help_LeavingLogMode(object sender, EventArgs e)
        {
            SwitchVisibility(true);
        }

        private void ClearText()
        {
            foreach (GameObject go in uiObjects)
            {
                go.GetComponent<Text>().text = "";
            }
        }

        private string GetStringData(UITextDataTag uiTextDataTag)
        {
            string text = GetComponent<UITextData>().GetStringData(
                UITextCategoryTag.Help, uiTextDataTag);
            GetComponent<ColorManager>().SetColor(text, ColorTag.Grey, out text);

            return text;
        }

        private void PrintAimModeText()
        {
            SearchText(UITag.Modeline).text = GetStringData(
                UITextDataTag.AimMode);
        }

        private void PrintExamineModeText()
        {
            SearchText(UITag.Modeline).text = GetStringData(
                UITextDataTag.ExamineMode);
        }

        private void PrintNormalModeText()
        {
            SearchText(UITag.Modeline).text = GetStringData(
                UITextDataTag.NormalMode);
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

            GetComponent<AimMode>().EnteringAimMode
                += Canvas_Help_EnteringAimMode;
            GetComponent<AimMode>().LeavingAimMode
                += Canvas_Help_LeavingAimMode;

            GetComponent<ExamineMode>().EnteringExamineMode
                += Canvas_Help_EnteringExamineMode;
            GetComponent<ExamineMode>().LeavingExamineMode
                += Canvas_Help_LeavingExamineMode;
        }

        private void SwitchVisibility(bool switchOn)
        {
            GetComponent<UIManager>().SwitchCanvasVisibility(canvasTag, switchOn);
        }
    }
}
