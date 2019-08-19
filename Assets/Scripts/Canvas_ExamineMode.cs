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
            PrintTargetData(target);

            SwitchVisibility(true);
        }

        private void Canvas_ExamineMode_EnteredExamineMode(object sender,
            EventArgs e)
        {
            ClearText();

            GameObject target = GetTargetUnderMarker(SubTag.ExamineMarker);
            PrintTargetData(target);

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
            PrintTargetData(target);
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
            int index;

            // Get marker position.
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

            // PC, NPC, Building, Trap, Floor.
            GameObject[] sortedTargets = new GameObject[5];
            MainTag[] sortedMainTags = new MainTag[]
            {
                MainTag.INVALID,
                MainTag.Actor,
                MainTag.Building,
                MainTag.Trap,
                MainTag.Floor,
            };

            // Sort targets under marker.
            if (GetComponent<SearchObject>().Search(x, y,
                out GameObject[] targets))
            {
                foreach (GameObject t in targets)
                {
                    index = Array.IndexOf(sortedMainTags,
                        t.GetComponent<MetaInfo>().MainTag);

                    if (t.GetComponent<MetaInfo>().SubTag == SubTag.PC)
                    {
                        sortedTargets[0] = t;
                    }
                    else if (index > 0)
                    {
                        sortedTargets[index] = t;
                    }
                }
            }
            else
            {
                return null;
            }

            // Return the target on the top layer.
            foreach (GameObject s in sortedTargets)
            {
                if (s != null)
                {
                    return s;
                }
            }
            return null;
        }

        private void PrintTargetData(GameObject target)
        {
            if (target == null)
            {
                return;
            }

            SearchText(UITag.Modeline).text
                = target.GetComponent<MetaInfo>().SubTag.ToString();
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
