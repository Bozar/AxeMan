using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.SchedulingSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AxeMan.DungeonObject.ActorSkill
{
    public interface ISkillCooldown
    {
        int GetCurrentCooldown(SkillNameTag skillNameTag);

        int GetCurrentCooldown(CommandTag commandTag);

        int GetCurrentCooldown(UITag uiTag);

        int GetMaxCooldown(SkillNameTag skillNameTag);

        int GetMaxCooldown(CommandTag commandTag);

        int GetMaxCooldown(UITag uiTag);
    }

    public class SkillCooldown : MonoBehaviour, ISkillCooldown
    {
        private int baseCD;
        private Dictionary<SkillNameTag, int> currentCDDict;
        private int invalidCD;
        private Dictionary<SkillNameTag, int> maxCDDict;
        private int minCD;

        public int GetCurrentCooldown(SkillNameTag skillNameTag)
        {
            if (currentCDDict.TryGetValue(skillNameTag, out int cooldown))
            {
                return cooldown;
            }
            return invalidCD;
        }

        public int GetCurrentCooldown(CommandTag commandTag)
        {
            return GetCurrentCooldown(
                GameCore.AxeManCore.GetComponent<ConvertSkillMetaInfo>()
                .GetSkillNameTag(commandTag));
        }

        public int GetCurrentCooldown(UITag uiTag)
        {
            return GetCurrentCooldown(
                GameCore.AxeManCore.GetComponent<ConvertSkillMetaInfo>()
                .GetSkillNameTag(uiTag));
        }

        public int GetMaxCooldown(SkillNameTag skillNameTag)
        {
            if (!maxCDDict.ContainsKey(skillNameTag))
            {
                return invalidCD;
            }

            if (maxCDDict[skillNameTag] == invalidCD)
            {
                SetMaxCooldown(skillNameTag);
            }
            return maxCDDict[skillNameTag];
        }

        public int GetMaxCooldown(CommandTag commandTag)
        {
            return GetMaxCooldown(
               GameCore.AxeManCore.GetComponent<ConvertSkillMetaInfo>()
               .GetSkillNameTag(commandTag));
        }

        public int GetMaxCooldown(UITag uiTag)
        {
            return GetMaxCooldown(
               GameCore.AxeManCore.GetComponent<ConvertSkillMetaInfo>()
               .GetSkillNameTag(uiTag));
        }

        private void Awake()
        {
            minCD = 0;
            baseCD = 5;
            invalidCD = 99;

            maxCDDict = new Dictionary<SkillNameTag, int>()
            {
                { SkillNameTag.Q, invalidCD }, { SkillNameTag.W, invalidCD },
                { SkillNameTag.E, invalidCD }, { SkillNameTag.R, invalidCD }
            };
            currentCDDict = new Dictionary<SkillNameTag, int>()
            {
                { SkillNameTag.Q, minCD }, { SkillNameTag.W, minCD },
                { SkillNameTag.E, minCD }, { SkillNameTag.R, minCD }
            };
        }

        private bool IsValidAction(ActionTag actionTag)
        {
            switch (actionTag)
            {
                case ActionTag.UseSkillQ:
                case ActionTag.UseSkillW:
                case ActionTag.UseSkillE:
                case ActionTag.UseSkillR:
                    return true;

                default:
                    return false;
            }
        }

        private void SetCurrentCooldown(SkillNameTag skillNameTag, int cooldown)
        {
            if (currentCDDict.ContainsKey(skillNameTag))
            {
                currentCDDict[skillNameTag] = cooldown;
                GetComponent<LocalManager>().ChangedSkillCooldown(
                    new ChangedSkillCooldownEventArgs(skillNameTag));
            }
        }

        private void SetMaxCooldown(SkillNameTag skillNameTag)
        {
            Dictionary<SkillSlotTag, SkillComponentTag> slotComp
                = GetComponent<PCSkillManager>().GetSkillSlot(skillNameTag);
            int cd = baseCD;

            foreach (SkillSlotTag sst in slotComp.Keys)
            {
                switch (sst)
                {
                    case SkillSlotTag.Merit1:
                    case SkillSlotTag.Merit2:
                    case SkillSlotTag.Merit3:
                        cd++;
                        break;

                    case SkillSlotTag.Flaw1:
                    case SkillSlotTag.Flaw2:
                    case SkillSlotTag.Flaw3:
                        cd--;
                        break;

                    default:
                        break;
                }
            }
            maxCDDict[skillNameTag] = cd;
        }

        private void SkillCooldown_StartingTurn(object sender,
            StartingTurnEventArgs e)
        {
            if (!GetComponent<LocalManager>().MatchID(e.ObjectID))
            {
                return;
            }

            // TODO: Freeze counting down if PC has Water- curse.
            foreach (SkillNameTag snt in currentCDDict.Keys.ToArray())
            {
                if (currentCDDict[snt] > minCD)
                {
                    SetCurrentCooldown(snt, --currentCDDict[snt]);
                }
            }
        }

        private void SkillCooldown_TakenAction(object sender,
            PublishActionEventArgs e)
        {
            if (!GetComponent<LocalManager>().MatchID(e.ObjectID)
                || !IsValidAction(e.Action))
            {
                return;
            }

            SkillNameTag skill = GetComponent<PCSkillManager>()
                .GetSkillNameTag(e.Action);
            SetCurrentCooldown(skill, GetMaxCooldown(skill));
        }

        private void SkillCooldown_VerifyingSkill(object sender,
            VerifyingSkillEventArgs e)
        {
            e.CanUseSkill.Push(GetCurrentCooldown(e.UseSkill) <= minCD);
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<PublishAction>().TakenAction
                += SkillCooldown_TakenAction;
            GameCore.AxeManCore.GetComponent<TurnManager>().StartingTurn
                += SkillCooldown_StartingTurn;
            GameCore.AxeManCore.GetComponent<AimMode>().VerifyingSkill
                += SkillCooldown_VerifyingSkill;
        }
    }
}
