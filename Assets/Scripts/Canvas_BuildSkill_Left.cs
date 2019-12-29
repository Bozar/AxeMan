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

            SearchText(UITag.Text1).GetComponent<Text>().text = "Type";
            SearchText(UITag.Text2).GetComponent<Text>().text = "Attack";

            SearchText(UITag.Text3).GetComponent<Text>().text = "Range";
            SearchText(UITag.Text4).GetComponent<Text>().text = "4";

            SearchText(UITag.Text5).GetComponent<Text>().text = "CD";
            SearchText(UITag.Text6).GetComponent<Text>().text = "5";

            SearchText(UITag.Text7).GetComponent<Text>().text = "Dmg";
            SearchText(UITag.Text8).GetComponent<Text>().text = "1";

            SearchText(UITag.Text9).GetComponent<Text>().text = "Fire?";
            SearchText(UITag.Text10).GetComponent<Text>().text = "4 x 4";

            SearchText(UITag.Text11).GetComponent<Text>().text = "Water?";
            SearchText(UITag.Text12).GetComponent<Text>().text = "8 x 8";

            SearchText(UITag.Text13).GetComponent<Text>().text = "Air-";
            SearchText(UITag.Text14).GetComponent<Text>().text = "2 x 2";

            SearchText(UITag.Text15).GetComponent<Text>().text = "Earth-";
            SearchText(UITag.Text16).GetComponent<Text>().text = "3 x 3";

            SearchText(UITag.Text17).GetComponent<Text>().text = "Fire-";
            SearchText(UITag.Text18).GetComponent<Text>().text = "1 x 1";
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
