using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SearchGameObject;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AxeMan.GameSystem.UserInterface
{
    public class Canvas_BuildSkill_Right : MonoBehaviour
    {
        private CanvasTag canvasTag;
        private GameObject[] uiObjects;

        private void Awake()
        {
            canvasTag = CanvasTag.Canvas_BuildSkill_Right;
        }

        private void Canvas_BuildSkill_Right_LoadingGameData(object sender,
            EventArgs e)
        {
            uiObjects = GetComponent<SearchUI>().Search(canvasTag);

            PrintText();
        }

        private void PrintText()
        {
            SearchText(UITag.Text1).GetComponent<Text>().text = "Fire Merit";
            SearchText(UITag.Text2).GetComponent<Text>().text = "Earth Curse";
            SearchText(UITag.Text3).GetComponent<Text>().text = "Water Flaw";
            SearchText(UITag.Text4).GetComponent<Text>().text = "Air Merit";

            SearchText(UITag.Text5).GetComponent<Text>().text = "Description";
        }

        private Text SearchText(UITag uiTag)
        {
            return GetComponent<SearchUI>().SearchText(uiObjects, uiTag);
        }

        private void Start()
        {
            GetComponent<InitializeStartScreen>().LoadingGameData
                += Canvas_BuildSkill_Right_LoadingGameData;
        }
    }
}
