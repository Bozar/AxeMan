using AxeMan.DungeonObject;
using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.SearchGameObject;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AxeMan.GameSystem.UserInterface
{
    public class Canvas_World : MonoBehaviour
    {
        private MetaInfo aimMetaInfo;
        private CanvasTag canvasTag;
        private LocalManager localManager;
        private PCSkillManager skillManager;
        private SkillNameTag skillNameTag;
        private GameObject[] uiObjects;

        private string AimModeText()
        {
            string mode = "Aim";
            string skillName = skillManager.GetSkillName(skillNameTag);

            int[] relativePos = localManager.GetRelativePosition(
                aimMetaInfo.Position);
            int relativeX = relativePos[0];
            int relativeY = relativePos[1];

            int distance = localManager.GetDistance(aimMetaInfo.Position);
            int range = skillManager.GetSkillRange(skillNameTag);

            string text
                = $"[ {mode} {skillName} ] [ {relativeX}, {relativeY} ]"
                + $" [ {distance} / {range} ]";

            return text;
        }

        private void Awake()
        {
            canvasTag = CanvasTag.Canvas_World;
        }

        private void Canvas_World_CreatedWorld(object sender, EventArgs e)
        {
            GameObject pc = GetComponent<SearchObject>().Search(SubTag.PC)[0];
            GameObject aim = GetComponent<SearchObject>().Search(
                SubTag.AimMarker)[0];

            uiObjects = GetComponent<SearchUI>().Search(canvasTag);
            skillManager = pc.GetComponent<PCSkillManager>();
            localManager = pc.GetComponent<LocalManager>();
            aimMetaInfo = aim.GetComponent<MetaInfo>();

            ClearModeline();
        }

        private void Canvas_World_EnteredAimMode(object sender,
            EnteredAimModeEventArgs e)
        {
            skillNameTag = skillManager.GetSkillNameTag(e.CommandTag);
            SearchText(UITag.Modeline).text = AimModeText();
        }

        private void Canvas_World_FailedVerifying(object sender,
            FailedVerifyingEventArgs e)
        {
            string skillName = skillManager.GetSkillName(e.UseSkill);

            SearchText(UITag.Modeline).text = $"Cannot use Skill {skillName}.";
        }

        private void Canvas_World_LeavingAimMode(object sender, EventArgs e)
        {
            skillNameTag = SkillNameTag.INVALID;
            ClearModeline();
        }

        private void Canvas_World_TakenAction(object sender,
            TakenActionEventArgs e)
        {
            if ((e.SubTag != SubTag.AimMarker) || (e.Action != ActionTag.Move))
            {
                return;
            }
            SearchText(UITag.Modeline).text = AimModeText();
        }

        private void ClearModeline()
        {
            SearchText(UITag.Modeline).text = "";
        }

        private Text SearchText(UITag uiTag)
        {
            return GetComponent<SearchUI>().SearchText(uiObjects, uiTag);
        }

        private void Start()
        {
            GetComponent<Wizard>().CreatedWorld += Canvas_World_CreatedWorld;
            GetComponent<AimMode>().EnteredAimMode
                += Canvas_World_EnteredAimMode;
            GetComponent<AimMode>().LeavingAimMode
                += Canvas_World_LeavingAimMode;
            GetComponent<AimMode>().FailedVerifying
                += Canvas_World_FailedVerifying;
            GetComponent<PublishAction>().TakenAction
                += Canvas_World_TakenAction;
        }
    }
}
