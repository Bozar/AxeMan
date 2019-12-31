using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SearchGameObject;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AxeMan.GameSystem.UserInterface
{
    public class Canvas_BuildSkill_Footer : MonoBehaviour
    {
        private CanvasTag canvasTag;
        private GameObject[] uiObjects;

        private void Awake()
        {
            canvasTag = CanvasTag.Canvas_BuildSkill_Footer;
        }

        private void Canvas_BuildSkill_Footer_LoadingGameData(object sender,
            EventArgs e)
        {
            uiObjects = GetComponent<SearchUI>().Search(canvasTag);
        }

        private void Canvas_BuildSkill_Footer_SwitchingGameMode(object sender,
            SwitchGameModeEventArgs e)
        {
            if (e.EnterMode == GameModeTag.BuildSkillMode)
            {
                PrintText();
            }
        }

        private void PrintText()
        {
            SearchText(UITag.Text1).GetComponent<Text>().text
                = "QWER / V - Switch skill. / View all skills.";
            SearchText(UITag.Text2).GetComponent<Text>().text
                = "Arrow keys - Select/Add/Remove a component.";
            SearchText(UITag.Text3).GetComponent<Text>().text
               = "Ctrl+D - Load default template.";
            SearchText(UITag.Text4).GetComponent<Text>().text
               = "Esc - Save and return to Start Menu.";
        }

        private Text SearchText(UITag uiTag)
        {
            return GetComponent<SearchUI>().SearchText(uiObjects, uiTag);
        }

        private void Start()
        {
            GetComponent<InitializeStartScreen>().LoadingGameData
                += Canvas_BuildSkill_Footer_LoadingGameData;
            GetComponent<GameModeManager>().SwitchingGameMode
                += Canvas_BuildSkill_Footer_SwitchingGameMode;
        }
    }
}
