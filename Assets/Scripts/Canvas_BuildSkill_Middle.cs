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
    public class Canvas_BuildSkill_Middle : MonoBehaviour
    {
        private CanvasTag canvasTag;
        private GameObject[] uiObjects;

        private void Awake()
        {
            canvasTag = CanvasTag.Canvas_BuildSkill_Middle;
        }

        private void Canvas_BuildSkill_Middle_LoadingGameData(object sender,
            EventArgs e)
        {
            uiObjects = GetComponent<SearchUI>().Search(canvasTag);
        }

        private void Canvas_BuildSkill_Middle_SwitchingGameMode(object sender,
            SwitchGameModeEventArgs e)
        {
            if (e.EnterMode == GameModeTag.BuildSkillMode)
            {
                PrintText();
            }
        }

        private void PrintText()
        {
            SearchText(UITag.Text1).GetComponent<Text>().text = ">";
            SearchText(UITag.Text2).GetComponent<Text>().text = "Attack";

            SearchText(UITag.Text4).GetComponent<Text>().text
                = GetComponent<SkillData>().GetLongSkillComponentName(
                    SkillComponentTag.WaterCurse);
            SearchText(UITag.Text6).GetComponent<Text>().text = "Water Merit";
            SearchText(UITag.Text8).GetComponent<Text>().text = "Merit Slot 3";
            SearchText(UITag.Text10).GetComponent<Text>().text = "Merit Slot 4";

            SearchText(UITag.Text12).GetComponent<Text>().text = "Earth Flaw";
            SearchText(UITag.Text14).GetComponent<Text>().text = "Flaw Slot 2";
            SearchText(UITag.Text16).GetComponent<Text>().text = "Water Flaw";
            SearchText(UITag.Text18).GetComponent<Text>().text = "Flaw Slot 4";
        }

        private Text SearchText(UITag uiTag)
        {
            return GetComponent<SearchUI>().SearchText(uiObjects, uiTag);
        }

        private void Start()
        {
            GetComponent<InitializeStartScreen>().LoadingGameData
                += Canvas_BuildSkill_Middle_LoadingGameData;
            GetComponent<GameModeManager>().SwitchingGameMode
                += Canvas_BuildSkill_Middle_SwitchingGameMode;
        }
    }
}
