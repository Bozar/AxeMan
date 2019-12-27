using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SearchGameObject;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AxeMan.GameSystem.UserInterface
{
    public class Canvas_BuildSkill_Left : MonoBehaviour
    {
        private CanvasTag canvasTag;
        private GameObject[] uiObjects;

        private void Awake()
        {
            canvasTag = CanvasTag.Canvas_BuildSkill_Left;
        }

        private void Canvas_BuildSkill_Left_LoadingGameData(object sender,
            EventArgs e)
        {
            uiObjects = GetComponent<SearchUI>().Search(canvasTag);

            PrintText();
        }

        private void PrintText()
        {
            SearchText(UITag.Modeline).GetComponent<Text>().text = "Skill Q";

            SearchText(UITag.Status1Text).GetComponent<Text>().text = "Type";
            SearchText(UITag.Status1Data).GetComponent<Text>().text = "Attack";

            SearchText(UITag.Status2Text).GetComponent<Text>().text = "Range";
            SearchText(UITag.Status2Data).GetComponent<Text>().text = "4";

            SearchText(UITag.Status3Text).GetComponent<Text>().text = "CD";
            SearchText(UITag.Status3Data).GetComponent<Text>().text = "5";

            SearchText(UITag.Status4Text).GetComponent<Text>().text = "Dmg";
            SearchText(UITag.Status4Data).GetComponent<Text>().text = "1";

            SearchText(UITag.Status5Text).GetComponent<Text>().text = "Fire?";
            SearchText(UITag.Status5Data).GetComponent<Text>().text = "4 x 4";

            SearchText(UITag.Status6Text).GetComponent<Text>().text = "Water?";
            SearchText(UITag.Status6Data).GetComponent<Text>().text = "8 x 8";

            SearchText(UITag.Status7Text).GetComponent<Text>().text = "Air-";
            SearchText(UITag.Status7Data).GetComponent<Text>().text = "2 x 2";

            SearchText(UITag.Status8Text).GetComponent<Text>().text = "Earth-";
            SearchText(UITag.Status8Data).GetComponent<Text>().text = "3 x 3";

            SearchText(UITag.Status9Text).GetComponent<Text>().text = "Fire-";
            SearchText(UITag.Status9Data).GetComponent<Text>().text = "1 x 1";
        }

        private Text SearchText(UITag uiTag)
        {
            return GetComponent<SearchUI>().SearchText(uiObjects, uiTag);
        }

        private void Start()
        {
            GetComponent<InitializeStartScreen>().LoadingGameData
                += Canvas_BuildSkill_Left_LoadingGameData;
        }
    }
}
