using AxeMan.DungeonObject;
using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.SearchGameObject;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AxeMan.GameSystem.UserInterface
{
    public class Canvas_PCStatus_Middle : MonoBehaviour
    {
        private ActorStatus actorStatus;
        private CanvasTag canvasTag;
        private SkillComponentTag[] orderedComponents;
        private UITag[] orderedUIStatusData;
        private UITag[] orderedUIStatusName;
        private PCSkillManager skillManager;
        private GameObject[] uiObjects;

        private void Awake()
        {
            canvasTag = CanvasTag.Canvas_PCStatus_Middle;

            orderedComponents = new SkillComponentTag[]
            {
                SkillComponentTag.FireMerit, SkillComponentTag.FireFlaw,
                SkillComponentTag.WaterMerit, SkillComponentTag.WaterFlaw,
                SkillComponentTag.AirMerit, SkillComponentTag.AirFlaw,
                SkillComponentTag.EarthMerit, SkillComponentTag.EarthFlaw,
            };

            orderedUIStatusName = new UITag[]
            {
                UITag.Status1Text, UITag.Status2Text,
                UITag.Status3Text, UITag.Status4Text,
            };

            orderedUIStatusData = new UITag[]
            {
                UITag.Status1Data, UITag.Status2Data,
                UITag.Status3Data, UITag.Status4Data,
            };
        }

        private void Canvas_PCStatus_Middle_ChangedActorStatus(object sender,
            EventArgs e)
        {
            ClearUIText(orderedUIStatusName);
            ClearUIText(orderedUIStatusData);
            PCStatus();
        }

        private void Canvas_PCStatus_Middle_CreatedWorld(object sender,
            EventArgs e)
        {
            GameObject pc = GetComponent<SearchObject>().Search(SubTag.PC)[0];
            skillManager = pc.GetComponent<PCSkillManager>();
            actorStatus = pc.GetComponent<ActorStatus>();
            uiObjects = GetComponent<SearchUI>().Search(canvasTag);

            ClearUIText(uiObjects);
            PCStatus();
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

        private SkillComponentTag[] GetOrderedComponents(
            Dictionary<SkillComponentTag, EffectData> compInt)
        {
            Queue<SkillComponentTag> ordered = new Queue<SkillComponentTag>();

            foreach (SkillComponentTag sct in orderedComponents)
            {
                if (compInt.ContainsKey(sct))
                {
                    ordered.Enqueue(sct);
                }
            }
            return ordered.ToArray();
        }

        private void PCStatus()
        {
            Dictionary<SkillComponentTag, EffectData> compInt
                = actorStatus.CurrentStatus;
            SkillComponentTag[] orderedComp = GetOrderedComponents(compInt);
            string statusName;
            string statusData;

            for (int i = 0; i < orderedComp.Length; i++)
            {
                statusName = skillManager.GetSkillComponentName(orderedComp[i]);
                statusData = skillManager.GetSkillEffectName(
                    orderedComp[i], compInt[orderedComp[i]]);

                SearchText(orderedUIStatusName[i]).text = statusName;
                SearchText(orderedUIStatusData[i]).text = statusData;
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
            GetComponent<PublishActorStatus>().ChangedActorStatus
                += Canvas_PCStatus_Middle_ChangedActorStatus;
        }
    }
}
