using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SearchGameObject;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace AxeMan.GameSystem.UserInterface
{
    public class Canvas_PCStatus_SkillData : MonoBehaviour
    {
        private CanvasTag canvasTag;
        private SkillTypeTag[] printEffect;
        private PCSkillManager skillManager;
        private SkillComponentTag[] sortedCurse;
        private SkillComponentTag[] sortedEnhance;
        private UITag[] uiCurseData;
        private UITag[] uiCurseText;
        private GameObject[] uiObjects;

        private void Awake()
        {
            canvasTag = CanvasTag.Canvas_PCStatus_SkillData;

            uiCurseText = new UITag[]
            {
                UITag.Status1Text, UITag.Status2Text, UITag.Status3Text,
            };

            uiCurseData = new UITag[]
            {
                UITag.Status1Data, UITag.Status2Data, UITag.Status3Data,
            };

            printEffect = new SkillTypeTag[]
            {
                SkillTypeTag.Curse, SkillTypeTag.Buff,
            };

            sortedCurse = new SkillComponentTag[]
            {
                SkillComponentTag.FireCurse,
                SkillComponentTag.WaterCurse,
                SkillComponentTag.AirCurse,
                SkillComponentTag.EarthCurse,
            };

            sortedEnhance = new SkillComponentTag[]
            {
                SkillComponentTag.FireMerit,
                SkillComponentTag.WaterMerit,
                SkillComponentTag.AirMerit,
                SkillComponentTag.EarthMerit,
            };
        }

        private void Canvas_PCStatus_SkillData_CreatedWorld(object sender,
            EventArgs e)
        {
            uiObjects = GetComponent<SearchUI>().Search(canvasTag);

            GetComponent<UIManager>().SwitchCanvasVisibility(canvasTag, false);
        }

        private void Canvas_PCStatus_SkillData_SettingReference(object sender,
            SettingReferenceEventArgs e)
        {
            skillManager = e.PC.GetComponent<PCSkillManager>();
        }

        private void Canvas_PCStatus_SkillData_SwitchingGameMode(object sender,
            SwitchGameModeEventArgs e)
        {
            if (e.EnterMode == GameModeTag.AimMode)
            {
                foreach (GameObject go in uiObjects)
                {
                    go.GetComponent<Text>().text = "";
                }

                Range(e.CommandTag);
                Cooldown(e.CommandTag);
                Damage(e.CommandTag);
                Effect(e.CommandTag);
                SkillNameType(e.CommandTag);

                if (e.LeaveMode == GameModeTag.NormalMode)
                {
                    GetComponent<UIManager>().SwitchCanvasVisibility(canvasTag,
                        true);
                }
            }
            else if (e.LeaveMode == GameModeTag.AimMode)
            {
                GetComponent<UIManager>().SwitchCanvasVisibility(canvasTag,
                    false);
            }
        }

        private void Cooldown(CommandTag commandTag)
        {
            string label = GetComponent<UITextData>().GetStringData(
                UITextCategoryTag.ActorStatus, UITextDataTag.Cooldown);
            int currentCooldown = skillManager.GetCurrentCooldown(commandTag);
            int maxCooldown = skillManager.GetMaxCooldown(commandTag);

            SearchText(UITag.CooldownText).text = label;
            SearchText(UITag.CooldownData).text
                = currentCooldown + " / " + maxCooldown;
        }

        private void Damage(CommandTag commandTag)
        {
            SkillTypeTag skillType = skillManager.GetSkillTypeTag(commandTag);
            if (skillType != SkillTypeTag.Attack)
            {
                return;
            }

            SearchText(UITag.Status1Text).text
                 = GetComponent<UITextData>().GetStringData(
                    UITextCategoryTag.ActorStatus, UITextDataTag.Damage);
            SearchText(UITag.Status1Data).text
                = skillManager.GetSkillDamage(commandTag).ToString();
        }

        private void Effect(CommandTag commandTag)
        {
            SkillTypeTag skillType = skillManager.GetSkillTypeTag(commandTag);
            if (!printEffect.Contains(skillType))
            {
                return;
            }

            SkillNameTag skillName = skillManager.GetSkillNameTag(commandTag);
            Dictionary<SkillComponentTag, EffectData> compInt
                = skillManager.GetSkillEffect(skillName);
            SkillComponentTag[] sortedComp = SortComponents(skillType, compInt);

            string comp;
            string effect;
            SkillComponentTag current;

            for (int i = 0; i < sortedComp.Length; i++)
            {
                current = sortedComp[i];
                comp = skillManager.GetSkillComponentName(current);
                effect = skillManager.GetSkillEffectName(current,
                    compInt[current]);

                SearchText(uiCurseText[i]).text = comp;
                SearchText(uiCurseData[i]).text = effect;
            }
        }

        private void Range(CommandTag commandTag)
        {
            SearchText(UITag.RangeText).text
                 = GetComponent<UITextData>().GetStringData(
                    UITextCategoryTag.ActorStatus, UITextDataTag.AttackRange);

            SkillNameTag skillName = skillManager.GetSkillNameTag(commandTag);
            SearchText(UITag.RangeData).text
                = skillManager.GetSkillRange(skillName).ToString();
        }

        private Text SearchText(UITag uiTag)
        {
            return GetComponent<SearchUI>().SearchText(uiObjects, uiTag);
        }

        private void SkillNameType(CommandTag e)
        {
            SkillNameTag skillName = skillManager.GetSkillNameTag(e);
            SkillTypeTag skillType = skillManager.GetSkillTypeTag(skillName);

            string name = skillManager.GetSkillName(skillName);
            string type = skillManager.GetLongSkillTypeName(skillType);

            SearchText(UITag.SkillText).text = type;
            SearchText(UITag.SkillData).text = name;
        }

        private SkillComponentTag[] SortComponents(SkillTypeTag skillTypeTag,
            Dictionary<SkillComponentTag, EffectData> compInt)
        {
            Queue<SkillComponentTag> sortedComp = new Queue<SkillComponentTag>();
            SkillComponentTag[] checkList;

            switch (skillTypeTag)
            {
                case SkillTypeTag.Buff:
                    checkList = sortedEnhance;
                    break;

                case SkillTypeTag.Curse:
                    checkList = sortedCurse;
                    break;

                default:
                    checkList = new SkillComponentTag[] { };
                    break;
            }

            foreach (SkillComponentTag sct in checkList)
            {
                if (compInt.ContainsKey(sct))
                {
                    sortedComp.Enqueue(sct);
                }
            }
            return sortedComp.ToArray();
        }

        private void Start()
        {
            GetComponent<InitializeMainGame>().SettingReference
                += Canvas_PCStatus_SkillData_SettingReference;
            GetComponent<InitializeMainGame>().CreatedWorld
                += Canvas_PCStatus_SkillData_CreatedWorld;
            GetComponent<GameModeManager>().SwitchingGameMode
                += Canvas_PCStatus_SkillData_SwitchingGameMode;
        }
    }
}
