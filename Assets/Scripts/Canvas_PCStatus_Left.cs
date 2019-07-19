using AxeMan.DungeonObject;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.SearchGameObject;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AxeMan.GameSystem.UserInterface
{
    public class Canvas_PCStatus_Left : MonoBehaviour
    {
        private CanvasTag canvasTag;
        private GameObject[] uiObjects;

        private void Awake()
        {
            canvasTag = CanvasTag.Canvas_PCStatus_Left;
        }

        private void BatchUpdate(UITag[] ui, string[] text)
        {
            for (int i = 0; i < ui.Length; i++)
            {
                GetComponent<SearchUI>().SearchText(uiObjects, ui[i]).text
                    = text[i];
            }
        }

        private void Canvas_PCStatus_ChangedHP(object sender,
            ChangedHPEventArgs e)
        {
            if (e.STag != SubTag.PC)
            {
                return;
            }
            HPData();
        }

        private void Canvas_PCStatus_CreatedWorld(object sender, EventArgs e)
        {
            uiObjects = GetComponent<SearchUI>().Search(canvasTag);

            HPText();
            HPData();

            SkillText();
            SkillData();
        }

        private void HPData()
        {
            Text ui = GetComponent<SearchUI>().SearchText(uiObjects, UITag.HPData);
            HP hp = GetComponent<SearchObject>().Search(SubTag.PC)[0]
                .GetComponent<HP>();
            ui.text = hp.Current + "/" + hp.Max;
        }

        private void HPText()
        {
            Text ui = GetComponent<SearchUI>().SearchText(uiObjects, UITag.HPText);
            ui.text = "HP";
        }

        private void SkillData()
        {
            UITag[] ui = new UITag[]
            {
                UITag.QData, UITag.WData, UITag.EData, UITag.RData
            };
            string[] text = new string[] { "1", "2", "3", "4" };

            BatchUpdate(ui, text);
        }

        private void SkillText()
        {
            UITag[] ui = new UITag[]
            {
                UITag.QText, UITag.WText, UITag.EText, UITag.RText
            };
            string[] text = new string[] { "Q", "W", "E", "R" };

            BatchUpdate(ui, text);
        }

        private void Start()
        {
            GetComponent<Wizard>().CreatedWorld += Canvas_PCStatus_CreatedWorld;
            GetComponent<PublishHP>().ChangedHP += Canvas_PCStatus_ChangedHP;
        }
    }
}
