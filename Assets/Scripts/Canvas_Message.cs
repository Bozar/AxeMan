using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SearchGameObject;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AxeMan.GameSystem.UserInterface
{
    public class Canvas_Message : MonoBehaviour
    {
        private CanvasTag canvasTag;
        private GameObject[] uiObjects;

        private void Awake()
        {
            canvasTag = CanvasTag.Canvas_Message;
        }

        private void Canvas_Message_CreatedWorld(object sender, EventArgs e)
        {
            uiObjects = GetComponent<SearchUI>().Search(canvasTag);

            SearchText(UITag.Modeline).text = "Message";
        }

        private void Canvas_Message_EnteredAimMode(object sender,
            EnterAimModeEventArgs e)
        {
            SwitchVisibility(false);
        }

        private void Canvas_Message_EnteredExamineMode(object sender,
            EventArgs e)
        {
            SwitchVisibility(false);
        }

        private void Canvas_Message_LeavingAimMode(object sender, EventArgs e)
        {
            SwitchVisibility(true);
        }

        private void Canvas_Message_LeavingExamineMode(object sender,
            EventArgs e)
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
                += Canvas_Message_CreatedWorld;

            GetComponent<AimMode>().EnteredAimMode
                += Canvas_Message_EnteredAimMode;
            GetComponent<AimMode>().LeavingAimMode
                += Canvas_Message_LeavingAimMode;

            GetComponent<ExamineMode>().EnteredExamineMode
                += Canvas_Message_EnteredExamineMode;
            GetComponent<ExamineMode>().LeavingExamineMode
                += Canvas_Message_LeavingExamineMode;
        }

        private void SwitchVisibility(bool switchOn)
        {
            GetComponent<UIManager>().SwitchCanvasVisibility(canvasTag, switchOn);
        }
    }
}
