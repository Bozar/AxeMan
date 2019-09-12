using AxeMan.DungeonObject;
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
            PrintExamineText(SubTag.AimMarker);
            SwitchVisibility(true);
        }

        private void Canvas_ExamineMode_EnteredExamineMode(object sender,
            EventArgs e)
        {
            ClearText();
            PrintExamineText(SubTag.ExamineMarker);
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

            string cooldownText = GetComponent<UILabelData>().GetStringData(
                UILabelDataTag.Cooldown);
            string cooldownData = GetComponent<AltarCooldown>().CurrentCooldown
                .ToString();

            SearchText(UITag.HPText).text = altarText;
            SearchText(UITag.HPData).text = altarData;

            SearchText(UITag.MoveText).text = cooldownText;
            SearchText(UITag.MoveData).text = cooldownData;
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
            string hpText = GetComponent<UILabelData>().GetStringData(
                UILabelDataTag.HP);
            string moveText = GetComponent<UILabelData>().GetStringData(
                UILabelDataTag.MoveDistance);
            string attackText = GetComponent<UILabelData>().GetStringData(
                UILabelDataTag.AttackRange);
            string cooldownText = GetComponent<UILabelData>().GetStringData(
                UILabelDataTag.Cooldown);

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
            string damageText = GetComponent<UILabelData>().GetStringData(
                UILabelDataTag.Damage);
            string curse1Text = "Earth?";
            string curse2Text = "Water?";

            SearchText(UITag.DamageText).text = damageText;
            SearchText(UITag.Curse1Text).text = curse1Text;
            SearchText(UITag.Curse2Text).text = curse2Text;

            int damageData = target.GetComponent<NPCAttack>().Damage;
            string curse1Data = "4 x 4";
            string curse2Data = "T x 2";

            SearchText(UITag.DamageData).text = damageData.ToString();
            SearchText(UITag.Curse1Data).text = curse1Data;
            SearchText(UITag.Curse2Data).text = curse2Data;
        }

        private void PrintNPCColumn3(GameObject target)
        {
            string status1Text = "Fire-";
            string status2Text = "Water-";
            string status3Text = "Air-";
            string status4Text = "Earth-";

            SearchText(UITag.Status1Text).text = status1Text;
            SearchText(UITag.Status2Text).text = status2Text;
            SearchText(UITag.Status3Text).text = status3Text;
            SearchText(UITag.Status4Text).text = status4Text;

            string status1Data = "T x 4";
            string status2Data = "T x 8";
            string status3Data = "4 x 3";
            string status4Data = "8 x 1";

            SearchText(UITag.Status1Data).text = status1Data;
            SearchText(UITag.Status2Data).text = status2Data;
            SearchText(UITag.Status3Data).text = status3Data;
            SearchText(UITag.Status4Data).text = status4Data;
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
