using AxeMan.DungeonObject;
using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SearchGameObject;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AxeMan.GameSystem.UserInterface
{
    public class Canvas_PCStatus_CurrentStatus : MonoBehaviour
    {
        private ActorStatus actorStatus;
        private CanvasTag canvasTag;
        private SkillComponentTag[] orderedComponents;
        private UITag[] orderedStatusData;
        private UITag[] orderedStatusName;
        private PCSkillManager skillManager;
        private GameObject[] uiObjects;

        private void Awake()
        {
            canvasTag = CanvasTag.Canvas_PCStatus_CurrentStatus;

            orderedComponents = new SkillComponentTag[]
            {
                SkillComponentTag.FireMerit,
                SkillComponentTag.WaterMerit,
                SkillComponentTag.AirMerit,
                SkillComponentTag.EarthMerit,

                SkillComponentTag.FireFlaw,
                SkillComponentTag.WaterFlaw,
                SkillComponentTag.AirFlaw,
                SkillComponentTag.EarthFlaw,
            };

            orderedStatusName = new UITag[]
            {
                UITag.Status1Text,
                UITag.Status2Text,
                UITag.Status3Text,
                UITag.Status4Text,
            };

            orderedStatusData = new UITag[]
            {
                UITag.Status1Data,
                UITag.Status2Data,
                UITag.Status3Data,
                UITag.Status4Data,
            };
        }

        private void Canvas_PCStatus_CurrentStatus_ChangedActorStatus(
            object sender, EventArgs e)
        {
            ClearUIText(orderedStatusName);
            ClearUIText(orderedStatusData);
            PCStatus();
        }

        private void Canvas_PCStatus_CurrentStatus_CreatedWorld(
            object sender, EventArgs e)
        {
            uiObjects = GetComponent<SearchUI>().Search(canvasTag);

            PCStatus();
        }

        private void Canvas_PCStatus_CurrentStatus_SettingReference(
            object sender, SettingReferenceEventArgs e)
        {
            GameObject pc = e.PC;
            skillManager = pc.GetComponent<PCSkillManager>();
            actorStatus = pc.GetComponent<ActorStatus>();
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

        private void PCStatus()
        {
            int index = 0;

            foreach (SkillComponentTag sct in orderedComponents)
            {
                if (actorStatus.HasStatus(sct, out EffectData effectData))
                {
                    SearchText(orderedStatusName[index]).text =
                        skillManager.GetSkillComponentName(sct);
                    SearchText(orderedStatusData[index]).text =
                        skillManager.GetSkillEffectName(sct, effectData);
                    index++;
                }
            }
        }

        private Text SearchText(UITag uiTag)
        {
            return GetComponent<SearchUI>().SearchText(uiObjects, uiTag);
        }

        private void Start()
        {
            GetComponent<InitializeMainGame>().SettingReference
                += Canvas_PCStatus_CurrentStatus_SettingReference;
            GetComponent<InitializeMainGame>().CreatedWorld
                += Canvas_PCStatus_CurrentStatus_CreatedWorld;
            GetComponent<PublishActorStatus>().ChangedActorStatus
                += Canvas_PCStatus_CurrentStatus_ChangedActorStatus;
        }
    }
}
