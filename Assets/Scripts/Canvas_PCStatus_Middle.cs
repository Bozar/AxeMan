using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.SearchGameObject;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem.UserInterface
{
    public class Canvas_PCStatus_Middle : MonoBehaviour
    {
        private CanvasTag canvasTag;
        private Dictionary<CommandTag, string> skillName;
        private GameObject[] uiObjects;

        private void Awake()
        {
            canvasTag = CanvasTag.Canvas_PCStatus_Middle;
            skillName = new Dictionary<CommandTag, string>()
            {
                { CommandTag.SkillQ, "Q" }, { CommandTag.SkillW, "W" },
                { CommandTag.SkillE, "E" }, { CommandTag.SkillR, "R" },
            };
        }

        private void Canvas_PCStatus_CreatedWorld(object sender, EventArgs e)
        {
            uiObjects = GetComponent<SearchUI>().Search(canvasTag);
            UITag[] uiTags = new UITag[] { UITag.SkillText, UITag.SkillData, };

            ClearUIContent(uiTags);
        }

        private void Canvas_PCStatus_Middle_EnteringAimMode(object sender,
            EnteringAimModeEventArgs e)
        {
            GetComponent<SearchUI>().SearchText(uiObjects, UITag.SkillText).text
                = "Skill";
            GetComponent<SearchUI>().SearchText(uiObjects, UITag.SkillData).text
                = skillName[e.CommandTag];
        }

        private void Canvas_PCStatus_Middle_LeavingAimMode(object sender,
            EventArgs e)
        {
            UITag[] uiTags = new UITag[] { UITag.SkillText, UITag.SkillData, };
            ClearUIContent(uiTags);
        }

        private void ClearUIContent(UITag[] uiTags)
        {
            foreach (UITag tag in uiTags)
            {
                GetComponent<SearchUI>().SearchText(uiObjects, tag).text = "";
            }
        }

        private void Start()
        {
            GetComponent<Wizard>().CreatedWorld += Canvas_PCStatus_CreatedWorld;
            GetComponent<AimMode>().EnteringAimMode
                += Canvas_PCStatus_Middle_EnteringAimMode;
            GetComponent<AimMode>().LeavingAimMode
                += Canvas_PCStatus_Middle_LeavingAimMode;
        }
    }
}
