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
        private CanvasTag cTag;

        private void Awake()
        {
            cTag = CanvasTag.Canvas_PCStatus_Left;
        }

        private void BatchUpdate(UITag[] ui, string[] text)
        {
            for (int i = 0; i < ui.Length; i++)
            {
                GetComponent<SearchUI>().SearchText(cTag, ui[i]).text
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
            UpdateHPData();
        }

        private void Canvas_PCStatus_CreatedWorld(object sender, EventArgs e)
        {
            UpdateHPText();
            UpdateHPData();

            UpdateSkillText();
            UpdateSkillData();
        }

        private void Start()
        {
            GetComponent<Wizard>().CreatedWorld += Canvas_PCStatus_CreatedWorld;
            GetComponent<PublishHP>().ChangedHP += Canvas_PCStatus_ChangedHP;
        }

        private void UpdateHPData()
        {
            Text ui = GetComponent<SearchUI>().SearchText(cTag, UITag.HPData);
            HP hp = GetComponent<SearchObject>().Search(SubTag.PC)[0]
                .GetComponent<HP>();
            ui.text = hp.Current + "/" + hp.Max;
        }

        private void UpdateHPText()
        {
            Text ui = GetComponent<SearchUI>().SearchText(cTag, UITag.HPText);
            ui.text = "HP";
        }

        private void UpdateSkillData()
        {
            UITag[] ui = new UITag[]
            {
                UITag.QText, UITag.WText, UITag.EText, UITag.RText
            };
            string[] text = new string[] { "Q", "W", "E", "R" };

            BatchUpdate(ui, text);
        }

        private void UpdateSkillText()
        {
            UITag[] ui = new UITag[]
            {
                UITag.QData, UITag.WData, UITag.EData, UITag.RData
            };
            string[] text = new string[] { "1", "2", "3", "4" };

            BatchUpdate(ui, text);
        }
    }
}
