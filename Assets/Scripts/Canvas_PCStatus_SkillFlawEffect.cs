using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SearchGameObject;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AxeMan.GameSystem.UserInterface
{
    public class Canvas_PCStatus_SkillFlawEffect : MonoBehaviour
    {
        private CanvasTag canvasTag;
        private SkillComponentTag[] orderedFlawEffect;
        private UITag[] orderedUIData;
        private UITag[] orderedUIText;
        private PCSkillManager skillManager;
        private GameObject[] uiObjects;

        private void Awake()
        {
            canvasTag = CanvasTag.Canvas_PCStatus_SkillFlawEffect;

            orderedFlawEffect = new SkillComponentTag[]
            {
                SkillComponentTag.FireFlaw,
                SkillComponentTag.WaterFlaw,
                SkillComponentTag.AirFlaw,
                SkillComponentTag.EarthFlaw,
            };

            orderedUIText = new UITag[]
            {
                UITag.Status1Text, UITag.Status2Text, UITag.Status3Text,
            };

            orderedUIData = new UITag[]
            {
                UITag.Status1Data, UITag.Status2Data, UITag.Status3Data,
            };
        }

        private void Canvas_PCStatus_SkillFlawEffect_CreatedWorld(object sender,
            EventArgs e)
        {
            uiObjects = GetComponent<SearchUI>().Search(canvasTag);
        }

        private void Canvas_PCStatus_SkillFlawEffect_SettingReference(
            object sender, SettingReferenceEventArgs e)
        {
            skillManager = e.PC.GetComponent<PCSkillManager>();
        }

        private void Canvas_PCStatus_SkillFlawEffect_SwitchingGameMode(
            object sender, SwitchGameModeEventArgs e)
        {
            if (e.EnterMode == GameModeTag.AimMode)
            {
                ClearUIText();
                SkillFlawEffect(e);
            }
            else if (e.LeaveMode == GameModeTag.AimMode)
            {
                ClearUIText();
            }
        }

        private void ClearUIText()
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

        private void SkillFlawEffect(SwitchGameModeEventArgs e)
        {
            SkillNameTag skillName = skillManager.GetSkillNameTag(e.CommandTag);
            Dictionary<SkillComponentTag, EffectData> compInt
                = skillManager.GetSkillEffect(skillName);

            int index = 0;
            Text uiText;
            Text uiData;
            string effectName;
            string effectData;

            foreach (SkillComponentTag sct in orderedFlawEffect)
            {
                if (compInt.ContainsKey(sct))
                {
                    uiText = SearchText(orderedUIText[index]);
                    uiData = SearchText(orderedUIData[index]);

                    effectName = skillManager.GetSkillComponentName(sct);
                    effectData = skillManager.GetSkillEffectName(sct,
                        compInt[sct]);

                    uiText.text = effectName;
                    uiData.text = effectData;

                    index++;
                }
            }
        }

        private void Start()
        {
            GetComponent<InitializeMainGame>().SettingReference
                += Canvas_PCStatus_SkillFlawEffect_SettingReference;
            GetComponent<InitializeMainGame>().CreatedWorld
                += Canvas_PCStatus_SkillFlawEffect_CreatedWorld;
            GetComponent<GameModeManager>().SwitchingGameMode
                += Canvas_PCStatus_SkillFlawEffect_SwitchingGameMode;
        }
    }
}
