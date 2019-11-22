using AxeMan.DungeonObject;
using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SearchGameObject;
using System;
using System.Collections.Generic;
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
        private Dictionary<UITag, Text> uiDict;
        private GameObject[] uiObjects;

        private string AimModeText()
        {
            string mode = GetText(UITextDataTag.AimMode);
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

        private void AltarCooldown()
        {
            string current = GetComponent<AltarCooldown>().CurrentCooldown
                .ToString();
            string max = GetComponent<AltarCooldown>().MaxCooldown.ToString();
            string text = GetText(UITextDataTag.AltarCooldown, current, max);

            SearchText(UITag.Line6).text = text;
        }

        private void Awake()
        {
            canvasTag = CanvasTag.Canvas_World;
            uiDict = new Dictionary<UITag, Text>();
        }

        private void Canvas_World_ChangedAltarCooldown(object sender,
            EventArgs e)
        {
            AltarCooldown();
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

            SearchText(UITag.Modeline).text = GetText(UITextDataTag.UseSkill,
                skillName);
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
            string mode = GetText(UITextDataTag.ExamineMode);

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

        private string GetColorfulText(string text, ColorTag colorTag)
        {
            GetComponent<ColorManager>().SetColor(text, colorTag, out text);
            return text;
        }

        private string GetText(UITextDataTag dataTag)
        {
            UITextCategoryTag categoryTag = UITextCategoryTag.World;

            return GetComponent<UITextData>().GetStringData(categoryTag, dataTag);
        }

        private string GetText(UITextDataTag dataTag, string newText)
        {
            string placeholder = "%PLACEHOLDER1%";
            string text = GetText(dataTag);
            text = text.Replace(placeholder, newText);

            return text;
        }

        private string GetText(UITextDataTag dataTag, string newText1,
            string newText2)
        {
            string placeholder = "%PLACEHOLDER2%";
            string text = GetText(dataTag, newText1);
            text = text.Replace(placeholder, newText2);

            return text;
        }

        private void NormalModeText()
        {
            string text;
            string current;
            string max;
            ColorTag color = ColorTag.Grey;

            string version = GetComponent<GameVersion>().Version;
            text = GetText(UITextDataTag.Version, version);
            SearchText(UITag.Line1).text = GetColorfulText(text, color);

            string seed = "123-456-789";
            text = GetText(UITextDataTag.Seed, seed);
            SearchText(UITag.Line2).text = GetColorfulText(text, color);

            string difficulty = "Hard";
            text = GetText(UITextDataTag.Difficulty, difficulty);
            SearchText(UITag.Line3).text = GetColorfulText(text, color);

            current = "12";
            max = "50";
            text = GetText(UITextDataTag.GameProgress, current, max);
            SearchText(UITag.Line4).text = text;

            current = "1";
            max = "3";
            text = GetText(UITextDataTag.AltarLevel, current, max);
            SearchText(UITag.Line5).text = text;

            AltarCooldown();
        }

        private Text SearchText(UITag uiTag)
        {
            if (!uiDict.ContainsKey(uiTag))
            {
                uiDict[uiTag] = GetComponent<SearchUI>().SearchText(uiObjects,
                    uiTag);
            }
            return uiDict[uiTag];
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
            GetComponent<AltarCooldown>().ChangedAltarCooldown
                += Canvas_World_ChangedAltarCooldown;
        }
    }
}
