using AxeMan.DungeonObject.ActorSkill;
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
        private PCSkillManager skillManager;
        private GameObject[] uiObjects;

        private void Awake()
        {
            canvasTag = CanvasTag.Canvas_PCStatus_Right;
        }

        private void Canvas_PCStatus_Right_CreatedWorld(object sender, EventArgs e)
        {
            uiObjects = GetComponent<SearchUI>().Search(canvasTag);
            skillManager = GetComponent<SearchObject>().Search(SubTag.PC)[0]
                .GetComponent<PCSkillManager>();

            ClearAllUIContent(uiObjects);
        }

        private void Canvas_PCStatus_Right_EnteringAimMode(object sender,
            EnteringAimModeEventArgs e)
        {
            Range(e.CommandTag);
            Cooldown(e.CommandTag);
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

        private void Cooldown(CommandTag commandTag)
        {
            SearchText(UITag.CooldownText).text = "CD";
            SearchText(UITag.CooldownData).text
                = skillManager.GetMaxCooldown(commandTag).ToString();
        }

        private void Curse1()
        {
            SearchText(UITag.Curse1Text).text = "Fire -";
            SearchText(UITag.Curse1Data).text = "T x 2";
        }

        private void Curse2()
        {
            SearchText(UITag.Curse2Text).text = "Water -";
            SearchText(UITag.Curse2Data).text = "T x 4";
        }

        private void Curse3()
        {
            SearchText(UITag.Curse3Text).text = "Earth -";
            SearchText(UITag.Curse3Data).text = "4 x 6";
        }

        private void Damage()
        {
            SearchText(UITag.DamageText).text = "Dmg";
            SearchText(UITag.DamageData).text = "4";
        }

        private void Range(CommandTag commandTag)
        {
            SearchText(UITag.RangeText).text = "Range";

            SkillNameTag skillName = skillManager.GetSkillNameTag(commandTag);
            SearchText(UITag.RangeData).text
                = skillManager.GetSkillRange(skillName).ToString();
        }

        private Text SearchText(UITag uiTag)
        {
            return GetComponent<SearchUI>().SearchText(uiObjects, uiTag);
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
