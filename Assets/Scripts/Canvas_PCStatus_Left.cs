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
            Text qUI = GetComponent<SearchUI>().SearchText(cTag, UITag.QText);
            Text wUI = GetComponent<SearchUI>().SearchText(cTag, UITag.WText);
            Text eUI = GetComponent<SearchUI>().SearchText(cTag, UITag.EText);
            Text rUI = GetComponent<SearchUI>().SearchText(cTag, UITag.RText);

            string qText = "Q";
            string wText = "W";
            string eText = "E";
            string rText = "R";

            qUI.text = qText;
            wUI.text = wText;
            eUI.text = eText;
            rUI.text = rText;
        }

        private void UpdateSkillText()
        {
            Text qUI = GetComponent<SearchUI>().SearchText(cTag, UITag.QData);
            Text wUI = GetComponent<SearchUI>().SearchText(cTag, UITag.WData);
            Text eUI = GetComponent<SearchUI>().SearchText(cTag, UITag.EData);
            Text rUI = GetComponent<SearchUI>().SearchText(cTag, UITag.RData);

            string qText = "1";
            string wText = "2";
            string eText = "3";
            string rText = "4";

            qUI.text = qText;
            wUI.text = wText;
            eUI.text = eText;
            rUI.text = rText;
        }
    }
}
