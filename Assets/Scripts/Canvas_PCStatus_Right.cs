using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.SearchGameObject;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AxeMan.GameSystem.UserInterface
{
    public class Canvas_PCStatus_Right : MonoBehaviour
    {
        private CanvasTag canvasTag;
        private GameObject[] uiObjects;

        private void Awake()
        {
            canvasTag = CanvasTag.Canvas_PCStatus_Right;
        }

        private void Canvas_PCStatus_Right_CreatedWorld(object sender, EventArgs e)
        {
            uiObjects = GetComponent<SearchUI>().Search(canvasTag);
            ClearAllUIContent(uiObjects);
        }

        private void Canvas_PCStatus_Right_EnteringAimMode(object sender,
            EnteringAimModeEventArgs e)
        {
            Range();
            Cooldown();
            Damage();
            Curse1();
            Curse2();
            Curse3();
        }

        private void Canvas_PCStatus_Right_LeavingAimMode(object sender, EventArgs e)
        {
            ClearAllUIContent(uiObjects);
        }

        private void ClearAllUIContent(GameObject[] uiObjects)
        {
            foreach (GameObject go in uiObjects)
            {
                go.GetComponent<Text>().text = "";
            }
        }

        private void Cooldown()
        {
            GetComponent<SearchUI>().SearchText(uiObjects, UITag.CooldownData).text
               = "8";
            GetComponent<SearchUI>().SearchText(uiObjects, UITag.CooldownText).text
               = "CD";
        }

        private void Curse1()
        {
            GetComponent<SearchUI>().SearchText(uiObjects, UITag.Curse1Text).text
               = "Fire -";
            GetComponent<SearchUI>().SearchText(uiObjects, UITag.Curse1Data).text
              = "T x 2";
        }

        private void Curse2()
        {
            GetComponent<SearchUI>().SearchText(uiObjects, UITag.Curse2Text).text
               = "Water -";
            GetComponent<SearchUI>().SearchText(uiObjects, UITag.Curse2Data).text
              = "T x 4";
        }

        private void Curse3()
        {
            GetComponent<SearchUI>().SearchText(uiObjects, UITag.Curse3Text).text
               = "Earth -";
            GetComponent<SearchUI>().SearchText(uiObjects, UITag.Curse3Data).text
              = "4 x 6";
        }

        private void Damage()
        {
            GetComponent<SearchUI>().SearchText(uiObjects, UITag.DamageText).text
               = "Dmg";
            GetComponent<SearchUI>().SearchText(uiObjects, UITag.DamageData).text
               = "4";
        }

        private void Range()
        {
            GetComponent<SearchUI>().SearchText(uiObjects, UITag.RangeText).text
               = "Range";
            GetComponent<SearchUI>().SearchText(uiObjects, UITag.RangeData).text
              = "5";
        }

        private void Start()
        {
            GetComponent<Wizard>().CreatedWorld
                += Canvas_PCStatus_Right_CreatedWorld;
            GetComponent<AimMode>().EnteringAimMode
                += Canvas_PCStatus_Right_EnteringAimMode;
            GetComponent<AimMode>().LeavingAimMode
                += Canvas_PCStatus_Right_LeavingAimMode;
        }
    }
}
