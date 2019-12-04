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

        private void Canvas_Help_LeavingAimMode(object sender, EventArgs e)
        {
            ClearText();
            PrintNormalModeText();
        }

        private void Canvas_Help_SwitchingGameMode(object sender,
            SwitchGameModeEventArgs e)
        {
            // Log mode.
            if (e.EnterMode == GameModeTag.LogMode)
            {
                SwitchVisibility(false);
            }
            else if (e.LeaveMode == GameModeTag.LogMode)
            {
                SwitchVisibility(true);
            }
            // Examine mode.
            else if (e.EnterMode == GameModeTag.ExamineMode)
            {
                ClearText();
                PrintExamineModeText();
            }
            else if (e.LeaveMode == GameModeTag.ExamineMode)
            {
                ClearText();
                PrintNormalModeText();
            }
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

            SearchText(UITag.Line1).text = GetStringData(
               UITextDataTag.MoveCursor);

            SearchText(UITag.Line2).text = GetStringData(
                UITextDataTag.UseSkill);

            SearchText(UITag.Line3).text = GetStringData(
                UITextDataTag.SwitchSkill);

            SearchText(UITag.Line4).text = GetStringData(
                UITextDataTag.ExitMode);
        }

        private void PrintExamineModeText()
        {
            SearchText(UITag.Modeline).text = GetStringData(
                UITextDataTag.ExamineMode);

            SearchText(UITag.Line1).text = GetStringData(
             UITextDataTag.MoveCursor);

            SearchText(UITag.Line2).text = GetStringData(
                UITextDataTag.ExitMode);
        }

        private void PrintNormalModeText()
        {
            SearchText(UITag.Modeline).text = GetStringData(
                UITextDataTag.NormalMode);

            SearchText(UITag.Line1).text = GetStringData(
                UITextDataTag.MovePC);

            SearchText(UITag.Line2).text = GetStringData(
                UITextDataTag.EnterExamine);

            SearchText(UITag.Line3).text = GetStringData(
                UITextDataTag.EnterAim);

            SearchText(UITag.Line4).text = GetStringData(
                UITextDataTag.ViewLog);

            SearchText(UITag.Line5).text = GetStringData(
                UITextDataTag.Save);
        }

        private Text SearchText(UITag uiTag)
        {
            return GetComponent<SearchUI>().SearchText(uiObjects, uiTag);
        }

        private void Start()
        {
            GetComponent<InitializeMainGame>().CreatedWorld
                += Canvas_Help_CreatedWorld;
            GetComponent<GameModeManager>().SwitchingGameMode
                += Canvas_Help_SwitchingGameMode;

            GetComponent<AimMode>().EnteringAimMode
                += Canvas_Help_EnteringAimMode;
            GetComponent<AimMode>().LeavingAimMode
                += Canvas_Help_LeavingAimMode;
        }

        private void SwitchVisibility(bool switchOn)
        {
            GetComponent<UIManager>().SwitchCanvasVisibility(canvasTag, switchOn);
        }
    }
}
