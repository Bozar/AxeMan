using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
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
        private Dictionary<SkillNameTag, int> currentCooldownDict;
        private int invalidCooldown;
        private Dictionary<SkillNameTag, int> maxCooldownDict;
        private int minCooldown;

        public int GetCurrentCooldown(SkillNameTag skillNameTag)
        {
            if (currentCooldownDict.TryGetValue(skillNameTag, out int cooldown))
            {
                return cooldown;
            }
            return invalidCooldown;
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
            if (maxCooldownDict.TryGetValue(skillNameTag, out int cooldown))
            {
                return cooldown;
            }
            return invalidCooldown;
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
            minCooldown = 0;
            invalidCooldown = 99;

            maxCooldownDict = new Dictionary<SkillNameTag, int>()
            {
                { SkillNameTag.Q, 3 }, { SkillNameTag.W, 5 },
                { SkillNameTag.E, 2 }, { SkillNameTag.R, 9 }
            };
            currentCooldownDict = new Dictionary<SkillNameTag, int>()
            {
                { SkillNameTag.Q, minCooldown }, { SkillNameTag.W, minCooldown },
                { SkillNameTag.E, minCooldown }, { SkillNameTag.R, minCooldown }
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

        private void SetCooldown(SkillNameTag skill, int cooldown)
        {
            if (currentCooldownDict.ContainsKey(skill))
            {
                currentCooldownDict[skill] = cooldown;
                GetComponent<LocalManager>().ChangedSkillCooldown(
                    new ChangedSkillCooldownEventArgs(skill));
            }
        }

        private void SkillCooldown_StartingTurn(object sender,
            StartingTurnEventArgs e)
        {
            if (!GetComponent<LocalManager>().MatchID(e.ObjectID))
            {
                return;
            }

            foreach (SkillNameTag snt in currentCooldownDict.Keys.ToArray())
            {
                if (currentCooldownDict[snt] > minCooldown)
                {
                    SetCooldown(snt, --currentCooldownDict[snt]);
                }
            }
        }

        private void SkillCooldown_TakenAction(object sender,
            TakenActionEventArgs e)
        {
            if (!GetComponent<LocalManager>().MatchID(e.ObjectID)
                || !IsValidAction(e.Action))
            {
                return;
            }

            SkillNameTag skill = GetComponent<PCSkillManager>()
                .GetSkillNameTag(e.Action);
            SetCooldown(skill, GetMaxCooldown(skill));
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<PublishAction>().TakenAction
                += SkillCooldown_TakenAction;
            GameCore.AxeManCore.GetComponent<TurnManager>().StartingTurn
                += SkillCooldown_StartingTurn;
        }
    }
}
