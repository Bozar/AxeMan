using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.SearchGameObject;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AxeMan.GameSystem.UserInterface
{
    public class Canvas_PCStatus_Middle : MonoBehaviour
    {
        private CanvasTag canvasTag;
        private PCSkillManager skillManager;
        private GameObject[] uiObjects;

        private void Awake()
        {
            canvasTag = CanvasTag.Canvas_PCStatus_Middle;
        }

        private void Canvas_PCStatus_Middle_CreatedWorld(object sender, EventArgs e)
        {
            GameObject pc = GetComponent<SearchObject>().Search(SubTag.PC)[0];
            UITag[] uiTags = new UITag[] { UITag.SkillText, UITag.SkillData, };

            uiObjects = GetComponent<SearchUI>().Search(canvasTag);
            skillManager = pc.GetComponent<PCSkillManager>();

            ClearUIContent(uiTags);
        }

        private void Canvas_PCStatus_Middle_EnteringAimMode(object sender,
            EnteringAimModeEventArgs e)
        {
            SearchText(UITag.SkillText).text = "Skill";
            SearchText(UITag.SkillData).text = skillManager.GetSkillName(
                skillManager.Convert(e.CommandTag));
        }

        private void Canvas_PCStatus_Middle_LeavingAimMode(object sender,
            EventArgs e)
        {
            UITag[] uiTags = new UITag[] { UITag.SkillText, UITag.SkillData, };
            ClearUIContent(uiTags);
        }

        private void ClearUIContent(UITag[] uiTags)
        {
            foreach (UITag t in uiTags)
            {
                SearchText(t).text = "";
            }
        }

        private Text SearchText(UITag uiTag)
        {
            return GetComponent<SearchUI>().SearchText(uiObjects, uiTag);
        }

        private void Start()
        {
            GetComponent<Wizard>().CreatedWorld
                += Canvas_PCStatus_Middle_CreatedWorld;
            GetComponent<AimMode>().EnteringAimMode
                += Canvas_PCStatus_Middle_EnteringAimMode;
            GetComponent<AimMode>().LeavingAimMode
                += Canvas_PCStatus_Middle_LeavingAimMode;
        }
    }
}
