using AxeMan.DungeonObject;
using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem.GameDataHub;
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
    public class Canvas_ExamineMode : MonoBehaviour
    {
        private MetaInfo aimMetaInfo;
        private CanvasTag canvasTag;
        private MetaInfo examineMetaInfo;
        private LocalManager pcLocalManager;
        private SkillComponentTag[] sortedFlaws;
        private UITag[] sortedStatusData;
        private UITag[] sortedStatusText;
        private GameObject[] uiObjects;

        private void Awake()
        {
            canvasTag = CanvasTag.Canvas_ExamineMode;

            sortedFlaws = new SkillComponentTag[]
            {
                SkillComponentTag.FireFlaw,
                SkillComponentTag.WaterFlaw,
                SkillComponentTag.AirFlaw,
                SkillComponentTag.EarthFlaw,
            };

            sortedStatusText = new UITag[]
            {
                UITag.Status1Text,
                UITag.Status2Text,
                UITag.Status3Text,
                UITag.Status4Text,
            };

            sortedStatusData = new UITag[]
            {
                UITag.Status1Data,
                UITag.Status2Data,
                UITag.Status3Data,
                UITag.Status4Data,
            };
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
            PrintExamineText(SubTag.AimMarker);
            SwitchVisibility(true);
        }

        private void Canvas_ExamineMode_LeavingAimMode(object sender,
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

        private void Canvas_ExamineMode_SwitchedGameMode(object sender,
            SwitchGameModeEventArgs e)
        {
            if (e.EnterMode == GameModeTag.ExamineMode)
            {
                ClearText();
                PrintExamineText(SubTag.ExamineMarker);
                SwitchVisibility(true);
            }
        }

        private void Canvas_ExamineMode_SwitchingGameMode(object sender,
            SwitchGameModeEventArgs e)
        {
            if (e.LeaveMode == GameModeTag.ExamineMode)
            {
                SwitchVisibility(false);
            }
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
            PrintExamineText(e.SubTag);
        }

        private void ClearText()
        {
            foreach (GameObject ui in uiObjects)
            {
                ui.GetComponent<Text>().text = "";
            }
        }

        private string GetAltarEffectName(SubTag subTag)
        {
            MainTag mainTag = MainTag.Altar;
            BuildingEffect building = GetComponent<BuildingEffect>();
            SkillComponentTag skill = building.GetEffect(mainTag, subTag);
            int data = building.GetPowerDuration(mainTag, subTag);

            switch (subTag)
            {
                case SubTag.FireAltar:
                case SubTag.WaterAltar:
                case SubTag.AirAltar:
                case SubTag.EarthAltar:
                    return GetComponent<ConvertSkillMetaInfo>()
                        .GetBuildingEffectName(skill, data);

                case SubTag.LifeAltar:
                    return data.ToString();

                default:
                    return null;
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

        private string GetTrapEffectName(SubTag subTag)
        {
            MainTag mainTag = MainTag.Trap;
            BuildingEffect building = GetComponent<BuildingEffect>();
            SkillComponentTag skill = building.GetEffect(mainTag, subTag);
            int data = building.GetPowerDuration(mainTag, subTag);

            return GetComponent<ConvertSkillMetaInfo>()
                .GetBuildingEffectName(skill, data);
        }

        private void PrintAltarData(GameObject target)
        {
            MainTag mainTag = target.GetComponent<MetaInfo>().MainTag;
            SubTag subTag = target.GetComponent<MetaInfo>().SubTag;

            string altarText
                = GetComponent<SkillData>().GetSkillComponentName(
                    GetComponent<BuildingEffect>().GetEffect(mainTag, subTag));
            string altarData = GetAltarEffectName(subTag);

            string cooldownText = GetComponent<UITextData>().GetStringData(
                UITextCategoryTag.ActorStatus, UITextDataTag.Cooldown);

            int currentCD = GetComponent<AltarCooldown>().CurrentCooldown;
            int maxCD = GetComponent<AltarCooldown>().MaxCooldown;
            string cooldownData = currentCD + "/" + maxCD;

            SearchText(UITag.HPText).text = altarText;
            SearchText(UITag.HPData).text = altarData;

            SearchText(UITag.MoveText).text = cooldownText;
            SearchText(UITag.MoveData).text = cooldownData;
        }

        private void PrintCurse(GameObject target)
        {
            SkillComponentTag curseEffect = target.GetComponent<NPCAttack>()
                .CurseEffect;
            int powerDuration = target.GetComponent<NPCAttack>().CurseData;

            if (curseEffect == SkillComponentTag.INVALID)
            {
                return;
            }

            string curseText = GetComponent<SkillData>().GetSkillComponentName(
               curseEffect);
            string curseData = GetComponent<ConvertSkillMetaInfo>()
                .GetSkillEffectName(curseEffect,
                new EffectData(powerDuration, powerDuration));

            SearchText(UITag.CurseText).text = curseText;
            SearchText(UITag.CurseData).text = curseData;
        }

        private void PrintDamage(GameObject target)
        {
            string damageText = GetComponent<UITextData>().GetStringData(
                UITextCategoryTag.ActorStatus, UITextDataTag.Damage);
            int damageData = target.GetComponent<NPCAttack>().Damage;

            SearchText(UITag.DamageText).text = damageText;
            SearchText(UITag.DamageData).text = damageData.ToString();
        }

        private void PrintExamineText(SubTag markerTag)
        {
            GameObject target = GetTargetUnderMarker(markerTag);
            if (target == null)
            {
                return;
            }

            PrintModeline(target);
            switch (target.GetComponent<MetaInfo>().MainTag)
            {
                case MainTag.Altar:
                    PrintAltarData(target);
                    break;

                case MainTag.Trap:
                    PrintTrapData(target);
                    break;

                case MainTag.Actor:
                    PrintNPCData(target);
                    break;

                default:
                    break;
            }
        }

        private void PrintModeline(GameObject target)
        {
            string name = target.GetComponent<MetaInfo>().ObjectName;

            int[] targetPos = target.GetComponent<MetaInfo>().Position;
            int[] relativePos = pcLocalManager.GetRelativePosition(targetPos);
            int relativeX = relativePos[0];
            int relativeY = relativePos[1];

            int distance = pcLocalManager.GetDistance(targetPos);

            string modeline
                = $"[ {name} ] [ {relativeX}, {relativeY} ] [ {distance} ]";

            SearchText(UITag.Modeline).text = modeline;
        }

        private void PrintNPCColumn1(GameObject target)
        {
            string hpText = GetComponent<UITextData>().GetStringData(
                UITextCategoryTag.ActorStatus, UITextDataTag.HP);
            string moveText = GetComponent<UITextData>().GetStringData(
                UITextCategoryTag.ActorStatus, UITextDataTag.MoveDistance);
            string attackText = GetComponent<UITextData>().GetStringData(
                UITextCategoryTag.ActorStatus, UITextDataTag.AttackRange);
            string cooldownText = GetComponent<UITextData>().GetStringData(
                UITextCategoryTag.ActorStatus, UITextDataTag.Cooldown);

            SearchText(UITag.HPText).text = hpText;
            SearchText(UITag.MoveText).text = moveText;
            SearchText(UITag.AttackText).text = attackText;
            SearchText(UITag.CooldownText).text = cooldownText;

            int hpData = target.GetComponent<HP>().Current;
            int moveData = target.GetComponent<NPCMove>().Distance;
            int attackData = target.GetComponent<NPCAttack>().AttackRange;
            int cooldownData = target.GetComponent<NPCBonusAction>()
                .CurrentCooldown;

            SearchText(UITag.HPData).text = hpData.ToString();
            SearchText(UITag.MoveData).text = moveData.ToString();
            SearchText(UITag.AttackData).text = attackData.ToString();
            SearchText(UITag.CooldownData).text = cooldownData.ToString();
        }

        private void PrintNPCColumn2(GameObject target)
        {
            PrintDamage(target);
            PrintCurse(target);
        }

        private void PrintNPCColumn3(GameObject target)
        {
            int index = 0;

            foreach (SkillComponentTag sct in sortedFlaws)
            {
                if (target.GetComponent<ActorStatus>().HasStatus(sct,
                    out EffectData effectData))
                {
                    SearchText(sortedStatusText[index]).text =
                        GetComponent<SkillData>().GetSkillComponentName(sct);
                    SearchText(sortedStatusData[index]).text =
                        GetComponent<ConvertSkillMetaInfo>().GetSkillEffectName(
                            sct, effectData);
                    index++;
                }
            }
        }

        private void PrintNPCData(GameObject target)
        {
            if (target.GetComponent<MetaInfo>().SubTag == SubTag.PC)
            {
                return;
            }
            PrintNPCColumn1(target);
            PrintNPCColumn2(target);
            PrintNPCColumn3(target);
        }

        private void PrintTrapData(GameObject target)
        {
            MainTag mainTag = target.GetComponent<MetaInfo>().MainTag;
            SubTag subTag = target.GetComponent<MetaInfo>().SubTag;

            string trapText
                = GetComponent<SkillData>().GetSkillComponentName(
                    GetComponent<BuildingEffect>().GetEffect(mainTag, subTag));
            string trapData = GetTrapEffectName(subTag);

            SearchText(UITag.HPText).text = trapText;
            SearchText(UITag.HPData).text = trapData;
        }

        private Text SearchText(UITag uiTag)
        {
            return GetComponent<SearchUI>().SearchText(uiObjects, uiTag);
        }

        private void Start()
        {
            GetComponent<InitializeMainGame>().SettingReference
                += Canvas_ExamineMode_SettingReference;
            GetComponent<InitializeMainGame>().CreatedWorld
                += Canvas_ExamineMode_CreatedWorld;

            GetComponent<AimMode>().EnteredAimMode
                += Canvas_ExamineMode_EnteredAimMode;
            GetComponent<AimMode>().LeavingAimMode
                += Canvas_ExamineMode_LeavingAimMode;

            GetComponent<GameModeManager>().SwitchingGameMode
                += Canvas_ExamineMode_SwitchingGameMode;
            GetComponent<GameModeManager>().SwitchedGameMode
                += Canvas_ExamineMode_SwitchedGameMode;

            GetComponent<PublishAction>().TakenAction
                += Canvas_ExamineMode_TakenAction;
        }

        private void SwitchVisibility(bool switchOn)
        {
            GetComponent<UIManager>().SwitchCanvasVisibility(canvasTag, switchOn);
        }
    }
}
