using AxeMan.DungeonObject;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.SearchGameObject;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AxeMan.GameSystem.UserInterface
{
    public class Canvas_ExamineMode : MonoBehaviour
    {
        private MetaInfo aimMetaInfo;
        private CanvasTag canvasTag;
        private MetaInfo examineMetaInfo;
        private LocalManager pcLocalManager;
        private GameObject[] uiObjects;

        private void Awake()
        {
            canvasTag = CanvasTag.Canvas_ExamineMode;
        }

        private void Canvas_ExamineMode_CreatedWorld(object sender, EventArgs e)
        {
            uiObjects = GetComponent<SearchUI>().Search(canvasTag);
            SwitchVisibility(false);
        }

        private void Canvas_ExamineMode_EnteredAimMode(object sender,
            EnterAimModeEventArgs e)
        {
            ClearText();

            GameObject target = GetTargetUnderMarker(SubTag.AimMarker);
            PrintModeline(target);

            SwitchVisibility(true);
        }

        private void Canvas_ExamineMode_EnteredExamineMode(object sender,
            EventArgs e)
        {
            ClearText();

            GameObject target = GetTargetUnderMarker(SubTag.ExamineMarker);
            PrintModeline(target);

            SwitchVisibility(true);
        }

        private void Canvas_ExamineMode_LeavingAimMode(object sender,
            EventArgs e)
        {
            SwitchVisibility(false);
        }

        private void Canvas_ExamineMode_LeavingExamineMode(object sender,
            EventArgs e)
        {
            SwitchVisibility(false);
        }

        private void Canvas_ExamineMode_SettingReference(object sender,
            SettingReferenceEventArgs e)
        {
            aimMetaInfo = e.AimMarker.GetComponent<MetaInfo>();
            examineMetaInfo = e.ExamineMarker.GetComponent<MetaInfo>();
            pcLocalManager = e.PC.GetComponent<LocalManager>();
        }

        private void Canvas_ExamineMode_TakenAction(object sender,
            PublishActionEventArgs e)
        {
            if (e.Action != ActionTag.Move)
            {
                return;
            }
            if ((e.SubTag != SubTag.AimMarker)
                && (e.SubTag != SubTag.ExamineMarker))
            {
                return;
            }

            ClearText();

            GameObject target = GetTargetUnderMarker(e.SubTag);
            if (target == null)
            {
                return;
            }
            PrintModeline(target);
        }

        private void ClearText()
        {
            foreach (GameObject ui in uiObjects)
            {
                ui.GetComponent<Text>().text = "";
            }
        }

        private GameObject GetTargetUnderMarker(SubTag markerTag)
        {
            int x, y;
            switch (markerTag)
            {
                case SubTag.AimMarker:
                    x = aimMetaInfo.Position[0];
                    y = aimMetaInfo.Position[1];
                    break;

                case SubTag.ExamineMarker:
                    x = examineMetaInfo.Position[0];
                    y = examineMetaInfo.Position[1];
                    break;

                default:
                    return null;
            }

            GameObject[] sorted
                = GetComponent<TileOverlay>().GetSortedObjects(x, y);
            foreach (GameObject s in sorted)
            {
                if (s.GetComponent<MetaInfo>().MainTag != MainTag.Marker)
                {
                    return s;
                }
            }
            return null;
        }

        private void PrintModeline(GameObject target)
        {
            // TODO: Get actor's name.
            string name = target.GetComponent<MetaInfo>().SubTag.ToString();

            int[] targetPos = target.GetComponent<MetaInfo>().Position;
            int[] relativePos = pcLocalManager.GetRelativePosition(targetPos);
            int relativeX = relativePos[0];
            int relativeY = relativePos[1];

            int distance = pcLocalManager.GetDistance(targetPos);

            string modeline
                = $"[ {name} ] [ {relativeX}, {relativeY} ] [ {distance} ]";

            SearchText(UITag.Modeline).text = modeline;
        }

        private Text SearchText(UITag uiTag)
        {
            return GetComponent<SearchUI>().SearchText(uiObjects, uiTag);
        }

        private void Start()
        {
            GetComponent<Wizard>().SettingReference
                += Canvas_ExamineMode_SettingReference;
            GetComponent<Wizard>().CreatedWorld
                += Canvas_ExamineMode_CreatedWorld;

            GetComponent<AimMode>().EnteredAimMode
                += Canvas_ExamineMode_EnteredAimMode;
            GetComponent<AimMode>().LeavingAimMode
                += Canvas_ExamineMode_LeavingAimMode;

            GetComponent<ExamineMode>().EnteredExamineMode
                += Canvas_ExamineMode_EnteredExamineMode;
            GetComponent<ExamineMode>().LeavingExamineMode
                += Canvas_ExamineMode_LeavingExamineMode;

            GetComponent<PublishAction>().TakenAction
                += Canvas_ExamineMode_TakenAction;
        }

        private void SwitchVisibility(bool switchOn)
        {
            GetComponent<UIManager>().SwitchCanvasVisibility(canvasTag, switchOn);
        }
    }
}
