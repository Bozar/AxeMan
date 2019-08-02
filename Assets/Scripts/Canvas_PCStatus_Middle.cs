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

        private void Canvas_PCStatus_Middle_CreatedWorld(object sender,
            EventArgs e)
        {
            GameObject pc = GetComponent<SearchObject>().Search(SubTag.PC)[0];
            skillManager = pc.GetComponent<PCSkillManager>();
            uiObjects = GetComponent<SearchUI>().Search(canvasTag);

            ClearUIText(uiObjects);
        }

        private void Canvas_PCStatus_Middle_EnteringAimMode(object sender,
            EnteringAimModeEventArgs e)
        {
            SkillNameTag skillName = skillManager.GetSkillNameTag(e.CommandTag);
            SkillTypeTag skillType = skillManager.GetSkillTypeTag(skillName);

            string name = skillManager.GetSkillName(e.CommandTag);
            string type = skillManager.GetLongSkillTypeName(skillType);

            SearchText(UITag.SkillText).text = type;
            SearchText(UITag.SkillData).text = name;
        }

        private void Canvas_PCStatus_Middle_LeavingAimMode(object sender,
            EventArgs e)
        {
            UITag[] uiTags = new UITag[] { UITag.SkillText, UITag.SkillData, };
            ClearUIText(uiTags);
        }

        private void ClearUIText(UITag[] uiTags)
        {
            foreach (UITag uit in uiTags)
            {
                SearchText(uit).text = "";
            }
        }

        private void ClearUIText(GameObject[] uiObjects)
        {
            foreach (GameObject go in uiObjects)
            {
                go.GetComponent<Text>().text = "";
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
