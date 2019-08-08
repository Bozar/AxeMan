using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
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
                UITag.Curse1Text, UITag.Curse2Text, UITag.Curse3Text,
            };

            uiCurseData = new UITag[]
            {
                UITag.Curse1Data, UITag.Curse2Data, UITag.Curse3Data,
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

        private void Canvas_PCStatus_Right_CreatedWorld(object sender,
            EventArgs e)
        {
            uiObjects = GetComponent<SearchUI>().Search(canvasTag);
            skillManager = GetComponent<SearchObject>().Search(SubTag.PC)[0]
                .GetComponent<PCSkillManager>();

            ClearUIText(uiObjects);
        }

        private void Canvas_PCStatus_Right_EnteringAimMode(object sender,
            EnteringAimModeEventArgs e)
        {
            ClearUIText(uiObjects);

            Range(e.CommandTag);
            Cooldown(e.CommandTag);
            Damage(e.CommandTag);
            Effect(e.CommandTag);
        }

        private void Canvas_PCStatus_Right_LeavingAimMode(object sender,
            EventArgs e)
        {
            ClearUIText(uiObjects);
        }

        private void ClearUIText(GameObject[] uiObjects)
        {
            foreach (GameObject go in uiObjects)
            {
                go.GetComponent<Text>().text = "";
            }
        }

        private void Cooldown(CommandTag commandTag)
        {
            SearchText(UITag.CooldownText).text = "CD";
            SearchText(UITag.CooldownData).text
                = skillManager.GetMaxCooldown(commandTag).ToString();
        }

        private void Damage(CommandTag commandTag)
        {
            SkillTypeTag skillType = skillManager.GetSkillTypeTag(commandTag);
            if (skillType != SkillTypeTag.Attack)
            {
                return;
            }

            SearchText(UITag.DamageText).text = "Dmg";
            SearchText(UITag.DamageData).text
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
            SearchText(UITag.RangeText).text = "Range";

            SkillNameTag skillName = skillManager.GetSkillNameTag(commandTag);
            SearchText(UITag.RangeData).text
                = skillManager.GetSkillRange(skillName).ToString();
        }

        private Text SearchText(UITag uiTag)
        {
            return GetComponent<SearchUI>().SearchText(uiObjects, uiTag);
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
            GetComponent<Wizard>().CreatedWorld
                += Canvas_PCStatus_Right_CreatedWorld;
            GetComponent<AimMode>().EnteringAimMode
                += Canvas_PCStatus_Right_EnteringAimMode;
            GetComponent<AimMode>().LeavingAimMode
                += Canvas_PCStatus_Right_LeavingAimMode;
        }
    }
}
