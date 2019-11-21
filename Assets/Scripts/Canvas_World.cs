using AxeMan.DungeonObject;
using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.InitializeGameWorld;
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
        private MetaInfo examineMetaInfo;
        private LocalManager pcLocalManager;
        private PCSkillManager skillManager;
        private SkillNameTag skillNameTag;
        private GameObject[] uiObjects;

        private string AimModeText()
        {
            string mode = "Aim";
            string skillName = skillManager.GetSkillName(skillNameTag);

            int[] relativePos = pcLocalManager.GetRelativePosition(
                aimMetaInfo.Position);
            int relativeX = relativePos[0];
            int relativeY = relativePos[1];

            int distance = pcLocalManager.GetDistance(aimMetaInfo.Position);
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
            uiObjects = GetComponent<SearchUI>().Search(canvasTag);

            NormalModeText();
        }

        private void Canvas_World_EnteredAimMode(object sender,
            EnterAimModeEventArgs e)
        {
            skillNameTag = skillManager.GetSkillNameTag(e.CommandTag);
            SearchText(UITag.Modeline).text = AimModeText();
        }

        private void Canvas_World_EnteredExamineMode(object sender, EventArgs e)
        {
            SearchText(UITag.Modeline).text = ExamineModeText();
        }

        private void Canvas_World_FailedVerifying(object sender,
            FailedVerifyingEventArgs e)
        {
            SkillNameTag tag = skillManager.GetSkillNameTag(e.UseSkill);
            string skillName = skillManager.GetSkillName(tag);

            SearchText(UITag.Modeline).text = $"Cannot use Skill {skillName}.";
        }

        private void Canvas_World_LeavingAimMode(object sender, EventArgs e)
        {
            skillNameTag = SkillNameTag.INVALID;
            ClearModeline();
        }

        private void Canvas_World_LeavingExamineMode(object sender, EventArgs e)
        {
            ClearModeline();
        }

        private void Canvas_World_SettingReference(object sender,
            SettingReferenceEventArgs e)
        {
            skillManager = e.PC.GetComponent<PCSkillManager>();
            pcLocalManager = e.PC.GetComponent<LocalManager>();
            aimMetaInfo = e.AimMarker.GetComponent<MetaInfo>();
            examineMetaInfo = e.ExamineMarker.GetComponent<MetaInfo>();
        }

        private void Canvas_World_TakenAction(object sender,
            PublishActionEventArgs e)
        {
            // Only responds to move action from aim or examine marker.
            if (e.Action != ActionTag.Move)
            {
                return;
            }
            if ((e.SubTag != SubTag.AimMarker)
                && (e.SubTag != SubTag.ExamineMarker))
            {
                return;
            }

            switch (e.SubTag)
            {
                case SubTag.AimMarker:
                    SearchText(UITag.Modeline).text = AimModeText();
                    break;

                case SubTag.ExamineMarker:
                    SearchText(UITag.Modeline).text = ExamineModeText();
                    break;

                default:
                    break;
            }
        }

        private void ClearModeline()
        {
            SearchText(UITag.Modeline).text = "";
        }

        private string ExamineModeText()
        {
            string mode = "Examine";

            int[] relativePos = pcLocalManager.GetRelativePosition(
                examineMetaInfo.Position);
            int relativeX = relativePos[0];
            int relativeY = relativePos[1];

            int distance = pcLocalManager.GetDistance(examineMetaInfo.Position);

            string text
                = $"[ {mode} ] [ {relativeX}, {relativeY} ]"
                + $" [ {distance} ]";

            return text;
        }

        private void NormalModeText()
        {
            SearchText(UITag.Line1).text = "Version: 0.0.1";
            SearchText(UITag.Line2).text = "Seed: 123-456-789";
            SearchText(UITag.Line3).text = "Difficulty: Hard";

            SearchText(UITag.Line4).text = "Remaining Enemy: 12/50";
            SearchText(UITag.Line5).text = "Altar Level: 1/3";
            SearchText(UITag.Line6).text = "Altar Cooldown: 12/20";
        }

        private Text SearchText(UITag uiTag)
        {
            return GetComponent<SearchUI>().SearchText(uiObjects, uiTag);
        }

        private void Start()
        {
            GetComponent<InitializeMainGame>().SettingReference
                += Canvas_World_SettingReference;
            GetComponent<InitializeMainGame>().CreatedWorld
                += Canvas_World_CreatedWorld;

            GetComponent<AimMode>().EnteredAimMode
                += Canvas_World_EnteredAimMode;
            GetComponent<AimMode>().LeavingAimMode
                += Canvas_World_LeavingAimMode;
            GetComponent<AimMode>().FailedVerifying
               += Canvas_World_FailedVerifying;

            GetComponent<ExamineMode>().EnteredExamineMode
                += Canvas_World_EnteredExamineMode;
            GetComponent<ExamineMode>().LeavingExamineMode
                += Canvas_World_LeavingExamineMode;

            GetComponent<PublishAction>().TakenAction
                += Canvas_World_TakenAction;
        }
    }
}
